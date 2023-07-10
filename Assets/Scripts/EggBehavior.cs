using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    static private GreenArrowBehavior sGreenArrow1 = null;
    static public void SetGreenArrow(GreenArrowBehavior g) { sGreenArrow1 = g; }


    private const float kEggSpeed = 40f;
    // Start is called before the first frame update
    void Start()
    {
    }

     //Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);

        CameraSupport s = Camera.main.GetComponent<CameraSupport>();  // Try to access the CameraSupport component on the MainCamera
        if (s != null)   // if main camera does not have the script, this will be null
        {
            Bounds myBound = GetComponent<Renderer>().bounds;  // this is the bound of the collider defined on GreenUp
            CameraSupport.WorldBoundStatus status = s.CollideWorldBound(myBound);

            if (status != CameraSupport.WorldBoundStatus.Inside)
            {
                Debug.Log("Touching the world edge: " + status);
                // now let's re-spawn ourself somewhere in the world
                Destroy(transform.gameObject);  // kills self
                sGreenArrow1.OneLessEgg();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "plane")
        {
            Destroy(transform.gameObject);  // kills self
            sGreenArrow1.OneLessEgg();

        }
    }
}

