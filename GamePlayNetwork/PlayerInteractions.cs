using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace NATTraversal
{
    public class PlayerInteractions : NetworkBehaviour
    {
        public GameObject InteractImage;
        public GameObject InteractImage2;
        public GameObject Projecter;
        public GameObject TextBox;

        public Camera MainCam;

        public VaribleHolder VaribleHolderScript;
        public float DistanceFromProjecter;
       
        [SyncVar] 
        public int CurrentProjectionImage;


        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            gameObject.name = "LocalPlayer";
        }
        // Start is called before the first frame update
        private void Awake()
        {
            VaribleHolderScript = GameObject.Find("VaribleHolder").GetComponent<VaribleHolder>();
        }
        void Start()
        {
           
            if (!isLocalPlayer)
            {
                
                VaribleHolderScript = GameObject.Find("VaribleHolder").GetComponent<VaribleHolder>();
                InteractImage = VaribleHolderScript.InteractImage;
                InteractImage2 = VaribleHolderScript.InteractImage2;
                Projecter = VaribleHolderScript.Projecter;

                //OnlyRaycasetVersion
                //MainCam = VaribleHolderScript.MainCam;


                TextBox = VaribleHolderScript.TextBox;
                TextBox.GetComponent<TMPro.TMP_InputField>().onValueChanged.AddListener(delegate { CmdUpdateWhiteBoardText(TextBox.GetComponent<TMPro.TMP_InputField>().text); });



               
            }
            else
            {
               
                VaribleHolderScript = GameObject.Find("VaribleHolder").GetComponent<VaribleHolder>();
                InteractImage = VaribleHolderScript.InteractImage;
                InteractImage2 = VaribleHolderScript.InteractImage2;
                Projecter = VaribleHolderScript.Projecter;

                //OnlyRaycasetVersion
                // MainCam = VaribleHolderScript.MainCam;


                TextBox = VaribleHolderScript.TextBox;
                TextBox.GetComponent<TMPro.TMP_InputField>().onValueChanged.AddListener(delegate { CmdUpdateWhiteBoardText(TextBox.GetComponent<TMPro.TMP_InputField>().text); });
               

            }
           // InteractImage.SetActive(false);
           // InteractImage2.SetActive(false);

        }
        
        // Update is called once per frame
        
        void Update()
        {

            if (!isLocalPlayer)
            {
              
            }
            else 
            {
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                Projecter = GameObject.Find("ButtonPillarInteract");
                StandNearVersion();

            }

            if (isServer)
            {
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                Projecter = GameObject.Find("ButtonPillarInteract");
                StandNearVersion();
            }
          
        }
        
       

        void StandNearVersion()
        {
            if (Projecter == null)
            {
                Debug.Log("Projecter null again");
            }
            else
            {
                DistanceFromProjecter = Vector3.Distance(transform.position, Projecter.transform.position);
            }
           

           

            if (DistanceFromProjecter <= 1.5f)
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
                Debug.Log("TestWhiteBoard is Null boi");
            }


            float DistanceFromWhiteBoard = Vector3.Distance(transform.position, GameObject.Find("TestWhiteBoard").transform.position);
            
            if (DistanceFromWhiteBoard <= 1.5f)
            {
                InteractImage.SetActive(true);             
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    // Projecter.GetComponent<ProjecterController>().CurrentImage ++;

                    // Projecter.GetComponent<ProjecterController>().NextImage();

                    TextBox.transform.parent.gameObject.SetActive(true);
                    GetComponent<Controls>().CanMove = false;
                   

                    // Projecter.GetComponent<ProjecterController>().CmdSendImage(Projecter.GetComponent<ProjecterController>().CurrentImage + 1);
                    //  CmdStandNearVersion(Projecter.GetComponent<ProjecterController>().CurrentImage + 1);
                }
                if (GetComponent<Controls>().CanMove == false)
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        TextBox.transform.parent.gameObject.SetActive(false);
                        GetComponent<Controls>().CanMove = true;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        TextBox.transform.parent.gameObject.SetActive(false);
                        GetComponent<Controls>().CanMove = true;
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
        [Command]
        public void CmdUpdateWhiteBoardText(string value)
        {
            RpcClientUpdateImageWhiteBoardText(value);
        }

        [ClientRpc]
        public void RpcClientUpdateImageWhiteBoardText(string value)
        {
            GameObject.Find("TestWhiteBoard").GetComponent<WhiteBoard>().WhiteBoardText.text = value;
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
            if (value > Projecter.GetComponent<ProjecterController>().ProjecterSprites.Length - 1)
            {
                value = 0;

            }
            if (value < 0)
            {
                value = Projecter.GetComponent<ProjecterController>().ProjecterSprites.Length - 1;
            }
            CurrentProjectionImage = value;
            Debug.Log("ClientRpc CALLESD");
            Projecter.GetComponent<ProjecterController>().ProjecterScreen.GetComponent<Image>().sprite = Projecter.GetComponent<ProjecterController>().ProjecterSprites[CurrentProjectionImage];
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
