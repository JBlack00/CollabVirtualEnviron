using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using UnityEngine.UI;

namespace NATTraversal
{
    public class ModelingUI : NetworkBehaviour
    {
        [SyncVar]
        public GameObject CurrentObjectToControl;
        public static float Speed = 1;
        public int TrasfromState;
        public static string TransformStateString;

        public GameObject ObjSelectUI;

        public Transform Spawn;

        public string PrevObjName;
        public GameObject PrevObj;

        public GameObject Cam;

        public ModelUI modelUIScript;

        public VaribleHolder varibleHolder;

        [SyncVar]
        public GameObject CurrentPersonModelingUI;
        // Start is called before the first frame update
        void Start()
        {
            /*
            if (!isLocalPlayer)
            {
                varibleHolder = VaribleHolder.thisVaribleHolder;
                Spawn = varibleHolder.UIObjSpawn.transform;
                Cam = varibleHolder.ModelingUICam;
                ObjSelectUI = varibleHolder.UIObjSelect;
                modelUIScript = varibleHolder.Modeling3DUI.GetComponent<ModelUI>();
                this.enabled = false;
            }
            else
            {
                if (isServer)
                {
                    varibleHolder = VaribleHolder.thisVaribleHolder;
                    Spawn = varibleHolder.UIObjSpawn.transform;
                    Cam = varibleHolder.ModelingUICam;
                    ObjSelectUI = varibleHolder.UIObjSelect;
                    modelUIScript = varibleHolder.Modeling3DUI.GetComponent<ModelUI>();
                    this.enabled = false;
                }
                else
                {
                    varibleHolder = VaribleHolder.thisVaribleHolder;
                    Spawn = varibleHolder.UIObjSpawn.transform;
                    Cam = varibleHolder.ModelingUICam;
                    ObjSelectUI = varibleHolder.UIObjSelect;
                    modelUIScript = varibleHolder.Modeling3DUI.GetComponent<ModelUI>();
                    this.enabled = false;
                }
            }
            this.enabled = false;
            */
        }
        public void OnConnected(NetworkMessage netMsg)
        {
            Debug.Log("Client Connected");
        }
        public static string GetTransformString()
        {
            return TransformStateString;
        }

