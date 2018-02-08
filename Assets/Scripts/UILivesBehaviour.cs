using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILivesBehaviour : MonoBehaviour {

	char c = '\u2605';

	// Use this for initialization
	void Start () {
		int lives = PlayerPrefs.GetInt("lives", 3);
		gameObject.GetComponent<TextMesh>().text = new string(c, lives);
	}
	
	// Update is called once per frame
	public void UpdateLivesShown(int lives) {
		gameObject.GetComponent<TextMesh>().text = new string(c, lives);
	}
}
