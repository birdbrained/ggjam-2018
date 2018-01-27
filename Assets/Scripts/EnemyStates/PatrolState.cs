using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyStates
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Enemy e)
    {
        enemy = e;
        patrolTimer = 0;
        patrolDuration = UnityEngine.Random.Range(3, 7);
    }

    public void Execute()
    {
        Patrol();
        enemy.Move();
        if (enemy.Target != null && enemy.InFireRange)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
        if (other.tag == "Bullet")
        {
            enemy.Target = PlayerController.Instance.gameObject;
        }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
