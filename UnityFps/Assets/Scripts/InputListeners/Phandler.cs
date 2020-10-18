using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phandler : MonoBehaviour {


    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;
    [SerializeField]
    private Material blue;
    [SerializeField]
    private Material yellow;
    [SerializeField]
    private Material cyan;

    private MeshRenderer myRend;
    
    public TerainCreator lifespan;

    [SerializeField]
    private int remain;

    // Use this for initialization
    void Start () {
        myRend = GetComponent<MeshRenderer>();
        lifespan = GameObject.FindObjectOfType(typeof(TerainCreator)) as TerainCreator;
    }
	
	
    public void ChangeState()
    {
        //Debug.Log("Will become yellow");
        //myRend.material = yellow;
        // remain = 2;
        
        switch (remain)
        {
            case 1:
                remain -= 1;
                myRend.material = blue;
                CubeStash.remain += 1;
                Scoring.scoreValue -= 5;
                CheckPoints();
                break;
            case 2:
                remain -= 1;
                myRend.material = yellow;
                CubeStash.remain += 1;
                Scoring.scoreValue -= 5;
                CheckPoints();
                break;
            case 3:
                remain -= 1;
                myRend.material = red;
                CubeStash.remain += 1;
                Scoring.scoreValue -= 5;
                CheckPoints();
                break;
            case -1:
                CylinderStash.remain += 1;
                Scoring.scoreValue -= 5;
                TerainCreator.grid[(int)gameObject.transform.position.x, (int)gameObject.transform.position.z, (int)gameObject.transform.position.y] = 0;
                Destroy(gameObject);
                CheckPoints();
                break;

            default:
                break;
        }

    }
    void UpdateStats()
    {
        remain -= 1;
        CubeStash.remain += 1;
        Scoring.scoreValue -= 5;
        CheckPoints();
    }

    public void Delete()
    {   
        Destroy(gameObject);
        TerainCreator.grid[(int)gameObject.transform.position.x, (int)gameObject.transform.position.z, (int)gameObject.transform.position.y] = 0;
    }

    public void CheckPoints()
    {
        if(Scoring.scoreValue <= 0)
        {
            lifespan.ReduceLifeSpan();
           
        }
    }
}
