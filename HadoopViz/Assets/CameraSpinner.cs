using System;
using UnityEngine;
using System.Collections;

public class CameraSpinner : MonoBehaviour{

	Camera cam;
	bool autoRotate = true;
	float scaleSpeed = 2f;
	float scaleSpeedPerspective = 15f;
	float turnSpeedAuto = 4f;
	float turnSpeedManual = 40f;

	void Awake(){
		cam = GetComponentInChildren<Camera> ();
	}

	void Update(){
		//toggle auto spinning the camera
		if(Input.GetKeyDown(KeyCode.Space)){
			autoRotate = !autoRotate;
		}
		if(autoRotate){
			transform.Rotate (new Vector3(0,Time.deltaTime * turnSpeedAuto,0));
		}
		//manually spin the cammera with left and right arrow keys
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Rotate (new Vector3(0,Time.deltaTime * turnSpeedManual,0));
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Rotate (new Vector3(0,-Time.deltaTime * turnSpeedManual,0));
		}

		//zoom in and out with up and down arrow keys
		if(Input.GetKey(KeyCode.DownArrow)){
			if (cam.orthographic) {
				cam.orthographicSize += Time.deltaTime * scaleSpeed;
			} else {
				cam.fieldOfView += Time.deltaTime * scaleSpeedPerspective;
				if(cam.fieldOfView > 170){
					cam.fieldOfView = 170;
				}
			}
		}
		if(Input.GetKey(KeyCode.UpArrow)){
			if (cam.orthographic) {
				cam.orthographicSize -= Time.deltaTime * scaleSpeed;
			} else {
				cam.fieldOfView -= Time.deltaTime * scaleSpeedPerspective;
				if(cam.fieldOfView < 10){
					cam.fieldOfView = 10;
				}
			}
		}

		//toggle between ortho and perspetive camera angles
		if(Input.GetKeyDown(KeyCode.O)){
			cam.orthographic = !cam.orthographic;
		}
	}
}

