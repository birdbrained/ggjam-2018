using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour 
{
    [SerializeField]
    private int scoreWorth;
    [SerializeField]
    private int seedWorth;
    [SerializeField]
    private int lifeWorth;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.Score += scoreWorth;
            GameManager.Instance.NumSeeds += seedWorth;
            GameManager.Instance.Lives += lifeWorth;
            Destroy(gameObject);
        }
    }
}
