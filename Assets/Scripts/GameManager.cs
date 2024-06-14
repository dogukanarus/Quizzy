using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    StartScreen startScreen;
    Quiz quiz;
    EndScreen endScreen;

    [SerializeField] Timer timer;


    void Awake()
    {
        startScreen = FindObjectOfType<StartScreen>();
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(false);

        startScreen.gameObject.SetActive(true);
        startScreen.ShowStartScreenText();

        timer.gameObject.SetActive(false);

        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (startScreen.isStarted)
        {
            startScreen.gameObject.SetActive(false);

            quiz.gameObject.SetActive(true);

            timer.gameObject.SetActive(true);
        }
        if (quiz.isComplete == true)
        {
            quiz.gameObject.SetActive(false);

            timer.gameObject.SetActive(false);

            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnStartLevel()
    {
        startScreen.gameObject.SetActive(false);
        startScreen.isStarted = true;
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene("StartScene");
    }
}
