using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
namespace NATTraversal
{
    public class PlayerCommands : NetworkBehaviour
    {
        public VaribleHolder varibleHolder;
       
        public Modeling ModleingScript;

        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(varibleHolder == null)
            {
                varibleHolder = GameObject.Find("VaribleHolder").GetComponent<VaribleHolder>();
            }
        }
        [Command]
        public void CmdSetSelectedObj(String ObjName)
        {
            Debug.Log("CmdSetSelectedObj SET SELECETED OBJ CALLED" + ObjName);
            //GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNet = Obj;
            RpcSetSelectedObj(ObjName);
        }
         [ClientRpc]
        public void RpcSetSelectedObj(String ObjName)
        {
          //  Debug.Log("RPC SET SELECETED OBJ CALLED" + Obj.name);
            GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName = ObjName;
        }

        [Command]
        public void CmdSpawnNew3DObj()
        {

            UnityEngine.Debug.Log("NAme of this model: " + GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName);
            UnityEngine.Debug.Log("does thos = the same as Prism " + GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName.Equals("Prism").ToString());
            switch (GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName)
            {
                case "Cube":
                    {
                        UnityEngine.Debug.Log("varible cube " + varibleHolder.CUBE.name);
                        GameObject obj = Instantiate(varibleHolder.CUBE, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Cone":
                    {
                        GameObject obj = Instantiate(varibleHolder.CONE, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Cylinder":
                    {
                        GameObject obj = Instantiate(varibleHolder.CYLINDER, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Gear":
                    {
                        GameObject obj = Instantiate(varibleHolder.GEAR, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Pipe":
                    {
                        GameObject obj = Instantiate(varibleHolder.PIPE, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Sphere":
                    {
                        GameObject obj = Instantiate(varibleHolder.SPHERE, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Torus":
                    {
                        GameObject obj = Instantiate(varibleHolder.TORUS, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Wall":
                    {
                        GameObject obj = Instantiate(varibleHolder.WALL, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Window":
                    {
                        GameObject obj = Instantiate(varibleHolder.WINDOW, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Prism":
                    {
                        UnityEngine.Debug.Log("varible cube " + varibleHolder.PRISM.name);
                        GameObject obj = Instantiate(varibleHolder.PRISM, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Pyramid":
                    {
                        GameObject obj = Instantiate(varibleHolder.PYRIMID, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Doorway":
                    {
                        GameObject obj = Instantiate(varibleHolder.DOORWAY, varibleHolder.Modeling3DSpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                default:
                    UnityEngine.Debug.Log("NAme of this model: " + GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName);
                    UnityEngine.Debug.Log("does thos = the same as Prism " + GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName.Equals("Prism").ToString());
                    break;
            }
        }

        [Command]
        public void CmdChange3DObj()
        {
            switch (GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName)
            {
                case "Cube":
                    {
                        UnityEngine.Debug.Log("varible cube " + varibleHolder.CUBE.name);
                        GameObject obj = Instantiate(varibleHolder.CUBE, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Cone":
                    {
                        GameObject obj = Instantiate(varibleHolder.CONE, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Cylinder":
                    {
                        GameObject obj = Instantiate(varibleHolder.CYLINDER, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Gear":
                    {
                        GameObject obj = Instantiate(varibleHolder.GEAR, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Pipe":
                    {
                        GameObject obj = Instantiate(varibleHolder.PIPE, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Sphere":
                    {
                        GameObject obj = Instantiate(varibleHolder.SPHERE, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Torus":
                    {
                        GameObject obj = Instantiate(varibleHolder.TORUS, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Wall":
                    {
                        GameObject obj = Instantiate(varibleHolder.WALL, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Window":
                    {
                        GameObject obj = Instantiate(varibleHolder.WINDOW, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Prism":
                    {
                        UnityEngine.Debug.Log("varible cube " + varibleHolder.PRISM.name);
                        GameObject obj = Instantiate(varibleHolder.PRISM, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Pyramid":
                    {
                        GameObject obj = Instantiate(varibleHolder.PYRIMID, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                case "Doorway":
                    {
                        GameObject obj = Instantiate(varibleHolder.DOORWAY, GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        NetworkServer.Destroy(GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<Modeling>().RpcChangeObj(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlObj(obj);
                        break;
                    }
                default:
                    UnityEngine.Debug.Log("NAme of this model: " + GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName);
                    UnityEngine.Debug.Log("does thos = the same as Prism " + GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName.Equals("Prism").ToString());
                    break;
            }
        }
        [Command]
        public void CmdSpawnNewUIObj()
        {
            string selectedObjNetName = GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName;
            switch (selectedObjNetName)
            {
                case "Square":
                    {
                        UnityEngine.Debug.Log("varible cube " + varibleHolder.CUBE.name);
                        GameObject obj = Instantiate(varibleHolder.SQUARE, varibleHolder.ModelingUISpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
                case "Circle":
                    {
                        GameObject obj = Instantiate(varibleHolder.CIRCLE, varibleHolder.ModelingUISpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
                case "Triangle":
                    {
                        GameObject obj = Instantiate(varibleHolder.TRIANGLE, varibleHolder.ModelingUISpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
                case "Text":
                    {
                        GameObject obj = Instantiate(varibleHolder.TEXT, varibleHolder.ModelingUISpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
                case "PLUS":
                    {
                        GameObject obj =Instantiate(varibleHolder.PLUS, varibleHolder.ModelingUISpawn.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
            }
            if (selectedObjNetName == "Arrow")
            {
                GameObject obj = Instantiate(varibleHolder.ARROW, varibleHolder.ModelingUISpawn.position, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(obj, base.gameObject);
                GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
            }
        }

        [Command]
        public void CmdChangeUIObj()
        {
            string selectedObjNetName = GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName;
            switch (selectedObjNetName)
            {
                case "Square":
                    {
                        UnityEngine.Debug.Log("varible cube " + varibleHolder.CUBE.name);
                        Debug.Log("modeling ui old pos " + varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl.transform.position);
                        GameObject obj = Instantiate(varibleHolder.SQUARE, varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl.transform.position, Quaternion.identity);
                     
                        NetworkServer.SpawnWithClientAuthority(obj, GameObject.Find("LocalPlayer").gameObject.GetComponent<NetworkIdentity>().connectionToClient);
                        varibleHolder.ModelingUI.GetComponent<ModelingUI>().GetComponent<NetworkIdentity>().AssignClientAuthority(GameObject.Find("LocalPlayer").gameObject.GetComponent<NetworkIdentity>().connectionToClient);
                        obj.GetComponent<NetworkIdentity>().AssignClientAuthority(GameObject.Find("LocalPlayer").gameObject.GetComponent<NetworkIdentity>().connectionToClient);
                        NetworkServer.Destroy(varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
                case "Circle":
                    {
                        GameObject obj = Instantiate(varibleHolder.CIRCLE, GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, GameObject.Find("LocalPlayer").gameObject);
                        NetworkServer.Destroy(varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
                case "Triangle":
                    {
                        GameObject obj = Instantiate(varibleHolder.TRIANGLE, varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, GameObject.Find("LocalPlayer").gameObject);
                        NetworkServer.Destroy(varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
                case "Text":
                    {
                        GameObject obj = Instantiate(varibleHolder.TEXT, GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, GameObject.Find("LocalPlayer").gameObject);
                        NetworkServer.Destroy(varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
                case "PLUS":
                    {
                        GameObject obj = Instantiate(varibleHolder.PLUS, GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().CurrentObjectToControl.transform.position, Quaternion.identity);
                        NetworkServer.SpawnWithClientAuthority(obj, GameObject.Find("LocalPlayer").gameObject);
                        NetworkServer.Destroy(varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
                        return;
                    }
            }
            if (selectedObjNetName == "Arrow")
            {
                GameObject obj = Instantiate(varibleHolder.ARROW, GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().CurrentObjectToControl.transform.position, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(obj, GameObject.Find("LocalPlayer").gameObject);
                NetworkServer.Destroy(varibleHolder.ModelingUI.GetComponent<ModelingUI>().CurrentObjectToControl);
                GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().RpcUpdateObjColourUI(obj, varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI);
                GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateControlUIObj(obj);
            }
        }
        [Command]
        public void CmdUpdateControlObj( GameObject Obj)
        {
            RpcUpdateControlObj( Obj);
        }

        [ClientRpc]
        public void RpcUpdateControlObj( GameObject Obj)
        {
            GameObject.Find("LocalPlayer").GetComponent<Modeling>().CurrentObjectToControl = Obj;
            if (varibleHolder.Modeling3DCam != null)
            {
                varibleHolder.Modeling3DCam.GetComponent<Cam3rdOffline>().lookAt = Obj.transform;
            }
           


        }
     
        [Command]
        public void CmdUpdatePersonControlUIObj(GameObject Obj)
        {
            RpcUpdatePersonControlUIObj(Obj);
        }
        [ClientRpc]
        public void RpcUpdatePersonControlUIObj(GameObject Obj)
        {
            GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().CurrentPersonModelingUI = Obj;
        }
        [Command]
        public void CmdUpdateControlUIObj(GameObject Obj)
        {
            RpcUpdateControlUIObj(Obj);
        }

        [ClientRpc]
        public void RpcUpdateControlUIObj(GameObject Obj)
        {
            Obj.transform.parent = varibleHolder.ModelingUISpawn;
            Obj.transform.localScale = new Vector3(1f, 1f, 1f);

           
           GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().CurrentObjectToControl = Obj;

        }
        [Command]
        public void CmdUpdateColour(Color colour)
        {
            RpcUpdateColour(colour);
        }
        [ClientRpc]
        public void RpcUpdateColour(Color colour)
        {
            varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI = colour;
        }
        [Command]
        public void CmdUpdateColourUI(Color colour)
        {
            RpcUpdateColourUI(colour);
        }

        [ClientRpc]
        public void RpcUpdateColourUI(Color colour)
        {
            varibleHolder.Colour3D.GetComponent<ColourUISelect>().ColorOfUI = colour;
        }

        [Command]
        public void CmdUpdateObjColourUI(GameObject Obj, Color colour)
        {
            RpcUpdateObjColourUI(Obj, colour);
        }

        [ClientRpc]
        public void RpcUpdateObjColourUI(GameObject Obj, Color colour)
        {
            if (GameObject.Find("MainScreenCanvas").GetComponent<ModelUI>().SelectedObjNetName == "Text")
            {
                Obj.GetComponent<Text>().color = colour;
            }
            else
            {
                Obj.GetComponent<Image>().color = colour;
            }
        }
        [Command]
        public void CmdUpdateUIWidthAndHeight(float NewWidth, float NewHeight)
        {
            RpcUpdateUIWidthAndHeight(NewWidth, NewHeight);
        }

        [ClientRpc]
        public void RpcUpdateUIWidthAndHeight(float NewWidth, float NewHeight)
        {
            GameObject.Find("UIModlingCanvas").GetComponent<ModelingUI>().CurrentObjectToControl.GetComponent<RectTransform>().sizeDelta = new Vector2(NewWidth, NewHeight);
        }


        //  RpcSpawnNew3DObj(Obj);
        // NetworkServer.Spawn(Obj);
        //GameObject NewObj = Instantiate(ObjPrefab, Spawn.transform.position, Quaternion.identity);


        //  Obj.GetComponent<Renderer>().material.SetColor("_Color", ColourUISelect.ColorOfUI);
        //  GameObject.Find("Modeling").GetComponent<Modeling>().SetNewObj(NewObj);
        // CurObjToSontrol = NewObj;
        //Cam.GetComponent<Cam3rdOffline>().lookAt = NewObj.transform;



        [ClientRpc]
        public void RpcSpawnNew3DObj(GameObject Obj)
        {

            //GameObject NewObj = Instantiate(Obj, Spawn.transform.position, Quaternion.identity);


            // NewObj.GetComponent<Renderer>().material.SetColor("_Color", ColourUISelect.ColorOfUI);
            // CurObjToSontrol = NewObj;
            // Cam.GetComponent<Cam3rdOffline>().lookAt = NewObj.transform;
            // return NewObj;

        }


    }
}
