using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public static int fscore = 0;
    private Text finalScore;

    // Use this for initialization
    void Start()
    {
        finalScore = GetComponent<Text>();
        fscore = Scoring.scoreValue;
    }

    // Update is called once per frame
    void Update()
    {
        finalScore.text = "Score: " + fscore;
    }
}