using UnityEngine;
using System.Collections;
using UnityEditor; //enables use of editor 

[CustomEditor (typeof (FieldOfView))] //indicates this is a custom editor of type FieldOfView
public class FieldOfViewEditor : Editor { //extends editor
	
	void OnSceneGUI(){ //this method allows the editor to handle events in the scene view
		FieldOfView fov = (FieldOfView)target; //gets reference to FieldOfView script, 
		//then sets it equal to the object for which this will be a custom editor of
		Handles.color = Color.white; //sets color of editor handles in scene view
		Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle/2, false); //find the angle at one edge of the fov
		Vector3 viewAngleB = fov.DirectionFromAngle(fov.viewAngle/2, false);//find the second edge angle
		//draws line between object position and the position plus the angle multiplied by the radius
		Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius); 
		Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);
		Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);/*for the radius, draws an arc using:
		the center of the circle to be drawn (the objects position), 
		the direction around which the arc will rotate (in this case the upward vector),
		where the angle will start along the arc (in this case the forward vector),
		how many degrees the arc will rotate (we want the possibility of full rotation, so 360 degrees),
		and finally the actual radius of the circle (we get this from our FieldOfView script which we just referenced)*/


		Handles.color = Color.red;//changes handle color to red
		foreach(Transform visibleTarget in fov.visibleTargets){ //for every visible target
			Handles.DrawLine (fov.transform.position, visibleTarget.position); //draw a line from our position to the targets position
		}
	}
}
