using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMatchController : MonoBehaviour
{
    public Text NameOfLobby;

    private void Awake()
    {
        NameOfLobby = transform.GetChild(2).GetChild(0).GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateName(string text)
    {
       // NameOfLobby.text = text;
    }
}
