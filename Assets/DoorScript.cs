using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public Quaternion closedRot;
    public Quaternion openRot;

    private bool open;

	// Use this for initialization
	void Start () {
        open = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(open) {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, 0.001f);
        }
        else {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRot, 0.2f);
        }
	}
}
