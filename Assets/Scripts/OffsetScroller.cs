using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScroller : MonoBehaviour 
{
	[SerializeField]
	private float scrollOffset;
	private Vector2 savedOffset;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		savedOffset = rend.sharedMaterial.GetTextureOffset("_MainTex");
	}

	// Update is called once per frame
	void Update () 
	{
		int direction = 0;

		if (Input.GetKeyDown(KeyCode.RightArrow))
			direction = 1;
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
			direction = -1;

		float x = Mathf.Repeat(/*direction * */Time.time * scrollOffset, 1);
		Vector2 offset = new Vector2(x, savedOffset.y);
		rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
