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
	
	private Vector2 mapBottomLeft;
	private Vector2 mapTopRight;
	private Vector2 mapSize;
	

	// Use this for initialization
	void Start () {
	
		/* These were "measured" in the editor */
		mapBottomLeft = new Vector2(636, -275);
		mapTopRight = new Vector2(1524, 745);
		mapSize = mapTopRight - mapBottomLeft;
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
		GUIUtility.RotateAroundPivot(-90, new Vector2(mapPosition.x + (mapPosition.width/2.0f),
		                                              mapPosition.y + (mapPosition.height/2.0f)));
		GUI.DrawTexture(mapPosition, mapTexture);


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
			
			newColour.a = playerAlpha;
			GUI.color = newColour;
			drawPlayer (rightEyePos);
		}
		
		GUI.color = oldColour;
		GUI.matrix = matrixBackup;

	}
}
