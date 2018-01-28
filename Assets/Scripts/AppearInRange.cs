using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearInRange : MonoBehaviour 
{
	[SerializeField]
	private SpriteRenderer[] sr;

	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < sr.Length; i++)
			sr[i].enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 9) //player layer
		{
			for (int i = 0; i < sr.Length; i++)
				sr[i].enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == 9)
		{
			for (int i = 0; i < sr.Length; i++)
				sr[i].enabled = false;
		}
	}
}
