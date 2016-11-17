using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

using RootMotion.FinalIK;
public class MyBehaviorTree4 : MonoBehaviour
{
    
    //  public Animation a;
    public GameObject guyWhoDrinks;

    public GameObject drink;

    public GameObject table;
    public GameObject participant2;

    private BehaviorAgent behaviorAgent;
    private BehaviorAgent behaviorAgent2;


    private Vector3 originalPositionOfGuy;
    // Use this for initialization
    void Start()
    {
        originalPositionOfGuy = guyWhoDrinks.transform.position;

        //kick
        behaviorAgent = new BehaviorAgent(this.barDrinkGuySequence(guyWhoDrinks, "DRINK",500));
        BehaviorManager.Instance.Register(behaviorAgent);




    
        behaviorAgent.StartBehavior();


        /*
        //die
        behaviorAgent2 = new BehaviorAgent(this.BuildTreeRoot3(participant2, "STEPBACK", 4000));
        BehaviorManager.Instance.Register(behaviorAgent2);
        behaviorAgent2.StartBehavior();
        */

    }

    protected Node ST_ApproachAndWait(GameObject p,Transform target)
    {
        Vector3 targetP = new Vector3(target.position.x + 0.8f, p.transform.position.y, target.position.z );
        Val<Vector3> position = Val.V(() => targetP);
        return new Sequence(p.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("guy : " + guyWhoDrinks.transform.position+" , table: "+table.transform.position+ ",table t: " + table.GetComponent<Renderer>().bounds.size + " drink: " + drink.transform.position);
       
    }

    protected Node BodyShit(GameObject p,string s,long milSeconds)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(p.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, milSeconds));
    }
    protected Node BodyShit2(GameObject p, string s, long milSeconds)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(new LeafWait(400),p.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, milSeconds));
    }
    protected Node HandShit(GameObject p,string s, long milSeconds)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(p.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, milSeconds));
    }
    protected Node FaceShit(GameObject p,string s, long milSeconds)
    {
        Val<string> name = Val.V(() => s);
        return new Sequence(p.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, milSeconds));
    }
    protected Node Interaction(GameObject p,FullBodyBipedEffector effector, InteractionObject obj)
    {
        Val<FullBodyBipedEffector> n = Val.V(() => effector);

        Val<InteractionObject> n1 = Val.V(() => obj);


        return new Sequence(p.GetComponent<BehaviorMecanim>().Node_StartInteraction(n, n1));
    }
    protected Node BuildTreeRoot(GameObject p,String s, long milSeconds)
    {
       // Animator f = participant.GetComponent<Animator>();
        
       // FullBodyBipedEffector g= f.GetComponent<FullBodyBipedEffector>();

        Node roaming = new DecoratorLoop(
                       new SequenceShuffle(

                          HandShit(p,s, milSeconds)
                        ));

      //  this.Interaction(participant.GetComponent<RootMotion.FinalIK.FullBodyBipedEffector>(),
        //                participant2.GetComponent<RootMotion.FinalIK.InteractionObject>()
        return roaming;
    }
    protected Node BuildTreeRoot2(GameObject p, String s, long milSeconds)
    {
        Node roaming = new DecoratorLoop(
                    

                        BodyShit(p, s, milSeconds)
                      );

        //  this.Interaction(participant.GetComponent<RootMotion.FinalIK.FullBodyBipedEffector>(),
        //                participant2.GetComponent<RootMotion.FinalIK.InteractionObject>()
        return roaming;
    }
    protected Node BuildTreeRoot3(GameObject p, String s, long milSeconds)
    {
        Node roaming = new DecoratorLoop(
                    

                        BodyShit2(p, s, milSeconds)
                      );

        //  this.Interaction(participant.GetComponent<RootMotion.FinalIK.FullBodyBipedEffector>(),
        //                participant2.GetComponent<RootMotion.FinalIK.InteractionObject>()
        return roaming;
    }

    protected Node BuildTreeRoot4(GameObject p, String s, long milSeconds)
    {
        Node roaming = new DecoratorLoop(


                       new Sequence (ST_ApproachAndWait(p,drink.transform), FaceShit(p, s, milSeconds))
                      );

        //  this.Interaction(participant.GetComponent<RootMotion.FinalIK.FullBodyBipedEffector>(),
        //                participant2.GetComponent<RootMotion.FinalIK.InteractionObject>()
        return roaming;
    }
    protected Node barDrinkGuySequence(GameObject p, String s, long milSeconds)
    {

        

     //   Val <Vector3> position = Val.V(() => targetP);
       //Val<string> name = Val.V(() => s);

        Node roaming = new DecoratorLoop(


                       new Sequence(ST_ApproachAndWait(p, drink.transform), FaceShit(p, s, milSeconds))
                      );
        return roaming;
    }
}
