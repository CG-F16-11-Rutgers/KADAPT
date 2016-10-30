using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class Behavior_Drink : MonoBehaviour {
    //public Transform wander1;
    //public Transform wander2;
    //public Transform wander3;
    public GameObject participant;
    public GameObject drink;
    public GameObject door;
    public Transform doorOpenPoint;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start() {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update() {
    }

    protected Node ST_ApproachAndWait(Transform target) {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(5000));
    }

    protected Node ST_Approach(Transform target) {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position));
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
            return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 500), new LeafInvoke(() => door.GetComponent<DoorScript>().openDoor()));
        }
    }

    protected Node BuildTreeRoot() {
        Node roaming =
            new Sequence(
                //ST_PickupObject(drink),
                //ST_Sit(),
                ST_Approach(doorOpenPoint),
                //new DecoratorLoop(
                //new Sequence(
                //ST_WaitRandom(2000, 6000),
                //ST_Drink();
                //)),
                ST_OpenDoor());
                //ST_Stand()
        return roaming;
    }
}