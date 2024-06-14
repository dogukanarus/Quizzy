using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int correctIncrement;
    [SerializeField] int wrongDecrement;
    [SerializeField] int HasNotAnsweredDecrement;

    public int score = 0;

    public void IncrementCorrectAnswer()
    {
        score += correctIncrement;
    }

    public void DecrementWrongAnswer()
    {
        score -= wrongDecrement;
    }

    public void DecrementNotAnswered()
    {
        score -= HasNotAnsweredDecrement;
    }

    public int GetScore()
    {
        return score;
    }
}
