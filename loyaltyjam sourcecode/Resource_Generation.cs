using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Generation : MonoBehaviour
{


    private int resource;
    private float tick = 0f;
    [SerializeField]private string type;
    [SerializeField] private int amount = 1;
    [SerializeField] private float delay = 1f;
 
    [SerializeField] private int l_amount = 1;
    [SerializeField] private float l_delay = 120;
    private float l_tick = 0f;
    //GameObject p;

   // int temp_lumber = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        // GameObject GM = GameObject.Find("Game_Controller");
        //game_manager gm = GameObject.Find("Game_controller").GetComponent<game_manager>();
    }
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        increase_resource(amount, delay);
        increase_loyalty(l_amount,l_delay);
        
        
    }

    void increase_loyalty(int l_amount, float l_delay)
    {
        l_tick += Time.deltaTime;
        //Debug.Log("L_tick:" + l_tick + "L_Delay" + l_delay);
        if (l_tick > l_delay)
        {
            
            l_tick = 0f;
            GameObject.Find("Game_controller").GetComponent<game_manager>().loyalty_change(1);
            Debug.Log(GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty);
        }
        
    }
    void increase_resource(int value, float new_delay)
    {
        //game_manager resource = GetComponent<game_manager>();
        tick += Time.deltaTime;
        //Debug.Log("tick:" + tick + "/" + delay + "food;" + food);
        if (tick > new_delay)
        {
            tick = 0f;
            if (type == "food")
            {

                // int food = the_player.get_food();
                //food += value;
                //the_player.set_food(food);
                GameObject.Find("Game_controller").GetComponent<game_manager>().food += value;
            }
            if (type == "lumber")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().wood += value;
                

            }
            if (type == "treasure")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().treasure += value;
                //the_player.set_treasure(value);
            }
        }
       
    }
    
}
