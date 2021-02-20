using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// kat 210219: trying something out for the scene management thing and the HUD task
/// so I made player a child class of element
/// </summary>

public class Player : Element
{
    private Rigidbody2D rb2d;
    public float speed;
    //public new float hp;

    private int direction, legion, dieForm;
    private Animator anim;

    private bool reloading = false, dead = false, armed;

    public override ElementType Type
    {
        get { return ElementType.PLAYER; }
    }

    public string attackScene = "AttackScene";

    private void Awake()
    {
        base.Start(); //kat added this!        
    }

    // Start is called before the first frame update
    protected new void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>(); WHEN WE GET ANIMATION SPRITES
        direction = 0;
        legion = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Move();
        }
    }

    private void Move()
    {
        #region kat added this!
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == attackScene) { return; }
        #endregion

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rb2d.velocity = new Vector2(speed * h, speed * v);

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            //anim.SetBool("Move", true);
            legion = -1;
            if (h == 0)
            {
                direction = 0;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            //anim.SetBool("Move", true);
            legion = 1;
            if (h == 0)
            {
                direction = 0;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //anim.SetBool("Move", true);
            direction = -1;
            if (v == 0)
            {
                legion = 0;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //anim.SetBool("Move", true);
            direction = 1;
            if (v == 0)
            {
                legion = 0;
            }
        }
        if (h == 0 && v == 0)
        {
            //anim.SetBool("Move", false);
        }
    }


}
