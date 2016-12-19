using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree3 : MonoBehaviour {
    public GameObject wander1;
    public GameObject wander2;
    public GameObject wander3;
    public GameObject wander4;
    public GameObject wander5;
    public GameObject wander6;
    public GameObject wander7;
    public GameObject p;
    public GameObject participant2;
    public GameObject participant3;
    public GameObject participant4;
    public GameObject participant5;
    public bool engine = false;
    public bool weapon = false;

    public GameObject f1, f2;

    private BehaviorAgent behaviorAgent, behaviorAgent2;
    // Use this for initialization
    void Start() {
        f1.SetActive(false);
        f2.SetActive(false);
        behaviorAgent2 = new BehaviorAgent(this.StartA());
        BehaviorManager.Instance.Register(behaviorAgent2);
        behaviorAgent2.StartBehavior();
        behaviorAgent = new BehaviorAgent(this.StartEngine());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
        behaviorAgent = new BehaviorAgent(this.StartWeapon());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();



    }
    public void startI()
    {
        
        

    }
    public void startII()
    {
        behaviorAgent = new BehaviorAgent(this.StartEngine());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();


    }

    // Update is called once per frame
    void Update() {

        if ((p.transform.position - wander1.transform.position).magnitude<0.5)
        {
            engine = true;
            f1.SetActive(true);
            f2.SetActive(true);
        }
        if ((participant4.transform.position - wander6.transform.position).magnitude < 1 && (participant5.transform.position - wander7.transform.position).magnitude < 1)
        {
            weapon = true;
            print(weapon);
            
        }

    }

    protected Node ST_ApproachAndWait(GameObject participant, Transform target)
    {
        Vector3 a = (target.position - participant.transform.position).normalized/2;
        Val<Vector3> position = Val.V(() => (target.position)-a);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    protected Node ST_LookStand(GameObject participant, GameObject participant2)
    {
        Vector3 a = new Vector3(0, 2f, 0);
        Val<Vector3> position = Val.V(() => new Vector3(participant2.transform.position.x, 1.8f, participant2.transform.position.z));
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_HeadLookTurnFirst(position), new LeafWait(500));
    }
    protected Node WalkTowards(GameObject participant, GameObject participant2)
    {
        return new Sequence(this.ST_LookStand(participant, participant2), this.ST_ApproachAndWait(participant, participant2.transform));
    }

    protected Node ST_OpenDoor(Transform target) {
        return null;
    }

    protected Node ST_Jump(GameObject participant) {
        Val<String> name = Val.V(() => "REACHRIGHT");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 1000));
    }
    protected Node ST_Jump2(GameObject participant)
    {
        
        Val<String> name = Val.V(() => "LOOKUP");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 1000));
    }

    protected Node StartEngine() {
        Node roaming = 
            new Sequence(
                this.WalkTowards(p,this.wander1),
                new DecoratorLoop(this.ST_Jump(p)
                ));
        return roaming;
    }
    protected Node StartWeapon()
    {
        Node roaming = new SequenceParallel(
            new Sequence(
                this.WalkTowards(participant4, this.wander6),
                new DecoratorLoop(this.ST_Jump(p)
                )),
                new Sequence(
                this.WalkTowards(participant5, this.wander7),
                new DecoratorLoop(this.ST_Jump(p)
                )));
        return roaming;
    }
    protected Node StartA()
    {
        Node roaming = new SequenceParallel(
            new DecoratorLoop(
                new Sequence(this.WalkTowards(participant2, this.wander2), this.WalkTowards(participant2, this.wander3))),
           new DecoratorLoop(
                new Sequence(this.WalkTowards(participant3, this.wander4), this.WalkTowards(participant3, this.wander5))));
        return roaming;
    }
}
