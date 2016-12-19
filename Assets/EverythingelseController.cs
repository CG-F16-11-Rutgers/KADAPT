using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EverythingelseController : MonoBehaviour
{

    public float speed;
    public bool start = false;
    private float timeLeft;
    private Rigidbody rb;
    public static int count = 0;

    public static void minusCount()
    {
        count--;
    }
    public void turnOn()
    {
        start = true;
    }
    void Start()
    {
        //start = false;
        rb = GetComponent<Rigidbody>();
        //count = 0;
        //timeLeft = 120.0f;
        //SetCountText();
        //winText.text = "";
    }
    void Update()
    {
        /*SetCountText();
        timeLeft -= Time.deltaTime;
        if (timeLeft >= 0)
        {
            time.text = "Time Left: " + ((int)timeLeft).ToString() + "s";
        }

        if (timeLeft < 0)
        {
            winText.text = "Time up!";
        }
        if (timeLeft == 30)
        {
            winText.text = "30 Seconds Left!";
        }
        if (timeLeft < -5)
        {
            Application.LoadLevel("Startpage");
        }*/
    }
    void FixedUpdate()
    {
        

        if(start)
        {
            if (Input.GetKey(KeyCode.I))
            {

                rb.MovePosition(transform.position - transform.forward * Time.deltaTime * speed);

            }
            if (Input.GetKey(KeyCode.K))
            {

                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);

            }
            if (Input.GetKey(KeyCode.J))
            {

                rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed);

            }
            if (Input.GetKey(KeyCode.L))
            {

                rb.MovePosition(transform.position - transform.right * Time.deltaTime * speed);

            }
        }
        


        //rb.AddForce(movement * speed);
        
        /*if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {

            rb.AddForce(0, 300, 0);

        }*/
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

    }*/

    /*void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 2 && collision.gameObject.CompareTag("Wall"))
        {
            count = count - 1;
            SetCountText();
            winText.text = "Player 1 Hits the Wall -1";
        }
        if (collision.relativeVelocity.magnitude > 2 && collision.gameObject.CompareTag("Ball") && transform.position.y > (collision.transform.position.y + 1))
        {
            count = count + 1;
            Player2Controller.minusCount();
            SetCountText();
            winText.text = "Player 1 Wins Collision!";
        }
    }*/


    /*void SetCountText()
    {
        //Player2Controller p1 = new Player2Controller();

        countText.text = "Player 1 Count: " + count.ToString();
        if (count >= 6 && count > Player2Controller.count)
        {
            winText.text = "Player 1 Wins!";
            Player2Controller.count = 0;
        }
    }*/
}