        [Command]
        public void CmdNewObj()
        {

            string selectedObjNetName = GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName;
            switch (selectedObjNetName)
            {
                case "Square":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNewUIObj();
                    return;
                case "Circle":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNewUIObj();
                    return;
                case "Triangle":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNewUIObj();
                    return;
                case "Text":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNewUIObj();
                    return;
                case "PLUS":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNewUIObj();
                    return;
            }
            if (selectedObjNetName == "Arrow")
            {
                GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdSpawnNewUIObj();
            }
        }
        [Command]
        public void CmdChangeObj()
        {

            string selectedObjNetName = GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName;
            switch (selectedObjNetName)
            {
                case "Square":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChangeUIObj();
                    return;
                case "Circle":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChangeUIObj();
                    return;
                case "Triangle":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChangeUIObj();
                    return;
                case "Text":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChangeUIObj();
                    return;
                case "PLUS":
                    GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChangeUIObj();
                    return;
            }
            if (selectedObjNetName == "Arrow")
            {
                GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdChangeUIObj();
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (!VaribleHolder.LocalPlayer.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                return;
            }
             

            Debug.Log(this.GetComponent<NetworkIdentity>().netId + " is ui moddling with the name" + gameObject.name);

            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                TrasfromState++;
                if (TrasfromState > 2)
                {
                    TrasfromState = 0;
                }
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                TrasfromState--;
                if (TrasfromState < 0)
                {
                    TrasfromState = 2;
                }
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.H))
            {
                UnityEngine.Debug.Log("H Pressed");
                if (modelUIScript.InteractionScript.HUD.activeInHierarchy)
                {
                    modelUIScript.InteractionScript.HUD.SetActive(value: false);
                }
                else
                {
                    modelUIScript.InteractionScript.HUD.SetActive(value: true);
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
            if (UnityEngine.Input.GetKey(KeyCode.T))
            {
                Speed += 4f * Time.deltaTime;
            }
            if (UnityEngine.Input.GetKey(KeyCode.G))
            {
                Speed -= 4f * Time.deltaTime;
            }
            if (UnityEngine.Input.GetKey(KeyCode.Mouse1))
            {
                ObjSelectUI.SetActive(value: true);
            }
            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                switch (TrasfromState)
                {
                    case 0:
                        CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x - Speed * Time.deltaTime, CurrentObjectToControl.transform.position.y, CurrentObjectToControl.transform.position.z);
                        break;
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x, CurrentObjectToControl.transform.eulerAngles.y, CurrentObjectToControl.transform.eulerAngles.z + Speed * Time.deltaTime);
                        break;
                    case 2:
                        CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta = new Vector3(CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x - Speed * Time.deltaTime, CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateUIWidthAndHeight(CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x, CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y);
                        break;
                }
            }
            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                switch (TrasfromState)
                {
                    case 0:
                        CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x + Speed * Time.deltaTime, CurrentObjectToControl.transform.position.y, CurrentObjectToControl.transform.position.z);
                        break;
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x, CurrentObjectToControl.transform.eulerAngles.y, CurrentObjectToControl.transform.eulerAngles.z - Speed * Time.deltaTime);
                        break;
                    case 2:
                        CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta = new Vector3(CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x + Speed * Time.deltaTime, CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateUIWidthAndHeight(CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x, CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y);
                        break;
                }
            }
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                switch (TrasfromState)
                {
                    case 0:
                        CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x, CurrentObjectToControl.transform.position.y + Speed * Time.deltaTime, CurrentObjectToControl.transform.position.z);
                        break;
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x + Speed * Time.deltaTime, CurrentObjectToControl.transform.eulerAngles.y, CurrentObjectToControl.transform.eulerAngles.z);
                        break;
                    case 2:
                        CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta = new Vector3(CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x, CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y + Speed * Time.deltaTime);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateUIWidthAndHeight(CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x, CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y);
                        break;
                }
            }
            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                switch (TrasfromState)
                {
                    case 0:
                        CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x, CurrentObjectToControl.transform.position.y - Speed * Time.deltaTime, CurrentObjectToControl.transform.position.z);
                        break;
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x - Speed * Time.deltaTime, CurrentObjectToControl.transform.eulerAngles.y, CurrentObjectToControl.transform.eulerAngles.z);
                        break;
                    case 2:
                        CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta = new Vector3(CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x, CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y - Speed * Time.deltaTime);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateUIWidthAndHeight(CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.x, CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta.y);
                        break;
                }
            }
            if (UnityEngine.Input.GetKey(KeyCode.F))
            {
                switch (TrasfromState)
                {
                    case 0:
                        CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x, CurrentObjectToControl.transform.position.y - Speed * Time.deltaTime, CurrentObjectToControl.transform.position.z);
                        break;
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x, CurrentObjectToControl.transform.eulerAngles.y - Speed * Time.deltaTime, CurrentObjectToControl.transform.eulerAngles.z);
                        break;
                }
            }
            if (UnityEngine.Input.GetKey(KeyCode.R))
            {
                switch (TrasfromState)
                {
                    case 0:
                        CurrentObjectToControl.transform.position = new Vector3(CurrentObjectToControl.transform.position.x, CurrentObjectToControl.transform.position.y + Speed * Time.deltaTime, CurrentObjectToControl.transform.position.z);
                        break;
                    case 1:
                        CurrentObjectToControl.transform.eulerAngles = new Vector3(CurrentObjectToControl.transform.eulerAngles.x, CurrentObjectToControl.transform.eulerAngles.y + Speed * Time.deltaTime, CurrentObjectToControl.transform.eulerAngles.z);
                        break;
                    case 2:
                        CurrentObjectToControl.transform.localScale = new Vector3(CurrentObjectToControl.transform.localScale.x, CurrentObjectToControl.transform.localScale.y + Speed * Time.deltaTime, CurrentObjectToControl.transform.localScale.z);
                        break;
                }
            }


            // GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUiMove(CurrentObjectToControl, Speed, TrasfromState);

        }
       

    }
}
