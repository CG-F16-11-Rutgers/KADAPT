using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public Transform attackOrigin;
    public float float1;
    public float float2;

    public GameObject door;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        //personDown = false;
	}

    // Update is called once per frame
    private float speedMul = 1.55f;
	void FixedUpdate () {
        controller();
	}

    void controller()
    {
        float deltaX = transform.position.x - door.transform.position.x;
        float deltaZ = transform.position.z - door.transform.position.z;
        float distance = Mathf.Sqrt(deltaX * deltaX + deltaZ * deltaZ);


      //  Debug.Log(distance);
        if (Input.GetKey(KeyCode.E) && distance<=3.0f)
        {
            animator.SetBool("B_PickupRight", true);
            door.GetComponent<DoorScript>().openDoor();
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMul = Mathf.Lerp(speedMul, 6.01f, 0.1f);
        }
        else
        {
            speedMul = Mathf.Lerp(speedMul, 1.55f, 0.1f);
        }
        float moveSpeed = Input.GetAxis("Vertical") * speedMul;
        float rotSpeed = Input.GetAxis("Horizontal") * 60;
        if (moveSpeed != animator.GetFloat("Speed"))
        {
            animator.SetFloat("Speed", moveSpeed);
        }
        if (rotSpeed != animator.GetFloat("Direction"))
        {
            animator.SetFloat("Direction", rotSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("B_PickupLeft", true);
        }
    }
    
    

}
