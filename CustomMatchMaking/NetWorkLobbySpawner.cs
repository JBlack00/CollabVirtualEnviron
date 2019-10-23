using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NATTraversal
{
    public class NetWorkLobbySpawner : MonoBehaviour
    {
        [Header("*LOBBY STUFF*")]
        [SerializeField] public GameObject LobbyPrefab;
        [SerializeField] public Transform SpawnPos;
        [SerializeField] public Transform Parent;
        [SerializeField] public GameObject ReadyScreen;
        public CustomUILobbyHUD Script;

       
        
        // Start is called before the first frame update
         void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Spawn()
        {
         
        }
        
    }
}
