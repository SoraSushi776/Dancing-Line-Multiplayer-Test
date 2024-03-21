/*
this script is attached to the camera to make sure it focuses on the center of the scene.
*/

using UnityEngine;
using System.Collections;

public class AlwaysFace : MonoBehaviour {

	public GameObject Target;
	public float Speed;

	public bool JustOnStart = false;

	// turn towards target
	void Start() 
	{
		Vector3 dir = Target.transform.position - transform.position;
		Quaternion Rotation = Quaternion.LookRotation(dir);
		
		gameObject.transform.rotation = Rotation;
	}
	
	// turn towards target
	void FixedUpdate () 
	{
		if (JustOnStart == false && Target != null)
		{
			Vector3 dir = Target.transform.position - transform.position;
			Quaternion Rotation = Quaternion.LookRotation(dir);

			gameObject.transform.rotation = Quaternion.Lerp (gameObject.transform.rotation,Rotation,Speed * Time.deltaTime);
		}
	}



}
