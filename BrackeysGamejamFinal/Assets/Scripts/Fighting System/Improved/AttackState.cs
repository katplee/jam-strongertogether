using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    public BattleState State { get { return BattleState.ATTACKING; } }

    private FightManager FM;
    private HUDManager HM;

    private Animator animator;
    private bool isPlayerTurn;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Attack state!");
        FightManager.Instance.ChangeStateName(State);

        this.animator = animator;
        isPlayerTurn = animator.GetBool("isPlayerTurn");

        SubscribeEvents();
        SetManagers();

        if (!isPlayerTurn) { FM.OnAttack(isPlayerTurn); }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //the HUDs are continuously updated during this state
        HM.HPlayer.UpdateHUD(FM.Player);
        HM.HEnemy.UpdateHUD(FM.Enemy);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Attack exit");
        UnsubscribeEvents();
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
        FM = FightManager.Instance;
        HM = HUDManager.Instance;
    }

    private void ChangePlayerState()
    {
        bool state = isPlayerTurn ^ true;
        animator.SetBool("isPlayerTurn", state);
        animator.SetBool("isAttacking", false);
    }

    private void OnFightEnd(BattleState state)
    {
        if (state == BattleState.WON) { animator.SetBool("playerWon", true); }
        else { animator.SetBool("enemyWon", true); }
    }

    private void SubscribeEvents()
    {
        FightManager.OnTurnEnd += ChangePlayerState;
        FightManager.OnFightEnd += OnFightEnd;
    }

    private void UnsubscribeEvents()
    {
        FightManager.OnTurnEnd -= ChangePlayerState;
        FightManager.OnFightEnd -= OnFightEnd;
    }
}
