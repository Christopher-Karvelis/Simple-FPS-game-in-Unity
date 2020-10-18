using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public static float time_counter;
    public static bool countdown_is_enabled;
    public GameObject GameOver;

    // Use this for initialization
    void Start()
    {
        countdown_is_enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown_is_enabled)
        {
            time_counter -= Time.deltaTime;
        }
       
        if (time_counter >= 0.0f && countdown_is_enabled)
        {
            string minutes = ((int)time_counter / 60).ToString();
            string seconds = (time_counter % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
        else if (time_counter <= 0.0f && countdown_is_enabled)
        {
            countdown_is_enabled = false;
            timerText.text = "0:00";
            time_counter = 0.0f;
            TerainCreator.run = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Instantiate(GameOver);
        }
    }
}
