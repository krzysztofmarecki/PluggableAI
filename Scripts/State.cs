using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.grey;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }
	
    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceded = transitions[i].decision.Decide(controller);
            State nextState;
            if (decisionSucceded)
            {
                nextState = transitions[i].trueState;
            }
            else
            {
                nextState = transitions[i].falseState;
            }

            // on first transition to other state we stop iterating
            // hierarchy of transistions is from most to least important
            // it's behaviour clarification, not needed to work
            if (nextState != controller.remainInState)
            {
                controller.TransitionToState(nextState);
                return;
            }
        }
    }
}
