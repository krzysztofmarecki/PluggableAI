using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour {

    public State currentState;
    public EnemyStats enemyStats;
    public Transform eyes;
    public State remainInState;

    public GameObject target;
    public Transform targetTransform { get; private set; }
    public Animator targetAnimator { get; private set; }

	public NavMeshAgent navMeshAgent { get; private set; }

    // PatrolAction
	[HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;

    // SearchAreaAction, LookDecision, HearDecision
    [HideInInspector] public Vector3 lastTargetLocation;
    private float timeInState = 0f;
    
    // dummy for now, maybe helpfull in future
	private bool aiActive = true;
    
	void Awake () 
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
        targetAnimator = target.GetComponent<Animator>();
        targetTransform = target.GetComponent<Transform>();
    }

    // unused for now, maybe will be helpfull in future
	public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
	{
		wayPointList = wayPointsFromTankManager;
		// aiActive = aiActivationFromTankManager;  // nie używam tego
        aiActive = true;
		if (aiActive) 
		{
			navMeshAgent.enabled = true;
		} else 
		{
			navMeshAgent.enabled = false;
		}
	}

    void FixedUpdate()
    {
        if (!aiActive) { return; }
        currentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
    //        Debug.Log("onDrawGizmos");
            Gizmos.color = currentState.sceneGizmoColor;
    //        Debug.Log(Gizmos.color.ToString());
            Gizmos.DrawWireSphere(lastTargetLocation, 1f);
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainInState)
        {
            Debug.Log("Changed state from " + currentState.name + ", to " + nextState.name);
            currentState = nextState;
            OnNewState();
        }
    }

    private void OnNewState()
    {
        timeInState = 0f;
    }

    public bool isSearchTimeElapsed()
    {
        // bc AI is in FixedUpdate
        timeInState += Time.fixedDeltaTime;
//        Debug.Log("TimeInState " + timeInState);
        return timeInState >= enemyStats.searchDuration;
    }
}