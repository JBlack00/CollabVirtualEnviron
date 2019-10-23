using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

public class IPManager : MonoBehaviour
{
   
    public static string GetLocalIPAddress()
    {
        return new WebClient().DownloadString("http://icanhazip.com/");
    }
}

