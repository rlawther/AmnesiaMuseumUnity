using UnityEngine;
using System.Collections;

public class Events
 : MonoBehaviour {
 
 	public GameObject terrain;
	public GameObject firstPersonController;
 	private bool mAmnesiaMode = true;
 	private GameObject mActiveNarrativeScenario = null;
 	
	private Texture [] layerTextures;
	
	private bool gotParent = false;
	private GameObject photoParent;
	
	private GameObject [] paths;
	
	public float delayUntilNextEvent;
	private float eventTimeout;

	private BillboardAndFade browserFadeScript;
	private BillboardAndFadeAmnesiaMode amnesiaFadeScript;
	private GameObject collisionZoneParent;

	private Vector3 cameraOriginalPos;
	private Quaternion cameraOriginalRotation;

	private float rotateAmount;
	public float rotationChange;
	// Use this for initialization
	void Start () {
	
		layerTextures = new Texture[6];
		layerTextures[0] = terrain.renderer.material.GetTexture("_Layer1");
		layerTextures[1] = terrain.renderer.material.GetTexture("_Layer2");
		layerTextures[2] = terrain.renderer.material.GetTexture("_Layer3");
		layerTextures[3] = terrain.renderer.material.GetTexture("_Layer4");
		layerTextures[4] = terrain.renderer.material.GetTexture("_Layer5");
		layerTextures[5] = terrain.renderer.material.GetTexture("_Layer6");
		
		paths = new GameObject[7];

		browserFadeScript = GameObject.Find ("Scripts").GetComponent<BillboardAndFade>();
		amnesiaFadeScript = GameObject.Find ("Scripts").GetComponent<BillboardAndFadeAmnesiaMode>();
		collisionZoneParent = GameObject.Find ("CollisionZones");

		cameraOriginalPos = firstPersonController.transform.position;
		cameraOriginalRotation = firstPersonController.transform.rotation;

		rotateAmount = 0.0f;
	}
	
	void findPaths(GameObject parent)
	{
		int i = 0;
		foreach (Transform scenario in parent.transform)
		{
			foreach (Transform episode in scenario.transform)
			{
				paths[i] = episode.gameObject;
				i++;
			}
		}
		
	}

	public void togglePathActive(int index)
	{
		Debug.Log ("toggling path " + index);
		if (paths[index].activeSelf)
		{
			setPathActive(index, false);
		}
		else
		{
			setPathActive(index, true);
		}
	}

	public void setPathActive(int index, bool active)
	{
		Debug.Log ("setting path " + index + ", active=" + active);
		if (!active)
		{
			paths[index].SetActive(false);
			terrain.renderer.material.SetTexture("_Layer" + (index + 1), null);
		}
		else
		{
			paths[index].SetActive(true);
			terrain.renderer.material.SetTexture("_Layer" + (index + 1), layerTextures[index]);
		}
	}

	public void resetCamera()
	{
		firstPersonController.transform.position = cameraOriginalPos;
		firstPersonController.transform.rotation = cameraOriginalRotation;
	}

	public void useAmnesiaMode()
	{
		Debug.Log ("Into Amnesia Mode");
		mAmnesiaMode = true;
		amnesiaFadeScript.enabled = true;
		browserFadeScript.enabled = false;
		collisionZoneParent.SetActive (true);
	}
	
	public void useBrowserMode()
	{
		Debug.Log ("Into Browser Mode");
		mAmnesiaMode = false;
		browserFadeScript.enabled = true;
		amnesiaFadeScript.enabled = false;
		collisionZoneParent.SetActive (false);
	}

	public void rotateCamera(float rot)
	{
		firstPersonController.transform.Rotate(0, rot, 0);
	}

	public void setCameraRotateAmount(float rot)
	{
		Debug.Log ("rot amt " + rot);
		if (Mathf.Abs (rotateAmount - rot) < rotationChange)
			rotateAmount = rot;
		else if (rot < rotateAmount)
			rotateAmount -= rotationChange;
		else
			rotateAmount += rotationChange;
		
	}
	
	/* sets the child at the given index to active, all other children
	 * to inactive.
	 * Returns the now active child
	 */
	GameObject setOnlyActiveChild(GameObject parent, int childIndex)
	{
		GameObject activeChild = null;
		int i = 1;
		foreach(Transform child in parent.transform)
		{
			Debug.Log (child.name);
			if (i == childIndex)
			{
				child.gameObject.SetActive(true);
				activeChild = child.gameObject;
			} 
			else
			{
				child.gameObject.SetActive(false);
			}
			
			i++;
		}
		return activeChild;
	}
	
	/*
	 * Sets all children of the parent to active
	 */
	void setAllActiveChildren(GameObject parent)
	{
		foreach(Transform child in parent.transform)
			child.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
		RenderSettingsSetter rss;

		rotateCamera(rotateAmount);

		if (!gotParent && (Time.time > 1.0)) {
				GameObject photoParent = GameObject.Find ("Photos");
				findPaths (photoParent);
	
				gotParent = true;
		}

		if (Input.GetKey ("o")) {
			if (!mAmnesiaMode) {
				useAmnesiaMode();
			}
		} else if (Input.GetKey ("p")) {
			if (mAmnesiaMode) {
				useBrowserMode();
			}
		}

		if (Input.GetKey ("z"))
			setCameraRotateAmount(-1.0f);
		else if (Input.GetKey ("c"))
			setCameraRotateAmount(1.0f);
		else if (Input.GetKey ("x"))
			setCameraRotateAmount(0.0f);
		
		
		if (Input.GetKeyDown ("1")) {
				togglePathActive (0);
		} else if (Input.GetKeyDown ("2")) {
				togglePathActive (1);
		} else if (Input.GetKeyDown ("3")) {
				togglePathActive (2);
		} else if (Input.GetKeyDown ("4")) {
				togglePathActive (3);
		} else if (Input.GetKeyDown ("5")) {
				togglePathActive (4);
		} else if (Input.GetKeyDown ("6")) {
				togglePathActive (5);
		} else if (Input.GetKeyDown ("m")) {
				HUDMap map = GameObject.Find ("BaseScripts").GetComponent<HUDMap> ();
				map.enabled = !map.enabled;
		} else if (Input.GetKeyDown ("r")) {
				resetCamera ();
		}
	}
}
