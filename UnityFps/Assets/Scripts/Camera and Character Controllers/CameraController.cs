using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
   
    Vector2 mouseLook;
    Vector2 smoothLook;
    public float sensitivity;
    public float smoothing;

    GameObject character;

    // Use this for initialization
    void Start () {
        character = this.transform.parent.gameObject;
        sensitivity = 1.3f;
        smoothing = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {

        var mouseInputs = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseInputs = Vector2.Scale(mouseInputs, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothLook.x = Mathf.Lerp(smoothLook.x, mouseInputs.x, 1f / smoothing);
        smoothLook.y = Mathf.Lerp(smoothLook.y, mouseInputs.y, 1f / smoothing);
        mouseLook += smoothLook;
        
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
       
        

       

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }

 
}
