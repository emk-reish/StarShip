using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour {

	public Transform enemyShipPrefab1;
	public Transform enemyShipPrefab2;
	public Transform enemyShipPrefab3;

	int numAlive, numKilled, numEnemiesTotal, numLeftAlive;

	int framesPassed, framesBetween;

	bool spawn = true;

	Camera cam;

	// Use this for initialization
	void Start () {
		numEnemiesTotal = 0;
		numAlive = 0;

		framesPassed = 0;
		framesBetween = 1500;

		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(spawn && (framesPassed > framesBetween || numAlive == 0)) {
			int type = Random.Range(0,6);
			if(type < 3) { // smallest enemy
				CreateEnemyOfType(enemyShipPrefab1, 1);
			} else if(type < 5) { // middle enemy
				CreateEnemyOfType(enemyShipPrefab2, 2); 
			} else { // largest enemy 
				CreateEnemyOfType(enemyShipPrefab3, 3);
			}
			framesPassed = 0;
			if(framesBetween > 400) {
				framesBetween -= 25;
			}
		}
		framesPassed++;
	}

	// use prefab to make enemy who does dmg damage to player upon collision, damage has 1:1 correlation with type
	void CreateEnemyOfType(Transform enemyShip, int dmg) {
		Transform enemyT = Instantiate(enemyShip, GetEnemyPostion(), new Quaternion()); 
		
		// make position relative to player view temporarily
		enemyT.SetParent(transform, false); 
		enemyT.parent = null;

		enemyT.eulerAngles = new Vector3(0,90,-90);

		GameObject enemy = enemyT.gameObject;
		enemy.AddComponent<EnemyBehaviour>();
		enemy.GetComponent<EnemyBehaviour>().damage = dmg;

		enemy.name = "Enemy_" + dmg + "_" + numEnemiesTotal;
		
		numEnemiesTotal++;
		numAlive++;
	}

	// returns random position to the top of the screen, just out of view
	Vector3 GetEnemyPostion() {
		Vector2 screenPos = new Vector2();
		float h = cam.orthographicSize;
		float w = h * cam.pixelWidth / cam.pixelHeight;
		screenPos.x = Random.Range(- w , w);
		screenPos.y = Random.Range( 3 * h / 2, 2 * h);
		return screenPos;
	}

	// modifies counters
	// accessed when enemies are killed or level reset
	public void UpdateEnemyCounts(bool lifeReset = false, bool hitByLaser = false) { 
		if(lifeReset) {
			framesPassed = 0;
			framesBetween = 1500; // reset
			numLeftAlive += numAlive;
			numAlive = 0;
		} else {
			if(numAlive > 0) {
				numAlive--;
			}
			if(hitByLaser) {
				numKilled++;
			}
		}
	}

	public void StopSpawning() { 
		spawn = false;
	}

	public string MakeEndGameMessages() { 
		string s = "";

		s += "Total Enemies Killed: " + numKilled + "\n\r";
		s += "Total Enemies Left Alive: " + numLeftAlive + "\n\r";

		return s;
	}
	
}
