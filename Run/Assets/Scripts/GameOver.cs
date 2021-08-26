using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Score score;

    void Start()
    {
        score = FindObjectOfType<Score>();
        transform.GetChild(2).GetComponent<Text>().text = "SCORE\n"+score.score.ToString("0");
    }
}
