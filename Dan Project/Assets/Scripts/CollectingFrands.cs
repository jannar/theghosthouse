using UnityEngine;
using System.Collections;

public class CollectingFrands : MonoBehaviour {

public bool found;
GameObject corgi;
GameObject frandCounter;
NavMeshAgent pathfinder;
FrandCounter fc;
float colliderRadius; 
float corgiColliderRadius;

	// Use this for initialization
	void Start () {


	corgi = GameObject.Find("Player 1");
	frandCounter = GameObject.Find("FrandCounter");
	fc = frandCounter.GetComponent<FrandCounter>();
	pathfinder = GetComponent<NavMeshAgent>(); 
	corgiColliderRadius = corgi.GetComponent<CapsuleCollider>().radius; 
	colliderRadius = GetComponent<CapsuleCollider>().radius; 

	found = false;
	
	}
	
	// Update is called once per frame
	void Update () {

	UpdatePath();
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("FrandFinder2") == true && found == false) 
		{
			found = true;
			fc.increaseFrands();
		} 
	}

	void UpdatePath(){ //coroutine to update nav mesh path
		float refreshRate = 0.25f; //time to wait before each run

		if (found == true){ //while there is a target
				Vector3 directionToTarget = (corgi.transform.position -transform.position).normalized;  //normalized direction to target
				Vector3 targetPosition = corgi.transform.position - directionToTarget * (colliderRadius + corgiColliderRadius);//new Vector3(target.position.x, 0, target.position.z); //the target's position
				pathfinder.SetDestination(targetPosition); //set the destination for the nav mesh agent
			}
		}
}
