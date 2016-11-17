using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public Quaternion closedRot;
    public Quaternion openRot;

    private bool open;

	// Use this for initialization
	void Start () {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        open = false;
	}

    public bool isOpen() {
        return open;
    }

    public void openDoor() {
        open = true;
    }

    public void closeDoor() {
        open = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(open) {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, 0.1f);
        }
        else {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRot, 0.1f);
        }
	}
}
