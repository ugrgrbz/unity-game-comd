using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_movement : Warrior_sword {
    public Camera cammy;
    float speed = 14.5f;
    Rigidbody rigid;
    Transform tr;
    Animator anima;
    Transform enemies;
    public float horizontalSpeed = 3.0f;

    PlayerHealth ph;

    void Start() {
        anima = GetComponent<Animator>();
        ph = GetComponent<PlayerHealth>();
        enemies = Monsterr;
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        InvokeRepeating("Choose_Monster", 0f, 2f);
    }
    public void rotateMouse()
    {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0f, h, 0f);
    }
    public override void Choose_Monster()
    {

        base.Choose_Monster();
    }
    
    public override void LookMonster()
    {
        base.LookMonster();
    }

    private void animControlWalk()
    {
            anima.SetBool("isIdling", true);
            anima.SetBool("isAttacking",false );
            anima.SetBool("isWalking", false);
    }

    private void animControlAttack()
    {
                
            anima.SetBool("isIdling", false);
            anima.SetBool("isAttacking", true);
            anima.SetBool("isWalking", false);
           
    }
    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;
      
        transform.position += transform.forward * Time.deltaTime * speed*moveVertical;
        transform.position += transform.right * Time.deltaTime * speed*moveHorizontal/2;

        if (Input.GetButton("Vertical"))
        {
            animControlWalk();
        }

        else
        {
            anima.SetBool("isIdling", false);
            anima.SetBool("isAttacking", false);
            anima.SetBool("isWalking", true);

        }

        if (Monsterr==null)
            {
            rotateMouse();
            }

        else
            {
                    if (Vector3.Distance(this.transform.position, Monsterr.position) < 2f)
                    {
                        LookMonster();
                    }

                    else
                    {
                        rotateMouse();
            }
            }
        

    }
    
    private void Attack()
    {
        animControlAttack();
    }
    private void FixedUpdate () {

        if (ph.Health > 5)
        {
            Move();
            if (Input.GetButton("Fire1"))
            {
                animControlAttack();
            }
            else
            {
                anima.SetBool("isIdling", false);
                anima.SetBool("isAttacking", false);
                anima.SetBool("isWalking", true);
            }


        }
    }
}
