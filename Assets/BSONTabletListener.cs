using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Toolbelt;
using Kernys.Bson;

public class BSONTabletListener : MonoBehaviour {

	BSONListener bl;
	public FPSInputController ic;
	public GameObject firstPersonController;
	public int listenPort;

	private bool joystickAtZero = false;

	private Events amnesiaEventHandler;
	private PositionSender posSender;

	public float moveSpeed;
	public float rotateSpeed;



	// Use this for initialization
	void Start () {
	
		bl = new BSONListener(listenPort);
		amnesiaEventHandler = GameObject.Find ("Scripts").GetComponent<Events> ();
		posSender = GetComponent<PositionSender>();
	}
	
	// Update is called once per frame
	void Update () {
	
		BSONObject bo;
		Vector3 newPosition = new Vector3(0, 0, 0);
		bool moveToNewPosition = false;
		float f;
		//ic.directionVector.x = 1.0f;
		//Debug.Log ("set dir");

		bo = bl.Receive();

		while (bo != null)
		{

			foreach (string k in bo.Keys)
			{
				Debug.Log (k + "," + bo[k] + ",\n");
			}

			if (bo.ContainsKey("positionListener"))
				posSender.enabled = true;

			if (bo.ContainsKey("y"))
			{
				ic.networkDirectionVector.z = ((float)bo["y"].doubleValue) * moveSpeed;
			}
			if (bo.ContainsKey("x"))
			{
				f = (float)bo["x"].doubleValue;
				Debug.Log ("set rot" + f);
				amnesiaEventHandler.setCameraRotateAmount(f * rotateSpeed);
				if (f == 0.0f)
					joystickAtZero = true;
				else
					joystickAtZero = false;
			}
			if (bo.ContainsKey("movex"))
			{
				moveToNewPosition = true;
				newPosition.x = bo["movex"];
				Debug.Log ("movex : " + bo["movex"]);
			}
			if (bo.ContainsKey("movey"))
			{
				moveToNewPosition = true;
				newPosition.y = bo["movey"];
			}
			if (bo.ContainsKey("movez"))
			{
				moveToNewPosition = true;
				newPosition.z = bo["movez"];
			}
			if (moveToNewPosition)
			{
				firstPersonController.transform.position = newPosition;
			}

			if (bo.ContainsKey("button"))
			{
				if (string.Equals (bo["button"], "amnesiaMode"))
					amnesiaEventHandler.useAmnesiaMode();
				else if (string.Equals (bo["button"], "browserMode"))
					amnesiaEventHandler.useBrowserMode();
				else if (string.Equals (bo["button"], "reset"))
					amnesiaEventHandler.resetCamera();
			}
			if (bo.ContainsKey("path1"))
			{
				amnesiaEventHandler.setPathActive(0, ((int)bo["path1"]) != 0);
			}
			if (bo.ContainsKey("path2"))
			{
				amnesiaEventHandler.setPathActive(1, ((int)bo["path2"]) != 0);
			}
			if (bo.ContainsKey("path3"))
			{
				amnesiaEventHandler.setPathActive(2, ((int)bo["path3"]) != 0);
			}
			if (bo.ContainsKey("path4"))
			{
				amnesiaEventHandler.setPathActive(3, ((int)bo["path4"]) != 0);
			}
			if (bo.ContainsKey("path5"))
			{
				amnesiaEventHandler.setPathActive(4, ((int)bo["path5"]) != 0);
			}
			if (bo.ContainsKey("path6"))
			{
				amnesiaEventHandler.setPathActive(5, ((int)bo["path6"]) != 0);
			}

			bo = bl.Receive();
		}
		if (joystickAtZero)
			amnesiaEventHandler.setCameraRotateAmount(0.0f);
	}
}
