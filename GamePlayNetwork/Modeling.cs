using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace NATTraversal
{
    public class Modeling : NetworkBehaviour
    {
       [SyncVar]
        public GameObject CurrentObjectToControl;
        public static float Speed = 1;
        public int TrasfromState;
        public static string TransformStateString;

        public static Vector3 TransformPosition;
        public static Vector3 TransformRotation;
        public static Vector3 TransformScale;

        public GameObject ObjSelectUI;

        public Transform Spawn;

        public GameObject Cam;

        public Color ThisColour;
       
        public ModelUI modelUIScript;

        public VaribleHolder varibleHolder;
        
        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer)
            {
                this.enabled = false;
            }
            else
            {
                if (isServer)
                {
                    varibleHolder = VaribleHolder.thisVaribleHolder;
                    Cam = GameObject.Find("ModelingCamera");
                    this.enabled = false;
                }
                else
                {
                    varibleHolder = VaribleHolder.thisVaribleHolder;
                    Cam = GameObject.Find("ModelingCamera");
                    this.enabled = false;
                }
            }
            

        }

        public static string GetTransformString()
        {
            return TransformStateString;
        }
        [Command]
        public void CmdNewObj()
        {
            // GameObject NewObj = Instantiate(modelUIScript.SelectedObj, Spawn.transform.position, Quaternion.identity);
            switch (GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName)
            {
                case "Cube":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    
                    // Debug.Log("SelectableObject = "+ varibleHolder.SelectableObject);
                    //CurrentObjectToControl = varibleHolder.SelectableObject;
                    //SetNewObj();

                    break;
                case "Cone":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Cylinder":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Gear":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Pipe":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Sphere":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Torus":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Wall":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Window":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Prism":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Pyramid":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
                case "Doorway":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNew3DObj();
                    break;
            }
        

      

            //
           // modelUIScript.InteractionScript.gameObject.GetComponent<PlayerCommands>().CmdSpawnNew3DObj(modelUIScript.InteractionScript.gameObject.GetComponent<PlayerCommands>().SelectableObject);
            /*
            NewObj.GetComponent<Renderer>().material.SetColor("_Color", ColourUISelect.ColorOfUI);
            GameObject.Find("Modeling").GetComponent<Modeling>().SetNewObj(NewObj);
            CurrentObjectToControl = NewObj;
            modelUIScript.InteractionScript.gameObject.GetComponent<PlayerCommands>().CmdSpawnNew3DObj(NewObj);
            Cam.GetComponent<Cam3rdOffline>().lookAt = CurrentObjectToControl.transform;
            */
        }

        public void SetNewObj()
        {
            if (CurrentObjectToControl == null)
            {
                Debug.Log("CurObj is null");
            }
            else
            {
                CurrentObjectToControl.GetComponent<Renderer>().material.SetColor("_Color", varibleHolder.PlayerControllingModeling.GetComponent<Modeling>().ThisColour);
                Cam.GetComponent<Cam3rdOffline>().lookAt = CurrentObjectToControl.transform;
            }
           
        }

        [Command]
        public void CmdChangeObj()
        {
            switch (GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName)
            {
                case "Cube":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Cone":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Cylinder":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Gear":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Pipe":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Sphere":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Torus":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Wall":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Window":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Prism":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Pyramid":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
                case "Doorway":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChange3DObj();
                    break;
            }

        }
        [ClientRpc]
        public void RpcChangeObj(GameObject Obj, Color colour)
        {
            Obj.GetComponent<Renderer>().material.SetColor("_Color", colour);
        }
        private void FixedUpdate()
        {
            if (!VaribleHolder.LocalPlayer.GetComponent<NetworkIdentity>().isLocalPlayer)
                return;
            if (Input.GetKey(KeyCode.A))
            {
                switch (TrasfromState)
                {   //Posistion
                    case 0:
                        CurrentObjectToControl.GetComponent<Rigidbody>().AddForce(Vector3.left * Speed * Time.deltaTime, ForceMode.VelocityChange);
                        // CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x - Speed * Time.deltaTime, CurrentObjectToControl.transform.position.y, CurrentObjectToControl.transform.position.z);
                        break;
                    //rotation
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x - Speed * Time.deltaTime, CurrentObjectToControl.transform.eulerAngles.y, CurrentObjectToControl.transform.eulerAngles.z);

                        break;
                    //scale
                    case 2:
                        CurrentObjectToControl.transform.localScale = new Vector3(CurrentObjectToControl.transform.localScale.x - Speed * Time.deltaTime, CurrentObjectToControl.transform.localScale.y, CurrentObjectToControl.transform.localScale.z);
                        break;
                    default:

                        break;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                switch (TrasfromState)
                {   //Posistion
                    case 0:
                        CurrentObjectToControl.GetComponent<Rigidbody>().AddForce(Vector3.left * -Speed * Time.deltaTime, ForceMode.VelocityChange);
                        //    CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x + Speed * Time.deltaTime, CurrentObjectToControl.transform.position.y, CurrentObjectToControl.transform.position.z);
                        break;
                    //rotation
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x + Speed * Time.deltaTime, CurrentObjectToControl.transform.eulerAngles.y, CurrentObjectToControl.transform.eulerAngles.z);
                        break;
                    //scale
                    case 2:
                        CurrentObjectToControl.transform.localScale = new Vector3(CurrentObjectToControl.transform.localScale.x + Speed * Time.deltaTime, CurrentObjectToControl.transform.localScale.y, CurrentObjectToControl.transform.localScale.z);
                        break;
                    default:

                        break;
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                switch (TrasfromState)
                {   //Posistion
                    case 0:
                        CurrentObjectToControl.GetComponent<Rigidbody>().AddForce(Vector3.forward * Speed * Time.deltaTime, ForceMode.VelocityChange);
                        //CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x , CurrentObjectToControl.transform.position.y, CurrentObjectToControl.transform.position.z+ Speed * Time.deltaTime);
                        break;
                    //rotation
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x, CurrentObjectToControl.transform.eulerAngles.y, CurrentObjectToControl.transform.eulerAngles.z + Speed * Time.deltaTime);
                        break;
                    //scale
                    case 2:
                        CurrentObjectToControl.transform.localScale = new Vector3(CurrentObjectToControl.transform.localScale.x, CurrentObjectToControl.transform.localScale.y, CurrentObjectToControl.transform.localScale.z + Speed * Time.deltaTime);
                        break;
                    default:

                        break;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                switch (TrasfromState)
                {   //Posistion
                    case 0:
                        //  CurrentObjectToControl.transform.Translate(Vector3.forward * -Speed * Time.fixedDeltaTime);
                        CurrentObjectToControl.GetComponent<Rigidbody>().AddForce(Vector3.forward * -Speed * Time.deltaTime, ForceMode.VelocityChange);
                        // CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x , CurrentObjectToControl.transform.position.y, CurrentObjectToControl.transform.position.z - Speed * Time.deltaTime);
                        break;
                    //rotation
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x, CurrentObjectToControl.transform.eulerAngles.y, CurrentObjectToControl.transform.eulerAngles.z - Speed * Time.deltaTime);
                        break;
                    //scale
                    case 2:
                        CurrentObjectToControl.transform.localScale = new Vector3(CurrentObjectToControl.transform.localScale.x, CurrentObjectToControl.transform.localScale.y, CurrentObjectToControl.transform.localScale.z - Speed * Time.deltaTime);
                        break;
                    default:

                        break;
                }
            }
            if (Input.GetKey(KeyCode.F))
            {
                switch (TrasfromState)
                {   //Posistion
                    case 0:
                        CurrentObjectToControl.GetComponent<Rigidbody>().AddForce(Vector3.up * -Speed * Time.deltaTime, ForceMode.VelocityChange);
                        // CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x, CurrentObjectToControl.transform.position.y - Speed * Time.deltaTime, CurrentObjectToControl.transform.position.z);
                        break;
                    //rotation
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x, CurrentObjectToControl.transform.eulerAngles.y - Speed * Time.deltaTime, CurrentObjectToControl.transform.eulerAngles.z);
                        break;
                    //scale
                    case 2:
                        CurrentObjectToControl.transform.localScale = new Vector3(CurrentObjectToControl.transform.localScale.x, CurrentObjectToControl.transform.localScale.y - Speed * Time.deltaTime, CurrentObjectToControl.transform.localScale.z);
                        break;
                    default:

                        break;
                }
            }
            if (Input.GetKey(KeyCode.R))
            {
                switch (TrasfromState)
                {   //Posistion
                    case 0:
                        CurrentObjectToControl.GetComponent<Rigidbody>().AddForce(Vector3.up * Speed * Time.deltaTime, ForceMode.VelocityChange);
                        //CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x, CurrentObjectToControl.transform.position.y + Speed * Time.deltaTime, CurrentObjectToControl.transform.position.z);
                        break;
                    //rotation
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x, CurrentObjectToControl.transform.eulerAngles.y + Speed * Time.deltaTime, CurrentObjectToControl.transform.eulerAngles.z);
                        break;
                    //scale
                    case 2:
                        CurrentObjectToControl.transform.localScale = new Vector3(CurrentObjectToControl.transform.localScale.x, CurrentObjectToControl.transform.localScale.y + Speed * Time.deltaTime, CurrentObjectToControl.transform.localScale.z);
                        break;
                    default:

                        break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(varibleHolder == null)
            {
               varibleHolder = VaribleHolder.thisVaribleHolder;
            }

            if (!VaribleHolder.LocalPlayer.GetComponent<NetworkIdentity>().isLocalPlayer)
                return;

            ThisColour = varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI;
            if (Input.GetKeyDown(KeyCode.E))
            {
                TrasfromState++;

                if (TrasfromState > 2)
                {
                    TrasfromState = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TrasfromState--;

                if (TrasfromState < 0)
                {
                    TrasfromState = 2;
                }
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (modelUIScript.InteractionScript.HUD.activeInHierarchy == true)
                {
                    modelUIScript.InteractionScript.HUD.SetActive(false);
                }
                else
                {
                    modelUIScript.InteractionScript.HUD.SetActive(true);
                }

            }

            switch (TrasfromState)
            {
                case 0:
                    TransformStateString = "Position";
                    break;
                case 1:
                    TransformStateString = "Rotation";
                    break;
                case 2:
                    TransformStateString = "Scale";
                    break;
                default:
                    TransformStateString = "Position";
                    break;
            }

            if (Input.GetKey(KeyCode.T))
            {
                Speed = Speed + 4 * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.G))
            {
                Speed = Speed - 4 * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                ObjSelectUI = varibleHolder.GameObjSelectMenu3D;
                ObjSelectUI.SetActive(true);

            }
        }
    }
}
