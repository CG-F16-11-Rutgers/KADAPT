using UnityEngine;
using System.Collections;

public class SystemController : MonoBehaviour {

    public Transform interactPos;
    public bool online;
    public bool interacting;
    public bool repairing;
    float repairStart;

	// Use this for initialization
	void Start () {
        online = true;
        interacting = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(!online && interacting && !repairing) {
            repairing = true;
            repairStart = Time.time;
        }
        else if(!online && interacting && repairing) {
            if(Time.time >= repairStart + 6) {
                repairing = false;
                activate();
            }
        }
        else {
            repairing = false;
        }
	}
    

    public void startInteraction() {
        interacting = true;
    }

    public void stopInteraction() {
        interacting = false;
    }

    public void activate() {
        online = true;
    }

    public void deactivate() {
        online = false;
    }

}
