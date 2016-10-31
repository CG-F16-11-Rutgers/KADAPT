using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class Behavior_Danceoff : MonoBehaviour {
    //public Transform wander1;
    //public Transform wander2;
    //public Transform wander3;
    public GameObject drink;
    public GameObject door;
    public Transform doorOpenPoint;

    public Transform wander1;
    public Transform wander2;

    public GameObject Darrell;

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
    void Start() {
        /*
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
        */
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update() {
    }

    protected Node Face(string s) {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 4000));
    }

    protected Node ST_ApproachAndWait(Transform target) {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(5000));
    }

    protected Node ST_ApproachAndWait4(Transform target) {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant4.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_Approach(Transform target) {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(Darrell.GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }

    protected Node ST_WaitRandom(long min, long max) {
        return new Sequence(new LeafWait((long)(((max - min) * UnityEngine.Random.value) + min)));
    }

    protected Node ST_Wait(long t) {
        return new Sequence(new LeafWait(t));
    }

    protected Node ST_PickupObject(GameObject target) {
        return null;
    }

    protected Node ST_Sit() {
        return new Sequence(new LeafInvoke(() => participant.GetComponent<BehaviorMecanim>().Character.SitDown()));
    }

    protected Node ST_Stand() {
        return new Sequence(new LeafInvoke(() => participant.GetComponent<BehaviorMecanim>().Character.StandUp()));
    }

    protected Node ST_Drink() {
        Val<String> name = Val.V(() => "DRINK");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 3000));
    }

    protected Node ST_OpenDoor() {
        Val<String> name = Val.V(() => "POINTING");
        if (door.GetComponent<DoorScript>().isOpen()) {
            return new Sequence();
        }
        else {
            return new Sequence(Darrell.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 500), new LeafInvoke(() => door.GetComponent<DoorScript>().openDoor()));
        }
    }

    //Made these different nodes for each of the different participants
    protected Node CrowdGather(string s) {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    protected Node CrowdGather4(string s) {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant4.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 4000));
    }
    protected Node CrowdGather5(string s) {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant5.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    protected Node CrowdGather6(string s) {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant6.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    protected Node CrowdGather1(string s) {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant1.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    protected Node CrowdGather7(string s) {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant7.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }

    protected Node ST_TurnToFace(GameObject g, Vector3 target) {
        Val<Vector3> position = Val.V(() => target);
        return new Sequence(g.GetComponent<BehaviorMecanim>().ST_TurnToFace(target));
    }


    //Test to see if it was working
    protected Node Tester() {
        return new DecoratorLoop(
                    new Randomm(
                        CrowdGather("CHEER"),
                        CrowdGather("CLAP")));
    }

    protected Node EndLoop() {
        return new DecoratorLoop(new Sequence(participant1.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("SATNIGHTFEVER", 1000)));
    }

    protected Node Talking() {
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
    protected Node Texting() {
        return new DecoratorLoop(
                new SequenceParallel(
                    CrowdGather5("TEXTING"),
                    CrowdGather6("TEXTING")));
    }

    //For Daniel 1, who dances until the danceoff
    protected Node Dance() {
        return new DecoratorLoop(
                new Sequence(CrowdGather1("SATNIGHTFEVER")));
    }

    //For Daniel 7, he dances forever
    protected Node DanceForever() {
        return new DecoratorLoop(
                new Sequence(CrowdGather7("SATNIGHTFEVER")));
    }

    protected Node GeneralCrowd() {
        return new Sequence(
            new DecoratorLoop(
                new SequenceParallel(
                    Talking(),
                    Texting(),
                    Dance(),
                    DanceForever()
            )));
    }

    protected Node PersonAEnter() {
        return new Sequence(
                ST_Approach(doorOpenPoint),
                ST_OpenDoor()
            );
    }

    protected Node BuildTreeRoot() {
        Node roaming =
            new Sequence(
                new SequenceParallel(
                    this.GeneralCrowd(),
                    this.PersonAEnter()
                ),
                new SequenceParallel(
                    //AB interaction
                    //cup grab
                    ));
        return roaming;
    }
}