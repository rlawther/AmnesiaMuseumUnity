using UnityEngine;
using System.Collections;

public class screenshotOnK : MonoBehaviour {

	//private bool takeHiResShot = false;
	private int shotNumber = 0;
	
	/*	
	public void TakeHiResShot() {
		takeHiResShot = true;
	}
	*/
	
	void LateUpdate() {
		//takeHiResShot |= ;
		if (Input.GetKeyDown("k")) {
			print ("screengrap");
			Application.CaptureScreenshot("C:/Dev/AmnesiaMuseumUnity/Assets/Screenshot" + shotNumber + ".png");
			shotNumber++;
			//takeHiResShot = false;
		}
	}
}