using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/CheckTargetPosition")]
public class CheckTargetPositionAction : Action {
    
    private static readonly Color orange = new Color(1, 0.5f, 0);

    public override void Act(StateController controller)
    {
        goToPosition(controller);
    }

    private void goToPosition(StateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, orange);
        // if current destination is not set to lastTargetLocation
        // change destination
        if (controller.navMeshAgent.destination != controller.lastKnownTargetLocation)
        {
            controller.navMeshAgent.SetDestination(controller.lastKnownTargetLocation);
        }
    }
}
