using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public Transform attackOrigin;
    //public float float1;
    public float float2;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    private float speedMul = 1.55f;
	void FixedUpdate () {
        controller();
	}

    void controller()
    {
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
            Kick();
        }
    }
    
    void Kick() {
        animator.SetBool("B_PickupLeft", true);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(attackOrigin.position, fwd, Color.red);
        if (Physics.Raycast(attackOrigin.position, fwd, out hitInfo)) {
            print(hitInfo.collider.gameObject.tag);
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void Kick2()
    {
        animator.SetBool("B_PickupLeft", true);
        RaycastHit hit;
        Ray kickingRay = new Ray(transform.position, Vector3.forward);
        if (Physics.Raycast(kickingRay, out hit, float2))
        {
            if(hit.collider.tag == "TheKicked")
            {
                Debug.Log("Kick Contact");
            }
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

}
