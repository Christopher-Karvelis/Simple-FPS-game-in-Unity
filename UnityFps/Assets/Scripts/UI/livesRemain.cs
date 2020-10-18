using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class livesRemain : MonoBehaviour
{
    public static int remain;
    private Text lives;

    // Use this for initialization
    void Start()
    {
        remain = 4;
        lives = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = remain + "x";
    }
}
