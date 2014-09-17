using UnityEngine;
using System.Collections;

public class Events
 : MonoBehaviour {
 
 	public GameObject terrain;
 	private bool mAmnesiaMode = true;
 	private GameObject mActiveNarrativeScenario = null;
 	
	private Texture [] layerTextures;
	
	private bool gotParent = false;
	private GameObject photoParent;
	
	private GameObject [] paths;
	
	public float delayUntilNextEvent;
	private float eventTimeout;

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
	
	void togglePathActive(int index)
	{
		Debug.Log ("toggling path " + index);
		if (paths[index].activeSelf)
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
		
		if (!gotParent && (Time.time > 1.0))
		{
			GameObject photoParent = GameObject.Find ("Photos");
			findPaths (photoParent);
			
			gotParent = true;
		}
	
		if (Input.GetKey("o"))
		{
			if (!mAmnesiaMode)
			{
				Debug.Log ("Into Amnesia Mode");
				mAmnesiaMode = true;
				LoadLevels.artisticSceneParent.SetActive(true);
				LoadLevels.browserSceneParent.SetActive(false);
				rss = LoadLevels.artisticSceneParent.transform.Find("Scripts/BaseScripts").GetComponent<RenderSettingsSetter>();
				rss.set();
				GameObject photoParent = GameObject.Find ("Photos");	
				findPaths (photoParent);
				
			}
		}
		else if (Input.GetKey("p"))
		{
			if (mAmnesiaMode)
			{
				Debug.Log ("Into Browser Mode");
				mAmnesiaMode = false;
				
				LoadLevels.artisticSceneParent.SetActive(false);
				LoadLevels.browserSceneParent.SetActive(true);
				rss = LoadLevels.browserSceneParent.transform.Find("Scripts/BaseScripts").GetComponent<RenderSettingsSetter>();
				rss.set();
				GameObject photoParent = GameObject.Find ("Photos");
				findPaths (photoParent);
				
			}
		}

		if (eventTimeout <= 0)
		{
			if (Input.GetKey("1"))
			{
				togglePathActive(0);
				eventTimeout = delayUntilNextEvent;
			}
			else if (Input.GetKey("2"))
			{
				togglePathActive(1);
				eventTimeout = delayUntilNextEvent;
				Debug.Log ("active path 1");
			}
			else if (Input.GetKey("3"))
			{
				togglePathActive(2);
				eventTimeout = delayUntilNextEvent;
			}
			else if (Input.GetKey("4"))
			{
				togglePathActive(3);
				eventTimeout = delayUntilNextEvent;
			}
			else if (Input.GetKey("5"))
			{
				togglePathActive(4);
				eventTimeout = delayUntilNextEvent;
			}
			else if (Input.GetKey("6"))
			{
				togglePathActive(5);
				eventTimeout = delayUntilNextEvent;
			}

		}
		else
		{
			if (eventTimeout >= 0)
				eventTimeout -= Time.deltaTime;
		}
			
		
	}
}
