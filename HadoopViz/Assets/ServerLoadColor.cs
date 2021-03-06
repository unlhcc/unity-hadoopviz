using System;
using UnityEngine;
using System.Collections;

public class ServerLoadColor : MonoBehaviour{

	Material mat;
	float incAmt = 0.07f;
	float reducAmt = 0.08f;
	float heightLimit = 10f;
	float alpha = 1.0f;
	float blue = 0;

	float heightCount = 0;

	bool showSmoke = false;

	public ParticleSystem smoke;
	public ParticleSystem fire;

	void Start(){
		mat = GetComponent<Renderer>().material;
		mat.color = new Color(0,1,0,alpha);
		InvokeRepeating ("ReduceColor", 1,2);
	}

	void Update(){
		//toggles if servers should smoke when the get lots of traffic
		if(Input.GetKeyDown(KeyCode.T)){
			showSmoke = !showSmoke;
		}
	}

	public Color GetMatColor(){
		return mat.color;
	}

	//changes the color of the selected server to highlight it
	public void ToggleSelected(float toggle){
		blue = toggle;
		mat.color = new Color (mat.color.r,mat.color.g,blue,alpha);
	}

	void OnParticleCollision(GameObject particalSystem){
		IncColor ();
	}

	//increases the redness of the server
	public void IncColor(){
		if (mat.color.r + incAmt < 1) {
			mat.color = new Color (mat.color.r + incAmt, mat.color.g, blue, alpha);
		} else if (mat.color.g - incAmt > 0) {
			mat.color = new Color (1, mat.color.g - incAmt, blue, alpha);
		} else {
			mat.color = new Color(1,0,blue,alpha);
		}
		ChangeHeight (incAmt);
	}

	//decreases the redness of the server
	void ReduceColor(){
		if (mat.color.r == 1) {
			if (mat.color.g + reducAmt < 1) {
			mat.color = new Color (1, mat.color.g + reducAmt, blue, alpha);
			} else {
			mat.color = new Color (mat.color.r - reducAmt, 1, blue, alpha);
			}
		} else if (mat.color.r - reducAmt > 0) {
			mat.color = new Color (mat.color.r - reducAmt, 1, blue, alpha);
		} else {
			mat.color = new Color(0,1,blue,alpha);
		}
		ChangeHeight (-reducAmt);
	}

	//sets the height of the server
	void ChangeHeight (float amt){
		if(amt + heightCount >= 0 && amt + heightCount <= 2){
			heightCount += amt;
			transform.position = new Vector3 (transform.position.x,heightCount/heightLimit,transform.position.z);
		}
		var smokeEmission = smoke.emission;
		var fireEmission = fire.emission;
		if (heightCount > 1.9f) {//if smoke is enabled, will smoke at specified height
			smokeEmission.enabled = showSmoke;
			fireEmission.enabled = showSmoke;
		} else {
			smokeEmission.enabled = false;
			fireEmission.enabled = false;
		}
	}

}