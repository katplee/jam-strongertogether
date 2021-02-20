using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//kat 210220: made pet dragon into a child of dragon class

public class PetDragon : Dragon
{
    private Rigidbody2D rb2d;
    public float speed;
    private float angle;
    public bool detect;
    private Vector3 rec, rotation;
    public GameObject target;    
    private Animator anim;

    private void Awake()
    {
        base.Start(); //kat added this!
    }

    protected new void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        rec = target.transform.position;
        speed = target.GetComponent<Player>().speed;
        anim = GetComponent<Animator>();        
    }
        
    void Update()
    {
        if (target != null)
        {
            Move();
            //Direction();         
        }
    }

    void Move()
    {

        if (Vector2.Distance(transform.position, target.transform.position)>=1.7f)
        {
            rec = (target.transform.position - transform.position).normalized * speed * 1.2f;
            rb2d.velocity = new Vector2(rec.x, rec.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }

    }

    /*
    void Direction()
    {
        rotation = (target.transform.position - transform.position).normalized;
        angle = Mathf.Atan2(rotation.y, rotation.x);
        angle = angle * (180 / Mathf.PI);

        if (angle < 157.5 && angle > 112.5)
        {
            anim.SetInteger("Direction", -1);
            anim.SetInteger("Legion", 1);
        }
        else if (angle < 112.5 && angle > 67.5)
        {
            anim.SetInteger("Direction", 0);
            anim.SetInteger("Legion", 1);

        }
        else if (angle < 67.5 && angle > 22.5)
        {
            anim.SetInteger("Direction", 1);
            anim.SetInteger("Legion", 1);

        }
        else if (angle < 22.5 && angle > -22.5)
        {
            anim.SetInteger("Direction", 1);
            anim.SetInteger("Legion", 0);

        }
        else if (angle < -22.5 && angle > -67.5)
        {
            anim.SetInteger("Direction", 1);
            anim.SetInteger("Legion", -1);

        }
        else if (angle < -67.5 && angle > -112.5)
        {
            anim.SetInteger("Direction", 0);
            anim.SetInteger("Legion", -1);

        }
        else if (angle < -112.5 && angle > -157.5)
        {
            anim.SetInteger("Direction", -1);
            anim.SetInteger("Legion", -1);

        }
        else
        {
            anim.SetInteger("Direction", -1);
            anim.SetInteger("Legion", 0);

        }
    }
    */

}
