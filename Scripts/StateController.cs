using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    public State currentState;
	public EnemyStats enemyStats;
	public Transform eyes;
    public State remainInState;
    public Transform target;   // Transform of f.e. player that AI will look for

	[HideInInspector] public NavMeshAgent navMeshAgent { get; private set; }
//	[HideInInspector] public Complete.TankShooting tankShooting;

    // PatrolAction
	[HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;

    [HideInInspector] public Vector3 lastSeenLocation;
    private float timeInState = 0f;
    

	private bool aiActive;


	void Awake () 
	{
//		tankShooting = GetComponent<Complete.TankShooting> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();

        // przekopiowane z SetupAI
        aiActive = true;
        if (aiActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

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

    void Update()
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
        timeInState += Time.deltaTime;
//        Debug.Log("TimeInState " + timeInState);
        return timeInState >= enemyStats.searchDuration;
    }
}