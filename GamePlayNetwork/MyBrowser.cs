using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using ZenFulcrum.EmbeddedBrowser;

namespace NATTraversal
{
    public class MyBrowser : NetworkBehaviour
    {
        [SyncVar]
        public string url;

        //public Text urlText;
        public string urlString;
        public Browser bScript;

        public VaribleHolder varibleHolder;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            urlString = bScript.Url;
            UpdateUrl(urlString);
        }

        public void UpdateUrl(string NewUrl)
        {
            
        }

        

        

    }
}