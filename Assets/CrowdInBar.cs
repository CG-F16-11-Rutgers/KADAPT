using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class CrowdInBar : MonoBehaviour {

    //Bartender
    public Transform point1;
    public Transform point2;
    public GameObject bartender;
    public GameObject table;
    private BehaviorAgent bartenderBehaviorAgent;

    //Two people talking
    public GameObject personTalk1;
    public GameObject personTalk2;
    private BehaviorAgent talkingAgent;

	void Start () {
        //Initializing the bartender
        bartenderBehaviorAgent = new BehaviorAgent(this.walkAroundBar());
        BehaviorManager.Instance.Register(bartenderBehaviorAgent);
        bartenderBehaviorAgent.StartBehavior();

        //Initialize talking people
        talkingAgent = new BehaviorAgent(this.talk());
        BehaviorManager.Instance.Register(talkingAgent);
        talkingAgent.StartBehavior();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    ///General Functions
    //Used for participant to walk to a certain location before talking
    protected Node ST_ApproachAndWait(GameObject participant, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    //Make participant face a target
    protected Node ST_TurnToFace(GameObject participant, Vector3 target)
    {
        Val<Vector3> position = Val.V(() => target);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_TurnToFace(target));
    }
    //Hand Gestures
    protected Node HandGesture(GameObject participant, string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    //Face Gestures
    protected Node FaceGesture(GameObject participant, string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 4000));
    }

    ///Bartender Tree
    protected Node walkAroundBar()
    {
        return new DecoratorLoop(new Sequence(
                                 ST_ApproachAndWait(bartender, point1),
                                 ST_TurnToFace(bartender, table.transform.position),
                                 HandGesture(bartender, "WRITING"),
                                 ST_ApproachAndWait(bartender, point2),
                                 ST_TurnToFace(bartender, table.transform.position),
                                 FaceGesture(bartender, "HEADSHAKE")
                                 ));
    }

    protected Node talk()
    {
        return new DecoratorLoop(
             new Sequence(
                 ST_TurnToFace(personTalk1, personTalk2.transform.position),
                 ST_TurnToFace(personTalk2, personTalk1.transform.position),

                 new SequenceParallel(
                 HandGesture(personTalk1,"BEINGCOCKY"),
                 FaceGesture(personTalk2,"HEADSHAKE")
                )));
    }
}
