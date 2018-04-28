using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject {
    
	public float moveSpeed = 1;         // not used so far
	public float lookRange = 40f;       // LookDecision
	public float lookSphereCastRadius = 1f;     // not used so far (for debug only)
    public float coneSightAngle = 65f;          // LookDecision, AttacAction

    public float attackRange = 1f;
	public float attackRate = 1f;
	public float attackForce = 15f;
	public int attackDamage = 50;

	public float searchDuration = 40f;
	public float searchingTurnSpeed = 120f;
    public float searchRadius = 40f;
}