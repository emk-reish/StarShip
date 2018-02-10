using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public Transform ship1;
	public Transform ship2;
	public Transform ship3;

	Transform shipT;
	
	float speed, agility;

	// Use this for initialization
	void Start () {
		// load chosen ship, defaults to ship1
		string shipName = PlayerPrefs.GetString("shipName","ship1");
		Quaternion rot = new Quaternion();
		rot.eulerAngles = new Vector3(-90,0,0);
		if(shipName == "ship1") {
			shipT = Instantiate(ship1, Vector3.zero, rot, gameObject.transform);
		} else if(shipName == "ship2") {
			shipT = Instantiate(ship2, Vector3.zero, rot, gameObject.transform);
		} else {
			shipT = Instantiate(ship3, Vector3.zero, rot, gameObject.transform);
		}

		GameObject ship = shipT.gameObject;

		ship.name = "Ship";
		ship.tag = "Player";

		ship.AddComponent<ShipBehaviour>();

		speed = PlayerPrefs.GetInt("speed", 5) / 20f;
		agility = PlayerPrefs.GetInt("agility", 4) / 20f;	

		transform.hasChanged = false;
	}
	
	// Update is called once per frame
	void Update () {
		// forward or backward (at one half speed)
        float verticalSpeed = Input.GetAxis("Vertical");
		if(verticalSpeed < 0) { // backwards -> slower
			transform.Translate(0, verticalSpeed * speed / 2, 0);
		} else { // forward
			transform.Translate(0, verticalSpeed * speed, 0);
		}

		// direction (changes angel)
		float horizontalSpeed = Input.GetAxis("Horizontal");
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - horizontalSpeed * agility);
	}

	// resets player position and destroys currently alive enemies
	public void Reset() {
		gameObject.transform.position = Vector3.zero;
		foreach(GameObject e in GameObject.FindGameObjectsWithTag("Enemy")) {
			Destroy(e);
		}
		GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().UpdateEnemyCounts(true);
	}
}
