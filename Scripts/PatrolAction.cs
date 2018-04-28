using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action {

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        if (controller.wayPointList.Count == 0) return;     // added for temporary bc no waypoints so far

        controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        controller.navMeshAgent.isStopped = false;      // in tut: .Resume()

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }

}
