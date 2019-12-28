using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NATTraversal
{
    public class ModelUI : NetworkBehaviour
    {
        public Text SpeedText;
        public Text TransformText;
        public Text VectorsText;
        public Text SpriteScaleA;
        public Text SpriteScaleD;
        public Text SpriteScaleS;
        public Text SpriteScaleW;
        [SyncVar]
        public string SelectedObjNetName;


        public GameObject SelectedObj;

        public string PrevObjName;
        public GameObject PrevObj;

        public Modeling modelingScript;
        public ModelingUI modelingUIScript;

        public GameObject Change3DButtonUI;
        public GameObject New3DButtonUI;

        public GameObject ChangeButtonUI;
        public GameObject NewButtonUI;

        public GameObject ModelForUI;
        public GameObject ModelFor3D;

        public static int State;

        public int VISState;

        public bool HudOnOrOff;
        public PlayerInteractions InteractionScript;

        
        // Start is called before the first frame update
        void Start()
        {
              State = 0;
            modelingUIScript.enabled = false;
            //  ModelForUI = GameObject.Find("UIModlingCanvas");
            // modelingScript = GameObject.Find("Modeling").GetComponent<Modeling>();

        }

        // Update is called once per frame
        void Update()
        {
            if (!VaribleHolder.LocalPlayer.GetComponent<NetworkIdentity>().isLocalPlayer)
                return;

            VISState = State;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (State)
                {
                    case 1:

                        if (modelingScript.gameObject.activeSelf == false)
                        {
                            QuitUIBuilding();
                           
                        }
                        break;
                    case 2:
                        
                        if (ModelFor3D.activeSelf == false)
                        {
                            Quit3DBuilding();
                           
                        }
                        break;
                    default:

                        break;

                }

            }

            switch (State)
            {
                case 0:

                    break;
                case 1:
                    
                   
                    SpeedText.text = "Speed: " + ModelingUI.Speed;
                    TransformText.text = "Transform to change: " + ModelingUI.GetTransformString();
                    if (ModelingUI.GetTransformString() == "Scale" || ModelingUI.GetTransformString() == "Position")
                    {
                        SpriteScaleA.text = "x - speed";
                        SpriteScaleD.text = "x + speed";
                        SpriteScaleW.text = "y + speed";
                        SpriteScaleS.text = "y - speed";
                    }
                    else
                    {
                        SpriteScaleA.text = "z + speed";
                        SpriteScaleD.text = "z - speed";
                        SpriteScaleW.text = "x + speed";
                        SpriteScaleS.text = "x - speed";
                    }
                    if (modelingUIScript.CurrentObjectToControl == null)
                    {
                        
                        ChangeButtonUI.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        switch (ModelingUI.GetTransformString())
                        {
                            case "Position":
                                VectorsText.text = "Vectors(" + ModelingUI.GetTransformString() + "): x = " + modelingUIScript.CurrentObjectToControl.transform.position.x.ToString("F2") + " y = " + modelingUIScript.CurrentObjectToControl.transform.position.y.ToString("F2");

                                break;
                            case "Rotation":
                                VectorsText.text = "Vectors(" + ModelingUI.GetTransformString() + "): x = " + modelingUIScript.CurrentObjectToControl.transform.localRotation.eulerAngles.x.ToString("F2") + " y = " + modelingUIScript.CurrentObjectToControl.transform.localRotation.eulerAngles.y.ToString("F2") + " z = " + modelingUIScript.CurrentObjectToControl.transform.localRotation.eulerAngles.z.ToString("F2");

                                break;
                            case "Scale":
                                VectorsText.text = "Vectors(" + ModelingUI.GetTransformString() + "): x = " + modelingUIScript.CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x.ToString("F2") + " y = " + modelingUIScript.CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y.ToString("F2");

                                break;
                            default:

                                break;
                        }
                        ChangeButtonUI.GetComponent<Button>().interactable = true;
                    }

                    if (SelectedObj == null)
                    {
                        NewButtonUI.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        NewButtonUI.GetComponent<Button>().interactable = true;
                    }


                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;


                    break;
                case 2:
                    SpeedText.text = "Speed: " + Modeling.Speed;
                    TransformText.text = "Transform to change: " + Modeling.GetTransformString();






                    if (GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl == null)
                    {
                        Change3DButtonUI.GetComponent<Button>().interactable = false;

                    }
                    else
                    {
                        modelingScript.CurrentObjectToControl = GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl;
                        Change3DButtonUI.GetComponent<Button>().interactable = true;
                        switch (Modeling.GetTransformString())
                        {
                            case "Position":
                                VectorsText.text = "Vectors(" + Modeling.GetTransformString() + "): x = " + modelingScript.CurrentObjectToControl.transform.position.x.ToString("F2") + " y = " + modelingScript.CurrentObjectToControl.transform.position.y.ToString("F2") + " z = " + modelingScript.CurrentObjectToControl.transform.position.z.ToString("F2");

                                break;
                            case "Rotation":
                                VectorsText.text = "Vectors(" + Modeling.GetTransformString() + "): x = " + modelingScript.CurrentObjectToControl.transform.rotation.x.ToString("F2") + " y = " + modelingScript.CurrentObjectToControl.transform.rotation.y.ToString("F2") + " z = " + modelingScript.CurrentObjectToControl.transform.rotation.z.ToString("F2");

                                break;
                            case "Scale":
                                VectorsText.text = "Vectors(" + Modeling.GetTransformString() + "): x = " + modelingScript.CurrentObjectToControl.transform.localScale.x.ToString("F2") + " y = " + modelingScript.CurrentObjectToControl.transform.localScale.y.ToString("F2") + " z = " + modelingScript.CurrentObjectToControl.transform.localScale.z.ToString("F2");

                                break;
                            default:

                                break;
                        }
                    }

                    if (SelectedObj == null)
                    {
                        New3DButtonUI.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        New3DButtonUI.GetComponent<Button>().interactable = true;
                    }


                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                default:

                    break;
            }

            if (ModelForUI.activeInHierarchy == true )
            {
                InteractionScript.HUD.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("modelui is true");
            }
            else
            {
               
            }
            if (ModelFor3D.activeInHierarchy == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("ModelFor3D is true");
            }




        }

        public void ModelingUINew()
        {
           // GameObject.Find("LocalPlayer").GetComponent<ModelingUI>().CmdNewObj();
        }
        public void ModelingUIChange()
        {
           // GameObject.Find("LocalPlayer").GetComponent<ModelingUI>().CmdChangeObj();
        }
        public void  TurnOnHUb()
        {
            InteractionScript.HUD.SetActive(true);
        }
        public void TurnOffHUb()
        {
            InteractionScript.HUD.SetActive(false);
        }

        void QuitUIBuilding()
        {
            State = 0;
            InteractionScript.HUD.SetActive(false);
            InteractionScript.Modeling3DUI.SetActive(false);
            InteractionScript.ModelingUICam.SetActive(false);
            InteractionScript.MainCam.gameObject.SetActive(true);
            InteractionScript.gameObject.GetComponent<Controls>().CanMove = true;
            // InteractionScript.ModelingUI.SetActive(false);
            GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

      public  void QuitUsingInternet()
        {
            State = 0;
            InteractionScript.VaribleHolderScript.InternetCam.SetActive(false);
            InteractionScript.MainCam.gameObject.SetActive(true);
            InteractionScript.gameObject.GetComponent<Controls>().CanMove = true;
            // InteractionScript.ModelingUI.SetActive(false);           
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        void Quit3DBuilding()
        {
            InteractionScript.HUD.SetActive(value: false);
            InteractionScript.gameObject.GetComponent<Modeling>().Cam.SetActive(value: false);
            InteractionScript.gameObject.GetComponent<Modeling>().enabled = false;
            InteractionScript.Modeling3DCam.SetActive(value: false);
            InteractionScript.MainCam.gameObject.SetActive(value: true);
            InteractionScript.Modeling3DUI.SetActive(value: false);
            InteractionScript.GameObjSelectMenu3D.SetActive(value: false);
            InteractionScript.gameObject.GetComponent<Controls>().CanMove = true;
            State = 0;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        
        public void SelectThisObj(GameObject Obj)
        {
            SelectedObj = Obj;
            Debug.Log(" found : " + SelectedObj);
            GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSetSelectedObj(SelectedObj.name);
           // InteractionScript.gameObject.GetComponent<PlayerCommands>().CmdSetNewSelectableObject(SelectedObj);
        }
        public void ChangeToSelected(GameObject Obj)
        {
            if (Obj.GetComponent<Text>().text == "Selected")
            {

            }
            else
            {

                if (PrevObj != null)
                {
                    PrevObj.GetComponent<Text>().text = PrevObjName;
                    PrevObjName = Obj.GetComponent<Text>().text;
                }
                else
                {
                    PrevObjName = Obj.GetComponent<Text>().text;
                }


                Obj.GetComponent<Text>().text = "Selected";
                PrevObj = Obj;
            }

        }
    }
}