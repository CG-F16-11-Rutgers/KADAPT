using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class ColliderScript : MonoBehaviour {

    private BehaviorAgent selfBehavior;
    public GameObject player;
    private Animator animator;
    public bool personDown;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        personDown = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space") && !personDown)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
            if (distance < 1.3)
            {
                Debug.Log("ded");
                personDown = true;
                animator.SetBool("B_Dying", true);
            }
        }
    }

    public void OnMouseUp()
    {
        Debug.Log("DED");
        animator.SetBool("B_Dying", true);
    }
}
