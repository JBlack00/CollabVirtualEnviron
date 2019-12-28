using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace NATTraversal
{
    public class VaribleHolder : NetworkBehaviour
    {
        public static VaribleHolder thisVaribleHolder;
        public GameObject InteractImage;
        public GameObject InteractImage2;
        public GameObject Projecter;
        public GameObject TextBox;

        public Camera MainCam;

        public GameObject[] WhiteBoards;

        public GameObject LoadingSceen;
        public GameObject InternetCam;
        public GameObject Modeling3DCam;
        public GameObject Modeling3DUI;
        public GameObject ModelingUI;
        public GameObject UIObjSelect;
        public GameObject UIObjSpawn;
        public GameObject GameObjSelectMenu3D;
        public GameObject Modeling3DObj;
        public GameObject MainCanvas;
        public GameObject ModelingUICam;
        public GameObject HUD;

        public GameObject Colour3D;
        public GameObject ColourUI;

        public Transform Modeling3DSpawn;
        public Transform ModelingUISpawn;

        public GameObject SelectableObject;

        public GameObject CUBE;
        public GameObject CONE;
        public GameObject SPHERE;
        public GameObject PYRIMID;
        public GameObject GEAR;
        public GameObject PIPE;
        public GameObject CYLINDER;
        public GameObject TORUS;
        public GameObject WALL;
        public GameObject PRISM;
        public GameObject WINDOW;
        public GameObject DOORWAY;
        public GameObject SQUARE;
        public GameObject TRIANGLE;
        public GameObject TEXT;
        public GameObject PLUS;
        public GameObject CIRCLE;
        public GameObject ARROW;

        public Transform[] Spawns;

        public MyBrowser browersScript;
        public static GameObject LocalPlayer;
        public GameObject VISLocalPlayer;
        [SyncVar]
        public GameObject PlayerControllingModeling;
        // Start is called before the first frame update
        void Awake()
        {
            thisVaribleHolder = this;
            if (InteractImage == null)
            {
                InteractImage = GameObject.Find("InteractUI");
            }
            if (InteractImage2 == null)
            {
                InteractImage2 = GameObject.Find("interactEandQ");
            }
            Projecter = GameObject.Find("ButtonPillarInteract");
            //OnlyRaycasetVersion
            //MainCam = GameObject.Find("Camera").GetComponent<Camera>();


            TextBox = GameObject.Find("TextBox");

            // InteractImage.SetActive(false);
            //InteractImage2.SetActive(false);

            WhiteBoards = GameObject.FindGameObjectsWithTag("WhiteBoard");

            //TextBox.transform.parent.gameObject.SetActive(false);

        }
        private void Start()
        {
            Invoke("DelayStart", 2f);

        }
        private void DelayStart()
        {
            Debug.Log("Called DelayStart");
            if (InteractImage == null)
            {
                InteractImage = GameObject.Find("InteractUI");
            }
            if (InteractImage2 == null)
            {
                InteractImage2 = GameObject.Find("interactEandQ");
            }
            Projecter = GameObject.Find("ButtonPillarInteract");


            TextBox = GameObject.Find("TextBoxInput");



            WhiteBoards = GameObject.FindGameObjectsWithTag("WhiteBoard");

            Modeling3DCam = GameObject.Find("ModelingCamera");

            ModelingUICam = GameObject.Find("ModelingUICamera");

            Modeling3DUI = GameObject.Find("ModelUI");

            //MainCam = GameObject.Find("Camera").GetComponent<Camera>();

            GameObjSelectMenu3D = GameObject.Find("GameObjSelect");

            ModelingUI = GameObject.Find("UIModlingCanvas");

            //   Modeling3DObj = GameObject.Find("Modeling");

            MainCanvas = GameObject.Find("MainScreenCanvas");

            HUD = GameObject.Find("UIModlingHud");

            Modeling3DSpawn = GameObject.Find("ObjectSpawn").transform;
            ModelingUISpawn = GameObject.Find("UIObjectSpawn").transform;

            Colour3D = GameObject.Find("RGB 3D Sliders").transform.GetChild(3).gameObject;
            ColourUI = GameObject.Find("RGB UI Sliders").transform.GetChild(3).gameObject;

            UIObjSelect = GameObject.Find("UIObjSelect");
            LocalPlayer = GameObject.Find("LocalPlayer");
            VISLocalPlayer = LocalPlayer;

            HUD.SetActive(false);
            ModelingUICam.SetActive(false);
            InternetCam.SetActive(false);
            // Modeling3DObj.SetActive(false);
           // ModelingUI.SetActive(false);
            GameObjSelectMenu3D.SetActive(false);
            Modeling3DCam.SetActive(false);
            Modeling3DUI.SetActive(false);
            UIObjSelect.SetActive(false);
            LoadingSceen.SetActive(false);

            TextBox.transform.parent.gameObject.SetActive(false);

            LocalPlayer.GetComponent<NATTraversal.PlayerInteractions>().Start();
            //  InteractImage.SetActive(false);
            // InteractImage2.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
