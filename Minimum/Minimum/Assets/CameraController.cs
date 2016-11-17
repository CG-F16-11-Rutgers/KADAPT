using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public int speed;
    public int rotSpeed;
    public GameObject door;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate() {
        if (Input.GetButtonDown("Jump")) {
                door.GetComponent<DoorScript>().openDoor();
        }
        if (Input.GetButton("Fire3")) {
            transform.Rotate(0.0f, Input.GetAxis("Mouse X") * rotSpeed, 0.0f, Space.World);
            transform.Rotate(-Input.GetAxis("Mouse Y") * rotSpeed, 0.0f, 0.0f);
        }
        Vector3 pan = new Vector3(Input.GetAxis("Horizontal") * 0.01f * speed, 0.0f, Input.GetAxis("Vertical") * 0.01f * speed);
        Vector3 zoom = new Vector3(0.0f, Input.GetAxis("Mouse ScrollWheel") * 0.05f * speed, 0.0f);
        transform.Translate(pan);
        transform.Translate(zoom);
    }
}
