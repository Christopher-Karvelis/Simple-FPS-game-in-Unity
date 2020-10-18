using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMenu : MonoBehaviour {

	
	// Update is called once per frame
	public void DestroyM() {
        if (Input.GetKeyDown("return")){
            Destroy(gameObject);
        }
	}
}
