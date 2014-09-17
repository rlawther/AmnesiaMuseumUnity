using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionZoneTrigger : MonoBehaviour {

	private bool isActive = false;
	public List<GameObject> affectedQuads;
	private bool calcedAffectedQuads = false;
	private GameObject photoParent;
	private SphereCollider col;


	void calcAffectedQuads()
	{
		/* Add every quad that is inside the sphere to its list of affected quads */
		foreach (Transform narrativeScenario in photoParent.transform) {
			foreach (Transform episode in narrativeScenario) {
				foreach (Transform photo in episode) {
					if (Vector3.Distance(photo.position, col.transform.position) < col.radius)
					{
						//Debug.Log ("adding affected quad");
						affectedQuads.Add(photo.gameObject);
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		col = GetComponent<SphereCollider> ();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Collision with " + other.name);
		if (calcedAffectedQuads)
			isActive = true;
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("Exit Collision with " + other.name);
		isActive = false;
	}

	void Update()
	{
		if (!calcedAffectedQuads && (Time.time > 1.0))
		{
			photoParent = GameObject.Find ("Photos");
			calcAffectedQuads ();
			calcedAffectedQuads = true;
		}

		if (!isActive)
			return;

		foreach (GameObject photo in affectedQuads)
		{
			photo.renderer.material.color = new Color (0.0f, 1.0f, 0.0f, 1.0f);
		}


	}

}
