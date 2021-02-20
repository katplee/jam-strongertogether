using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [Header("Transition Values")]
    public string sceneName = "AttackScene";
    public SceneTransition sceneTransition;

    [HideInInspector]
    public bool interacting = false, press=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!interacting && press && Input.GetKeyDown(KeyCode.E))
        {        
            interacting = true;
            sceneTransition.FadeTo(sceneName);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            press = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            press = false;
        }
    }
}
