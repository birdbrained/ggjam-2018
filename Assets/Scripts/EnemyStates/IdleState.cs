using UnityEngine;
using System.Collections;

public class IdleState : IEnemyStates
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration;

    public void Enter(Enemy e)
    {
        enemy = e;
        idleTimer = 0;
        idleDuration = UnityEngine.Random.Range(1, 4);
        Debug.Log("Idle");
    }

    public void Execute()
    {
        Idle();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            enemy.Target = PlayerController.Instance.gameObject;
        }
    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("walkSpeed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
