﻿#if !DISABLE_NAT_TRAVERSAL
#if !UNITY_5_3
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

namespace NATTraversal
{
    [DisallowMultipleComponent]
    public class CustomNatLobbyPlayer : NetworkBehaviour
    {
        [SerializeField]
        public bool ShowLobbyGUI = true;
        [SerializeField] public GameObject ReadyScreen;

        byte m_Slot;
        bool m_ReadyToBegin;

        public byte slot { get { return m_Slot; } set { m_Slot = value; } }
        public bool readyToBegin { get { return m_ReadyToBegin; } set { m_ReadyToBegin = value; } }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            ReadyScreen = GameObject.Find("ReadyScreen");

            ReadyScreen.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { StartButton(); });
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public override void OnStartClient()
        {
            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                lobby.lobbySlots[m_Slot] = this;
                m_ReadyToBegin = false;
                OnClientEnterLobby();
            }
            else
            {
                Debug.LogError("LobbyPlayer could not find a NATLobbyManager. The LobbyPlayer requires a NATLobbyManager object to function. Make sure that there is one in the scene.");
            }
        }

        public void SendReadyToBeginMessage()
        {
            if (LogFilter.logDebug) { Debug.Log("CustomNATLobbyPlayer SendReadyToBeginMessage"); }

            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                var msg = new NATLobbyManager.LobbyReadyToBeginMessage();
                msg.slotId = (byte)playerControllerId;
                msg.readyState = true;
                lobby.client.Send(MsgType.LobbyReadyToBegin, msg);
            }
        }

        public void SendNotReadyToBeginMessage()
        {
            if (LogFilter.logDebug) { Debug.Log("CustomNATLobbyPlayer SendReadyToBeginMessage"); }

            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                var msg = new NATLobbyManager.LobbyReadyToBeginMessage();
                msg.slotId = (byte)playerControllerId;
                msg.readyState = false;
                lobby.client.Send(MsgType.LobbyReadyToBegin, msg);
            }
        }

        public void SendSceneLoadedMessage()
        {
            if (LogFilter.logDebug) { Debug.Log("CustomNATLobbyPlayer SendSceneLoadedMessage"); }

            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                var msg = new IntegerMessage(playerControllerId);
                lobby.client.Send(MsgType.LobbySceneLoaded, msg);
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                // dont even try this in the startup scene
                // Should we check if the LoadSceneMode is Single or Additive??
                // Can the lobby scene be loaded Additively??
                string loadedSceneName = scene.name;
                if (loadedSceneName == lobby.lobbyScene)
                {
                    return;
                }
            }

            if (isLocalPlayer)
            {
                SendSceneLoadedMessage();
            }
        }

        public void RemovePlayer()
        {
            if (isLocalPlayer && !m_ReadyToBegin)
            {
                if (LogFilter.logDebug) { Debug.Log("CustomNatMatchMakingLobby RemovePlayer"); }

                ClientScene.RemovePlayer(GetComponent<NetworkIdentity>().playerControllerId);
            }
        }

        // ------------------------ callbacks ------------------------

        public virtual void OnClientEnterLobby()
        {
        }

        public virtual void OnClientExitLobby()
        {
        }

        public virtual void OnClientReady(bool readyState)
        {
        }

        // ------------------------ Custom Serialization ------------------------

        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            // dirty flag
            writer.WritePackedUInt32(1);

            writer.Write(m_Slot);
            writer.Write(m_ReadyToBegin);
            return true;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            var dirty = reader.ReadPackedUInt32();
            if (dirty == 0)
                return;

            m_Slot = reader.ReadByte();
            m_ReadyToBegin = reader.ReadBoolean();
        }

        // ------------------------ optional UI ------------------------

        public void StartButton()
        {
            SendReadyToBeginMessage();
        }
        public void StopButton()
        {
            SendNotReadyToBeginMessage();
        }
        public void RemoveButton()
        {
            ClientScene.RemovePlayer(GetComponent<NetworkIdentity>().playerControllerId);
        }

        private void Update()
        {
            if (isLocalPlayer)
            {
                string youStr;
                if (m_ReadyToBegin)
                {
                    youStr = "Ready";
                }
                else
                {
                    youStr = "Not Ready";
                }
              //  GUI.Label(rec, youStr);
   //             ReadyScreen.transform.GetChild(1).GetComponent<Text>().text = youStr;
            }
            else
            {

                //GUI.Label(rec, "Player [" + netId + "]");
             
                //GUI.Label(rec, "Ready [" + m_ReadyToBegin + "]");
            }
        }
        void OnGUI()
        {
            if (!ShowLobbyGUI)
                return;

            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                if (!lobby.showLobbyGUI)
                    return;

                string loadedSceneName = SceneManager.GetSceneAt(0).name;
                if (loadedSceneName != lobby.lobbyScene)
                    return;
            }

            Rect rec = new Rect(100 + m_Slot * 100, 200, 90, 20);

            if (isLocalPlayer)
            {
                string youStr;
                if (m_ReadyToBegin)
                {
                    youStr = "(Ready)";
                }
                else
                {
                    youStr = "(Not Ready)";
                }
                GUI.Label(rec, youStr);

                if (m_ReadyToBegin)
                {
                    rec.y += 25;
                    if (GUI.Button(rec, "STOP"))
                    {
                        SendNotReadyToBeginMessage();
                    }
                }
                else
                {
                    rec.y += 25;
                    if (GUI.Button(rec, "START"))
                    {
                        SendReadyToBeginMessage();
                    }

                    rec.y += 25;
                    if (GUI.Button(rec, "Remove"))
                    {
                        ClientScene.RemovePlayer(GetComponent<NetworkIdentity>().playerControllerId);
                    }
                }
            }
            else
            {
                GUI.Label(rec, "Player [" + netId + "]");
                rec.y += 25;
                GUI.Label(rec, "Ready [" + m_ReadyToBegin + "]");
            }
        }
    }
}


