using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class Behavior_Drink : MonoBehaviour {
    //public Transform wander1;
    //public Transform wander2;
    //public Transform wander3;
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

    protected Node ST_OpenDoor(Transform target) { //UNIMPLEMENTED
        return null;
    }

    protected Node ST_WaitRandom(long min, long max) {
        return new Sequence(new LeafWait((long)((max - min) * UnityEngine.Random.value + min)));
    }

    protected Node ST_Drink() {
        Val<String> name = Val.V(() => "DRINK");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 1000));
    }

    protected Node BuildTreeRoot() {
        Node roaming = new DecoratorLoop(
            new Sequence(
                //ST_WaitRandom(1000, 5000),
                this.ST_Drink()
                ));
        return roaming;
    }
}