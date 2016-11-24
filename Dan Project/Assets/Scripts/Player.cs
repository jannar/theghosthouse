using UnityEngine;
using System.Collections;

public class Player : LivingEntity {
	
	public float moveSpeed = 6;
	Vector3 velocity;
	Rigidbody rb; 
	Camera mainCam;


	// Use this for initialization
	public override void Start () { //overrides base class start method
		base.Start(); //calls start method of base class, allowing both start methods to run
		rb = GetComponent<Rigidbody>();
		mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y));
		transform.LookAt (mousePos + Vector3.up * transform.position.y);
		velocity = new Vector3(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;
	}

	void FixedUpdate() {
		rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
	}


}
