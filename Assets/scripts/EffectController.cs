using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour {
	private ParticleSystem pa;
	public KeyCode key;

	// Use this for initialization
	void Start () {
		pa = this.GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		tap_effect ();
	}

	void tap_effect(){
		if (Input.GetKeyDown(this.key)) {
//			Debug.Log ("タップされた");
			pa.Play ();

		}

	}








}
