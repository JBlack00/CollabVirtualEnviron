using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class csToText : MonoBehaviour
{
    public Text TextObj;
    public TextAsset TestObj;
    public string stringTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stringTest = TestObj.text;
        TextObj.text = stringTest;
    }
}
