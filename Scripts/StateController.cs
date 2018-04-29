using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshBuilder))]
[RequireComponent(typeof(Collider2D))]
public class StateController : MonoBehaviour {

    public State currentState;
	public EnemyStats enemyStats;
	public Transform eyes;
    public State remainInState;

    public GameObject target;
    [HideInInspector]
    public Transform targetTransform;   // Transform of f.e. player that AI will look for
    [HideInInspector]
    public Animator targetAnimator;

	[HideInInspector] public NavMeshAgent navMeshAgent { get; private set; }

    // PatrolAction
	[HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;

    [HideInInspector] public Vector3 lastSeenLocation;
    private float timeInState = 0f;
    

	private bool aiActive;


	void Awake () 
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
        targetAnimator = target.GetComponent<Animator>();
        targetTransform = target.GetComponent<Transform>();
        aiActive = true;
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
            Gizmos.DrawWireSphere(lastSeenLocation, 1f);
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