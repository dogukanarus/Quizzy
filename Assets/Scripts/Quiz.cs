using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Collections;


public class Quiz : MonoBehaviour
{

    QuestionSO currentQuestion;

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] List<QuestionSO> currentQuestions = new List<QuestionSO>();


    [Header("Answers")]
    [SerializeField] GameObject[] answerButton;
    bool hasAnswered = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite, correctAnswerSprite, wrongAnswerSprite;

    [Header("Audio")]
    [SerializeField] AnswerSFX answerSFX;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    [SerializeField] int defaultProgressValue;
    [SerializeField] int maxProgressValue;
    public int progress = 0;


    [Header("Hardness")]
    [SerializeField] StartScreen startScreen;


    public bool isComplete;


    void Start()
    {
        timer = FindObjectOfType<Timer>();

        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        progressBar.maxValue = maxProgressValue;
        progressBar.value = defaultProgressValue;

        if (!startScreen.GetIsHard())
        {
            GetRandomQuestion();
        }
        else
        {
            GetRandomHardQuestion();
        }
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnswered = false;

            GetNextQuestion();

            timer.loadNextQuestion = false;
        }
        else if (!hasAnswered && !timer.isAnsweringQuestion)
        {
            HasNotAnswered();

            hasAnswered = true;

            SetButtonState(false);
        }
    }

    void DisplayQuestion()
    {
        currentQuestion = currentQuestions[progress];

        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswers(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnswered = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        timer.TimerAudioCancel();
        scoreText.text = "Puan: " + scoreKeeper.GetScore();
        progress++;
    }

    void DisplayAnswer(int index)
    {
        int correctAnswerIndex = currentQuestion.GetCorrectAnswer();

        if (index == correctAnswerIndex)
        {
            questionText.text = "Doğru Cevap!";
            Image buttonImage = answerButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            answerSFX.CorrectAnswerAudio();
            scoreKeeper.IncrementCorrectAnswer();
        }
        else
        {
            string correctAnswer = currentQuestion.GetAnswers(correctAnswerIndex);
            questionText.text = "Tüh! Doğru Cevap '" + correctAnswer + "' olmalıydı";

            Image wrongButtonImage = answerButton[index].GetComponent<Image>();
            wrongButtonImage.sprite = wrongAnswerSprite;

            Image correctButtonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;

            answerSFX.WrongAnswerAudio();

            scoreKeeper.DecrementWrongAnswer();
        }
    }

    void HasNotAnswered()
    {
        int correctAnswerIndex = currentQuestion.GetCorrectAnswer();
        string correctAnswer = currentQuestion.GetAnswers(correctAnswerIndex);
        questionText.text = "Çok yavaşsın, biraz hızlanmaya ne dersin? Cevap '" + correctAnswer + "' olacaktı";

        Image correctButtonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
        correctButtonImage.sprite = correctAnswerSprite;

        answerSFX.WrongAnswerAudio();

        timer.TimerAudioCancel();

        scoreKeeper.DecrementNotAnswered();
        scoreText.text = "Puan: " + scoreKeeper.GetScore();
        progress++;

    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetButtonSprite()
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Image button = answerButton[i].GetComponent<Image>();
            button.sprite = defaultAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (currentQuestions.Count > 0 && !isComplete)
        {
            SetButtonSprite();
            SetButtonState(true);
            DisplayQuestion();
            timer.TimerAudio();
            progressBar.value++;
        }
    }

    void GetRandomQuestion()
    {
        for (int i = 0; i < 10; i++)
        {
            int index = Random.Range(0, questions.Count);
            if (!currentQuestions.Contains(questions[index]))
            {
                currentQuestions.Add(questions[index]);
            }
            else
            {
                i--;
            }
        }
    }

    void GetRandomHardQuestion()
    {
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                List<QuestionSO> currentList = new List<QuestionSO>();
                foreach (QuestionSO question in questions)
                {
                    int questionDifficulty = question.GetQuestionDifficulty();

                    if (questionDifficulty == i && !currentQuestions.Contains(question))
                    {
                        currentList.Add(question);
                    }
                }
                int index = Random.Range(0, currentList.Count - 1);
                currentQuestions.Add(currentList[index]);
            }
        }
    }
}
