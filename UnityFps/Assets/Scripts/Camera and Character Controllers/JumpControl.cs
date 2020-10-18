using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class JumpControl : MonoBehaviour {

    private FirstPersonController controller;
    private TerainCreator Grid;
    // Use this for initialization
    void Start () {

        Grid = GameObject.Find("Terain Creator").GetComponent<TerainCreator>();
        controller = gameObject.GetComponent<FirstPersonController>();
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            int xp = (int)Math.Round(GameObject.Find("FPSController(Clone)").transform.position.x);
            int zp = (int)Math.Round(GameObject.Find("FPSController(Clone)").transform.position.z);

            int x = (int)Math.Round(gameObject.transform.position.x);
            int y = (int)(gameObject.transform.position.y);
            int z = (int)Math.Round(gameObject.transform.position.z);


            if (gameObject.transform.rotation.eulerAngles.y <= 20 || gameObject.transform.rotation.eulerAngles.y >= 340)
            {
                z += 1;
            }
            else if (gameObject.transform.rotation.eulerAngles.y > 55 && gameObject.transform.rotation.eulerAngles.y <= 100)
            {
                x += 1;
            }
            else if (gameObject.transform.rotation.eulerAngles.y > 150 && gameObject.transform.rotation.eulerAngles.y <= 195)
            {
                z -= 1;
            }
            else if (gameObject.transform.rotation.eulerAngles.y > 244 && gameObject.transform.rotation.eulerAngles.y <= 300)
            {
                x -= 1;
            }

            if (Grid.IsInsideJump(x, y, z))
            {
                if (isCube(x, y, z) && Input.GetKeyDown("space"))
                {
                    controller.UpdateJump(6);
                }
                else if (isCylinder(x, y, z) && Input.GetKeyDown("space"))
                {
                    controller.UpdateJump(7.5f);
                    if ((int)Math.Round(GameObject.Find("FPSController(Clone)").transform.position.x) != xp || (int)Math.Round(GameObject.Find("FPSController(Clone)").transform.position.z) != zp)
                    {
                        controller.fixGravity(6);
                    }

                }
                else if (!isCylinder(x, y, z) && !isCube(x, y, z) && Input.GetKeyDown("space"))
                {
                    controller.fixGravity(2);
                    controller.UpdateJump(4);
                }
            }
        }
    }
   
    bool isCube(int x, int y, int z)
    {
       return (TerainCreator.grid[x, z, y] == 1 && TerainCreator.grid[x, z, y + 1] == 0 && Input.GetKeyDown("space")); 
    }
    bool isCylinder(int x, int y, int z)
    {
       return (TerainCreator.grid[x, z, y] == 2 && TerainCreator.grid[x, z, y + 1] == 2 && TerainCreator.grid[x, z, y + 2] == 0);
    }

}
