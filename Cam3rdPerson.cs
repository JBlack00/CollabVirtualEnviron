using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace NATTraversal
{
    public class Cam3rdPerson : NetworkBehaviour
    {
        [SerializeField]
        private float hightOffset = 5;
        [SerializeField]
        private const float MinYAngle = 0f;
        [SerializeField]
        private const float MaxYAngle = 35f;

        [SerializeField]
        private float MinDistance = 6;
        [SerializeField]
        private float MaxDistance = 10;

        public Transform lookAt;
        public Transform TheCamera;
        [SerializeField]
        private Camera cam;
        [SerializeField]
        private float distance = 10f;
        [SerializeField]
        private float curX = 0f;
        [SerializeField]
        private float curY = 0f;
        [SerializeField]
        private float sensitivityX = 4;
        [SerializeField]
        private float sensitivityY = 1;

        [SerializeField]
        private float smooth = 1000;

        private GameObject Parent;



        // Start is called before the first frame update
        void Start()
        {

            if (!transform.parent.GetComponent<Controls>().isLocalPlayer)
            {
                gameObject.SetActive(false);
                return;
            }

            Parent = transform.parent.gameObject;
            TheCamera = transform;
            cam = this.GetComponent<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            lookAt = transform.parent;


            transform.parent = null;
            // this.gameObject.transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + hightOffset, transform.parent.position.z);
        }

        private void LateUpdate()
        {
            if (!Parent.GetComponent<Controls>().isLocalPlayer)
                return;
        }

        // Update is called once per frame
        void Update()
        {
            if (!Parent.GetComponent<Controls>().isLocalPlayer)
                return;

            Vector3 dir = new Vector3(0, 0, -distance);
            curX += Input.GetAxisRaw("Mouse X");

            curY -= Input.GetAxisRaw("Mouse Y");
            curY = Mathf.Clamp(curY, MinYAngle, MaxYAngle);



            Quaternion targetRotation = Quaternion.Euler(curY, curX, 0);

            //Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smooth * Time.deltaTime);
            //Quaternion rotation = this.transform.rotation;
            // Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, smooth * Time.deltaTime);

            TheCamera.position = new Vector3(Parent.transform.position.x, Parent.transform.position.y + hightOffset, Parent.transform.position.z) + targetRotation * dir;


            TheCamera.LookAt(new Vector3(Parent.transform.position.x, Parent.transform.position.y + hightOffset, Parent.transform.position.z));



            distance -= Input.mouseScrollDelta.y * 0.3f;
            if (distance < MinDistance)
            {
                distance = MinDistance;
            }
            if (distance > MaxDistance)
            {
                distance = MaxDistance;
            }




        }

        //acidentally made this and think its kind of cool
        void DrunkRotationBug()
        {
            Vector3 dir = new Vector3(0, 0, -distance);

            Quaternion targetRotation = Quaternion.Euler(curY, curX, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
            Quaternion rotation = this.transform.rotation;
            TheCamera.position = new Vector3(transform.parent.position.x, transform.parent.position.y + hightOffset, transform.parent.position.z) + rotation * dir;
            TheCamera.LookAt(new Vector3(transform.parent.position.x, transform.parent.position.y + hightOffset, transform.parent.position.z));



            distance -= Input.mouseScrollDelta.y * 0.3f;
            if (distance < MinDistance)
            {
                distance = MinDistance;
            }
            if (distance > MaxDistance)
            {
                distance = MaxDistance;
            }

            curX += Input.GetAxisRaw("Mouse X");
            curY -= Input.GetAxisRaw("Mouse Y");

            curY = Mathf.Clamp(curY, MinYAngle, MaxYAngle);

        }
    }
}
