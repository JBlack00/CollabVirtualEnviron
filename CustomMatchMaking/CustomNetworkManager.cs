﻿#if !DISABLE_NAT_TRAVERSAL
using NATTraversal;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;

[HelpURL("http://nattraversal.noblewhale.com/docs/class_n_a_t_traversal_1_1_network_manager.html")]
public class CustomNetworkManager : NATTraversal.NetworkManager
{

#if !UNITY_5_3
    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        int matchCount = matchList.Count;
        MatchInfoSnapshot match = null;
#else
    public override void OnMatchList(ListMatchResponse matchList)
    {
        bool success = matchList.success;
        int matchCount = matchList.matches.Count;
        MatchDesc match = null;
#endif
        if (!success)
        {
            Debug.Log("Failed to retrieve match list");
            return;
        }

        if (matchCount == 0)
        {
            Debug.Log("Match list is empty");
            return;
        }

#if !UNITY_5_3
        match = matchList[0];
#else
        match = matchList.matches[0];
#endif

        if (match == null)
        {
            Debug.Log("Match list is empty");
            return;
        }

        Debug.Log("Found a match, joining");

        matchID = match.networkId;
        StartClientAll(match);
    }

    public void OnHostClick()
    {
        if (matchMaker == null) matchMaker = gameObject.AddComponent<NetworkMatch>();
        StartMatchMaker();

        StartHostAll("Hello World", customConfig ? (uint)(maxConnections + 1) : matchSize);
    }

    public void OnJoinClick()
    {
        if (matchMaker == null) matchMaker = gameObject.AddComponent<NetworkMatch>();
        StartMatchMaker();
#if !UNITY_5_3
        matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
#else
            matchMaker.ListMatches(0, 10, "", OnMatchList);
#endif
    }

    public void OnDisconnectClick()
    {
        if (NetworkServer.active)
        {
            NetworkServer.SetAllClientsNotReady();
            StopHost();
        }
        else
        {
            StopClient();
        }
    }

    public void OnSendToAllClick()
    {
        if (GUI.Button(new Rect(10, 310, 150, 100), "Send To All"))
        {
            NetworkServer.SendToAll(MsgType.OtherTestMessage, new EmptyMessage());
        }
    }

    /*
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Host"))
        {
            if (matchMaker == null) matchMaker = gameObject.AddComponent<NetworkMatch>();
            StartMatchMaker();

            StartHostAll("Hello World", customConfig ? (uint)(maxConnections + 1) : matchSize);
        }
        if (GUI.Button(new Rect(10, 110, 150, 100), "Join"))
        {
            if (matchMaker == null) matchMaker = gameObject.AddComponent<NetworkMatch>();
            StartMatchMaker();
#if !UNITY_5_3
            matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
#else
            matchMaker.ListMatches(0, 10, "", OnMatchList);
#endif
        }
        if (GUI.Button(new Rect(10, 210, 150, 100), "Disconnect"))
        {
            if (NetworkServer.active)
            {
                NetworkServer.SetAllClientsNotReady();
                StopHost();
            }
            else
            {
                StopClient();
            }
        }

        if (NetworkServer.active)
        {
            if (GUI.Button(new Rect(10, 310, 150, 100), "Send To All"))
            {
                NetworkServer.SendToAll(MsgType.OtherTestMessage, new EmptyMessage());
            }
        }

        GUI.Label(new Rect(10, 410, 300, 30), "Is connected to Facilitator: " + natHelper.isConnectedToFacilitator);

        if (NetworkClient.active && !NetworkServer.active)
        {
            if (directClient != null && directClient.isConnected)
            {
                GUI.Label(new Rect(10, 450, 300, 30), "Client connected directly");
            }
            else if (punchthroughClient != null && punchthroughClient.isConnected)
            {
                GUI.Label(new Rect(10, 450, 300, 30), "Client connected via punchthrough");
            }
            else if (relayClient != null && relayClient.isConnected)
            {
                GUI.Label(new Rect(10, 450, 300, 30), "Client connected via relay");
            }
        }
    }
    */
    public override void OnDoneConnectingToFacilitator(ulong guid)
    {
        if (guid == 0)
        {
            Debug.Log("Failed to connect to Facilitator");
        }
        else
        {
            Debug.Log("Facilitator connected");
        }
    }

    private void OnTestMessage(NetworkMessage netMsg)
    {
        Debug.Log("Received test message");
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("on server add player: " + playerControllerId);
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        NetworkServer.RegisterHandler(MsgType.OtherTestMessage, OnTestMessage);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        conn.RegisterHandler(MsgType.OtherTestMessage, OnTestMessage);
    }
}


#endif