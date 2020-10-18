using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pselect : MonoBehaviour {

    [SerializeField] private LayerMask clickable;

	// Update is called once per frame
	void Update() {
      
		if (Input.GetKeyDown(KeyCode.P))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickable))
            {
               Phandler handlerScript = rayHit.collider.GetComponent<Phandler>();
               handlerScript.ChangeState();
            }
        }
	}
}
