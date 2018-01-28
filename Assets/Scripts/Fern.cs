using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fern : MonoBehaviour 
{
	private Animator ani;
	private ParticleSystem ps;

	public bool Swaying 
	{
		get; set;
	}

	// Use this for initialization
	void Start () 
	{
		ani = GetComponent<Animator>();
		ps = GetComponentInChildren<ParticleSystem>();
		if (ps != null)
		{
			ps.Stop();
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*if (!Swaying)
		{
			ps.Stop();
		}*/
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && !Swaying)
		{
			GameManager.Instance.CurrAmmo = GameManager.Instance.MaxAmmo;
			ani.SetTrigger("sway");
			if (ps != null)
			{
				ps.Play();
				StartCoroutine(ParticleManagement());
			}
		}
	}

	private IEnumerator ParticleManagement()
	{
		yield return new WaitForSeconds(1);
		ps.Stop();
	}
}
