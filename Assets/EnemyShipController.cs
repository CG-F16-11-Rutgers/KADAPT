using UnityEngine;
using System.Collections;

public class EnemyShipController : MonoBehaviour {

    public float shieldStrength;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(shieldStrength <= 0) {
            destroyVessel();
        }
	}

    public void damage(float dmg) {
        shieldStrength -= dmg;
    }

    void destroyVessel() {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(this.gameObject, exp.duration);
    }
}
