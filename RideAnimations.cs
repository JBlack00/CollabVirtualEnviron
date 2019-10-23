using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideAnimations : MonoBehaviour
{
    //All animated game objects
    private GameObject ferrisWheel;
    [SerializeField]
    private GameObject carousel;
    public GameObject teacup1;//Spins left
    public GameObject teacup2;//SPins right
    private GameObject teacupPlatform;
    //Select these in the inspector to select what model it is (the script is on the movable part on the corresponding model)
    public bool isCarousel;
    public bool isFerrisWheel;
    public bool isTeacup1;
    public bool isTeacup2;
    public bool isTeacupPlatform;

    public void Start()
    {
     //   carousel = GameObject.FindGameObjectWithTag("Carousel");
       // ferrisWheel = GameObject.FindGameObjectWithTag("FerrisWheel");
       // teacupPlatform = GameObject.FindGameObjectWithTag("TeacupPlatform");
    }

    // Update is called once per frame
    void Update()
    {
        CheckRide();
    }

    public void CheckRide()
    {
        if(isFerrisWheel)
        {
            gameObject.transform.Rotate(Vector3.right, 10.0f * Time.deltaTime);
        }
        if (isCarousel)
        {
          gameObject.transform.Rotate(Vector3.down, 20.0f * Time.deltaTime);
        }
        if (isTeacup1)
        {
            gameObject.transform.Rotate(Vector3.up, 40.0f * Time.deltaTime);
        }
        if (isTeacup2)
        {
            gameObject.transform.Rotate(Vector3.down, 40.0f * Time.deltaTime);
        }
        if (isTeacupPlatform)
        {
            gameObject.transform.Rotate(Vector3.down, 20.0f * Time.deltaTime);
        }
    }
}
