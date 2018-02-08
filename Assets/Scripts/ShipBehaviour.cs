using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipBehaviour : MonoBehaviour {

	int lives;
	int health = 4;

	int numLaser = 0;

	new Light light;

	List<Color> colors;
	
	void Start() {
		lives = PlayerPrefs.GetInt("lives", 3);
		
		// give the ship a light who's color changes with health 
		// using Light not Halo since Halo cannot be changed in script
		GameObject shipLight = (GameObject) Instantiate(Resources.Load("GameAssets/Light"), new Vector3(0, -5, -0.3f), new Quaternion());
		shipLight.transform.SetParent(gameObject.transform, false);
		light = shipLight.GetComponent<Light>();


		// designate colors to use in the glow
		Color red = Color.red, orange = new Color(1,.6f,0,.75f), yellow = new Color(1,.92f,0.016f,.75f), green = new Color(0,1,0,.75f);
		colors = new List<Color>{ red, orange, yellow, green };

		UpdateLight();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			FireWeapon();
		}
	}

	// Creates a laser gameObject which damages any enemies it hits and is destroyed after some time
	void FireWeapon() {
		GameObject laser = new GameObject("laser" + numLaser);

		// set Player as parent to use it's positioning
		laser.transform.SetParent(GameObject.Find("Player").transform, false);
		laser.transform.Translate(0,5,0);

		laser.AddComponent<LaserBehaviour>();
		laser.AddComponent<SphereCollider>();
		laser.GetComponent<SphereCollider>().isTrigger = true;
		laser.AddComponent<Rigidbody>();
		laser.GetComponent<Rigidbody>().useGravity = false;

		laser.transform.parent = null;

		numLaser++;
	}

	// For sending player how to message and end game stats
	public int GetNumLaser() { 
		return numLaser;
	}

	public void EnemyHitShip(int damage) {
		if(health <= damage) {
			lives--;
			GameObject.Find("Messages").GetComponent<UIMessageBehaviour>().ShowLoseLifeMessage(lives);
			GameObject.Find("Lives").GetComponent<UILivesBehaviour>().UpdateLivesShown(lives);
			gameObject.GetComponentInParent<PlayerBehaviour>().Reset();
			if(lives < 1) { // player is out of health
				PlayerDies();
			} else { // player just looses a life
				health = 4; 
			}
		} else { // does not use remaininly health of this life
			health -= damage;
		}

		UpdateLight();
	}

	void PlayerDies() {
		// stop creating enemies
		GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().StopSpawning();

		// Game Over message appears
		GameObject.Find("GameOver").GetComponent<MeshRenderer>().enabled = true;
		string stats = GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().MakeEndGameMessages();
		stats += "Total Shots Fired: " + numLaser + "\n\r";

		GameObject.Find("Stats").GetComponent<TextMesh>().text = stats;
		
		int secondsUntilNewGame = 10;
		GameObject.Find("Messages").GetComponent<UIMessageBehaviour>().ShowGameOverMessage(secondsUntilNewGame);
		Invoke("GameOver", secondsUntilNewGame);
	}

	// changes ship glow color to reflect health
	void UpdateLight() {
		light.color = colors[health - 1];
	}

	// goes back to main menu
	void GameOver() {
		SceneManager.LoadScene("MainMenu");
	}

	void OnTriggerEnter(Collider c) {
		GameObject other = c.gameObject;
		Debug.Log("hit " + other.name);
	}
}
