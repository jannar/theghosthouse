using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable { //implements IDamageable

	public float startingHealth; //starting health variable
	public float health; //variable for health
	protected bool dead; // is object dead or not
	protected bool won;

	public event System.Action OnDeath;
	public event System.Action OnWin;

	public virtual void Start(){ //virtual allows classes that use this base to override this method
		health = startingHealth;//set health equal to starting health
	}

	public void TakeDamage(float damage){ //uses take hit method from IDamageable
		health -= damage; //reduce health by damage amount

		if (health <= 0 && !dead){ //if health is zero or less and the object is not already dead
			Die(); //call die method
		}
	}

	public void OnTriggerEnter(Collider other){
			if(other.gameObject.tag == "Goal"){
			Win();
			}
		}

		
	[ContextMenu("Die")]
	public void Die(){ 
		dead = true; //set dead equal to true
		if (OnDeath != null){
			OnDeath();
		}
		GameObject.Destroy(gameObject); //calls destroy method on gameobject
	}

	[ContextMenu("Win")]
	public void Win(){ 
		won = true; //set won equal to true
		if (OnWin != null){
			OnWin();
		}
		GameObject.Destroy(gameObject); //calls destroy method on gameobject
	}


}
