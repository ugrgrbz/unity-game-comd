using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villagerspawner : MonoBehaviour {


    public Transform[] villager;
    public Transform instanciate_point;
    public float Swapn_time = 10f;
    private float countdown = 3f;
    int i;
    
    void Start () {
        i = Random.Range(0, villager.Length);
        //InvokeRepeating("Spawn", Swapn_time, Swapn_time);
    
	}
    void FixedUpdate()
    {

        if(countdown<=0)
        {
            
            SpawnWave();

            countdown = Swapn_time;

        }
        countdown -= Time.deltaTime;
    }

    private void SpawnWave () {

        Instantiate(villager[i], instanciate_point.position, instanciate_point.rotation);
        	
	}

}
