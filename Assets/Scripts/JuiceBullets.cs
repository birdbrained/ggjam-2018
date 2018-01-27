using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]

public class JuiceBullets : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb2d;
    private Vector2 direction;
    [SerializeField]
    private GameObject splat;

    private bool downAttack;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
    void FixedUpdate()
    {
        //if (!downAttack)
            rb2d.velocity = direction * speed;
        //else
            //rb2d.velocity = new Vector2(direction.x * speed, -speed);
    }

    public void Initialize(Vector2 dir, bool down)
    {
        direction = dir;
        /*if (down)
        {
            downAttack = true;
            direction.y = -45;
        }
        else
        {
            downAttack = false;
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(splat, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
