using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Director : MonoBehaviour {

    public GameObject playerShip;
    public GameObject enemyShip;
    public GameObject everythingelse;
    public EverythingelseController elc;
    private ShipController playerShipController;
    public GameObject cam;
    private CameraController cameraController;
    public GameObject updater;
    private MyBehaviorTree3 update;
    public Text gameText;
    public GameObject selected;
    public GameObject missile;
    public GameObject missile2;
    public MissileControler mc;
    public MissileControler mc2;

    // Use this for initialization
    void Start () {
        playerShipController = playerShip.GetComponent<ShipController>();
        cameraController = cam.GetComponent<CameraController>();
        //enemyShip.SetActive(false);
        update = updater.GetComponent<MyBehaviorTree3>();

        
        print(update.engine);
        elc = everythingelse.GetComponent<EverythingelseController>();
        mc = missile.GetComponent<MissileControler>();
        mc2 = missile2.GetComponent<MissileControler>();

        update.startI();
    }
    void FixedUpdate()
    {


        
    }
    // Update is called once per frame
    void Update () {
        print(update.engine);
        if (update.engine == true)
        {
            elc.turnOn();
            
        }
        print(update.weapon);
        if (update.weapon)
        {

            if (Input.GetKey(KeyCode.Space))
            {

                mc.reset();
                mc2.reset();

            }



        }



        cameraController.move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Input.GetAxis("Mouse ScrollWheel"), Input.GetKey(KeyCode.LeftShift));
        if(Input.GetKeyDown(KeyCode.Mouse2)) {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKey(KeyCode.Mouse2)) {
            cameraController.rotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        if(Input.GetKeyUp(KeyCode.Mouse2)) {
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            RaycastHit hitInfo;
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo)) {
                if(hitInfo.transform.gameObject.CompareTag("Player")) {
                    selected = hitInfo.transform.gameObject;
                    
                }
                else {
                    selected = null;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            if(selected != null) {
                RaycastHit hitInfo;
                Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo)) {
                    if (hitInfo.transform.gameObject.CompareTag("Ship System")) {
                        /*LI CALL YOUR CODE FROM HERE WITH AGENT REFERENCED BY GAMEOBJECT SELECTED AND OBJECT RETURNED FROM RAYCAST*/
                    }
                }
            }
        }
        updateProgress();

	}

    void updateProgress() {
        if(playerShipController.sys_Helm.GetComponent<SystemController>().interacting) {
            repairShip();
        }
        if(playerShipController.sys_Engines.GetComponent<SystemController>().online) {
            spawnEnemyShip();
        }
    }

    void repairShip() {
        gameText.text = "The ship can't move until the engines are repaired. Select another crewman and order them to repair the engines (that's the block in the large room at the back of the ship).";
    }

    void spawnEnemyShip() {
        enemyShip.SetActive(true);
        gameText.text = "Hostile ship detected! Order a third crewman to man the weapons systems on the port-side module (the one without the window). Then acquire the target and fire until it's destroyed.";
    }

}
