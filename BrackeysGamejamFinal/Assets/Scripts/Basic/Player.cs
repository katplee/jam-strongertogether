using System;
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

    [SerializeField] private float moveHorizontal;
    [SerializeField] private float moveVertical;
    [SerializeField] private float moveSpeed;

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
        anim = GetComponent<Animator>(); 
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
            moveHorizontal = 0;
            moveVertical = -1;
            moveSpeed = 1f;            
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveHorizontal = 0;
            moveVertical = 1;
            moveSpeed = 1f;            
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;
            moveVertical = 0;
            moveSpeed = 1f;            
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;
            moveVertical = 0;
            moveSpeed = 1f;            
        }
        else
        {   
            moveSpeed = 0f;
        }        

        anim.SetFloat("Horizontal", moveHorizontal);
        anim.SetFloat("Vertical", moveVertical);
        anim.SetFloat("Speed", moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckForVictories(collision.gameObject);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");
            Element enemy = collision.gameObject.GetComponent<Element>();
            LastEnemyData.Instance.SaveEnemyData(enemy);
        }
    }

    private void CheckForVictories(GameObject enemyGO)
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == attackScene) { return; }

        if (LastEnemyData.enemyDefeated == false) { return; }

        LastEnemyData.enemyDefeated = false;
        Destroy(enemyGO);
    }
}
