using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

	SpriteRenderer spriteR;
	Sprite[] sprites;

	int frame = 0;
	int frameNum = 0,  spriteNum = 0, disolvesAfter = 2000;


	// Use this for initialization
	void Start () {
		gameObject.transform.localScale = new Vector3(3,3,1);

		sprites = Resources.LoadAll<Sprite>("ArtAssets/Particle");
		frameNum = spriteNum = sprites.Length;

		gameObject.tag = "Laser";

		gameObject.AddComponent<SpriteRenderer>();
		spriteR = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(frame < frameNum) {// charge builds up
			spriteR.sprite = sprites[--spriteNum];
		} else if(frame < disolvesAfter + frameNum) { // and then shoots
		 	gameObject.transform.Translate(0, 0.3f, 0);
		} else if(frame < disolvesAfter + frameNum * 2 - 1) {
			spriteR.sprite = sprites[spriteNum++];
		} else {
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
