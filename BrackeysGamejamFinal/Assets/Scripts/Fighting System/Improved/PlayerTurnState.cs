using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : StateMachineBehaviour
{
    public BattleState State { get { return BattleState.PLAYERTURN; } }

    private ActionManager AM;
    private FightManager FM;

    private Animator animator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Player turn state start!");
        FightManager.Instance.ChangeStateName(State);
        animator.SetBool("isPlayerTurn", true);
        
        this.animator = animator;

        SubscribeEvents();
        SetManagers();
        SetButtonAccess();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
        AM = ActionManager.Instance;
        FM = FightManager.Instance;
    }

    private void SetButtonAccess()
    {
        //player can attack
        AM.AAttack.SetInteractability(true);
        //player can leave
        AM.ALeave.SetInteractability(true);
        //player can fuse with a dragon
        AM.ASwitch.SetInteractability(true);
    }

    private void FireButtonResponse(string buttonName)
    {
        switch (buttonName)
        {
            case "Attack":
                animator.SetBool("isAttacking", true);
                break;

            case "Switch":
                //do something
                //animator.SetBool("isPlayerTurn", false);
                break;

            default:
                break;
        }
    }

    private void SubscribeEvents()
    {
        ActionManager.OnButtonPress += FireButtonResponse;
    }

    private void UnsubscribeEvents()
    {
        ActionManager.OnButtonPress -= FireButtonResponse;
    }
}
