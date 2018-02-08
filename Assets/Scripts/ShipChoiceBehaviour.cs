using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipChoiceBehaviour : MonoBehaviour {

	GameObject stats; 
	MeshRenderer statsRenderer;

	string shipName;

	char c = '\u2605';
	int health, speed, agility;

	// Use this for initialization
	void Start() {
		stats = GameObject.Find("ShipStats");
		statsRenderer = stats.GetComponent<MeshRenderer>();
	}

	void OnMouseEnter() {
		switch(name) {
			case "ship1":
				health = 2;
				speed = 4;
				agility = 4;
				shipName = GetComponentInParent<ShipChooseBehaviour>().shipPrefab1.name;
			break;
			case "ship2":
				health = 2;
				speed = 3;
				agility = 5;
				shipName = GetComponentInParent<ShipChooseBehaviour>().shipPrefab2.name;
			break;
			case "ship3":
				health = 3;
				speed = 4;
				agility = 2;
				shipName = GetComponentInParent<ShipChooseBehaviour>().shipPrefab3.name;
			break;
		}

		stats.GetComponent<TextMesh>().text = "Lives: " +new string(c, health) + "\n\rSpeed: " + new string(c, speed) + "\n\rAgility: " + new string(c, agility);
		statsRenderer.enabled = true;
	}

	void OnMouseExit() {
		statsRenderer.enabled = false;
	}

	void OnMouseDown() {
		if(shipName != "") { // ship selected
			// save ship information
			PlayerPrefs.SetString("shipName",shipName);
			PlayerPrefs.SetInt("lives", health);
			PlayerPrefs.SetInt("speed",speed);
			PlayerPrefs.SetInt("agility", agility);

			// load main game scene
			SceneManager.LoadScene("MainScene");
		}
	}
}
