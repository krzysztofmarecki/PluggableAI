using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Hear")]
public class HearDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return Hear(controller);
    }

    private bool Hear(StateController controller)
    {
        var targetAnim = controller.targetAnimator;

        // determine state of animator
        // at final use Animator::StringToHash and have hashIds of State names to check at e.g. EnemyStats
        // or maybe have some bools at Target (player's GO) to determine is running, walking, crounching etc.
        // or int and bunch of enums
        if (targetAnim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-WalkRun-Blend"))
        {
            Transform target = controller.targetTransform;
            float distance = Vector3.Distance(target.position, controller.eyes.transform.position);
            if (distance <= 15f)    // hardCoded for now
                                    // place for distance modifiers for crounching, walking, running etc.
            {
                Debug.Log("I heard you");
                // randomize target's position, we only heard him after all
                var heardPosition = heardRandomizedPosition(target.position, distance);
                
                // set current position as destination position to make NavMeshAgent to stop
                controller.navMeshAgent.ResetPath();
                controller.navMeshAgent.isStopped = false;

                controller.lastTargetLocation = heardPosition;

                return true;
            }

        }

        return false;
    }

    Vector3 heardRandomizedPosition(Vector3 truePosition, float distance)
    {
        var searchRadius = distance * 0.3f;
        // create random point in NavMesh to go to
        Vector3 randomDirection = Random.insideUnitSphere * searchRadius;
        randomDirection += truePosition;

        // sample vector to the NavMesh
        NavMeshHit newDestination;
        NavMesh.SamplePosition(randomDirection, out newDestination, searchRadius, 1);

        return newDestination.position;

    }
}
