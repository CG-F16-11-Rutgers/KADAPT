using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class ColliderScript : MonoBehaviour {

    BehaviorAgent selfBehavior;
    private Animator animator;

    // Use this for initialization
    void Start () {
        selfBehavior = new BehaviorAgent(Die());
        BehaviorManager.Instance.Register(selfBehavior);
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    protected Node FallDown(GameObject participant)
    {
        Val<String> name = Val.V(() => "DYING");
        return participant.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, 3000);
    }

    protected Node Die()
    {
        return new Sequence(FallDown(this.gameObject));
    }

    void OnCollisionEnter(Collision col)
    {
        animator.SetBool("B_Dying", true);
        //if (col.gameObject.CompareTag("Player")) ;
       // {
         //   Debug.Log("Collision");
        //    animator.SetBool("B_Dying", true);
      //  }
    }

    public void OnMouseUp()
    {
        Debug.Log("DED");
        animator.SetBool("B_Dying", true);
    }
}
