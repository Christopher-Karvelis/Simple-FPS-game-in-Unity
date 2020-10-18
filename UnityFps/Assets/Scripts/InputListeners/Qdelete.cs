using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Qdelete : MonoBehaviour
{


    [SerializeField] private LayerMask clickable;
    private TerainCreator terainCreator;

    void Awake()
    {
        terainCreator = GameObject.FindObjectOfType<TerainCreator>();
    }
    
    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickable))
            {
                GameObject selectedObjet = rayHit.collider.gameObject;
                Vector3 object_position = selectedObjet.transform.position;
                int y = (int)object_position.y;
                int x = (int)object_position.x;
                int z = (int)object_position.z;
                terainCreator.DeleteCube(x, y, z);

            }
        }
    }
}
