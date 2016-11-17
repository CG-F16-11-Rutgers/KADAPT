using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree3 : MonoBehaviour {
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    public GameObject participant;

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
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_OpenDoor(Transform target) {
        return null;
    }

    protected Node ST_Jump() {
        Val<String> name = Val.V(() => "DUCK");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, 2000));
    }

    protected Node BuildTreeRoot() {
        Node roaming = new DecoratorLoop(
            new Sequence(
                this.ST_ApproachAndWait(this.wander1),
                this.ST_Jump(),
                this.ST_ApproachAndWait(this.wander2),
                this.ST_Jump(),
                this.ST_ApproachAndWait(this.wander3),
                this.ST_Jump()));
        return roaming;
    }
}
