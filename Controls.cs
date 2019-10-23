using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using Mirror;

public class Controls : NetworkBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;
    public float vertical;
    public float horizontal;
    public GameObject directionObject;

    public bool CanMove = true;
    // Start is called before the first frame update
    void Start()
    {
        

        if (!isLocalPlayer)
            return;
        rb = GetComponent<Rigidbody>();
          directionObject = GameObject.Find("Camera");
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawns");
        int rng = Random.Range(0,3);
        transform.position = spawns[rng].transform.position;
       // directionObject = transform.GetChild(0).gameObject;
    }

    // Update is called o\\nce per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        if (CanMove)
        {
            CmdMove();
        }
      
    }
    
    void CmdMove()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        // CurMovespeed = moveSpeed - (moveSpeed * 0.3f);
        rb.velocity = (transform.forward * vertical) * moveSpeed * Time.fixedDeltaTime + (transform.right * horizontal) * moveSpeed * Time.fixedDeltaTime;
        Quaternion newRotation = Quaternion.LookRotation(directionObject.transform.forward);
        newRotation.x = 0;
        newRotation.z = 0;

        gameObject.transform.rotation = newRotation;
       

    }
 
}
