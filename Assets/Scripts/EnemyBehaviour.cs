﻿using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public int damage = 1;

	Transform shipT;

	Rigidbody rb;

	float speed;

	// Use this for initialization
	void Start () {
		speed = .08f + damage / 60;

		shipT = GameObject.Find("Ship").transform;

		InvokeRepeating("ChangeDirection", 0.0f, .1f);
	}

	void Update() { 
		transform.position = Vector3.MoveTowards(transform.position, shipT.position, speed);
	}

	void ChangeDirection() {
		transform.LookAt(shipT);
		Vector3 rot = transform.eulerAngles;

		if(Math.Round(rot.y) == 90)	{ 
			transform.eulerAngles = new Vector3(180 - rot.x, -90, 90);
		} else {
			transform.eulerAngles = new Vector3(rot.x, -90, 90);
		}
	}

	void OnCollisionEnter(Collision c) {
		GameObject other = c.gameObject;
		Debug.Log(other.name);
		if(other.tag == "Laser") {
			// delete both objects
			DestroyEnemy(true);
			Destroy(other.transform.parent.gameObject);
		} else if(other.tag == "Player" || other.name == "Ship") {
			other.GetComponent<ShipBehaviour>().EnemyHitShip(damage);
			Debug.Log(c.contacts[0].point);
			PlayExplosion(c.contacts[0].point);
			DestroyEnemy(false);
		}
	}

	void PlayExplosion(Vector3 pt) { 
		Debug.Log(pt);
		Instantiate(Resources.Load("GameAssets/Explosion"), pt, transform.rotation);
		// explosion.
	}

	void DestroyEnemy(bool hitByLaser = false) {
		GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerBehaviour>().UpdateEnemyCounts(false, hitByLaser);
		if(hitByLaser) { // add explosion to scene
			GameObject explosion = (GameObject) Instantiate(Resources.Load("GameAssets/Explosion"), transform.position, transform.rotation);
			Destroy(explosion, 1.75f); // remove explosion after time
		}
		Destroy(gameObject);
	}

}
