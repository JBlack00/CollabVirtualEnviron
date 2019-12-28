using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using Mirror;
namespace NATTraversal
{
    public class Controls : NetworkBehaviour
    {
        public float moveSpeed;
        public Rigidbody rb;
        public float vertical;
        public float horizontal;
        public GameObject directionObject;
        private Animator Ani;
        public VaribleHolder varibleHolderScript;

        public bool CanMove = true;
        // Start is called before the first frame update
        void Start()
        {


            if (!isLocalPlayer)
                return;
            moveSpeed = 500;
            rb = GetComponent<Rigidbody>();
            directionObject = GameObject.Find("Camera");
           
            Ani = GetComponent<Animator>();
           
            // directionObject = transform.GetChild(0).gameObject;
        }

        public void GoToSpawn()
        {
            if (varibleHolderScript == null)
            {
                varibleHolderScript = GameObject.Find("VaribleHolder").GetComponent<VaribleHolder>();
                
                Transform[] spawns = varibleHolderScript.Spawns;
                int rng = Random.Range(0, 3);
                transform.position = spawns[rng].transform.position;
            }
        }
        // Update is called o\\nce per frame
        void Update()
        {
            if (!isLocalPlayer)
                return;
            GoToSpawn();
            if (CanMove)
            {
                CmdMove();
                SetAnimation();
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }

        }
        private void FixedUpdate()
        {
            if (!isLocalPlayer)
                return;
            if (rb.velocity.magnitude > 10f)
            {
                rb.velocity = rb.velocity.normalized * 10f;
            }
            if (vertical == 0 && horizontal == 0)
            {
                rb.velocity = Vector3.zero;
            }
        }

        void CmdMove()
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
            // CurMovespeed = moveSpeed - (moveSpeed * 0.3f);
            //  rb.velocity = (transform.forward * vertical) * moveSpeed * Time.fixedDeltaTime + (transform.right * horizontal) * moveSpeed * Time.fixedDeltaTime + Physics.gravity * Time.fixedDeltaTime;

            //  rb.AddForce(Physics.gravity, ForceMode.VelocityChange);

            //rb.AddForce((transform.forward * vertical) * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            //
            // rb.AddForce((transform.right * horizontal) * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            rb.velocity = (transform.forward * vertical) * (moveSpeed) * Time.fixedDeltaTime + (transform.right * horizontal) * (moveSpeed) * Time.fixedDeltaTime;

            // rb.MovePosition((transform.forward * vertical) * moveSpeed * Time.fixedDeltaTime);
            // rb.MovePosition((transform.right * horizontal) * moveSpeed * Time.fixedDeltaTime);

            //   rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10f);

            Quaternion newRotation = Quaternion.LookRotation(directionObject.transform.forward);
            newRotation.x = 0;
            newRotation.z = 0;

            gameObject.transform.rotation = newRotation;


        }
        void SetAnimation()
        {
            Ani.SetFloat("Vertical", vertical);
            Ani.SetFloat("Horizontal", horizontal);
        }
    }
}
