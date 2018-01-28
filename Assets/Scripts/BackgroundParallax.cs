using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour 
{
	[SerializeField]
	private Transform[] backgrounds;
	[SerializeField]
	private float parallaxScale;
	[SerializeField]
	private float parallaxReductionFactor;
	[SerializeField]
	private float smoothing;

	private Transform cam;
	private Vector3 previousCamPos;

	void Awake()
	{
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () 
	{
		previousCamPos = cam.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float parallax = (previousCamPos.x - cam.position.x) * parallaxScale;

		for (int i = 0; i < backgrounds.Length; i++)
		{
			float backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactor + 1);
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		previousCamPos = cam.position;
	}
}
