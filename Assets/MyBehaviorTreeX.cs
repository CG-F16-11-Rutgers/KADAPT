using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTreex : MonoBehaviour {
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    public GameObject main1;
    public GameObject main2;
    public GameObject cup;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start() {
        behaviorAgent = new BehaviorAgent(this.AbInteraction());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update() {

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
                        //this.ST_Drink(main1),
                        //this.ST_LookStand(main2, main1),
                        this.ST_LookStand(main1, main2),
                        this.ST_GetCloseTo(main1, main2),
                        this.ST_CallOver(main1),
                        this.WalkTowards(main2, main1),
                        this.ST_ShakeHead(main2),
                        this.ST_StepBack(main2),
                        new SequenceParallel(this.DanceOfff(),
                        this.ST_LookDance(main1, main2))

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

                        this.ST_Dance(main2), this.DanceReact(main1),
                        new DecoratorInvert(new DecoratorLoop(new Sequence(
                        this.ST_StepBack(main1), this.ST_Dance(main1), this.DanceReact(main2), RandonSuccess(), this.ST_StepBack(main2), this.ST_Dance(main2), this.DanceReact(main1)))),
                        this.ST_CallOver(main1)

                        );
        //new DecoratorLoop(new Sequence(this.RandonSuccess()));
        return dance;
    }
}