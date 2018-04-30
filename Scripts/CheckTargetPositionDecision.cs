using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/CheckTargetPosition")]
public class CheckTargetPositionDecision : Decision {

    public override bool Decide(StateController controller)
    {
        // if already at destination, return true
        return ( (  controller.navMeshAgent.remainingDistance <=
                    controller.navMeshAgent.stoppingDistance    ) && !controller.navMeshAgent.pathPending);
    }
}
