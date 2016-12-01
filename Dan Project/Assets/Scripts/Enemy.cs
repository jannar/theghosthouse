using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : MonoBehaviour {

	public enum State {Idle, Chasing, Attacking};
	State currentState;

	NavMeshAgent pathfinder; //our nav mesh agent
	Transform target; //our target
	LivingEntity targetEntity;
	public bool hasTarget;
	public bool movementActive = false; // we are moving
	public bool targetInSight= false; //we have a target in line of sight
	public float viewDistance = 10f; //line of sight distance
	public float viewAngle = 30f; //line of sight angle
	public float counter = 5; //counter for how long this enemy will keep moving after losing sight of the target
	public float attackDistance = 3f;
	public float attackTimer = 1;
	public float damage = 1;
	float nextAttack; 
	float colliderRadius;
	float targetColliderRadius;
	public AudioClip wakeAudio; //sound to play when target is sighted
	public AudioClip chaseAudio; //sound to play while in pursuit
	public AudioClip sleepAudio; //sound to play when target is lost

	void Start () {
		pathfinder = GetComponent <NavMeshAgent>(); //gets the nav mesh agent component
		if(GameObject.FindGameObjectWithTag("Player") !=null){
			hasTarget = true;
			target = GameObject.FindGameObjectWithTag("Player").transform; //sets the target as the player
			targetEntity = target.GetComponent<LivingEntity>();
			targetEntity.OnDeath += targetDeath;
			targetEntity.OnWin += targetWin;
			viewDistance = 20f; 
			viewAngle = 360f;
			counter = 10;
			currentState = State.Chasing;
			colliderRadius = GetComponent<CapsuleCollider>().radius;
			targetColliderRadius = target.GetComponent<CapsuleCollider>().radius;
			//movementActive = false;
			//StartCoroutine (UpdatePath());
		}
	}

	void targetWin(){
		hasTarget = false;
		currentState = State.Idle;
	}
	void targetDeath(){
		hasTarget = false;
		currentState = State.Idle;
	}

	void Update () {

	Debug.Log(currentState); 
		if(hasTarget){
			Vector3 targetDirection = target.position - this.transform.position; //gets direction of target 
			float targetAngle = Vector3.Angle(targetDirection,this.transform.forward); //gets angle to target

			if(Vector3.Distance(target.position, this.transform.position) < viewDistance && targetAngle < viewAngle){ //checks if target is in line of sight
				targetInSight = true;
			}else{
				targetInSight = false;
			}

			if(Time.time > nextAttack){
				float squareDistanceToTarget = (target.position - transform.position).sqrMagnitude; //distance squared between us and target
					if (squareDistanceToTarget < Mathf.Pow(attackDistance,2)){
						nextAttack = Time.time + attackTimer;
					StartCoroutine(Attack());
					}
				}

		 	if(targetInSight == true && movementActive == false){ //if the target is in sight but we're not moving
				movementActive = true; //set moving to true
				AudioManager.instance.PlaySound(wakeAudio, transform.position); //play wake sound
				//pathfinder.Resume(); //resume nav mesh agent if it was previously paused
				pathfinder.enabled = true;
				StartCoroutine(UpdatePath()); //starts update path coroutine
			}else if(targetInSight == false && movementActive == true ){ //if there is no target in sight and we're moving
				if(counter <= 0){ //if the counter is 0 or less
					currentState = State.Idle;
					movementActive = false; //set movement to false
					AudioManager.instance.PlaySound(sleepAudio, transform.position); //play sleep sound
					StopCoroutine(UpdatePath()); //stop update path coroutine
					//pathfinder.Stop(); //stop nav mesh agent
					pathfinder.enabled = false;

				}else{
					counter -= 1 * Time.deltaTime; //else reduce the counter
				}
			}else if(targetInSight == false && movementActive == false){ //if there is no target in sight and we are not moving
				counter = 5; //reset counter
			}
		}
	}


	IEnumerator UpdatePath(){ //coroutine to update nav mesh path
		float refreshRate = 0.25f; //time to wait before each run

		while (hasTarget){ //while there is a target
			if(currentState == State.Chasing){
				Vector3 directionToTarget = (target.position -transform.position).normalized; //normalized direction to target
				Vector3 targetPosition = target.position - directionToTarget * (colliderRadius + targetColliderRadius);//new Vector3(target.position.x, 0, target.position.z); //the target's position
				pathfinder.SetDestination(targetPosition); //set the destination for the nav mesh agent
			}
			yield return new WaitForSeconds(refreshRate); //waits the refresh rate before calling again
		}
	}

	IEnumerator Attack(){ //coroutine for attacking player
		currentState = State.Attacking;
		pathfinder.enabled = false;
		Vector3 startingPosition = transform.position; //position at start of attack
		Vector3 attackPosition = target.position; //position at moment of hit

		float percent = 0; //percentage of the way through the attack
		float attackSpeed = 3; //speed of the attack
		bool hasDamaged = false;
		while (percent <= 1) { //while attack is less than 100% complete
			if (percent >= .5f && !hasDamaged){
				hasDamaged = true;
				targetEntity.TakeDamage(damage);
			}

			percent += Time.deltaTime * attackSpeed; //increase attack percentage of completion by time multiplied by speed of attack
			float interpolation = (-Mathf.Pow(percent,2) + percent) * 4; //parabola from 0 to 1 to 0
			transform.position = Vector3.Lerp(startingPosition, attackPosition, interpolation); //lerps position from starting to attack back to starting,  using our parabola

			yield return null;
		}
		currentState = State.Chasing;
		pathfinder.enabled = true;
	}
}



