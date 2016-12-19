using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float speed, fastSpeed, rotSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void move(float x, float z, float y, bool fast) {
        Quaternion originalRotation = transform.rotation;
        transform.Rotate(-transform.rotation.eulerAngles.x, 0.0f, 0.0f, Space.Self);
        if (fast) {
            transform.Translate(z * fastSpeed * Time.deltaTime, 0.0f, x * fastSpeed * Time.deltaTime, Space.Self);
        }
        else {
            transform.Translate(z * speed * Time.deltaTime, 0.0f, x * speed * Time.deltaTime, Space.Self);
        }
        transform.rotation = originalRotation;
        transform.Translate(0.0f, y * 15 * speed * Time.deltaTime, 0.0f, Space.World);
    }

    public void rotate(float x, float y) {
        transform.Rotate(0.0f, x * rotSpeed * Time.deltaTime, 0.0f, Space.World);
        transform.Rotate(- y * rotSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }

}
