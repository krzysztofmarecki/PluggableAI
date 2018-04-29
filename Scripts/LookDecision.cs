using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision {
    

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        Transform target = controller.targetTransform;
        if (inConeSight(target, controller))
        {
            Debug.Log("I See you");
            controller.lastSeenLocation = target.transform.position;
            FaceTarget(target, controller);
            return true;
        }
        return false;
    }

    bool inConeSight(Transform target, StateController controller)
    {
        float distance = Vector3.Distance(target.position, controller.eyes.transform.position);

        // check distance
        if (distance <= controller.enemyStats.lookRange)
        {
            // calculate and check angle
            Vector3 direction = (target.position - controller.eyes.transform.position);
            var angle = Vector3.Angle(controller.eyes.transform.forward, direction);
            if (angle <= controller.enemyStats.coneSightAngle)
            {
                // check if is actually in sight (no X-Ray allowed)
                RaycastHit hit;
                if (Physics.Raycast(controller.eyes.transform.position, direction, out hit, distance))
                {
                    return hit.transform == target;
                }
            }
        }
        return false;
    }

    void FaceTarget(Transform target, StateController controller)
    {
        Vector3 direction = (target.position - controller.eyes.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        controller.eyes.transform.rotation = Quaternion.Slerp(controller.eyes.transform.rotation, lookRotation, Time.fixedDeltaTime * 5f);
    }
}
