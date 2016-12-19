using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public Transform target;
    public GameObject laser;
    public float weaponPower;
    private LineRenderer laserLine;
    private float enableTime;
    public float rechargeTime;
    public bool ready;

	// Use this for initialization
	void Start () {
        ready = true;
        laserLine = laser.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time >= enableTime+1) {
            laserLine.enabled = false;
        }
        if(Time.time >= enableTime + rechargeTime) {
            ready = true;
        }
	}

    public void fireWeapon() {
        if (ready) {
            laserLine.SetPosition(1, target.position);
            laserLine.enabled = true;
            if (target != null) {
                target.gameObject.GetComponent<EnemyShipController>().damage(weaponPower);
            }
            enableTime = Time.time;
            ready = false;
        }
        //RaycastHit hitInfo;
        /*if(Physics.Raycast(transform.position, target.position - transform.position, out hitInfo, 2000.0f)) {
            print("raycast success");
            if(!hitInfo.transform.CompareTag("playerShip")) {
                print("not player ship");
                laserLine.SetPosition(1, hitInfo.transform.position);
                laserLine.enabled = true;
                enableTime = Time.time;
                if(hitInfo.transform.CompareTag("enemyShip")) {
                    print("enemy ship");
                    hitInfo.transform.gameObject.GetComponent<EnemyShipController>().shieldStrength -= weaponPower;
                }
            }
        }*/
    }

    public void setTarget(Transform target) {
        this.target = target;
    }

    public bool hasTarget() {
        return !(target == null);
    }

}
