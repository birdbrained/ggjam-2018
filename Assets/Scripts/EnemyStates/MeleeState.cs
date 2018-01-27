using UnityEngine;
using System.Collections;
using System;

public class MeleeState : IEnemyStates
{
    private Enemy enemy;
    private float attackTimer;
    private float attackCooldown = 3;
    private bool canAttack = true;

    public void Enter(Enemy e)
    {
        enemy = e;
    }

    public void Execute()
    {
        Attack();
        if (enemy.InFireRange && !enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState());
        }
        else if (enemy.Target == null)
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

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            canAttack = true;
            attackTimer = 0;
        }
        if (canAttack)
        {
            canAttack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}
