using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected Transform JuicePos;
    [SerializeField]
    protected GameObject JuicePrefab;
    [SerializeField]
    private EdgeCollider2D meleeCollider;
    [SerializeField]
    protected List<string> damageSources = new List<string>();
    public float speed;
    protected bool facingRight;
    [SerializeField]
    protected int health;
    protected int startHealth;
    public abstract bool IsDead { get; }
    public bool Attacking { get; set; }
    public bool TakingDamage { get; set; }
    public Animator MyAnimator { get; private set; }

    public EdgeCollider2D MeleeCollider
    {
        get
        {
            return meleeCollider;
        }
    }

    public bool hasARangedAttack;
    public bool hasAMeleeAttack;


    // Use this for initialization
    public virtual void Start ()
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
        startHealth = health;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract IEnumerator TakeDamage();
    public abstract void Death();

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public virtual void FireJuice(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(JuicePrefab, JuicePos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            if (tag == "Player")
            {
                tmp.GetComponent<JuiceBullets>().Initialize(Vector2.right, false);
            }
            else
            {
                tmp.GetComponent<JuiceEnemyBullets>().Initialize(Vector2.right, false);
            }
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(JuicePrefab, JuicePos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            if (tag == "Player")
            {
                tmp.GetComponent<JuiceBullets>().Initialize(Vector2.left, false);
            }
            else
            {
                tmp.GetComponent<JuiceEnemyBullets>().Initialize(Vector2.left, false);
            }
        }
    }

    public void MeleeAttack()
    {
        if (hasAMeleeAttack)
        {
            //MeleeCollider.enabled = !MeleeCollider.enabled;
            MeleeCollider.enabled = true;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("My name :" + gameObject + " / My tag: " + gameObject.tag + " / Other tag: " + other.tag);
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
