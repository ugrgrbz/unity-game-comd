using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warriorhealth : MonoBehaviour {


    public float Health = 150f;
    public Transform nully;
    private GameObject[] damagers;
    private GameObject Claw;
    float damage_range;
    

    void Start()
    {

        InvokeRepeating("warinteract", 0f, 0.7f);
    }
    void FixedUpdate()
    {
        if (Health >= 0)
        {
            if (nully != null)
            {
                Health -= 15 * Time.deltaTime;
            }

        }

        else
        {
            // just incase
        }

    }
    //detacts closest claw
    public virtual void warinteract()
    {
        damagers = GameObject.FindGameObjectsWithTag("claw");
        Claw = null;
        float shortdist = Mathf.Infinity;

        foreach (GameObject g in damagers)
        {
            float dist = Vector3.Distance(this.transform.position, g.transform.position);
            if (dist < 3f)
            {

                shortdist = dist;
                Claw = g;
            }

        }
        if ((Claw != null) && (shortdist <= 3f))
        {
            nully = Claw.transform;

        }
        else
        {
            nully = null;
        }



    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

}
