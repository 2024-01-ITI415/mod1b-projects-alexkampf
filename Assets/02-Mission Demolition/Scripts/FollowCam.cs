using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;
    }
    void FixedUpdate()
    {
        //if no POI, return
        //if (POI == null) return;

        //get POI pos
        // Vector3 destination = POI.transform.position;
        Vector3 destination;
        //if no POI, return to P: 0,0,0
        if (POI == null)
        {
            destination = Vector3.zero;
        } else
        {
            //get POI pos
            destination = POI.transform.position;
            //if POI is a projectile, check if its at rest
            if(POI.tag == "Projectile")
            {
                //if not moving
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    //return to default view
                    POI = null;
                    //next update
                    return;
                }
            }
        }
        //limit x & y to min values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //interpolate from current cam pos to destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //force destination z to camz to keep cam far enough
        destination.z = camZ;
        //set camera
        transform.position = destination;
        //set orthographicSize of cam to keep ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }
}
