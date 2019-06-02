using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource audio_source;
    public AudioClip audio1;
	private float count;
	public float bgm_time;
	public int elapsed_time;
	// private GameObject audio;


    void Start(){
		audio_source = gameObject.GetComponent<AudioSource> ();
		audio_source.clip = audio1;

        
    }

    void Update(){
		count++;
		if (count == 30) {
			this.audio_source.Play ();
			this.bgm_time = Time.time;
		}

		float elapsed = Time.time - this.bgm_time;
		this.elapsed_time = elapsedToMilliseconds(elapsed);
//		Debug.Log (elapsed);

	}


	private static int elapsedToMilliseconds(float elapsed) {
		int secs = (int) elapsed;
		int extra = (int) (1000 * (elapsed % 1.0f));
		int ms = (secs * 1000) + extra;
		return ms;
	}

}