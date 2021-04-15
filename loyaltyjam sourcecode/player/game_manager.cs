using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour
{
    public int game_mode;
    public static game_manager Instance { get; private set; }
    private float tick;
    private float l_penalty = 15f;
    private float delay = 1f;
    //loyalty will only range from 0 to 100%
    public int loyalty { get { return loyalty; } }
    public int current_loyalty = 0;
    public int max_loyalty = 1000; // each orb will be worth 20
    //Resources
    public int food;
    public int qued_food;
    public int displayed_food;
    public int castle_LV = 1;
    public int treasure;
    public int qued_treasure;
    public int displayed_treasure;
    public int num_of_donations;
    public int wood;
    public int qued_wood;
    public int displayed_wood;

    bool press;
    public int population = 0;
    public int population_max = 4;

    public int happiness = 0;
    public int qued_loyalty = 0;


    public int church_level_2;
    public int black_smith_level_2;
    public int church_level_3;
    public int black_smith_level_3;
    public int houses;
    public int farms;
    public int lumberyards;
    public int churches;
    public int markets;
    public int taverns;
    public int parks;
    public int towers;
    public int blacksmiths;
    public int current_happiness;
    public float calculate_happiness_tick;
    
    // Start is called before the first frame update



    private void Awake()
    {
        
    }


    //going to play around with procederal generation
    //
    
    void Start()
    {
        
        
        food = 250;
        treasure = 500;
        wood = 250;
        qued_food = 250;
        qued_treasure = 500;
        qued_wood = 250;
        StartCoroutine(food_que());
        StartCoroutine(wood_que());
        StartCoroutine(treasure_que());

    }

    public void loyalty_change(int amount)
    {
        current_loyalty = Mathf.Clamp(current_loyalty + amount, 0, max_loyalty);
        
        loyalty_meter.instance.SetValue(current_loyalty / (float)max_loyalty);
    }
    public void food_change(int amount)
    {
        food = Mathf.Clamp(food + amount, 0, 99999);

        
    }
    public void wood_change(int amount)
    {
        wood = Mathf.Clamp(wood + amount, 0, 99999);


    }
    public void treasure_change(int amount)
    {
        treasure = Mathf.Clamp(treasure + amount, 0, 99999);


    }

    private IEnumerator food_que()
    {
        while (true)
        {

            if (qued_food < 0)
            {
                qued_food++;
                displayed_food--;

                //tick = 0f;
            }
            if (qued_food > 0)
            {
                qued_food--;
                displayed_food++;

            }
            if (qued_food <= 5)
            {
                yield return new WaitForSeconds(0.005f);
            }
            
            else
            {
                yield return new WaitForSeconds(0.005f);
            }
        }
    }
    private IEnumerator wood_que()
    {
        while (true)
        {
            
            if (qued_wood < 0)
            {
                qued_wood++;
                displayed_wood--;

                //tick = 0f;
            }
            if(qued_wood > 0)
            {
                qued_wood--;
                displayed_wood++;
                
            }

            if (qued_wood >= 5)
            {
                yield return new WaitForSeconds(0.005f);
            }
            
            else
            {
                yield return new WaitForSeconds(0.005f);
            }


        }
    }
    private IEnumerator treasure_que()
    {
        while (true)
        {

            if (qued_treasure < 0)
            {
                qued_treasure++;
                displayed_treasure--;

                //tick = 0f;
            }
            if (qued_treasure > 0)
            {
                qued_treasure--;
                displayed_treasure++;

            }
            
            if (qued_treasure <= 5)
            {
                yield return new WaitForSeconds(0.005f);
            }
            
            else
            {
                yield return new WaitForSeconds(0.005f);
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        happiness_calculation();
        loyalty_change(0);
        // Debug.Log("qued: " + qued_loyalty + " tick: " + tick + " delay: " + delay + "= "+current_loyalty);
        tick += Time.deltaTime;
        if (tick > 0.01f)
        {
            if(current_loyalty == max_loyalty)
            {
                qued_loyalty = 0;
            }
            if (qued_loyalty > 0)
            {
                qued_loyalty--;
                loyalty_change(1);
                tick = 0f;
            }
            if (qued_loyalty < 0)
            {
                qued_loyalty++;
                loyalty_change(-1);
                tick = 0f;
            }
            if(qued_loyalty == 0)
            {
                //loyalty_change(1);
                tick = 0f;
            }
            



        }
        

        //if(food < 0)
        //{
            
        //    l_penalty += Time.deltaTime;
        //    if (tick < l_penalty)
        //    {
        //        l_penalty = 0f;
        //        qued_loyalty -= 1;
        //    }
        //}
        
        //the only way I coould figure out how to initalize this value.
       // if(current_loyalty == 0)
        //{

        //    loyalty_change(1);
       // }

        if(population > population_max)
        {
            //population--;
        }
        
    }
    void happiness_calculation()
    {

        calculate_happiness_tick += Time.deltaTime;
        //Debug.Log(calculate_happiness_tick + "/60");
        if (calculate_happiness_tick > 60)
        {
            //always startover and check for it
            int x = 0;

            //more farms than house + 1 else - 1
            if (farms > houses)
            {
                x++;
            }
            else
            {
                x--;
            }

            for (int p = 0; p < parks; p++)
            {
                x += 2;
            }

            for (int c = 0; c < churches; c++)
            {
                x += 1;
            }
            for (int m = 0; m < markets; m++)
            {
                x += 1;
            }

            int l = (-1 * (Mathf.RoundToInt(lumberyards) / 3));
            l += x;

            int t = Random.Range(-2, 2);

            t += x;

            int tw = (Mathf.RoundToInt(towers) / 2);
            tw += x;
            //Debug.Log(x);
            current_happiness = x;
            calculate_happiness_tick = 0f;
            qued_loyalty += happiness * 2;
            if(parks > 1)
            {
                qued_loyalty += (happiness * 3) + 30;
            }
        }


    }


}
