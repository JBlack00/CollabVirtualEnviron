using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMakingUseDatabase : IPManager
{
    private string IP = GetLocalIPAddress();
    // Start is called before the first frame update
    void Start()
    {
        IP = GetLocalIPAddress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
