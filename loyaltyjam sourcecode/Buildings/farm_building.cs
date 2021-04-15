using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farm_building : MonoBehaviour
{



    //loyalty drain variables
    private int l_amount = -1;
    private float l_delay = 15;
    private float l_tick = 0f;
    //
    float delay = 5f;
    float tick;
    float construction_time = 10f;
    Construction construct;
    // Start is called before the first frame update
    
   
    void Awake()
    {
        construct = this.gameObject.GetComponent<Construction>();
        construct.change_delay(construction_time);
    }
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (construct.is_constructed() == true)
        {
           // food_produce(10, 5);
            increase_loyalty(2, 5);
        }
        //logic here
        
        

    }

    
    void increase_population(int x)
    {
        GameObject.Find("Game_controller").GetComponent<game_manager>().population_max += x;
    }

    void increase_loyalty(int l_amount, float l_delay)
    {

        l_tick += Time.deltaTime;
        //Debug.Log("L_tick:" + l_tick + "L_Delay" + l_delay);
        if (l_tick > l_delay)
        {

            
            
                //Debug.Log("gainLoyal");
            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += l_amount;
            l_tick = 0f;

          
            
        }


    }




}
