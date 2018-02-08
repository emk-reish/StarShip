using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMessageBehaviour : MonoBehaviour {

	TextMesh tm;

	// Use this for initialization
	void Start () {
		tm = gameObject.GetComponent<TextMesh>();
		tm.text = "";

		Invoke("ShowDamageMessage", 1);
	}

	void ShowDamageMessage() {
		tm.text = "You take damage when hit by another ship.";
		Invoke("ShowMovementMessage", 4);
	}
	
	void ShowMovementMessage() {
		if(! transform.root.hasChanged) { // if the player has not already moved
			tm.text = "Use the arrow keys to move and turn.";
			Invoke("ShowShootingMessage", 4);
		} else { 
			ShowShootingMessage();
		}
	}

	void ShowShootingMessage() {
		if(GameObject.Find("Ship").GetComponent<ShipBehaviour>().GetNumLaser() < 1) {
			tm.text = "Use the space bar to shoot.";
		} 
		Invoke("RemoveMessages", 4);
	}

	void RemoveMessages() {
		tm.text = "";
	}

	public void ShowLoseLifeMessage(int lives) {
		if(lives == 0) { 
			RemoveMessages();
		} else if(lives == 1) { 
			tm.text = "Careful! Only one life left...";
		} else {
			tm.text = "Oh no! You lost a life!";
		}
	}

	public void ShowGameOverMessage(int t = 10) { 
		tm.text = "New Game will begin after " + t + " seconds.";
	}
}
