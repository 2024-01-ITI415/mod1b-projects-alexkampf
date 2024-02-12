using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;

    [Header("Set Dynamically")]
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;
    }
    void FixedUpdate()
    {
        //if no POI, return
        if (POI == null) return;

        //get POI pos
        Vector3 destination = POI.transform.position;
        //force destination z to camz to keep cam far enough
        destination.z = camZ;
        //set camera
        transform.position = destination;
    }
}
