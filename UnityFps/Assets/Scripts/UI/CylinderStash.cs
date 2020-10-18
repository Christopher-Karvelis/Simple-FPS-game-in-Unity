using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CylinderStash : MonoBehaviour
{

    public static int remain;
    private static Text Stash;

    // Use this for initialization
    void Start()
    {
        remain = 0;
        Stash = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Stash.text = remain + "x";
    }
}