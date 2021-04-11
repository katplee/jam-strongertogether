using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartState : StateMachineBehaviour
{
    public BattleState State { get { return BattleState.START; } }

    private FightManager FM;
    private PrefabManager PM;
    private TransformManager TM;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FM = FightManager.Instance;
        PM = PrefabManager.Instance;
        TM = TransformManager.Instance;

        //instantiate the player prefab at the player transform
        FM.Player = Instantiate(PM.PPlayer, TM.TPlayer);
        //add the player script
        FM.PScript = FM.Player.AddComponent<Player>();
        //update the player's HUD
        //FM.PHUD.UpdateHUD(FM.PScript);

        //do the same for the enemy
        FM.Enemy = Instantiate(PM.PEnemy, TM.TEnemy);
        FM.EScript = FM.Enemy.AddComponent<Enemy>();
        //FM.EHUD.UpdateHUD(FM.EScript);
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
}
