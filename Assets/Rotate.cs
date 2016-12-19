using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(5, 30, 15) * Time.deltaTime);

    }
}
