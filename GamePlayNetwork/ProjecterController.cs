using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace NATTraversal
{
    public class ProjecterController : NetworkBehaviour
    {
        public GameObject ProjecterScreen;

        public Sprite[] ProjecterSprites;

        [SyncVar]
        public int CurrentImage;

        public GameObject Player;
        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.Find("LocalPlayer");
            //  WhiteBoardText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {

            if (isServer)
            {
             //   ProjecterScreen.GetComponent<Image>().sprite = ProjecterSprites[CurrentImage];
            }

            

            //   CmdSendImage(CurrentImage);




            //  RpcClientUpdateImage(CurrentImage);

           
          
        }
        [Command]
        public void CmdSendImage(int Value)
        {
          //  ProjecterScreen.GetComponent<Image>().sprite = ProjecterSprites[Value];
         //   Debug.Log("CMD CALLESD");
            RpcClientUpdateImage(Value);
        }
        [ClientRpc]
        public void RpcClientUpdateImage(int value)
        {
            if (value> ProjecterSprites.Length-1)
            {
                value = 0;
              
            }
            if (value < 0)
            {
                value = ProjecterSprites.Length - 1;
            }
            CurrentImage = value;
          //  Debug.Log("ClientRpc CALLESD");
        }

        public void changeImage(int value)
        {

            CmdSendImage(value);

            /*
            if (!isServer)
            {
                CurrentImage = value;
                CmdSendImage(value);
            }
            else
            {
                CurrentImage = value;
                RpcClientUpdateImage(CurrentImage);
            }
               
     */

        }
      

        public void LastImage()
        {
            CurrentImage--;
        }
    }
}
