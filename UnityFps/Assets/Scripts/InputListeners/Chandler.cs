using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandler : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.C))
        {

            if (CylinderStash.remain > 0)
            {
                RaycastHit rayHit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickable))
                {
                    GameObject selectedObjet = rayHit.collider.gameObject;
                    Vector3 object_position = selectedObjet.transform.position;
                    int y;
                    if (selectedObjet.tag == "Cylinder")
                    {
                        print("pass");
                       y = (int)(object_position.y + 1.5f);
                    }
                    else
                    {
                        y = (int)object_position.y + 1;
                    }
                    int x = (int)object_position.x;
                    int z = (int)object_position.z;
                    terainCreator.BuildCylinder(x, y, z);
                }
            }
        }

    }
}
