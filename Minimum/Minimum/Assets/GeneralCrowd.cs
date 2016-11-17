using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class GeneralCrowd : MonoBehaviour
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

    private BehaviorAgent mainBehaviorAgent;
    private BehaviorAgent behaviorAgent;
    private BehaviorAgent behaviorAgent1;
    private BehaviorAgent behaviorAgent2;
    private BehaviorAgent behaviorAgent3;

    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.Talking());
        behaviorAgent1 = new BehaviorAgent(this.Texting());
        behaviorAgent2 = new BehaviorAgent(this.Dance());
        behaviorAgent3 = new BehaviorAgent(this.DanceForever());
        BehaviorManager.Instance.Register(behaviorAgent);
        BehaviorManager.Instance.Register(behaviorAgent1);
        BehaviorManager.Instance.Register(behaviorAgent2);
        BehaviorManager.Instance.Register(behaviorAgent3);
        behaviorAgent.StartBehavior();
        behaviorAgent1.StartBehavior();
        behaviorAgent2.StartBehavior();
        behaviorAgent3.StartBehavior();
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




    //Used for participant and participant4 so they walk to a certain location before talking
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



    //Made these different nodes for each of the different participants
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
    protected Node CrowdGather5(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant5.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    protected Node CrowdGather6(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant6.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    protected Node CrowdGather1(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant1.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    protected Node CrowdGather7(string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant7.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }

    //Make participants face each other
    protected Node ST_TurnToFace(GameObject g, Vector3 target)
    {
        Val<Vector3> position = Val.V(() => target);
        return new Sequence(g.GetComponent<BehaviorMecanim>().ST_TurnToFace(target));
    }


    //Test to see if it was working
    protected Node Tester()
    {
        return new DecoratorLoop(
                    new Randomm(
                        CrowdGather("CHEER"),
                        CrowdGather("CLAP")));
    }

    protected Node EndLoop()
    {
        return new DecoratorLoop(new Sequence(participant1.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("SATNIGHTFEVER", 1000)));
    }

    //This is where the main part should happen currently it just makes a person cheer and clap at random then ends the loop
    // by making a random person in the back perform a constant action.
    protected Node BuildTreeRoot()
    {
        Node roaming = new SequenceParallel(

                                new Sequence(this.ST_ApproachAndWait(wander1),
                                                Tester()),
                                EndLoop());

        return roaming;
    }

    //Behavior Between Daniel and Daniel4
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

    //Behavior for Daniels 5&6
    protected Node Texting()
    {
        return new DecoratorLoop(
                new SequenceParallel(
                    CrowdGather5("TEXTING"),
                    CrowdGather6("TEXTING")));
    }

    //For Daniel 1, who dances until the danceoff
    protected Node Dance()
    {
        return new DecoratorLoop(
                new Sequence(CrowdGather1("SATNIGHTFEVER")));
    }

    //For Daniel 7, he dances forever
    protected Node DanceForever()
    {
        return new DecoratorLoop(
                new Sequence(CrowdGather7("SATNIGHTFEVER")));
    }
}