#else
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

namespace NATTraversal
{
    [DisallowMultipleComponent]
    public class CustomNatLobbyPlayer : NetworkBehaviour
    {
        [SerializeField]
        public bool ShowLobbyGUI = true;

        byte m_Slot;
        bool m_ReadyToBegin;

        public byte slot { get { return m_Slot; } set { m_Slot = value; } }
        public bool readyToBegin { get { return m_ReadyToBegin; } set { m_ReadyToBegin = value; } }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void OnStartClient()
        {
            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                lobby.lobbySlots[m_Slot] = this;
                m_ReadyToBegin = false;
                OnClientEnterLobby();
            }
            else
            {
                Debug.LogError("LobbyPlayer could not find a NATLobbyManager. The LobbyPlayer requires a NATLobbyManager object to function. Make sure that there is one in the scene.");
            }
        }

        public void SendReadyToBeginMessage()
        {
            if (LogFilter.logDebug) { Debug.Log("NATLobbyPlayer SendReadyToBeginMessage"); }

            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                var msg = new NATTraversal.NATLobbyManager.LobbyReadyToBeginMessage();
                msg.slotId = (byte)playerControllerId;
                msg.readyState = true;
                lobby.client.Send(MsgType.LobbyReadyToBegin, msg);
            }
        }

        public void SendNotReadyToBeginMessage()
        {
            if (LogFilter.logDebug) { Debug.Log("NATLobbyPlayer SendReadyToBeginMessage"); }

            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                var msg = new NATTraversal.NATLobbyManager.LobbyReadyToBeginMessage();
                msg.slotId = (byte)playerControllerId;
                msg.readyState = false;
                lobby.client.Send(MsgType.LobbyReadyToBegin, msg);
            }
        }

        public void SendSceneLoadedMessage()
        {
            if (LogFilter.logDebug) { Debug.Log("NATLobbyPlayer SendSceneLoadedMessage"); }

            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                var msg = new IntegerMessage(playerControllerId);
                lobby.client.Send(MsgType.LobbySceneLoaded, msg);
            }
        }

        void OnLevelWasLoaded()
        {
            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                // dont even try this in the startup scene
                string loadedSceneName = SceneManager.GetSceneAt(0).name;
                if (loadedSceneName == lobby.lobbyScene)
                {
                    return;
                }
            }

            if (isLocalPlayer)
            {
                SendSceneLoadedMessage();
            }
        }

        public void RemovePlayer()
        {
            if (isLocalPlayer && !m_ReadyToBegin)
            {
                if (LogFilter.logDebug) { Debug.Log("CustomNatMatchMakingLobby RemovePlayer"); }

                ClientScene.RemovePlayer(GetComponent<NetworkIdentity>().playerControllerId);
            }
        }

        // ------------------------ callbacks ------------------------

        public virtual void OnClientEnterLobby()
        {
        }

        public virtual void OnClientExitLobby()
        {
        }

        public virtual void OnClientReady(bool readyState)
        {
        }

        // ------------------------ Custom Serialization ------------------------

        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            // dirty flag
            writer.WritePackedUInt32(1);

            writer.Write(m_Slot);
            writer.Write(m_ReadyToBegin);
            return true;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            var dirty = reader.ReadPackedUInt32();
            if (dirty == 0)
                return;

            m_Slot = reader.ReadByte();
            m_ReadyToBegin = reader.ReadBoolean();
        }

        // ------------------------ optional UI ------------------------

        void OnGUI()
        {
            if (!ShowLobbyGUI)
                return;

            var lobby = NetworkManager.singleton as CustomNatMatchMakingLobby;
            if (lobby)
            {
                if (!lobby.showLobbyGUI)
                    return;

                string loadedSceneName = SceneManager.GetSceneAt(0).name;
                if (loadedSceneName != lobby.lobbyScene)
                    return;
            }

            Rect rec = new Rect(100 + m_Slot * 100, 200, 90, 20);

            if (isLocalPlayer)
            {
                string youStr;
                if (m_ReadyToBegin)
                {
                    youStr = "(Ready)";
                }
                else
                {
                    youStr = "(Not Ready)";
                }
                GUI.Label(rec, youStr);

                if (m_ReadyToBegin)
                {
                    rec.y += 25;
                    if (GUI.Button(rec, "STOP"))
                    {
                        SendNotReadyToBeginMessage();
                    }
                }
                else
                {
                    rec.y += 25;
                    if (GUI.Button(rec, "START"))
                    {
                        SendReadyToBeginMessage();
                    }

                    rec.y += 25;
                    if (GUI.Button(rec, "Remove"))
                    {
                        ClientScene.RemovePlayer(GetComponent<NetworkIdentity>().playerControllerId);
                    }
                }
            }
            else
            {
                GUI.Label(rec, "Player [" + netId + "]");
                rec.y += 25;
                GUI.Label(rec, "Ready [" + m_ReadyToBegin + "]");
            }
        }
    }
}
#endif
#endif