using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    private Transform target;
    [SerializeField]
    private string PlayerName;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find(PlayerName).transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
	}

	public void ChangeTarget(string newTarget)
	{
		target = GameObject.Find(newTarget).transform;
	}
}
