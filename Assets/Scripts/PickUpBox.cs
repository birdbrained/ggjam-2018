using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour {

    [SerializeField]
    private bool holdObject;

	// Use this for initialization
	void Start ()
    {
        holdObject = false;
        this.transform.parent = null;
	}
	
    private void OnCollisionStay2D(Collision2D coll)
    {
		PlayerController otherPlayer = coll.gameObject.GetComponent<PlayerController>();
		if (otherPlayer != null)
		{
			if (coll.gameObject.GetComponent<PlayerController>().IsAnAnt())
			{
				if (Input.GetAxis("Fire2") > 0.0f)
					holdObject = true;
				else
					holdObject = false;

				if (!holdObject)
				{
					this.transform.parent = null;
					Debug.Log("dropped the object");
				}

				if (holdObject)
				{
					this.transform.parent = coll.transform;
					Debug.Log("holding the object");
				}
			}
		}
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        holdObject = false;
        this.transform.parent = null;
    }
}
