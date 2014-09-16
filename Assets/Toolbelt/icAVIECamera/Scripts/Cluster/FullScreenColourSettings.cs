using UnityEngine;
using System.Collections;
using Toolbelt;

public class FullScreenColourSettings : MonoBehaviour 
{
	// Sets HSL/BCG of all children. Useful for warpmeshes that are children of this transform.
	public Vector3 HSL = new Vector3(0f,1f,1f);
	public Vector3 BrightnessContrastGamma = new Vector3(0.5f,0.5f,1.0f);
	
	// Set this to true to force HSL and Gamma to propagate to cameras next frame
	public bool forcePropagate = true;
	
	protected Vector3 prevHSL = new Vector3(0f,1f,1f);
	protected Vector3 prevBCG = new Vector3(0.5f,0.5f,1.0f);
	
	void Update() {
		if (HSL != prevHSL || forcePropagate)
			PropagateHSL();
			
		if (BrightnessContrastGamma != prevBCG || forcePropagate)
			PropagateBCG();
			
		if (forcePropagate)
			forcePropagate = false;
	}
	
	public void PropagateHSL() {
		ImageShaderScript[] scripts = this.GetComponentsInChildren<ImageShaderScript>();
		if (scripts.Length > 0) {
			foreach (ImageShaderScript iss in scripts) {
				iss.HSL = this.HSL;
			}
			this.prevHSL = this.HSL;
		}
	}
	
	public void PropagateBCG() {
		GammaLUT[] scripts = this.GetComponentsInChildren<GammaLUT>();
		if (scripts.Length > 0) {
			foreach (GammaLUT glut in scripts) {
				glut.brightness= BrightnessContrastGamma.x;
				glut.contrast= BrightnessContrastGamma.y;
				glut.gamma= BrightnessContrastGamma.z;
			}
			this.prevBCG = this.BrightnessContrastGamma;
		}
	}	
}
