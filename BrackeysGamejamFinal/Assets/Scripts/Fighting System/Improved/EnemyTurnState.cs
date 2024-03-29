﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : StateMachineBehaviour
{
    public BattleState State { get { return BattleState.ENEMYTURN; } }

    private ActionManager AM;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enemy turn state start!");
        FightManager.Instance.ChangeStateName(State);

        SetManagers();
        SetButtonAccess();

        MoveToEnemyTurn(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void SetManagers()
    {
        AM = ActionManager.Instance;
    }

    private void SetButtonAccess()
    {
        //player cannot attack
        //player cannot leave
        //player cannot fuse with a dragon
        AM.SetAllInteractability(false);
    }

    private void MoveToEnemyTurn(Animator animator)
    {
        animator.SetBool("isAttacking", true);
    }
}
