using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree : MonoBehaviour
{
    public Transform wander1;
    public Transform wander2;

    //  public Animation a;
    public GameObject participant;
    public GameObject participant1;
    public GameObject participant2;
    public GameObject participant3;
    public GameObject participant4;
    public GameObject participant5;
    public GameObject participant6;
    public GameObject participant7;


    private BehaviorAgent behaviorAgent;
    private BehaviorAgent behaviorAgent1;
    private BehaviorAgent behaviorAgent2;

    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.Talking());
        behaviorAgent1 = new BehaviorAgent(this.Texting());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected Node Face(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 4000));
    }




    //Things used in the subtree
    protected Node ST_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    protected Node ST_ApproachAndWait4(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant4.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }


    protected Node CrowdGather(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    protected Node CrowdGather4(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant4.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 4000));
    }
    protected Node ST_TurnToFace(GameObject g, Vector3 target)
    {
        Val<Vector3> position = Val.V(() => target);
        return new Sequence(g.GetComponent<BehaviorMecanim>().ST_TurnToFace(target)); 
    }



    protected Node Tester()
    {
        return new DecoratorLoop(
                    new SequenceShuffle(
                        CrowdGather("CHEER"),
                        CrowdGather("CLAP")));
    }

    protected Node EndLoop()
    {
        return new DecoratorLoop(new Sequence(participant1.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("SATNIGHTFEVER",1000)));
    }

    protected Node BuildTreeRoot()
    {
        Node roaming = new SequenceParallel(

                                new Sequence(this.ST_ApproachAndWait(wander1),
                                                Tester()),
                                EndLoop());

        return roaming;
    }

    protected Node Talking()
    {
        return new DecoratorLoop(
                    new Sequence(
                        ST_ApproachAndWait(wander2),
                        ST_ApproachAndWait4(wander1),
                        ST_TurnToFace(participant, participant4.transform.position),
                        ST_TurnToFace(participant4, participant.transform.position),

                        new SequenceParallel(
                        CrowdGather("BEINGCOCKY"),
                        CrowdGather4("HEADSHAKE")
                       )));
    }
    protected Node Texting()
    {

    }

}
