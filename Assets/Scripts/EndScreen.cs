using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endScreenText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        if (scoreKeeper.score == 100)
        {
            endScreenText.text = "Eiiciro Oda? Sen mi geldin?\nToplam Puanın: " + scoreKeeper.GetScore();
        }
        else if (scoreKeeper.score >= 70 && scoreKeeper.score < 100)
        {
            endScreenText.text = "Korsanlar Kralı olacak adamsın!\nToplam Puanın: " + scoreKeeper.GetScore();
        }
        else if (scoreKeeper.score < 70 && scoreKeeper.score >= 50)
        {
            endScreenText.text = "Ama yine de iyi ilerledin!\nToplam Puanın: " + scoreKeeper.GetScore();
        }
        else if (scoreKeeper.score < 50 && scoreKeeper.score >= 0)
        {
            endScreenText.text = "Kanka nerenle izledin animeyi!\nToplam Puanın: " + scoreKeeper.GetScore();
        }
        else if (scoreKeeper.score < 0)
        {
            endScreenText.text = "Sil kanka uygulamayı, sil!\nToplam Puanın: " + scoreKeeper.GetScore();
        }
    }
}
