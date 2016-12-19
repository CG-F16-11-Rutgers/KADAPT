using UnityEngine;
using System.Collections;

public class MissileControler : MonoBehaviour {

    // Use this for initialization
    public float speed;
    public Detonator d;
    public GameObject m, s;
    public Transform st;
    void Start()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(speed * transform.forward * Time.deltaTime);
        
            

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            other.gameObject.SetActive(false);
            d.Explode();
            m.gameObject.SetActive(false);
            s.gameObject.SetActive(false);





        }

    }
    public void reset()
    {
        
        if(!gameObject.activeSelf || !m.activeSelf || (gameObject.transform.position - st.transform.position).magnitude>50)
        {
            gameObject.SetActive(true);
            m.gameObject.SetActive(true);
            s.gameObject.SetActive(true);
            gameObject.transform.position = st.transform.position;
        }
        
    }
}
