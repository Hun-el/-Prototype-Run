using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    GameManager gameManager;
    Text scoreText;
    bool startScore;
    [HideInInspector] public float score;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        scoreText = GetComponent<Text>();
    }

    public void StartScore()
    {
        startScore = true;
    }

    private void Update() {
        if(!gameManager.gameOver && startScore)
        {
            score += 100*Time.deltaTime;
            scoreText.text = score.ToString("0");
        }
    }
}
