using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/*
 * Things to decide on:
 * *when fused with a dragon, how will the attack of the player change?
 * *how will the player's attacks be initialized? there should be some correlation with the current level?
 *      (for now it is same with the enemy)
 */

public class Player : Element
{
    #region Variables to sort
    private int direction, legion, dieForm;
    private bool reloading = false, dead = false, armed;
    #endregion
    
    public override ElementType Type
    {
        get { return ElementType.PLAYER; }
    }    
    
    private Rigidbody2D rb2d;
    public float speed;    
    
    //variables used in the animator
    private Animator anim;
    private float moveHorizontal;
    private float moveVertical;
    private float moveSpeed;
    
    public bool isChoosingTame = false;
       
    private Scene currentScene;
    public string attackScene = "AttackScene";       


    protected override void Start()
    {
        base.Start();
        SubscribeEvents();
        
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        #region Private parameters check
        string playerStats =
            $"dType : {DType}\n" +
            $"hp : {hp}\n" +
            $"maxHP : {maxHP}\n" +
            $"armor : {Armor}\n" +
            $"maxArmor : {maxArmor}\n" +
            $"fireAttack : {fireAttack}\n" +
            $"waterAttack : {waterAttack}\n" +
            $"windAttack : {windAttack}\n" +
            $"earthAttack : {earthAttack}\n" +
            $"baseAttack : {baseAttack}\n";

        Debug.Log(playerStats);
        #endregion
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    protected override void InitializeAttributes()
    {
        //set DType
        DType = Dragon.DragonType.NOTDRAGON;

        //each element's max HP is dependent on the level (temporary fix)
        float elementMaxHP = (GameManager.currLvl * hpLevelFactor) +
            Random.Range(-hpMargin, hpMargin);
        SetStatMaximum(ref maxHP, elementMaxHP);
        hp = maxHP;

        //player's max armor is initialized to 0
        SetStatMaximum(ref maxArmor, 0);
        Armor = maxArmor;

        //determine weakness
        //in the meantime, weakness is randomly generated
        //  but for the final version, we might want to establish a correlation
        //  between the element and its weakness
        //weakness will be set at the moment of instantiation
        int weaknessInd = Random.Range(0, 4);
        Weakness = (WeaknessType)weaknessInd;
    }

    protected override void InitializeAttacks()
    {
        int currentLvl = GameManager.currLvl;
        int attack = Random.Range(1, GameManager.currLvl * attackMargin + 1);

        baseAttack = ((currentLvl == GameManager.baseLevel) ? specialtyAttackMultiplier : 0) * attack;
        fireAttack = ((currentLvl == GameManager.fireLevel) ? specialtyAttackMultiplier : 0) * attack;
        waterAttack = ((currentLvl == GameManager.waterLevel) ? specialtyAttackMultiplier : 0) * attack;
        windAttack = ((currentLvl == GameManager.windLevel) ? specialtyAttackMultiplier : 0) * attack;
        earthAttack = ((currentLvl == GameManager.earthLevel) ? specialtyAttackMultiplier : 0) * attack;
    }

    private void SubscribeEvents()
    {
        SceneTransition.JustAfterSceneTransition += MovePlayerToPreTransPosition;
    }

    private void UnsubscribeEvents()
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
