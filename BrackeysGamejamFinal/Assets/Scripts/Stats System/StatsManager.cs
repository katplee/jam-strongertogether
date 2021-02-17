using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    /// <summary>
    /// base the instance on the other script associated with the game object
    /// so, the first line below will be changed to something like
    /// TryGetComponent<Dragon>(out Dragon dragon); that's inside a switch case
    /// </summary>
    
    Dragon dragon = new Dragon(Element.ElementType.DRAGON);    

    private void Start()
    {
        Debug.Log(dragon.FireAttack);
        
    }
}
