using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Warriorhealth {

    public static float pLAYER_HEALTH = 300f;
    public float health = 300;

    void Start () {
        InvokeRepeating("warinteract", 0f, 0.7f);
	}

    void healthdecrease()
    {
            health -= 20 * Time.deltaTime;

    }

    void healthincrese()
    {
        if(health<300f)
        {
                health += 10 * Time.deltaTime;
        }

        else
        {
            health = 300;
        }


    }
    public override void warinteract()
    {
        base.warinteract();
    }

    void FixedUpdate () {
       
        if(health>0)
        {
            if(nully!=null)
            {
                healthdecrease();
            }
            else
            {
                healthincrese();
            }

        }
        else
        {
            Debug.Log("dead");
            // SceneManager.LoadScene("dead scene");
        }

    }
}
