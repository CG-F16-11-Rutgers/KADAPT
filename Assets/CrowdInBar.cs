using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class CrowdInBar : MonoBehaviour {

    //Bartender
    public Transform point1;
    public Transform point2;
    public GameObject bartender;
    public GameObject table;

    //Two people talking
    public GameObject personTalk1;
    public GameObject personTalk2;

    //Sitting guy
    public GameObject sittingGuy1;
    public GameObject sittingGuy2;
    public GameObject sittingGuy3;
    public GameObject sittingGuy4;
    public GameObject sittingGuy5;
 
    //Dancers
    public GameObject dancer1;
    public GameObject dancer2;
    public GameObject dancer3;
    public GameObject danceTarget;

    //Merged Behavior
    private BehaviorAgent crowdBehavior;

    void Start () {
        //Initialize the merged behavior
        crowdBehavior = new BehaviorAgent(this.CrowdBehavior());
        BehaviorManager.Instance.Register(crowdBehavior);
        crowdBehavior.StartBehavior();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    ///General Functions
    //Used for participant to walk to a certain location before talking
    protected Node ST_ApproachAndWait(GameObject participant, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    //Make participant face a target
    protected Node ST_TurnToFace(GameObject participant, Vector3 target)
    {
        Val<Vector3> position = Val.V(() => target);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_TurnToFace(target));
    }
    //Hand Gestures
    protected Node HandGesture(GameObject participant, string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, 4000));
    }
    //Face Gestures
    protected Node FaceGesture(GameObject participant, string s)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, 4000));
    }
    //sit
    protected Node ST_Sit(GameObject participant)
    {
        return new Sequence(new LeafInvoke(() => participant.GetComponent<BehaviorMecanim>().Character.SitDown()));
    }

    /// <summary>
    /// This is where the person actions are defined
    /// </summary>
    /// <returns></returns>
    //Bartender Tree
    protected Node WalkAroundBar()
    {
        return new DecoratorLoop(new Sequence(
                                 ST_ApproachAndWait(bartender, point1),
                                 ST_TurnToFace(bartender, table.transform.position),
                                 HandGesture(bartender, "WRITING"),
                                 ST_ApproachAndWait(bartender, point2),
                                 ST_TurnToFace(bartender, table.transform.position),
                                 FaceGesture(bartender, "HEADSHAKE")
                                 ));
    }
    //Two people talking
    protected Node Talk()
    {
        return new DecoratorLoop(
                             new Sequence(
                                 ST_TurnToFace(personTalk1, personTalk2.transform.position),
                                 ST_TurnToFace(personTalk2, personTalk1.transform.position),

                                 new SequenceParallel(
                                 HandGesture(personTalk1,"BEINGCOCKY"),
                                 FaceGesture(personTalk2,"HEADSHAKE")
                                )));
    }
    //Sitting people
    protected Node Sit()
    {
        return new Sequence(
                        ST_Sit(sittingGuy1),
                        ST_Sit(sittingGuy2),
                        ST_Sit(sittingGuy3),
                        ST_Sit(sittingGuy4),
                        ST_Sit(sittingGuy5),
                        new DecoratorLoop( new Randomm(
                            FaceGesture(sittingGuy1, "DRINK"),
                            FaceGesture(sittingGuy2, "DRINK"),
                            FaceGesture(sittingGuy3, "DRINK"),
                            FaceGesture(sittingGuy4, "DRINK"),
                            FaceGesture(sittingGuy5, "DRINK"))));
    }
    //Dancers
    protected Node DanceForever()
    {
        return new DecoratorLoop(

            new Sequence(
                ST_TurnToFace(dancer1, danceTarget.transform.position),
                ST_TurnToFace(dancer2, danceTarget.transform.position),
                ST_TurnToFace(dancer3, danceTarget.transform.position),


                new SequenceParallel(
                    new Randomm(HandGesture(dancer1,"SATNIGHTFEVER"),
                        HandGesture(dancer1, "CLAP")),
                   new Randomm(HandGesture(dancer2, "SATNIGHTFEVER"),
                        HandGesture(dancer2, "CLAP")),
                   new Randomm(HandGesture(dancer3, "SATNIGHTFEVER"),
                        HandGesture(dancer3, "CLAP")))));
    }


    //Merge all trees into one
    protected Node CrowdBehavior()
    {
        return new DecoratorLoop(new SequenceParallel(
                            WalkAroundBar(),
                            Talk(),
                            Sit(),
                            DanceForever()));
    }
}
