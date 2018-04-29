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
        
        var target = controller.target;

        
       
        if (inConeFight(target, controller))
        {
            // Call attack method

        }
    }

    bool inConeFight(Transform target, StateController controller)
    {
        float distance = Vector3.Distance(target.position, controller.eyes.transform.position);
        Debug.Log("distance" + distance);

        // check distance
        if (distance <= controller.enemyStats.attackRange)
        {
            // calculate and check angle
            Vector3 direction = (target.position - controller.eyes.transform.position);
            var angle = Vector3.Angle(controller.eyes.transform.forward, direction);
            Debug.Log("angle: " + angle);
            if (angle <= controller.enemyStats.coneSightAngle)
            {
                // check if is actually in sight (no X-Ray allowed)
                RaycastHit hit;
                if (Physics.Raycast(controller.eyes.transform.position, direction, out hit))
                {
                    Debug.DrawRay(controller.eyes.transform.position, direction, Color.red);
                    //    Debug.Log(hit.transform.ToString());
                    //    Debug.Log(target.ToString());
                    return hit.transform == target;
                }
            }
        }
        return false;
    }
}
