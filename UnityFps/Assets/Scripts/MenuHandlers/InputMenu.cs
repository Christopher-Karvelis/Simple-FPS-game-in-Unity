using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputMenu : MonoBehaviour
{

    private TerainCreator terainCreator;
    private DestroyMenu menu;
    private CharacterController controller;
    private int Size;

    //Notify TerainCreator
    private void Awake()
    {
        Size = 0;
        menu = GameObject.FindObjectOfType<DestroyMenu>();
        terainCreator = GameObject.FindObjectOfType<TerainCreator>();
        //controller = GameObject.FindObjectOfType<CharacterController>();
    }

    //Handle Input From User
    public void GetInput(string value)
    {
        //Some debuging Lines
        //Debug.Log("" + value);

        Size = Int32.Parse(value);
        try
        {
            Size = Int32.Parse(value);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
        if (Size > 1)
        { 

            ////Pass size value to TerainCreator script
            terainCreator.InitializeSize(Size);
            menu.DestroyM();
        }else{
 
            SceneManager.LoadScene("TheGame");
        }




    }


}
