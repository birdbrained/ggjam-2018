using UnityEngine;
using System.Collections;

public class RangedState : IEnemyStates
{
    private Enemy enemy;
    private float fireTimer;
    private float fireCooldown = 3;
    private bool canFire = true;

    public void Enter(Enemy e)
    {
        enemy = e;
    }

    public void Execute()
    {
        Fire();
        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }
        else if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireCooldown)
        {
            canFire = true;
            fireTimer = 0;
        }
        if (canFire && enemy.hasARangedAttack)
        {
            canFire = false;
            enemy.MyAnimator.SetTrigger("fire");
        }
    }
}
