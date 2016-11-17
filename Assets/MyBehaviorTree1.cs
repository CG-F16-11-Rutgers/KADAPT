using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree1 : MonoBehaviour
{
	
	public GameObject green1;
    public GameObject green2;
    public GameObject green3;
    public GameObject red1;
    public GameObject red2;
    public GameObject red3;

    public Transform doorPosition;

    public Transform gT1;
    public Transform gT2;
    public Transform gT3;
    public Transform rT1;
    public Transform rT2;
    public Transform rT3;

    private BehaviorAgent behaviorAgent;
	// Use this for initialization
	void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
    /*
	protected Node ST_ApproachAndWait(Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}
    */
    private Node goToTarget(GameObject guy ,Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(guy.GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }

    protected Node BuildTreeRoot()
	{
        System.Random rnd = new System.Random();

        int winner = rnd.Next(0, 2);

        GameObject w1, w2, w3;
        //red wins
        if(winner==0)
        {
            w1 = red1;
            w2 = red2;
            w3 = red3;
        }
        //Green wins
        else
        {
            w1 = green1;
            w2 = green2;
            w3 = green3;
        }

        Node roaming = new DecoratorLoop(
                 new Sequence(new SequenceParallel(
                            goToTarget(red1, rT1),
                            goToTarget(red2, rT2),
                            goToTarget(red3, rT3),
                            goToTarget(green1, gT1),
                            goToTarget(green2, gT2),
                            goToTarget(green3, gT3)
                        ), new SequenceParallel(
                        faceOpponent(red1, green1),
                        faceOpponent(red2, green2),
                        faceOpponent(red3, green3),

                        faceOpponent(green1, red1),
                        faceOpponent(green2, red2),
                        faceOpponent(green3, red3)
                     ), new SequenceParallel(
                            createRandomFightInteraction(red1, green1, winner),

                        createRandomFightInteraction(red2, green2, winner),
                        createRandomFightInteraction(red3, green3, winner)
                        ),
                        // the winners go to the door
                        new SequenceParallel(
                            goToTarget(w1, doorPosition),
                            goToTarget(w2, doorPosition),
                            goToTarget(w3, doorPosition)
                            )
                        ,
                        //makes everyone stay IDLE
                        new Sequence(new Sequence(faceOpponent(w1,w2)))));
        
		return roaming;
	}
   
    private Node createRandomFightInteraction(GameObject redGuy, GameObject greenGuy, int winner)
    {
        System.Random rnd = new System.Random();
        int maximumPunches = 6;

        int fightLength = rnd.Next(0, maximumPunches + 1); // creates a number between 0 and maximumPunches

        Node[] redParas = new Node[fightLength+1];
        Node[] greenParas = new Node[fightLength+1];
        
        for (int a = 0; a < fightLength; a++)
        {
            int moveType = rnd.Next(0, 2);

            //red guy punches, green guy gets hit
            if (moveType == 0)
            {
                redParas[a] = punch(redGuy);
                greenParas[a]= getsHit(greenGuy);
            }
            //green guy punches, red guy gets hit
            else if (moveType == 1)
            {
                redParas[a] = getsHit(redGuy);
                greenParas[a] = punch(greenGuy);
            }
        }

        //red guys wins the fight
        if(winner==0)
        {
            redParas[fightLength] = performFinishingMove(redGuy);
            greenParas[fightLength] = die(greenGuy);
        }
        else
        {
            redParas[fightLength] = die(redGuy);
            greenParas[fightLength] = performFinishingMove(greenGuy);
        }

        return new SequenceParallel(new Sequence(redParas), new Sequence(greenParas));
    }
    
    private Node performFinishingMove(GameObject guy)
    {
        Val<string> name = Val.V(() => "PICKUPLEFT");
        return new Sequence(guy.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, 3000));
    }
    private Node die(GameObject guy)
    {
        Val<string> name = Val.V(() => "DYING");
        return new Sequence(guy.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture(name, 3000));
    }

    private Node getsHit(GameObject guy)
    {
        Val<string> name = Val.V(() => "SPEW");
        long punchTime = 2000;
        long waitTime = 300;
        return new Sequence(new LeafWait(waitTime),guy.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture(name, punchTime-waitTime));
    }
    private Node faceOpponent(GameObject guy, GameObject opponent)
    {
        Val<Vector3> position = Val.V(() => opponent.transform.position);
        return (guy.GetComponent<BehaviorMecanim>().ST_TurnToFace(position));

    }
    private Node punch(GameObject guy)
    {
        long punchTime = 2000;
        Val<string> name = Val.V(() => "BASH");
        return new Sequence(guy.GetComponent<BehaviorMecanim>().ST_PlayHandGesture(name, punchTime));
    }
}
