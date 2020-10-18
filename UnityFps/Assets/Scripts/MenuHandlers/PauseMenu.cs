using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {
   
	public void Continue()
    {
        Destroy(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        TerainCreator.pause = 1;
    }

    public void Quit()
    {
        SceneManager.LoadScene("TheGame");
    }
}
