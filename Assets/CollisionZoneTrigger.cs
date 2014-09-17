using UnityEngine;
using System.Collections;

public class CollisionZoneTrigger : MonoBehaviour {

	private bool isActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Collision with " + other.name);
		isActive = true;
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("Exit Collision with " + other.name);
		isActive = false;
	}

}
