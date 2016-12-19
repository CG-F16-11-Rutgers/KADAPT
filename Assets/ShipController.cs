using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public float velMove, velYaw, velPitch, velRoll;
    public float accelMove, accelYaw, accelPitch, accelRoll;
    public float tgtMove, tgtYaw, tgtPitch, tgtRoll;
    public GameObject sys_Helm, sys_Engines, sys_Weapons, sys_Shields;
    public Transform sceneRotPoint;
    public Transform sceneMovePoint;
    public GameObject weapon;
    public bool engineOn, weaponOn;

	// Use this for initialization
	void Start () {
        /*sys_Engines.GetComponent<SystemController>().activate();
        sys_Weapons.GetComponent<SystemController>().activate();
        sys_Shields.GetComponent<SystemController>().activate();*/
        engineOn = false;
        weaponOn = false;
    }
	
	// Update is called once per frame
	void Update () {
        sceneRotPoint.Rotate(velRoll * Time.deltaTime, -velYaw * Time.deltaTime, -velPitch * Time.deltaTime, Space.World);
        sceneMovePoint.Translate(-velMove * Time.deltaTime, 0.0f, 0.0f, Space.World);
        if(sys_Engines.GetComponent<SystemController>().online && sys_Helm.GetComponent<SystemController>().interacting) {
            updateVelocities();
        }
	}

    public void fire() {
        if (weapon.GetComponent<WeaponController>().hasTarget() && sys_Weapons.GetComponent<SystemController>().online && sys_Weapons.GetComponent<SystemController>().interacting) {
            weapon.GetComponent<WeaponController>().fireWeapon();
        }
    }

    void updateVelocities() {
        if (velMove != tgtMove) {
            velMove = Mathf.Lerp(velMove, tgtMove, accelMove);
        }
        if (velYaw != tgtYaw) {
            velYaw = Mathf.Lerp(velYaw, tgtYaw, accelYaw);
        }
        if (velPitch != tgtPitch) {
            velPitch = Mathf.Lerp(velPitch, tgtPitch, accelPitch);
        }
        if (velRoll != tgtRoll) {
            velRoll = Mathf.Lerp(velRoll, tgtRoll, accelRoll);
        }
    }

    public void setTarget(Transform target) {
        weapon.GetComponent<WeaponController>().setTarget(target);
    }

    public void set_tgtMove(float value) {
        tgtMove = value;
    }

    public void set_tgtPitch(float value) {
        tgtPitch = value;
    }
    
    public void set_tgtYaw(float value) {
        tgtYaw = value;
    }

    public void set_tgtRoll(float value) {
        tgtRoll = value;
    }
}
