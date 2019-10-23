using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaribleHolder : MonoBehaviour
{

    public GameObject InteractImage;
    public GameObject InteractImage2;
    public GameObject Projecter;
    public GameObject TextBox;

    public Camera MainCam;


    // Start is called before the first frame update
    void Awake()
    {
        InteractImage = GameObject.Find("InteractUI");
        InteractImage2 = GameObject.Find("interactEandQ");
        Projecter = GameObject.Find("ButtonPillarInteract");
        //OnlyRaycasetVersion
        //MainCam = GameObject.Find("Camera").GetComponent<Camera>();


        TextBox = GameObject.Find("TextBox");

        InteractImage.SetActive(false);
        InteractImage2.SetActive(false);

        TextBox.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
