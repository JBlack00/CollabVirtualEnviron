﻿#if !DISABLE_NAT_TRAVERSAL
#if !UNITY_5_3
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NATTraversal
{
    [AddComponentMenu("Network/NATNetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [System.Serializable]
    public class CustomUILobbyHUD : NetworkManagerHUD
    {
        bool showServer;

       

        NetWorkLobbySpawner script;
        void Awake()
        {
            manager = GetComponent<NetworkManager>();
          
            script = GetComponent<NetWorkLobbySpawner>();
            StopEverything();
        }

        void Update()
        {



            //GUI SHIT
            NetworkManager manager = (NetworkManager)this.manager;

            //Debug.Log(base.manager);
            bool noConnection = (manager.client == null || manager.client.connection == null ||
                                 manager.client.connection.connectionId == -1);

            if (!NetworkServer.active && !manager.IsClientConnected() && noConnection)
            {
                
                if (manager.matches != null)
                {
                   // Debug.Log(manager.matches);
                    for (int i = 0; i < manager.matches.Count; i++)
                    {
                        var match = manager.matches[i];
                        // if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Match:" + match.name))
                        // {

                        if (script.Parent.childCount < manager.matches.Count)
                        {
                            GameObject lobbyGame = Instantiate(script.LobbyPrefab, script.SpawnPos.position, Quaternion.identity, script.Parent);
                            
                            lobbyGame.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { StartClientAll(i); });
                            lobbyGame.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(EnableReadyScreen);
                            // if (manager.IsClientConnected())
                            // {
                            if (lobbyGame.GetComponent<LobbyMatchController>().NameOfLobby == null)
                            {
                          //      Debug.LogError("working");
                            }
                            lobbyGame.GetComponent<LobbyMatchController>().UpdateName(manager.matchName+ " (Client: address=" + manager.networkAddress + " port=" + manager.networkPort +")");

                            // 
                            // }
                        }
                        //manager.StartClientAll(match);



                        //manager.matchName = match.name;
                        //manager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
                        //  }
                        //manager.StartClientAll(match);
                    }
                }
                
            }
            }
       
       public void EnableReadyScreen()
        {
            Debug.Log("EnablingScreen");
           script.ReadyScreen.SetActive(true);
        }
        public void StartHost()
        {
            manager.StartHost();
        }
        public void StopHost()
        {
            manager.StopHost();

        }
        public void StartClient()
        {
            manager.StartClient();
        }
        public void StartClientToLan(Text textInput)
        {
            manager.networkAddress = textInput.text;
            manager.StartClient();
        }
        public void StartServer()
        {
            manager.StartServer();
        }
        public void StopClient()
        {
            manager.StopClient();
        }
        public void ClientReady()
        {
            ClientScene.Ready(manager.client.connection);

            if (ClientScene.localPlayers.Count == 0)
            {
                ClientScene.AddPlayer(0);
            }
        }

        public void StopEverything()
        {
            
            NetworkServer.SetAllClientsNotReady();
            if (NetworkServer.active)
            {
              //  NetworkManager manager = (NetworkManager)this.manager;
                manager.StopHost();
            }
            else
            {
               // NetworkManager manager = (NetworkManager)this.manager;
                manager.StopClient();
            }
        }
        public void StartMatchMaking()
        {
           // NetworkManager manager = (NetworkManager)this.manager;
            manager.StartMatchMaker();
        }
       
        public void CreateInteretMatch()
        {
            NetworkManager manager = (NetworkManager)this.manager;
            manager.StartHostAll(manager.matchName, manager.matchSize);
        }

        public void FindMatch()
        {
            //NetworkManager manager = (NetworkManager)this.manager;
            manager.matchMaker.ListMatches(0, 20, "", false, 0, 0, manager.OnMatchList);

            /*
                Debug.Log(manager.matches);
                for (int i = 0; i < manager.matches.Count; i++)
                {
                    var match = manager.matches[i];
                    // if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Match:" + match.name))
                    // {


                    GameObject lobbyGame = Instantiate(script.LobbyPrefab, script.SpawnPos.position, Quaternion.identity);

                    lobbyGame.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { StartClientAll(i); });

                    //manager.StartClientAll(match);



                    //manager.matchName = match.name;
                    //manager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
                    //  }
                    //manager.StartClientAll(match);
                }
            */
        }

        //Change this to Prefabs rather than GUI
        public void StartClientAll(int matchNum)
        {
            NetworkManager manager = (NetworkManager)this.manager;
            Debug.Log(matchNum);
            var match = manager.matches[matchNum-1];
               // if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Match:" + match.name))
               // {
                    manager.StartClientAll(match);
                    //manager.matchName = match.name;
                    //manager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
              //  }
                //manager.StartClientAll(match);
            

            //GameObject obj = Instantiate(lobbyPlayerPrefab.gameObject) as GameObject;

          //  LobbyPlayer newPlayer = obj.GetComponent<LobbyPlayer>();
          //  newPlayer.ToggleJoinButton(numPlayers + 1 >= minPlayers);


           // for (int i = 0; i < lobbySlots.Length; ++i)
           // {
           //     LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

            //    if (p != null)
            //    {
             //       p.RpcUpdateRemoveButton();
             ///       p.ToggleJoinButton(numPlayers + 1 >= minPlayers);
             //   }
          //  }
        }
        public void BackToMainMenu()
        {
            manager.matches = null;
        }

        public void StopMatchMaking()
        {
            manager.StopMatchMaker();
        }
        void OnGUI()
        {
            


           // apparntly needs to be one cause amazing so this stops it really being shown
            if (!showGUI)
            {
                return;
                //offsetX = -10000000;
            }
                
            /*OldGUI

           */
            NetworkManager manager = (NetworkManager)this.manager;

            int xpos = 10 + offsetX;
            int ypos = 40 + offsetY;
            const int spacing = 24;

            bool noConnection = (manager.client == null || manager.client.connection == null ||
                                 manager.client.connection.connectionId == -1);

            if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
            {
                if (noConnection)
                {
                    if (UnityEngine.Application.platform != RuntimePlatform.WebGLPlayer)
                    {
                        if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Host(H)"))
                        {
                            manager.StartHost();
                        }
                        ypos += spacing;
                    }

                    if (GUI.Button(new Rect(xpos, ypos, 105, 20), "LAN Client(C)"))
                    {
                        manager.StartClient();
                    }

                    manager.networkAddress = GUI.TextField(new Rect(xpos + 100, ypos, 95, 20), manager.networkAddress);
                    ypos += spacing;

                    if (UnityEngine.Application.platform == RuntimePlatform.WebGLPlayer)
                    {
                        // cant be a server in webgl build
                        GUI.Box(new Rect(xpos, ypos, 200, 25), "(  WebGL cannot be server  )");
                        ypos += spacing;
                    }
                    else
                    {
                        if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Server Only(S)"))
                        {
                            manager.StartServer();
                        }
                        ypos += spacing;
                    }
                }
                else
                {
                    GUI.Label(new Rect(xpos, ypos, 200, 20), "Connecting to " + manager.networkAddress + ":" + manager.networkPort + "..");
                    ypos += spacing;


                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Cancel Connection Attempt"))
                    {
                        manager.StopClient();
                    }
                }
            }
            else
            {
                if (NetworkServer.active)
                {
                    string serverMsg = "Server: port=" + manager.networkPort;
                    if (manager.useWebSockets)
                    {
                        serverMsg += " (Using WebSockets)";
                    }
                    GUI.Label(new Rect(xpos, ypos, 300, 20), serverMsg);
                    ypos += spacing;
                }
                if (manager.IsClientConnected())
                {
                    GUI.Label(new Rect(xpos, ypos, 300, 20), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
                    ypos += spacing;
                }
            }

            if (manager.IsClientConnected() && !ClientScene.ready)
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Client Ready"))
                {
                    ClientScene.Ready(manager.client.connection);

                    if (ClientScene.localPlayers.Count == 0)
                    {
                        ClientScene.AddPlayer(0);
                    }
                   
                }
                ypos += spacing;
            }

            if (NetworkServer.active || manager.IsClientConnected())
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Stop (X)"))
                {
                    NetworkServer.SetAllClientsNotReady();
                    if (NetworkServer.active)
                    {
                        manager.StopHost();
                    }
                    else
                    {
                        manager.StopClient();
                    }
                }
                ypos += spacing;
            }

            if (!NetworkServer.active && !manager.IsClientConnected() && noConnection)
            {
                ypos += 10;

                if (UnityEngine.Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    GUI.Box(new Rect(xpos - 5, ypos, 220, 25), "(WebGL cannot use Match Maker)");
                    return;
                }

                if (manager.matchMaker == null)
                {
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Enable Match Maker (M)"))
                    {
                        manager.StartMatchMaker();
                    }
                    ypos += spacing;
                }
                else
                {
                    if (manager.matchInfo == null)
                    {
                        if (manager.matches == null)
                        {
                            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Create Internet Match"))
                            {
                                manager.StartHostAll(manager.matchName, manager.matchSize);
                                //manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", "", "", 0, 0, manager.OnMatchCreate);
                            }
                            ypos += spacing;

                            GUI.Label(new Rect(xpos, ypos, 100, 20), "Room Name:");
                            manager.matchName = GUI.TextField(new Rect(xpos + 100, ypos, 100, 20), manager.matchName);
                            ypos += spacing;

                            ypos += 10;

                            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Find Internet Match"))
                            {

                                manager.matchMaker.ListMatches(0, 20, "", false, 0, 0, manager.OnMatchList);

                            }
                            ypos += spacing;
                        }
                        else
                        {
                            for (int i = 0; i < manager.matches.Count; i++)
                            {
                                var match = manager.matches[i];
                                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Match:" + match.name))
                                {
                                    manager.StartClientAll(match);
                                    //manager.matchName = match.name;
                                    //manager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
                                }
                                ypos += spacing;
                            }

                            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Back to Match Menu"))
                            {
                                manager.matches = null;
                            }
                            ypos += spacing;
                        }
                    }

                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Change MM server"))
                    {
                        showServer = !showServer;
                    }
                    if (showServer)
                    {
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Local"))
                        {
                            manager.SetMatchHost("localhost", 1337, false);
                            showServer = false;
                        }
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Internet"))
                        {
                            manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
                            showServer = false;
                        }
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Staging"))
                        {
                            manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
                            showServer = false;
                        }
                    }

                    ypos += spacing;

                    GUI.Label(new Rect(xpos, ypos, 300, 20), "MM Uri: " + manager.matchMaker.baseUri);
                    ypos += spacing;

                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Disable Match Maker"))
                    {
                        manager.StopMatchMaker();
                    }
                    ypos += spacing;
                }
            }
        }
    }
}

