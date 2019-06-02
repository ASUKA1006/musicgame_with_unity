using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPoint : MonoBehaviour {
	public float move_time;
	private Vector3 start_vector;
	public Vector3? destination = null;
	private float passed_time = 0f;



	// Use this for initialization
	void Start () {

		start_vector = this.gameObject.transform.position;

			
	}

	// Update is called once per frame
	void Update () {
		
		start_circle();

		// after this game object reached the destination
		delete_circle ();

		
	}

	void start_circle(){
		this.transform.LookAt (Camera.main.transform);

		// actual passed_time / move_time = passing rate
		var rate = passed_time / move_time;
		if (destination.HasValue) {
			transform.position = Vector3.Lerp (start_vector, destination.Value, rate);
			Debug.Log ("value: " + destination.Value);
			Debug.Log ("loc: " + this.gameObject.transform.position);
		}
		passed_time += Time.deltaTime;
	}

	void delete_circle(){
	
		if (destination.Value == this.gameObject.transform.position) {
			this.gameObject.SetActive (false);
		}	
	}	
		
}
