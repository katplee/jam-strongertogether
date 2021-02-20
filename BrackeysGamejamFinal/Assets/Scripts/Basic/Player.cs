using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <notes>
/// kat 210219: trying something out for the scene management thing and the HUD task
/// so I made player a child class of element
/// </notes>

public class Player : Element
{
    private Rigidbody2D rb2d;
    public float speed;

    private int direction, legion, dieForm;
    private Animator anim;
    public bool isChoosingTame = false;

    private bool reloading = false, dead = false, armed;

    public override ElementType Type
    {
        get { return ElementType.PLAYER; }
    }

    private Scene currentScene;
    public string attackScene = "AttackScene";

    private void Awake()
    {
        base.Start(); //kat added this!        
    }

    // Start is called before the first frame update
    protected new void Start()
    {
        SceneTransition.JustAfterSceneTransition += MovePlayerToPreTransPosition;

        rb2d = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>(); WHEN WE GET ANIMATION SPRITES
        direction = 0;
        legion = -1;
    }

    private void OnDestroy()
    {
        SceneTransition.JustAfterSceneTransition -= MovePlayerToPreTransPosition;
    }

    ///in transitioning back to the basic scene,
    ///the 
    private void MovePlayerToPreTransPosition()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == attackScene) { return; }

        if (PlayerData.Instance == null) { return; }

        transform.position = PlayerData.Instance.playerBasicPosition;
        Debug.Log(PlayerData.Instance.playerBasicPosition);

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
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == attackScene) { return; }
        #endregion
        if (isChoosingTame)
        {
            if (rb2d.velocity.magnitude != 0)
            {
                rb2d.velocity = Vector2.zero;
            }
            return;
        }


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