#else

using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Networking;

namespace NATTraversal
{
    [AddComponentMenu("Network/NATNetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class NATNetworkManagerHUD : NetworkManagerHUD
    {
        // Runtime variable
        bool showServer;

        void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }

        void Update()
        {
            if (!showGUI)
                return;

            NetworkManager manager = (NetworkManager)this.manager;

            if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    manager.StartServer();
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    manager.StartHost();
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    manager.StartClient();
                }
            }
            if (NetworkServer.active && manager.IsClientConnected())
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    manager.StopHost();
                }
            }
        }

        void OnGUI()
        {
            if (!showGUI)
                return;

            NetworkManager manager = (NetworkManager)this.manager;

            int xpos = 10 + offsetX;
            int ypos = 40 + offsetY;
            const int spacing = 24;

            if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Host(H)"))
                {
                    manager.StartHost();
                }
                ypos += spacing;

                if (GUI.Button(new Rect(xpos, ypos, 105, 20), "LAN Client(C)"))
                {
                    manager.StartClient();
                }
                manager.networkAddress = GUI.TextField(new Rect(xpos + 100, ypos, 95, 20), manager.networkAddress);
                ypos += spacing;

                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Server Only(S)"))
                {
                    manager.StartServer();
                }
                ypos += spacing;
            }
            else
            {
                if (NetworkServer.active)
                {
                    GUI.Label(new Rect(xpos, ypos, 300, 20), "Server: port=" + manager.networkPort);
                    ypos += spacing;
                }
                if (manager.IsClientConnected())
                {
                    GUI.Label(new Rect(xpos, ypos, 300, 20), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
                    ypos += spacing;
                }
            }

            if (manager.IsClientConnected() && !ClientScene.ready)
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Client Ready"))
                {
                    ClientScene.Ready(manager.client.connection);

                    if (ClientScene.localPlayers.Count == 0)
                    {
                        ClientScene.AddPlayer(0);
                    }
                }
                ypos += spacing;
            }

            if (NetworkServer.active || manager.IsClientConnected())
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Stop (X)"))
                {
                    manager.StopHost();
                }
                ypos += spacing;
            }

            if (!NetworkServer.active && !manager.IsClientConnected())
            {
                ypos += 10;

                if (manager.matchMaker == null)
                {
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Enable Match Maker (M)"))
                    {
                        manager.StartMatchMaker();
                    }
                    ypos += spacing;
                }
                else
                {
                    if (manager.matchInfo == null)
                    {
                        if (manager.matches == null)
                        {
                            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Create Internet Match"))
                            {
                                manager.StartHostAll(manager.matchName, manager.matchSize);
                                //manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", manager.OnMatchCreate);
                            }
                            ypos += spacing;

                            GUI.Label(new Rect(xpos, ypos, 100, 20), "Room Name:");
                            manager.matchName = GUI.TextField(new Rect(xpos + 100, ypos, 100, 20), manager.matchName);
                            ypos += spacing;

                            ypos += 10;

                            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Find Internet Match"))
                            {
                                manager.matchMaker.ListMatches(0, 20, "", manager.OnMatchList);
                            }
                            ypos += spacing;
                        }
                        else
                        {
                            foreach (var match in manager.matches)
                            {
                                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Match:" + match.name))
                                {
                                    //manager.matchName = match.name;
                                    //manager.matchSize = (uint)match.currentSize;

                                    manager.StartClientAll(match);
                                    //manager.matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
                                }
                                ypos += spacing;
                            }
                        }
                    }

                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Change MM server"))
                    {
                        showServer = !showServer;
                    }
                    if (showServer)
                    {
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Local"))
                        {
                            manager.SetMatchHost("localhost", 1337, false);
                            showServer = false;
                        }
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Internet"))
                        {
                            manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
                            showServer = false;
                        }
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Staging"))
                        {
                            manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
                            showServer = false;
                        }
                    }

                    ypos += spacing;

                    GUI.Label(new Rect(xpos, ypos, 300, 20), "MM Uri: " + manager.matchMaker.baseUri);
                    ypos += spacing;

                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Disable Match Maker"))
                    {
                        manager.StopMatchMaker();
                    }
                    ypos += spacing;
                }
            }
        }
    }
}
#endif
#endif