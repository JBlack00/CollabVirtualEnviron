using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NATTraversal;
using UnityEngine.Networking;
namespace NATTraversal
{
    public class WhiteBoard : NetworkBehaviour
    {
        
        public TMPro.TextMeshProUGUI WhiteBoardText;
        [SyncVar]
        public string stringOnBoard;
        // Start is called before the first frame update
        void Start()
        {
          //  WhiteBoardText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
           // if (isServer)
         //   {
          //      RpcClientUpdateText(stringOnBoard);
           //   
        //    }
         //   else
         //   {
         //       
          //      CmdSendText(stringOnBoard);
         //   }
        }
        [Command]
        public void CmdSendText(string Value)
        {
            WhiteBoardText.text = Value;
            RpcClientUpdateText(Value);
        }
        [ClientRpc]
        public void RpcClientUpdateText(string value)
        {
            WhiteBoardText.text = value;
           // Debug.Log("Am i Local PLayer " + GameObject.Find("LocalPlayer").GetComponent<NetworkBehaviour>().isLocalPlayer + " has athority? " + GameObject.Find("LocalPlayer").GetComponent<NetworkBehaviour>().hasAuthority);
        }
    }
}

