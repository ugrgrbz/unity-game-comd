using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] wps;

    void Awake ()
    { // this code have taken from Brackeys. Unfortunately without his methods i couldn't handle to create a patrol.
        wps = new Transform[transform.childCount];

        for (int i=0;i<wps.Length;i++)
            {
                wps[i] = transform.GetChild(i);
            }

    }
}
