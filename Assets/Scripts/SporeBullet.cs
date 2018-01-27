using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeBullet : MonoBehaviour 
{
	[SerializeField]
	private float speed;
	private Vector2 direction;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (rb != null)
		{
			rb.velocity = direction * speed;
		}
	}

	public void Initialize(Vector2 dir)
	{
		direction = dir;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "possessable")
		{
			GameManager.Instance.PossessedObj = other.gameObject;
			other.gameObject.GetComponent<PlayerController>().enabled = true;
			GameManager.Instance.PlayerObj.GetComponent<PlayerController>().enabled = false;
			GameObject.Find("Main Camera").GetComponent<CameraController>().ChangeTarget(other.gameObject.name);
			Destroy(gameObject);
		}
	}

	void OnBecomeInvisible()
	{
		Destroy(gameObject);
	}
}
