using UnityEngine;
using System.Collections;

public class HUDMap : MonoBehaviour {

	public Texture playerTexture;
	public Vector2 playerSize;
	public float playerAlpha;
	public Texture mapTexture;
	public float mapAlpha;
	public Rect mapPosition;
	public bool drawInBothEyes;
	public Vector2 rightEyeOffset;
	public GameObject firstPersonController;

	public Texture [] pathTextures;
	
	private Vector2 mapBottomLeft;
	private Vector2 mapTopRight;
	private Vector2 mapSize;

	private bool gotParent = false;
	private GameObject photoParent;
	private GameObject [] paths;

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

	
	// Use this for initialization
	void Start () {
	
		/* These were "measured" in the editor */
		mapBottomLeft = new Vector2(636, -275);
		mapTopRight = new Vector2(1524, 745);
		mapSize = mapTopRight - mapBottomLeft;

		paths = new GameObject[7];

	}
	
	// Update is called once per frame
	void Update () {
		if (!gotParent && (Time.time > 1.0))
		{
			GameObject photoParent = GameObject.Find ("Photos");
			findPaths (photoParent);
			
			gotParent = true;
		}

	}
	
	void drawPlayer(Rect mapPos)
	{
	
		Vector3 pos = firstPersonController.transform.position;
		Rect playerRect = new Rect(
			(((pos.z - mapBottomLeft.y)/mapSize.y) * mapPos.width) 
		  	+ mapPos.x - (playerSize.y / 2.0f),
			(((pos.x - mapBottomLeft.x)/mapSize.x) * mapPos.height) + mapPos.y - (playerSize.x / 2.0f),
			playerSize.x,
		  playerSize.y);
		
		GUI.DrawTexture(playerRect, playerTexture);
	}
	
	void OnGUI()
	{
		Color oldColour;
		Color newColour;
		oldColour = GUI.color;
		newColour = GUI.color;
		newColour.a = mapAlpha;
		GUI.color = newColour;

		Matrix4x4 matrixBackup = GUI.matrix;
		/*
		GUIUtility.RotateAroundPivot(-90, new Vector2(mapPosition.x + (mapPosition.width/2.0f),
		                                              mapPosition.y + (mapPosition.height/2.0f)));
		                                              */
		GUI.DrawTexture(mapPosition, mapTexture);

		for (int i = 0; i < 6; i++)
		{
			if (paths[i].activeSelf)
				GUI.DrawTexture (mapPosition, pathTextures[i]);
		}

		newColour.a = playerAlpha;
		GUI.color = newColour;
		drawPlayer (mapPosition);
		GUI.matrix = matrixBackup;

		newColour.a = mapAlpha;
		GUI.color = newColour;
		
		if (drawInBothEyes)
		{
			Rect rightEyePos;
			GUIUtility.RotateAroundPivot(-90, new Vector2(rightEyeOffset.x + mapPosition.x + (mapPosition.width/2.0f),
			                                              rightEyeOffset.y + mapPosition.y + (mapPosition.height/2.0f)));
			rightEyePos = mapPosition;
			rightEyePos.x += rightEyeOffset.x;
			rightEyePos.y += rightEyeOffset.y;
			GUI.DrawTexture(rightEyePos, mapTexture);

			for (int i = 0; i < 6; i++)
			{
				if (paths[i].activeSelf)
					GUI.DrawTexture (rightEyePos, pathTextures[i]);
			}

			newColour.a = playerAlpha;
			GUI.color = newColour;
			drawPlayer (rightEyePos);
		}
		
		GUI.color = oldColour;
		GUI.matrix = matrixBackup;

	}
}
