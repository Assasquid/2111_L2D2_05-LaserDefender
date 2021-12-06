using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int currentScore = 0;

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void AddToScore(int points)
    {
        currentScore += points;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
