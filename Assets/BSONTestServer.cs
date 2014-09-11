using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Toolbelt;
using Kernys.Bson;

public class BSONTestServer : MonoBehaviour {

	BSONListener bl;
	public FPSInputController ic;
	public bool yAxisTurn;
	public GameObject firstPersonController;
	public int listenPort;

	// Use this for initialization
	void Start () {
	
		bl = new BSONListener(listenPort);
	}
	
	// Update is called once per frame
	void Update () {
	
		BSONObject bo;
		Vector3 newPosition = new Vector3(0, 0, 0);
		bool moveToNewPosition = false;
		//ic.directionVector.x = 1.0f;
		//Debug.Log ("set dir");

		bo = bl.Receive();

		while (bo != null)
		{
			/*
			foreach (string k in bo.Keys)
			{

				Debug.Log (k + "," + bo[k] + ",\n");
			}
			*/


			if (bo.ContainsKey("y"))
			{
				ic.networkDirectionVector.z = bo["y"];
			}
			if (bo.ContainsKey("x"))
			{
				if (yAxisTurn)
					firstPersonController.transform.Rotate(0, bo["x"] * 1.0f, 0);
				else
					ic.networkDirectionVector.x = bo["x"];
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
				if (string.Equals (bo["button"], "artistic"))
					Debug.Log ("ARTISTIC\n");
				else
					Debug.Log ("not ARTISTIC\n");
			}

			bo = bl.Receive();
		}

	}
}
