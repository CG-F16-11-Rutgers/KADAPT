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
    public GameObject Damien;

    //  public Animation a;
    public GameObject participant;
    public GameObject participant1;
    public GameObject participant2;
    public GameObject participant3;
    public GameObject participant4;
    public GameObject participant5;
    public GameObject participant6;
    public GameObject participant7;

    private bool idleMode;

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
        idleMode = true;
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

    protected Node ST_ApproachAndWait(GameObject participant, Transform target) {
        Vector3 a = (target.position - participant.transform.position).normalized;
        Val<Vector3> position = Val.V(() => (target.position - a));
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_ApproachAndWait(GameObject participant, GameObject participant2) {
        Vector3 a = (participant2.transform.position - participant.transform.position).normalized * 1;
        Vector3 b = new Vector3(a.x, participant2.transform.position.y, a.z);


        Val<Vector3> position = Val.V(() => (participant2.transform.position - b));
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }

    protected Node ST_ApproachAndWait4(Transform target) {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant4.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_Approach(Transform target) {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(Darrell.GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }

    protected Node ST_Approach(GameObject p, Vector3 t) {
        Val<Vector3> position = Val.V(() => t);
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
        return new Sequence(new LeafInvoke(() => Damien.GetComponent<BehaviorMecanim>().Character.SitDown()));
    }

    protected Node ST_Stand() {
        return new Sequence(new LeafInvoke(() => Damien.GetComponent<BehaviorMecanim>().Character.StandUp()));
    }

    protected Node ST_Drink() {
        Val<String> name = Val.V(() => "DRINK");
        return new Sequence(Damien.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 3000));
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

    protected Node ST_GetCloseTo(GameObject participant, GameObject participant2) {
        Vector3 a = (participant2.transform.position - participant.transform.position).normalized * 3;
        Vector3 b = new Vector3(a.x, participant2.transform.position.y, a.z);


        Val<Vector3> position = Val.V(() => (participant2.transform.position - b));
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }

    protected Node ST_Dance(GameObject participan) {
        Val<String> be = Val.V(() => "BREAKDANCE");
        return new Sequence(participan.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(be, 5000), new LeafWait(1000));
    }
    protected Node ST_StepBack(GameObject participant) {
        Val<String> be = Val.V(() => "STEPBACK");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(be, 2000), new LeafWait(500));
    }
    protected Node ST_CallOver(GameObject participant) {
        print("x");
        Val<String> be = Val.V(() => "CALLOVER");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(be, 1500), new LeafWait(500));
    }
    protected Node ST_Drink(GameObject participant) {
        Val<String> be = Val.V(() => "DRINK");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(be, 5000), new LeafWait(500));
    }
    protected Node ST_ShakeHead(GameObject participant) {
        Val<String> be = Val.V(() => "HEADSHAKETHINK");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(be, 3000), new LeafWait(500));
    }
    protected Node ST_Yawn(GameObject participant) {
        Val<String> be = Val.V(() => "YAWN");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(be, 7000), new LeafWait(2000));
    }
    protected Node ST_Clap(GameObject participant) {
        Val<String> be = Val.V(() => "CLAP");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(be, 2000), new LeafWait(2000));
    }
    protected Node ST_BeingCocky(GameObject participant) {
        Val<String> be = Val.V(() => "BEINGCOCKY");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(be, 2000), new LeafWait(2000));
    }

    protected Node ST_LookStand(GameObject participant, GameObject participant2) {
        Vector3 a = new Vector3(0, 1.8f, 0);
        Val<Vector3> position = Val.V(() => (participant2.transform.position + a));
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_HeadLookTurnFirst(position), new LeafWait(500));
    }
    protected Node ST_LookDance(GameObject participant, GameObject participant2) {
        Vector3 a = new Vector3(0, 0.8f, 0);
        Val<Vector3> position = Val.V(() => (participant2.transform.position + a));
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_HeadLookTurnFirst(position), new LeafWait(500));
    }
    protected Node WalkTowards(GameObject participant, GameObject participant2) {
        return new Sequence(this.ST_LookStand(participant, participant2), this.ST_ApproachAndWait(participant, participant2));
    }

    protected Node AbInteraction() {

        Node roaming =
                        new Sequence(
                            this.ST_Stand(),
                        //this.ST_Drink(main1),
                        //this.ST_LookStand(main2, main1),
                        this.ST_LookStand(Darrell, Damien),
                        this.ST_GetCloseTo(Darrell, Damien),
                        this.ST_CallOver(Darrell),
                        this.WalkTowards(Damien, Darrell),
                        this.ST_ShakeHead(Damien),
                        this.ST_StepBack(Damien),
                        new SequenceParallel(this.DanceOfff(),
                        this.ST_LookDance(Darrell, Damien))

                        );
        return roaming;
    }
    protected Node RandonSuccess() {
        Node result = new TreeSharpPlus.RandomS();
        return result;

    }
    protected Node DanceReact(GameObject participant) {
        Node result = new Randomm(this.ST_Yawn(participant), this.ST_BeingCocky(participant), this.ST_Clap(participant));
        return result;

    }
    protected Node DanceOfff() {
        //float c = 5;
        Node dance =
                        new Sequence(

                        this.ST_Dance(Damien), this.DanceReact(Darrell),
                        new DecoratorInvert(new DecoratorLoop(new Sequence(
                        this.ST_StepBack(Darrell), this.ST_Dance(Darrell), this.DanceReact(Damien), RandonSuccess(), this.ST_StepBack(Damien), this.ST_Dance(Damien), this.DanceReact(Darrell)))),
                        this.ST_CallOver(Darrell)

                        );
        //new DecoratorLoop(new Sequence(this.RandonSuccess()));
        return dance;
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

    protected Node PersonBIdle() {
        return new Sequence(ST_Sit(), 
            new DecoratorLoop(
                new Sequence(
                    ST_WaitRandom(2000, 6000), 
                    ST_Drink())));
    }

    protected Node Kick() {

        Val<String> name = Val.V(() => "PICKUPLEFT");
        return Darrell.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, 3000);
    }

    protected Node FallDown() {
        Val<String> name = Val.V(() => "PICKUPRIGHT");
        return participant2.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, 3000);
    }
    protected Node PersonKickInteraction() {
        return new Sequence(this.PersonAEnter(), new LeafWait(2000), ST_Approach(Darrell, new Vector3(-6.6f, 0.1f, 10.2f)),
            new SequenceParallel(
                    this.Kick(),
                     this.FallDown(),
                     new LeafInvoke(() => setMode(false))

                    ));
    }

    void setMode(bool val) {
        idleMode = val;
    }

    bool assertMode2() {
        return idleMode;
    }

    protected Node assertMode() {
        return new LeafAssert(assertMode2);
    }

    protected Node BuildTreeRoot() {
        Node roaming =
            new Selector(
                new SequenceParallel(
                    this.GeneralCrowd(),
                    this.PersonKickInteraction(),
                    this.PersonBIdle(),
                    new DecoratorLoop(
                    this.assertMode())
                ),
                new SequenceParallel(
                    this.AbInteraction()
                    //cup grab
                    ));
        return roaming;
    }
}