using UnityEngine;
using System.Collections;

public class SkyboxCameraController : MonoBehaviour {

    public Transform cam;
    public Transform rotPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Inverse(rotPoint.rotation) * cam.rotation;
	}
}
