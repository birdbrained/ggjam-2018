using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour 
{
    [SerializeField]
    private float springForce;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().AddForce(springForce * transform.up, ForceMode2D.Impulse);
        }
    }

}
