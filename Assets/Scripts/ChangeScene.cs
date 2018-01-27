using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
