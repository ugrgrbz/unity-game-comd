using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class villager_movement : MonoBehaviour {

    Transform escapepoint;
    float range = 5f;
    public Transform Monster_arrived;
    public NavMeshAgent villager;
    public GameObject[] attackers;
    GameObject Monster;
    bool detected;
    Animator anim;

    Warriorhealth wh;
    private void Start()
    {
       wh = GetComponent<Warriorhealth>();
        anim = GetComponent<Animator>();
        escapepoint = Escape_Point.escapepoint[0];
        villager = GetComponent<NavMeshAgent>();
        InvokeRepeating("DetectedByMonster", 0f, 0.5f);
    }

    private void FixedUpdate()
    {
        if (wh.Health >= 5)
        {
            if (Monster_arrived == null)
            {
                detected = false;
                if (!detected)
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isTerrified", true);
                    villager.isStopped = false;
                    villager.SetDestination(escapepoint.transform.position);
                    // if villager escapes
                    if (Vector3.Distance(this.transform.position, escapepoint.transform.position) < 3f)
                    {

                        Game_User.lastScore += 200;
                        Destroy(this.gameObject);

                    }
                }
            }
            else
            {
                detected = true;
                if (detected)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isTerrified", false);
                    villager.isStopped = true;
                    LookToMonster();

                }

            }
        }
        else
        {
            //Goodbye cruel world :(
            Game_User.lastScore -= 100;
            wh.Die();
        }

        
    }
       private void DetectedByMonster()
    {
        attackers = GameObject.FindGameObjectsWithTag("monster");
        float shortdist = Mathf.Infinity;
        Monster = null;
   
        foreach (GameObject monster in attackers)
        {
            float dist = Vector3.Distance(this.transform.position, monster.transform.position);
            if (dist<shortdist)
            {
                shortdist = dist;
                Monster = monster;
            }
        }
        if((Monster!=null)&&(shortdist<=range))
        {           
            Monster_arrived = Monster.transform;
        }
        else
        {
            Monster_arrived = null;
        }

    }

        private void LookToMonster()
    {
        Vector3 direction = (Monster_arrived.position - this.transform.position).normalized;
        Quaternion looking = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, looking, Time.deltaTime * 4f);
    }

}