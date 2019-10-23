using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUTORegistration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CallRegister();
    }
    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", "nope" );
        form.AddField("password", "123fgdf45678");
        //WWW www = new WWW("https://db123infane.000webhostapp.com/register.php", form);
        WWW www = new WWW("http://localhost:8889/register.php", form);
        
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("User created successfully.");

        }
        else
        {
            Debug.Log("www text" + www.text);
        }
    }
    public void VerifyInputs()
    {

    }
}
