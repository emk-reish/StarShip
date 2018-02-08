using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipChooseBehaviour : MonoBehaviour {

	public Transform shipPrefab1;
	public Transform shipPrefab2;
	public Transform shipPrefab3;

	private Quaternion shipRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
		shipRotation.eulerAngles = new Vector3(-90, 0, 0);

		Transform ship1 = Instantiate(shipPrefab1, new Vector3(-5, 0, 0), shipRotation);
		Transform ship2 = Instantiate(shipPrefab2, Vector3.zero, shipRotation);
		Transform ship3 = Instantiate(shipPrefab3, new Vector3(5, 0, 0), shipRotation);

		CreateShipPlane(ship1);
		CreateShipPlane(ship2);
		CreateShipPlane(ship3);
	}

	void CreateShipPlane(Transform ship) {
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.name = ship.name.Replace("(Clone)", "");
		plane.transform.parent = gameObject.transform;
		plane.transform.position = new Vector3(ship.position.x, 0, 0);
		plane.transform.eulerAngles = new Vector3(0,90,-90);
		plane.transform.localScale = new Vector3(0.5f, 1, 0.5f);
		plane.GetComponent<MeshRenderer>().enabled = false;
		plane.AddComponent<ShipChoiceBehaviour>();
	}
}
