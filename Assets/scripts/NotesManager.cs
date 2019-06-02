using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongMetadata {
	public int duration;
	public double milllisecondsPerBeat;

	private SongMetadata(int duration, double milllisecondsPerBeat) {
		this.duration = duration;
		this.milllisecondsPerBeat = milllisecondsPerBeat;
	}

	public static SongMetadata createFromFile(string filePath) {
		// TODO: load this from file
		int minutes = 4;
		int seconds = 51;
		int bpm = 170;

		int duration = SongMetadata.durationToMilliseconds (minutes, seconds);
		double milllisecondsPerBeat = bpmToMilliseconds (bpm);
		return new SongMetadata (duration, milllisecondsPerBeat);
	}

	private static int durationToMilliseconds(int minutes, int seconds) {
		var sec = (minutes * 60) + seconds;
		return sec * 1000;
	}

	private static double bpmToMilliseconds(int bpm) {
		double beatsPerSecond = bpm / 60;
		return beatsPerSecond * 1000;
	}
}





public class Settings{
	public static float difficulty = 0.7f;

	private Settings(){}
}

public class NotesManager : MonoBehaviour {
	public SongMetadata currentSong;
	private static readonly Vector3[] laneStart = new Vector3[] {
		new Vector3 (-0.329f, -218.342f, -6.68f),
		new Vector3 (-0.186f, -218.342f, -6.68f),
		new Vector3 (-0.061f, -218.342f, -6.68f),
		new Vector3 (0.08f, -218.342f, -6.68f),
		new Vector3 (0.214f, -218.342f, -6.68f),
		new Vector3 (0.356f, -218.342f, -6.68f)
	};
//	private static readonly Vector3[] laneStart = new Vector3[] {
//		new Vector3 (-0.329f, -218.342f, -6.68f),
//		new Vector3 (-0.186f, -218.342f, -6.68f),
//		new Vector3 (-0.061f, -218.342f, -6.68f),
//		new Vector3 (0.08f, -218.342f, -6.68f),
//		new Vector3 (0.214f, -218.342f, -6.68f),
//		new Vector3 (0.356f, -218.342f, -6.68f)
//	};
	List<notes_system> notes;
	GameObject obj;
	AudioManager audio_manager;

    void Start(){
		currentSong = SongMetadata.createFromFile("foo");
		notes = createRandomTrack (currentSong.duration, currentSong.milllisecondsPerBeat);
//		Debug.Log (notes.Count);
		obj = (GameObject)Resources.Load ("paddle");
//		obj.gameObject.SetActive (false);
		GameObject audio_obj = GameObject.Find("game_audio");
		audio_manager = audio_obj.GetComponent<AudioManager> ();

    }

	void Update(){
//		Debug.Log ("elapsed: " + audio_manager.elapsed_time);
//		Debug.Log ("time: " + notes[notes.Count - 1].Time);
		while (notes.Count > 0 && audio_manager.elapsed_time >= notes[notes.Count - 1].Time){
			notes_system note = notes[notes.Count - 1];
			notes.RemoveAt(notes.Count - 1);
			create_tap_circle (note);
	
//			Debug.Log (tap_circle_copy);
//			this.start_circle(note);
		}

	}



	private void create_tap_circle(notes_system note){
		TapPoint TapPointScript;

		GameObject tap1 = Instantiate (obj, NotesManager.laneStart[note.Lane], Quaternion.identity);
		tap1.gameObject.SetActive (true);
		TapPointScript = tap1.GetComponent<TapPoint> ();
		TapPointScript.destination = GameObject.Find ("tapline" + (note.Lane + 1) + "-des").transform.position;
		TapPointScript.transform.LookAt (Camera.main.transform);
	}	

	public struct notes_system{
		public int Lane;
		public string Type;
		public int Time;

		public notes_system(int lane, string type, int time) {
			Lane = lane;
			Type = type;
			Time = time;
		}
	}

	private static List<notes_system> createRandomTrack(int song_duration, double song_beat) {
		List<notes_system> notes = new List<notes_system>();
		double subBeat = song_beat / 5;
		for(double time = song_beat; time < song_duration; time += subBeat) {
			if(Random.value < Settings.difficulty) { 
				var note = new notes_system(
					(int) Mathf.Round(Random.value * (NotesManager.laneStart.Length - 1)),
					"tap",
					(int) time
				);
				notes.Add(note);
			}
		}
		notes.Reverse();
		return notes;
	}
}