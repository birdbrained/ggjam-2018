using UnityEngine;
using System.Collections;
using System;

public class Enemy : Character
{
    protected IEnemyStates currentState;
    public GameObject Target { get; set; }
    [SerializeField]
    protected float meleeRange;
    [SerializeField]
    protected float fireRange;
    //[SerializeField]

    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    [SerializeField]
    private bool spawnFacingLeft;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }
    public bool InFireRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= fireRange;
            }
            return false;
        }

    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        PlayerController.Instance.Dead += new DeadEventHandler(RemoveTarget);
        if (spawnFacingLeft)
        {
            ChangeDirection();
        }
        ChangeState(new IdleState());
	}

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    protected void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }
	
	// Update is called once per frame
	void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
	}

    public void ChangeState(IEnemyStates newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        if (!Attacking)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("walkSpeed", 1);
                transform.Translate(GetDirection() * (speed * Time.deltaTime));
            }
            else if (currentState is PatrolState)
            {
                ChangeDirection();
            }
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public override IEnumerator TakeDamage()
    {
        //Debug.Log("Taking damage");
        health -= 10;
        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            MyAnimator.SetTrigger("die");
            Debug.Log(gameObject.tag);
            gameObject.tag = "Corpse";
            Debug.Log(gameObject.tag);
            yield return null;
        }
    }

    public override void Death()
    {
        //Instantiate(GameManager.Instance.ScorePrefab, new Vector3(transform.position.x, transform.position.y + 1), Quaternion.identity);
        Destroy(gameObject);
    }
}
