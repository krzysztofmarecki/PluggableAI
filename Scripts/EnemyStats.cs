using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject {
    
	public float lookRange = 40f;               // LookDecision
    public float coneSightAngle = 65f;          // LookDecision, AttacAction

    public float attackRange = 1f;              // AttackAction in future as weaponStat

	public float searchDuration = 40f;          // SearchAreaDecision ( call method StateController::isSearchTimeElapsed() )
    public float searchRadius = 40f;            // SearchAreaDecision
}