using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace NATTraversal
{
    public class PlayerInteractions : NetworkBehaviour
    {

        private float WebPageLoadTimer = 1;
        public GameObject InteractImage;
        public GameObject InteractImage2;
        public GameObject Projecter;
        public GameObject TextBox;

        public Camera MainCam;
        [SyncVar]
        public bool UsingInternet;

        public VaribleHolder VaribleHolderScript;
        public float DistanceFromProjecter;

        public GameObject[] WhiteBoards;

        public string LastUrl;
        public int CurrentProjectionImage;

        [SyncVar]
        public string myUrl;
        public MyBrowser browersScript;
        public GameObject Modeling3DCam;
        public GameObject Modeling3DUI;
        public GameObject ModelingUI;
        public GameObject GameObjSelectMenu3D;
        public GameObject GameObjSelectMenuUI;
        public GameObject Modeling3DObj;
        public GameObject MainCanvas;
        public GameObject ModelingUICam;
        public GameObject HUD;


        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            gameObject.name = "LocalPlayer";
        }
        // Start is called before the first frame update
        private void Awake()
        {
            VaribleHolderScript = VaribleHolder.thisVaribleHolder;
            LastUrl = "www.google.com";
        }
        public void Start()
        {
            myUrl = "www.google.com";
            if (!isLocalPlayer)
            {
                VaribleHolderScript = GameObject.Find("VaribleHolder").GetComponent<VaribleHolder>();

                Modeling3DCam = VaribleHolderScript.Modeling3DCam;

                ModelingUICam = VaribleHolderScript.ModelingUICam;

                Modeling3DUI = VaribleHolderScript.Modeling3DUI;

                MainCam = GetComponent<Controls>().directionObject.GetComponent<Camera>();

                GameObjSelectMenu3D = VaribleHolderScript.GameObjSelectMenu3D;

                GameObjSelectMenuUI = VaribleHolderScript.UIObjSelect;

                ModelingUI = GetComponent<ModelingUI>().gameObject;

                Modeling3DObj = VaribleHolderScript.Modeling3DObj;

                MainCanvas = VaribleHolderScript.MainCanvas;

                HUD = VaribleHolderScript.HUD;

                browersScript = VaribleHolderScript.browersScript;

                InteractImage = VaribleHolderScript.InteractImage;
                InteractImage2 = VaribleHolderScript.InteractImage2;
                Projecter = VaribleHolderScript.Projecter;


                //OnlyRaycasetVersion
                // MainCam = VaribleHolderScript.MainCam;

                GameObject.Find("ShowOthers").GetComponent<Button>().onClick.AddListener(() => ShowOthers());
                TextBox = VaribleHolderScript.TextBox;


                WhiteBoards = VaribleHolderScript.WhiteBoards;

                for (int i =0; i < WhiteBoards.Length; i++)
                {
                 //   TextBox.GetComponent<TMPro.TMP_InputField>().onEndEdit.AddListener(delegate { CmdUpdateWhiteBoardText(TextBox.GetComponent<TMPro.TMP_InputField>().text, WhiteBoards[i]); });
                }
                this.enabled = false;
            }
            else
            {
                VaribleHolderScript = GameObject.Find("VaribleHolder").GetComponent<VaribleHolder>();

                Modeling3DCam = VaribleHolderScript.Modeling3DCam;

                ModelingUICam = VaribleHolderScript.ModelingUICam;

                Modeling3DUI = VaribleHolderScript.Modeling3DUI;

                MainCam = GetComponent<Controls>().directionObject.GetComponent<Camera>();

                GameObjSelectMenu3D = VaribleHolderScript.GameObjSelectMenu3D;

                GameObjSelectMenuUI = VaribleHolderScript.UIObjSelect;

                ModelingUI = VaribleHolderScript.ModelingUI;

                Modeling3DObj = VaribleHolderScript.Modeling3DObj;

                MainCanvas = VaribleHolderScript.MainCanvas;

                HUD = VaribleHolderScript.HUD;

                browersScript = VaribleHolderScript.browersScript;

                InteractImage = VaribleHolderScript.InteractImage;
                InteractImage2 = VaribleHolderScript.InteractImage2;
                Projecter = VaribleHolderScript.Projecter;

               // GameObject.Find("ShowOthers").GetComponent<Button>().onClick.AddListener(() => TestUrl());
                GameObject.Find("ShowOthers").GetComponent<Button>().onClick.AddListener(() => ShowOthers());
                //OnlyRaycasetVersion
                // MainCam = VaribleHolderScript.MainCam;


                TextBox = VaribleHolderScript.TextBox;


                WhiteBoards = VaribleHolderScript.WhiteBoards;

                for (int i = 0; i < WhiteBoards.Length; i++)
                {
                    //   TextBox.GetComponent<TMPro.TMP_InputField>().onEndEdit.AddListener(delegate { CmdUpdateWhiteBoardText(TextBox.GetComponent<TMPro.TMP_InputField>().text, WhiteBoards[i]); });
                }


            }
           // InteractImage.SetActive(false);
           // InteractImage2.SetActive(false);

        }
        
        void TestUrl()
        {
            LoadUrl("www.youtube.com");
        }
        // Update is called once per frame
        
        void Update()
        {
            DoBrowerCheck();
            if (isLocalPlayer)
            {
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else if (Input.GetKeyUp(KeyCode.LeftAlt))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                Projecter = GameObject.Find("ButtonPillarInteract");
                StandNearVersion();

            }else  if (isServer)
            {
                /*
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else if(Input.GetKeyUp(KeyCode.LeftAlt))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                */
                Projecter = GameObject.Find("ButtonPillarInteract");
               // StandNearVersion();
            }

            if(GetComponent<Controls>().CanMove == false)
            {
                InteractImage.SetActive(false);
                InteractImage2.SetActive(false);
            }
          
        }
        
       

        void StandNearVersion()
        {
            RaycastHit hit; 
       
            DistanceFromProjecter = 2000;
            

            float DistanceFromWhiteBoard = 2000;

            float DistanceFromInternet = 2000;

            float DistanceFrom3dModelActivator = 2000;

            float DistanceFromUIModelActivator = 2000;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), gameObject.transform.TransformDirection(Vector3.forward), out hit))
            {
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), gameObject.transform.TransformDirection(Vector3.forward), Color.green);
                if (hit.transform.tag == "WhiteBoard")
                {
                    DistanceFromWhiteBoard = Vector3.Distance(transform.position, hit.transform.position);

                }
                else if (hit.transform.tag == "Projecter")
                {
                    DistanceFromProjecter = Vector3.Distance(transform.position, hit.transform.position);
                }
                else if (hit.transform.name == "3DTestModlingActivater")
                {

                    DistanceFrom3dModelActivator = Vector3.Distance(transform.position, hit.transform.position);                    

                }
                else if (hit.transform.name == "desk")
                {

                    DistanceFromInternet = Vector3.Distance(transform.position, hit.transform.position);

                }
                else if (hit.transform.name == "UITestModlingActivater")
                {

                    DistanceFromUIModelActivator = Vector3.Distance(transform.position, hit.transform.position);

                }
                else
                {
                    Debug.Log("Tag is " + hit.transform.gameObject.tag);
                    Debug.Log("name is " + hit.transform.gameObject.name);
                    InteractImage.SetActive(false);
                    InteractImage2.SetActive(false);

                }

            }

            if(GetComponent<Controls>().CanMove == false)
            {
              //  DistanceFromProjecter = 2000;
                //DistanceFromWhiteBoard = 2000;
               // DistanceFromInternet = 2000;
               // DistanceFrom3dModelActivator = 2000;
               // DistanceFromUIModelActivator = 2000;
            }

            if (DistanceFromProjecter <= 3f)
            {
                InteractImage2.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //  Debug.Log("Q is pressed" + " CurrentImage int: " + (Projecter.GetComponent<ProjecterController>().CurrentImage - 1) + "NextImage: " + Projecter.GetComponent<ProjecterController>().CurrentImage);

                    // Projecter.GetComponent<ProjecterController>().CurrentImage --;

                    CurrentProjectionImage--;

                    if (CurrentProjectionImage < 0)
                    {
                        CurrentProjectionImage = Projecter.GetComponent<ProjecterController>().ProjecterSprites.Length - 1;
                    }

                    // Projecter.GetComponent<ProjecterController>().NextImage(CurrentProjectionImage);
                    //  Projecter.GetComponent<ProjecterController>().LastImage();
                    CmdSendImage(CurrentProjectionImage);
                    // Projecter.GetComponent<ProjecterController>().CmdSendImage(Projecter.GetComponent<ProjecterController>().CurrentImage - 1);
                    // CmdStandNearVersion(Projecter.GetComponent<ProjecterController>().CurrentImage - 1);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {

                    // Projecter.GetComponent<ProjecterController>().CurrentImage ++;

                    // Projecter.GetComponent<ProjecterController>().NextImage();


                    CurrentProjectionImage++;
                    if (CurrentProjectionImage > Projecter.GetComponent<ProjecterController>().ProjecterSprites.Length - 1)
                    {
                        CurrentProjectionImage = 0;

                    }
                    CmdSendImage(CurrentProjectionImage);
                   

                        // Projecter.GetComponent<ProjecterController>().CmdSendImage(Projecter.GetComponent<ProjecterController>().CurrentImage + 1);
                        //  CmdStandNearVersion(Projecter.GetComponent<ProjecterController>().CurrentImage + 1);
                    }
               
            }
            else
            {
                if(InteractImage2 == null)
                {
                    Debug.Log("InteractImage2 is null");
                }
                else
                {
                    InteractImage2.SetActive(false);
                }
               
            }

            if (GameObject.Find("TestWhiteBoard") ==null)
            {
             //   Debug.Log("TestWhiteBoard is Null boi");
            }
    
            if (DistanceFromWhiteBoard <= 3f)
            {
                InteractImage.SetActive(true);             
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    // Projecter.GetComponent<ProjecterController>().CurrentImage ++;

                    // Projecter.GetComponent<ProjecterController>().NextImage();
                    TextBox.GetComponent<TMPro.TMP_InputField>().onEndEdit.AddListener(delegate { CmdUpdateWhiteBoardText(TextBox.GetComponent<TMPro.TMP_InputField>().text, hit.transform.gameObject); });

                    TextBox.transform.parent.gameObject.SetActive(true);
                    GetComponent<Controls>().CanMove = false;
                   

                    // Projecter.GetComponent<ProjecterController>().CmdSendImage(Projecter.GetComponent<ProjecterController>().CurrentImage + 1);
                    //  CmdStandNearVersion(Projecter.GetComponent<ProjecterController>().CurrentImage + 1);
                }
                if (GetComponent<Controls>().CanMove == false)
                {
                    Debug.Log("can move");
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Debug.Log("Return hit");
                        TextBox.transform.parent.gameObject.SetActive(false);
                        GetComponent<Controls>().CanMove = true;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        TextBox.GetComponent<TMPro.TMP_InputField>().onEndEdit.RemoveAllListeners();
                    }
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Debug.Log("Escape hit");
                        TextBox.transform.parent.gameObject.SetActive(false);
                        GetComponent<Controls>().CanMove = true;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        TextBox.GetComponent<TMPro.TMP_InputField>().onEndEdit.RemoveAllListeners();
                    }
                }
                
            }
            else if (DistanceFromUIModelActivator <= 3f)
            {
                InteractImage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HUD.SetActive(true);
                    ModelingUICam.SetActive(true);
                    Modeling3DUI.SetActive(true);
                    if (MainCam.gameObject.activeSelf == true)
                    {
                        GameObjSelectMenuUI.SetActive(true);
                    }
                    MainCam.gameObject.SetActive(false);
                   // ModelingUI.SetActive(true);
                    
                  
                    ModelUI.State = 1;
                    
                    MainCanvas.GetComponent<ModelUI>().InteractionScript = this;
                    GetComponent<Controls>().CanMove = false;
                    CmdUpdatePlayerCurModeling();
                    GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().enabled = true;
                    GetComponent<PlayerCommands>().CmdUpdatePersonControlUIObj(this.gameObject);
                   
                }
            }else if (DistanceFrom3dModelActivator <= 3f)
            {
                InteractImage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HUD.SetActive(true);
                    Modeling3DCam.SetActive(true);
                    GetComponent<Modeling>().enabled = true;
                    // Modeling3DObj.SetActive(true);
                    if (MainCam.gameObject.activeSelf == true)
                    {
                        GameObjSelectMenu3D.SetActive(true);
                    }
                    MainCam.gameObject.SetActive(false);
                    Modeling3DUI.SetActive(true);
                   
                   
                    ModelUI.State = 2;
                    MainCanvas.GetComponent<ModelUI>().InteractionScript = this;
                    GetComponent<Controls>().CanMove = false;
                    CmdUpdatePlayerCurModeling();


                }
            }
            else if (DistanceFromInternet <= 3f)
            {
                InteractImage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    // Projecter.GetComponent<ProjecterController>().CurrentImage ++;
                    CmdUpdateInternet(true);
                    // Projecter.GetComponent<ProjecterController>().NextImage();
                    VaribleHolderScript.InternetCam.SetActive(true);
                    GetComponent<Controls>().CanMove = false;

                    MainCanvas.GetComponent<ModelUI>().InteractionScript = this;
                    // Projecter.GetComponent<ProjecterController>().CmdSendImage(Projecter.GetComponent<ProjecterController>().CurrentImage + 1);
                    //  CmdStandNearVersion(Projecter.GetComponent<ProjecterController>().CurrentImage + 1);
                }
                if (GetComponent<Controls>().CanMove == false)
                {
                 
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        VaribleHolderScript.InternetCam.SetActive(false);
                        GetComponent<Controls>().CanMove = true;
                        GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().QuitUsingInternet();
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                       
                    }
                }

            }
            else
            {
                InteractImage.SetActive(false);

            }
        }
        void DoBrowerCheck()
        {
               
        
        }

        public void ShowOthers()
        {
         
            browersScript = GameObject.Find("desk").GetComponent<MyBrowser>();
            CmdUpdateURL(browersScript.bScript.Url);
        }
        [Command]
        void CmdUpdateInternet(bool isUsing)
        {
            RpcUpdateInternet(isUsing);
        }

        [ClientRpc]
        void RpcUpdateInternet(bool isUsing)
        {
            UsingInternet = isUsing;
        }
        [Command]
        void CmdUpdateURL(string URL)
        {
            RpcUpdateURL(URL);

        }
        [ClientRpc]
        void RpcUpdateURL(string URL)
        {
            myUrl = URL;
            // Debug.Log("Doing a browser with url:" + browersScript.url);
            WebPageLoadTimer = 1;
            LastUrl = myUrl;

            LoadUrl(myUrl);
        }

        void LoadUrl(string URL)
        {
            browersScript = VaribleHolderScript.browersScript;
            Debug.Log("LoadingURL");
            WebPageLoadTimer++;
            if (WebPageLoadTimer == 2)
            {
                if (browersScript.bScript._url != URL)
                {
                    browersScript.bScript.LoadURL(URL, true);
                }
            
                    WebPageLoadTimer = 0;
                
            }
           
            
        }

        [Command]
        public void CmdUpdatePlayerCurModeling()
        {
            RpcUpdatePlayerCurModeling();
        }
        [ClientRpc]
        public void RpcUpdatePlayerCurModeling()
        {
            VaribleHolderScript.PlayerControllingModeling = this.gameObject;
        }


        [Command]
        public void CmdUpdateWhiteBoardText(string value, GameObject Obj)
        {
            RpcClientUpdateImageWhiteBoardText(value,  Obj);
        }

        [ClientRpc]
        public void RpcClientUpdateImageWhiteBoardText(string value, GameObject Obj)
        {
            Obj.GetComponent<WhiteBoard>().WhiteBoardText.text = value;
        }

        [Command]
        public void CmdSendImage(int Value)
        {
              
            Debug.Log("CMD CALLESD");
            RpcClientUpdateImage(Value);
        }
        [ClientRpc]
        public void RpcClientUpdateImage(int value)
        {

            Projecter.GetComponent<ProjecterController>().CurrentImage = value;
            Debug.Log("ClientRpc CALLESD");
            //Projecter.GetComponent<ProjecterController>().ProjecterScreen.GetComponent<Image>().sprite = Projecter.GetComponent<ProjecterController>().ProjecterSprites[Projecter.GetComponent<ProjecterController>().CurrentImage];
            
        }

      //  public void CmdSendImage(int Value)
       // {
      //      Projecter.GetComponent<ProjecterController>().changeImage(CurrentProjectionImage);
//
       //     Debug.Log("Sent new value To Client " + Value);
      //      //  Projecter.GetComponent<ProjecterController>().CurrentImage = Value;

   //     }


       
        void RayCastVersion()
        {
            RaycastHit hit;
            Camera CameraOBJ = MainCam;

            if (Physics.Raycast(CameraOBJ.gameObject.transform.position, CameraOBJ.gameObject.transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.transform.tag == "Button1")
                {
                    InteractImage.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                   
                    }

                }
                else if (hit.transform.tag == "Button2")
                {
                    InteractImage.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                      
                    }

                }
              
                else
                {
                    InteractImage.SetActive(false);
                }
            }
        }
        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            base.OnDeserialize(reader, initialState);
        }

    }
}
