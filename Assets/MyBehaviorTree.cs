using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree : MonoBehaviour
{
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    //  public Animation a;
    public GameObject participant;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected Node Body(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, 4000));
    }

    protected Node Hand(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }

    protected Node Face(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 4000));
    }
    protected Node ST_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }


    protected Node BuildTreeRoot()
    {
        Node roaming = new DecoratorLoop(
                        new Sequence(

                        this.Face("HEADSHAKE"),
                        this.ST_ApproachAndWait(wander1),
                        this.Hand("CHEER"),
                        this.ST_ApproachAndWait(wander2)));

        return roaming;
    }
}
