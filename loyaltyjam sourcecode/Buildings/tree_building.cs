using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_building : MonoBehaviour
{

    public int hp = 50;



    
    public void change_hp(int amt)

    {
        amt = hp;
        if (hp < 0)
        {
            Destroy(this.gameObject);
        }
    }
    void dosomething()
    {
        Debug.Log("Hit");
    }
    
}
