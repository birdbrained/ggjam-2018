using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour 
{
	private ParticleSystem ps;
	private bool startGame = false;

	// Use this for initialization
	void Start () 
	{
		ps = GetComponentInChildren<ParticleSystem>();
		if (ps != null)
		{
			ps.Stop();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startGame)
		{
			if (ps != null)
			{
				ps.Play();
			}
			StartCoroutine(WaitASec());
			startGame = false;
		}
	}

	private IEnumerator WaitASec()
	{
		yield return new WaitForSeconds(1.0f);
		ps.Stop();
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("Level1");
	}

	public void PlayGame()
	{
		startGame = true;
	}
}
