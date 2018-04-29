using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action {

    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);
        
        Transform target = controller.target;
        if (isTargetInRange(target, controller))
        {
            // Call attack method
            // remember to delete controller.enemyStats.attackRange, attackRange should be weapon's variable

        }
    }

    bool isTargetInRange(Transform target, StateController controller)
    {
        float distance = Vector3.Distance(target.position, controller.eyes.transform.position);

        // check distance
        if (distance <= controller.enemyStats.attackRange)
        {
            Vector3 direction = (target.position - controller.eyes.transform.position);

            // check if is actually in sight (no X-Ray allowed)
            RaycastHit hit;
            if (Physics.Raycast(controller.eyes.transform.position, direction, out hit, distance))
            {
                Debug.DrawRay(controller.eyes.transform.position, direction, Color.red);
                return hit.transform == target;
            }
        }
        return false;
    }
}
