using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/SearchArea")]
public class SearchAreaDecision : Decision {

    public override bool Decide(StateController controller)
    {
        // keep moving till time elapsed
        return controller.isSearchTimeElapsed();
    }

    
    
}
