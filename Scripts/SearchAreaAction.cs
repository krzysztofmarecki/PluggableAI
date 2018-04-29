using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/SearchArea")]
public class SearchAreaAction : Action {

    public override void Act(StateController controller)
    {
        SearchArea(controller);
    }

    private void SearchArea(StateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.yellow);
        
        if (controller.timeInState == 0f) // if 1st iteration (Acts are executed before Decisions, so timeInState can be 0, bc timeInState is incremented in Decision)
        {
            controller.navMeshAgent.SetDestination(controller.lastTargetLocation);
        }
        
        // check is already in position
        // if so, generate next point and set new destination
        else if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance &&
            !controller.navMeshAgent.pathPending)
        {
            var searchRadius = controller.enemyStats.searchRadius;
            // create random point in NavMesh to go to
            Vector3 randomDirection = Random.insideUnitSphere * searchRadius;
            randomDirection += controller.lastTargetLocation;

            // sample vector to the NavMesh
            NavMeshHit newDestination;
            NavMesh.SamplePosition(randomDirection, out newDestination, searchRadius, 1);

            // set new destination
            controller.navMeshAgent.SetDestination(newDestination.position);
            controller.navMeshAgent.isStopped = false;
        }

    }
}
