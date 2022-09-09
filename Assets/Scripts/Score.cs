using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : Singleton<Score>  
{
    private float score = 0000;
    [SerializeField] private float scorePerKill = 10;
    [SerializeField] private TextMeshProUGUI scoreTextMeshPro;
    public Shooter shooter;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreTextMeshPro.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0000;
        UpdateScore();
    }

    public void ScoreUp()
    {
        score += scorePerKill * shooter.multiplier;
        score = Math.Clamp(score, 0000, 9999);
        UpdateScore();
        shooter.multiplier++;
    }
}
