using UnityEngine;
using System.Collections;

public class NavMeshAgentScript : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	

	void Update () {
		agent.SetDestination(target.position);
	}
}
