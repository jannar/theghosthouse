using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

	[HideInInspector] //keeps list from being displayed in the inspecor
	public List<Transform> visibleTargets = new List<Transform>(); //list of transforms of visible targets
	public float viewRadius; //radius within which objects are considered viewable
	[Range(0,360)] //clamps viewAngle to a value between 0 and 360
	public float viewAngle; //angle within which objects are considered viewable
	public LayerMask obstacleMask; //layer mask for obstacles
	public LayerMask targetMask; //layer mask for targets
	public float meshResolution; // how many raycasts will be made per degree of angle
	public int edgeResolveIterations; //number of times to iterate on edge finding method
	public float edgeDistanceThreshold; //used to aid in properly resolving edge cases involving multiple obstacles
	public float maskCutawayDistance = .1f;

	public MeshFilter viewMeshFilter; //where we will assign our mesh filter in the inspector
	Mesh viewMesh; //the mesh variable

	// Use this for initialization
	void Start(){
		viewMesh = new Mesh (); //create our mesh
		viewMesh.name = "View Mesh"; //name our mesh
		viewMeshFilter.mesh = viewMesh; //set our mesh as the one used by our meshfilter
		StartCoroutine("FindTargetsWithDelay", .2f); //initiates the target finder coroutine and passes in delay of .2 seconds
	}

	void Update(){
		
	}

	IEnumerator FindTargetsWithDelay(float delay){ //coroutine that will regularly call our target finding method
		while(true){
			yield return new WaitForSeconds (delay); //wait for the appropriate amount of time
			FindVisibleTargets(); //call this method
		}
	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();//clears the visible targets list so that targets no longer in fov will not remain in list
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask); 
		//gets array of all colliders on targets touching or within our view radius using a sphere.
		//to ensure they are targets we use our target layermask 
		for(int i = 0; i < targetsInViewRadius.Length; i++){ //runs a for loop on each of these colliders
			Transform target = targetsInViewRadius [i].transform; //gets the transform of the target
			Vector3 dirToTarget = (target.position - transform.position).normalized; //check if target is within field of view angle
			//finds the direction to the target by taking it's position, subtracting our position, normalized
			if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2){ //looks to see if the distance between
				//our forward vector and the direction to target is less than half of our view angle
				float distanceToTarget = Vector3.Distance (transform.position, target.position); //gets the distance between our position and the target's
				if(!Physics.Raycast (transform.position, dirToTarget, distanceToTarget, obstacleMask)){ //if there is no collision when performing a raycast:
					//when raycast returns false from our position in the direction of the target with the correct distance and layer
					visibleTargets.Add (target);//adds target to visible targets list
				}
			}
		}
	}

	void LateUpdate() { //updates after the player controller
		DrawFieldOfView(); //calls method to draw our fov
	}

	void DrawFieldOfView(){ //will visualize the fov using raycasts
		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution); //number of raycasts equals the fov angle multiplied by the defined resolution, rounded to an integer
		float stepAngleSize = viewAngle / stepCount; //number of degrees per step, reached by dividing the fov angle by the number of steps
		List<Vector3> viewPoints = new List<Vector3> (); //list of view points
		RaycastInfo oldViewRaycast = new RaycastInfo(); //information for what will be a previous raycast
		for (int i = 0; i <= stepCount; i++){ //for each step
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;/* current angle is equal to:
				objects rotation minus half of our fov angle, plus the number of degrees per step multiplied by the step count*/
			//Debug.DrawLine(transform.position,transform.position + DirectionFromAngle(angle,true) * viewRadius, Color.red);
			RaycastInfo newViewRaycast = ViewRaycast (angle); //creates a new raycastinfo by passing in our angle to the view raycast method

			if (i > 0){//this will check to see if the old and new raycasts are on either side of an obstacle edge
				bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewRaycast.length - newViewRaycast.length) > edgeDistanceThreshold; 
				//bool that check whether the distance between our old and new raycasts exceeds our set threshold 
				if (oldViewRaycast.hit != newViewRaycast.hit || (oldViewRaycast.hit && newViewRaycast.hit && edgeDistanceThresholdExceeded)){//checks if either:
					//1. the old cast is hitting and obstacle and the new cast is not, or vice versa
					//2. if both hit something and the distance between the two exceeds our distance threshold
					EdgeInfo edge = FindEdge (oldViewRaycast, newViewRaycast); //creates edge information using our find edge method, passing in our old and new raycasts
					if(edge.pointA != Vector3.zero){ //checks that edge point A is no longer at its default value
						viewPoints.Add (edge.pointA); //add edge point A to view points
					}
					if(edge.pointB != Vector3.zero){ //checks that edge point B is no longer at its default value
						viewPoints.Add (edge.pointB); //add edge point B to view points
					}
				}
			}


			viewPoints.Add(newViewRaycast.point); //adds the new view point from our raycast to our list
			oldViewRaycast = newViewRaycast; //sets the old raycast equal to the new raycast
		}

		int vertexCount = viewPoints.Count + 1; //number of vertices equals the number of view points plus 1 (our origin point)
		Vector3[] vertices = new Vector3[vertexCount];//array for vertices 
		int[] triangles = new int[(vertexCount - 2) * 3]; //array for triangles

		vertices[0] = Vector3.zero; //first vertex equals origin position
		for (int i = 0; i < vertexCount - 1; i++){ //loop through remaining vertices not including origin
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints [i]) + Vector3.forward * maskCutawayDistance;//converts view points to global points
			// adds forward vector multiplied by our cutaway distance to show edges of obstacles
			if (i < vertexCount - 2){//MATH THAT I DON'T FULLY UNDERSTAND BUT WORKS FOR SOME REASON
				triangles [i * 3] = 0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}
		viewMesh.Clear (); //clears the mesh to reset
		viewMesh.vertices = vertices; //sets mesh vertices to our array of vertices
		viewMesh.triangles = triangles; //sets mesh triangles to our array of triangles
		viewMesh.RecalculateNormals (); //we've just added a lot of information to the mesh, so we recalculate its normals
	}

	EdgeInfo FindEdge(RaycastInfo minimumViewRaycast, RaycastInfo maximumViewRaycast){ //edge information method that takes in raycast information
		float minimumAngle = minimumViewRaycast.angle; //the angle of the passed in minimum raycast
		float maximumAngle = maximumViewRaycast.angle; //the angle of the passed in maximum raycast
		Vector3 minimumPoint = Vector3.zero;  //minimum raycast point, defaulted at 0,0,0
		Vector3 maximumPoint = Vector3.zero; //maximum raycast point, defaulted at 0,0,0

		for (int i = 0; i < edgeResolveIterations; i++) { //for each iteration of the edge resolution
			float angle = (minimumAngle + maximumAngle) / 2; //gets our new angle, halfway between our minumum and maximum
			RaycastInfo newViewRaycast = ViewRaycast (angle); //raycast with our new angle

			bool edgeDistanceThresholdExceeded = Mathf.Abs(minimumViewRaycast.length - newViewRaycast.length) > edgeDistanceThreshold;
			//bool that check whether the distance between our old and new raycasts exceeds our set threshold 
			if (newViewRaycast.hit == minimumViewRaycast.hit && !edgeDistanceThresholdExceeded){ // if the hit result of our raycast is equal to that 
				//of our minimum hit and the distance threshold has not been exceeded
				minimumAngle = angle; //set the minimum angle to our new angle
				minimumPoint = newViewRaycast.point; //set the minimum point to our new raycast point
			} else { 
				maximumAngle = angle; //set the maximum angle to our new angle
				maximumPoint = newViewRaycast.point; //set the maximum point to our new raycast point
			}
		}

		return new EdgeInfo(minimumPoint, maximumPoint); //returns edge info containing our new minimum and maximum points
	}

	RaycastInfo ViewRaycast(float globalAngle){ //returns a raycast information struct that accepts a float parameter
		Vector3 direction = DirectionFromAngle (globalAngle, true);  //sets the direction using the directionFromAngle method
		RaycastHit hit; //gets info back from raycast

		if (Physics.Raycast (transform.position, direction, out hit, viewRadius, obstacleMask)){ //if the raycast hits something
			return new RaycastInfo(true, hit.point, hit.distance, globalAngle);//return the raycast info saying that we hit something,
			//the point we hit, the distance to the hit, and the angle of the direction
		} else { //if the raycast hit nothing
			return new RaycastInfo(false, transform.position + direction * viewRadius, viewRadius, globalAngle);//return the info saying no hit,
			//the point as our position plus the direction multiplied by our fov radius, the length as our fov radius, and the angle of the direction
		}
	}

	public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal){ //this takes our angle and converts it to a direction
		if (!angleIsGlobal){  //this bool check ensures the fov rotates appropriately with the object in scene view by ensuring the angle is global
			angleInDegrees += transform.eulerAngles.y;
		}
		/*Boy is this part confusing. Because degrees work a little differently in unity, we need to do some conversion. 
		In trigonometry a unit circle starts with 0 degrees at 3:00 and increases going counterclockwise (90 at 12:00, 180 at 9:00, and 270 at 6:00).
		But in Unity for whatever reason, 0 degrees starts at 12:00 and increases going 'clockwise' (90 at 3:00, 180 at 6:00, and 270 at 9:00).
		To convert we'll need to take angle x and subtract 90. This effectively means that cosine and sin need to be swapped whenever we use them.*/
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0 , Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)); 
		//so in the x axis we're getting the sin of our angle and converting it to radians , and in the z we're doing the same thing but with cos
	}

	public struct RaycastInfo{ //holds information for each raycast
		public bool hit; //did the raycast hit something
		public Vector3 point; //endpoint of raycast
		public float length; //length of the ray
		public float angle; //angle the ray was cast at

		public RaycastInfo(bool _hit, Vector3 _point, float _length, float _angle) { //constructor for raycastInfo
			hit = _hit;
			point = _point;
			length = _length;
			angle = _angle;
		}
	}

	//for resolving edge issues
	public struct EdgeInfo { //information for raycasts around obstacle edges
		public Vector3 pointA; //closest points to the edge, on and off the obstacle
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB){ //constructor for edgeinfo
			pointA = _pointA;
			pointB = _pointB;
		}
	}
}
