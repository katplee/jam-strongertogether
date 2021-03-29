using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PStone : MonoBehaviour
{
    public enum StoneType
    {
        FIRE, WATER, WIND, EARTH, BASE
    }

    public StoneType type;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Total Number of Stones" + Inventory.Instance.baseStones);

            //calls the AddStone method @ Inventory
            Inventory.Instance.AddStone(this);

            //then destroys the game object soon after
            Destroy(gameObject);

            Debug.Log("Total Number of Stones" + Inventory.Instance.baseStones);
        }
    }
}
