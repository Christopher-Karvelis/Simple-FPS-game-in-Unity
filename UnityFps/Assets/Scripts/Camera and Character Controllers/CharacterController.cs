using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour {

    public float terainSize; 
    public float moveSpeed;
    public float jumpForce;
    public float previousY;

    public TerainCreator spawner;
    private CharacterController charCon;
	// Use this for initialization

	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        jumpForce = 1.0f;
        moveSpeed = 4.0f;
        spawner = GameObject.FindObjectOfType(typeof(TerainCreator)) as TerainCreator;
    }
    private void Awake()
    {
        charCon = GetComponent<CharacterController>();
    }

    public void InitializeSize(int size)
    {
        terainSize = size;

    }

    // Update is called once per frame
    void Update () {
        //Forwards And Backwards Moving
        float translation = Input.GetAxis("Vertical");
        //Side Moving
        float straffe = Input.GetAxis("Horizontal");

        float jump = Input.GetAxis("Jump");
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;
        jump *= Time.deltaTime;

        float X = straffe * moveSpeed;
        float Z = translation * moveSpeed;

       

        transform.Translate(straffe* moveSpeed, 0, translation *moveSpeed);
       
       
        if (transform.position.y == 2.5 && jump != 0)
        {

            transform.Translate(straffe * moveSpeed, jumpForce, translation * moveSpeed);

        }
        
        /*if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }*/

      
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CylinderStash.remain > 0)
            {


                int x = (int)Math.Round(transform.position.x);
                int y = (int)(transform.position.y);
                int z = (int)Math.Round(transform.position.z); ;
                if (transform.rotation.eulerAngles.y <= 45 || transform.rotation.eulerAngles.y >= 315)
                {
                    
                    z += 1;
                    
                }
                else if (transform.rotation.eulerAngles.y > 45 && transform.rotation.eulerAngles.y <= 135)
                {

                    x += 1;
                   
                  
                   
                }
                else if (transform.rotation.eulerAngles.y > 135 && transform.rotation.eulerAngles.y <= 229)
                {

                    
                    z -= 1;
                    
                }
                else if (transform.rotation.eulerAngles.y > 229 && transform.rotation.eulerAngles.y <= 315)
                {

                    x -= 1;
                    
                    
                }

                while (true)
                {
                    
                        if (TerainCreator.grid[x, z, y] != 1 && TerainCreator.grid[x, z, y+1] != 1)
                        {
                            spawner.SpawnCylinder(new Vector3(x, y + 0.5f, z));
                            TerainCreator.grid[x, z, y+1] = 1;
                            TerainCreator.grid[x, z, y] = 1;
                            Scoring.scoreValue += 20;
                            CylinderStash.remain -= 1;
                            break;
                        }
                        y++;
                    
                    
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if(CubeStash.remain > 0)
            {
                int x = (int)Math.Round(transform.position.x); ;
                int y = (int)transform.position.y;
                int z = (int)Math.Round(transform.position.z);

                if (transform.rotation.eulerAngles.y <= 45 || transform.rotation.eulerAngles.y >= 315)
                {

                    z += 1;
                    
                }
                else if (transform.rotation.eulerAngles.y > 45 && transform.rotation.eulerAngles.y <= 135)
                {

                    x  += 1;
                      
                    
                }
                else if (transform.rotation.eulerAngles.y > 135 && transform.rotation.eulerAngles.y <= 229)
                {

                    z  -= 1;
                   
                }
                else if (transform.rotation.eulerAngles.y > 229 && transform.rotation.eulerAngles.y <= 315)
                {

                    x  -= 1;
                    
                }

                while (true)
                {
                    if (TerainCreator.grid[x, z, y] != 1)
                    {
                        spawner.GenerateRandomBlock(new Vector3(x, y, z));
                        TerainCreator.grid[x, z, y] = 1;
                        Scoring.scoreValue += 10;
                        CubeStash.remain -= 1;
                        break;
                    }
                    y++;
                }
            }
            
           
        }
    }
}
