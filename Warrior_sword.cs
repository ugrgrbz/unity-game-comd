using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior_sword : MonoBehaviour
{

    public Transform Soldierwaypoints;
    public Transform Monsterr;
    private GameObject[] monsters;
    GameObject closest_monster;
    public float range = 8f;
    private int waypointindex = 0;
    public NavMeshAgent S_agent;
    bool readyforcombat;
    Warriorhealth wh;
    Animator anim;

    void Start()
    {
        
        wh = GetComponent<Warriorhealth>();
        anim = GetComponent<Animator>();
        Soldierwaypoints = waypoint_for_soldiers.waypointsforsoldiers[0];
        S_agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Choose_Monster", 0f, 0.5f);
    }
    //Looking target with quaternians
    public virtual void LookMonster()
    {
        if(Monsterr != null)
        {
            Vector3 direction = (Monsterr.position - this.transform.position).normalized;
            Quaternion looking = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, looking, Time.deltaTime * 4f);
        }
            
       

    }

    //choosing nearest enemy
    public virtual void Choose_Monster()
    {
        monsters = GameObject.FindGameObjectsWithTag("monster");
        float shortdist = Mathf.Infinity;
        GameObject closest_monster = null;

        foreach (GameObject trgts in monsters)
        {
            float dist = Vector3.Distance(this.transform.position, trgts.transform.position);

            if (dist < shortdist)
            {
                shortdist = dist;
                closest_monster = trgts;
            }

        }

        if ((closest_monster != null) && (shortdist <= range))
        {
            Monsterr = closest_monster.transform;
        }

        else
        {
            Monsterr = null;
        }

    }


    private void FixedUpdate()
    {  if(wh.Health>=10)
        {
            //if there is no target in the range
            if (Monsterr == null)
            {
                readyforcombat = false;

                if (readyforcombat == false)
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", true);
                    S_agent.isStopped = false;
                    S_agent.SetDestination(Soldierwaypoints.position);

                    if (Vector3.Distance(transform.position, Soldierwaypoints.position) < 1f)
                    {
                        NextPoint();
                    }
                    return;
                }

            }
            //if creature finds one
            else
            {
                readyforcombat = true;

                if (readyforcombat)
                {
                    S_agent.SetDestination(Monsterr.position);
                    LookMonster();
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isAttacking", false);

                    if (Vector3.Distance(this.transform.position, Monsterr.position) >= 1.5f)
                    { S_agent.isStopped = false; }

                    else
                    { S_agent.isStopped = true; }
                  
                 
                  
                }
            }
        }
        else
        {
            wh.Die();

        }
       
    }

    private void OnDrawGizmos()
    {
        if (Monsterr == null)
        {  //if creature cannot find a target
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
        else
        {  //if creature find a target
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, range);
        }

    }

    private void NextPoint()
    {
        waypointindex = Random.Range(0, Waypoints.wps.Length);

        if (waypointindex >= Waypoints.wps.Length-1)
        {
            waypointindex = Random.Range(0, Waypoints.wps.Length-1);
        }

        Soldierwaypoints = waypoint_for_soldiers.waypointsforsoldiers[waypointindex];

    }
}
