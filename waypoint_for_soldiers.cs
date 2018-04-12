using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint_for_soldiers : MonoBehaviour
{

    public static Transform[] waypointsforsoldiers;

    void Awake()
    { 
       waypointsforsoldiers = new Transform[transform.childCount];

        for (int i=0;i<waypointsforsoldiers.Length;i++)
        {
            waypointsforsoldiers[i] = transform.GetChild(i);
        }

    }
}
