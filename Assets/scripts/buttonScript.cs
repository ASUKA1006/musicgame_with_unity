using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class buttonScript : MonoBehaviour {

	public void OnClick(){
		SceneManager.LoadScene("musicgame");
	}
}
