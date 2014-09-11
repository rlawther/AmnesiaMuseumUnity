using UnityEngine;
using System.Collections;
using Toolbelt;

public class PositionSender : MonoBehaviour {

	public string remoteHost;
	public int remotePort;
	public int sendEveryXFrames = 1;
	public GameObject playerPosition;
	 
	private BSONSender bsonSender;
	private int framesTillSend;

	// Use this for initialization
	void Start ()
	{
		bsonSender = new BSONSender(remoteHost, remotePort);
		framesTillSend = sendEveryXFrames;
	}
	
	// Update is called once per frame
	void Update () {

		framesTillSend--;

		if (framesTillSend <= 0)
		{
			Kernys.Bson.BSONObject bsonObj = new Kernys.Bson.BSONObject ();
			bsonObj.Add ("newx", playerPosition.transform.position.x);
			bsonObj.Add ("newy", playerPosition.transform.position.y);
			bsonObj.Add ("newz", playerPosition.transform.position.z);
			bsonSender.SendUncompressed (bsonObj);

			framesTillSend = sendEveryXFrames;
		}

	}
}
