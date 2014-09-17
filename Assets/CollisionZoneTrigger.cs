using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AffectedQuad
{
	public GameObject quad;
	public float idleTime;
	public float animTime;
}

public class CollisionZoneTrigger : MonoBehaviour {

	public AnimationCurve fadeCurve;
	public float animationLength;
	private bool isActive = false;

	private List<AffectedQuad> affectedQuads;
	private bool calcedAffectedQuads = false;
	private GameObject photoParent;
	private SphereCollider collider;

	void resetAnimation(AffectedQuad aq)
	{
		aq.idleTime = 0.0f;
		aq.animTime = 0.0f;
	}

	void stepAnimation(AffectedQuad aq)
	{
		if (aq.idleTime > 0) {
			aq.idleTime -= Time.deltaTime;
			return;
		}

		aq.animTime += Time.deltaTime;
		if (aq.animTime >= animationLength)
		{
			resetAnimation(aq);
		}
	}
	void calcAffectedQuads()
	{
		affectedQuads = new List<AffectedQuad> ();
		/* Add every quad that is inside the sphere to its list of affected quads */
		foreach (Transform narrativeScenario in photoParent.transform) {
			foreach (Transform episode in narrativeScenario) {
				foreach (Transform photo in episode) {
					if (Vector3.Distance(photo.position, collider.transform.position) < collider.radius)
					{
						//Debug.Log ("adding affected quad");
						AffectedQuad aq = new AffectedQuad();
						aq.quad = photo.gameObject;
						resetAnimation(aq);
						affectedQuads.Add(aq);
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		collider = GetComponent<SphereCollider> ();
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

		foreach (AffectedQuad aq in affectedQuads)
		{
			if (aq.idleTime <= 0)
				aq.quad.renderer.material.color = new Color (0.0f, fadeCurve.Evaluate(aq.animTime), 0.0f, 1.0f);
			stepAnimation(aq);
		}


	}

}
