using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Scoring : MonoBehaviour
{
    public static int scoreValue;
    private static Text score;

    // Use this for initialization
    void Start()
    {
        scoreValue = 100;
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreValue < 0)
        {
            scoreValue = 0;
        }
        score.text = "x" + scoreValue;
    }
}
