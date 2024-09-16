using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{

    private Enemy parent;

    private float attackCoolDown = 0.5f;

   // private float extraRange = .1f;
    public void Enter(Enemy parent)
    {
        this.parent = parent;  
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
        if (parent.MyAttackTime >= attackCoolDown && !parent.IsAttacking)
        {
            parent.MyAttackTime = 0;

            parent.StartCoroutine(Attack());
        }
        if (parent.MyTarget != null)
        {
            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);

            if (distance >= parent.MyAttackRange && !parent.IsAttacking)
            {
                parent.ChangeState(new FollowState());
            }
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }
    public IEnumerator Attack()
    {
        parent.IsAttacking = true;

        parent.MyAnimator.SetTrigger("attackTrigger");

        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(2).length);

        parent.IsAttacking = false;

    }
}
