using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

	int frame = 0, destroyAfter = 2000;
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(0, 0.3f, 0);
		if(frame > destroyAfter) {
			Destroy(gameObject);
		}
		frame++;
	}

	void OnTriggerEnter(Collider c) {
		GameObject enemy = c.gameObject;
		if(enemy.tag == "Enemy") {
			// delete both objects
			Destroy(gameObject);
			Destroy(enemy);
		}
	}
}
