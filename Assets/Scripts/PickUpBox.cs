using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour {

    bool holdObject;

	// Use this for initialization
	void Start ()
    {
        holdObject = false;
        this.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Fire2") > 0.0f)
        {
            if (holdObject)
                holdObject = false;
            else
                holdObject = true;
        }
        if(!holdObject)
        {
            this.transform.parent = null;
            Debug.Log("dropped the object");
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.GetComponent<PlayerController>().IsAnAnt() && holdObject)
        {
            this.transform.parent = coll.transform;
            Debug.Log("holding the object");
        }
    }
}
