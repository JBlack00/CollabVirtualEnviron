using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NATTraversal { 
public class ColourUISelect : NetworkBehaviour
{
    [SyncVar]
    public Color ColorOfUI;

    public GameObject RValueObj;
    public GameObject GValueObj;
    public GameObject BValueObj;

    public float RValue;
    public float GValue;
    public float BValue;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    // Update is called once per frame
    void Update()
    {
        RValue = RValueObj.GetComponent<Slider>().value;
        GValue = GValueObj.GetComponent<Slider>().value;
        BValue = BValueObj.GetComponent<Slider>().value;

        UpdateColour();
    }
    public  void UpdateColour()
    {
        ColorOfUI = new Color(RValue, GValue, BValue, 1);
        GetComponent<Image>().color = ColorOfUI;
        GameObject.Find("LocalPlayer").GetComponent<PlayerCommands>().CmdUpdateColour(ColorOfUI);
    }
}
}
