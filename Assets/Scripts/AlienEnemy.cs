using UnityEngine;
using System.Collections;
using System;

public class AlienEnemy : Enemy
{
    private bool canGivePoints = true;
    private bool canSpawnPointPickup = true;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
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

    void FixedUpdate()
    {
        if (IsDead)
        {
            if (canSpawnPointPickup)
            {
                GameObject score = (GameObject)Instantiate(GameManager.Instance.ScorePrefab, new Vector3(transform.position.x, transform.position.y + 1), Quaternion.identity);
                Physics2D.IgnoreCollision(score.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                canSpawnPointPickup = false;
            }
            transform.Rotate(new Vector3(0, 0, 6));
            if (Mathf.Abs(transform.localScale.x) > 0.01)
            {
                if (facingRight)
                {
                    transform.localScale -= new Vector3(0.005f, 0.005f, 0);
                }
                else
                {
                    transform.localScale -= new Vector3(-0.005f, 0.005f, 0);
                }
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public override IEnumerator TakeDamage()
    {
        base.TakeDamage();
        health -= 10;
        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            MyAnimator.SetTrigger("die");
            //Debug.Log(gameObject.tag);
            //Debug.Log(gameObject.tag);
            yield return null;
            gameObject.tag = "Corpse";
            if (canGivePoints)
            {
                GameManager.Instance.Score += 50;
                canGivePoints = false;
            }

        }
    }
}
