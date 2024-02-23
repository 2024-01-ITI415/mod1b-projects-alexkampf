using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S;

    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    void Awake()
    {
        S = this; //set singleton
        //get ref to line renderer
        line = GetComponent<LineRenderer>();
        //disable linerenderer until needed
        line.enabled = false;
        //initiate list
        points = new List<Vector3>();
    }
    
    //property, method masking as field
    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                //when _poi is something new, reset everything
                line.enabled = false;
                points = new List<Vector3> ();
                AddPoint();
            }
        }
    }
    //can be used to clear line directly
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        //called to add a point to the line
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist) {
            //return if point isnt far enough from last point
            return;
        }
        if(points.Count == 0 )//if this is launch point
        {
            Vector3 launchPosDiff = pt - SlingShot.LAUNCH_POS;
            points.Add (pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            //set first 2 points
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        } else {
            //normal beahvior of adding a point
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition( points.Count - 1, lastPoint );
            line.enabled = true;
        }
    }
    
    //returns location of most recently added point
    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                //if no points, vector3.zero is returned
                return(Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }
    void FixedUpdate()
    {
        if (poi == null) 
        {
            //search for poi if there isnt one
            if(FollowCam.POI != null)
            {
                if(FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                } else {
                    return; //no POI found
                }
            } else {
                return; //no POI found
            }
        }
        //if there is a POI, location added every fixed update 
        AddPoint();
        if(FollowCam.POI == null)
        {
            //Once followcam poi is null, make local poi null
            poi = null;
        }
    }
}
