using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class TakeCup : MonoBehaviour {

    public GameObject person;
    public GameObject can;
    private BehaviorAgent personBehavior;

	// Use this for initialization
	void Start () {
        personBehavior = new BehaviorAgent(takeCan());
        BehaviorManager.Instance.Register(personBehavior);
        personBehavior.StartBehavior();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Hand Gestures
    protected Node HandGesture(GameObject participant, string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    //Make participant face a target
    protected Node ST_TurnToFace(GameObject participant, Vector3 target)
    {
        Val<Vector3> position = Val.V(() => target);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_TurnToFace(target));
    }
    //Used for participant to walk to a certain location before talking
    protected Node ST_ApproachAndWait(GameObject participant, Vector3 target)
    {
        Val<Vector3> position = Val.V(() => target);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node takeCan()
    {
        return new Sequence(
                    ST_TurnToFace(person, can.transform.position),
                    HandGesture(person, "REACHRIGHT"),
                    ST_ApproachAndWait(can, person.transform.position),
                    new DecoratorLoop(HandGesture(person,"DRINK")));
    }
}
