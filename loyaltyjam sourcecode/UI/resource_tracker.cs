using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resource_tracker : MonoBehaviour
{
    public GameObject loyalty_amt_txt_obj;
    public Text loyal_amt;
    [SerializeField] private string type;
    private Text resource_text;
    // Start is called before the first frame update
    void Start()
    {
        
        resource_text = this.GetComponent<Text>();
        loyal_amt = this.GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        

        if (type == "food")
        {
            
            resource_text.text = GameObject.Find("Game_controller").GetComponent<game_manager>().displayed_food.ToString();
        }
        if (type == "wood")
        {
            resource_text.text = GameObject.Find("Game_controller").GetComponent<game_manager>().displayed_wood.ToString();


        }
        if (type == "treasure")
        {
            resource_text.text = GameObject.Find("Game_controller").GetComponent<game_manager>().displayed_treasure.ToString();
            //the_player.set_treasure(value);
        }

        if(type == "population")
        {
            resource_text.text = GameObject.Find("Game_controller").GetComponent<game_manager>().population.ToString();
        }
        if(type == "population_max")
        {
            if(GameObject.Find("Game_controller").GetComponent<game_manager>().population >= GameObject.Find("Game_controller").GetComponent<game_manager>().population_max)
            {
                resource_text.text = GameObject.Find("Game_controller").GetComponent<game_manager>().population_max.ToString();
                resource_text.color = Color.red;
            }
            else
            {
                resource_text.text = GameObject.Find("Game_controller").GetComponent<game_manager>().population_max.ToString();
                resource_text.color = Color.black;
            }

            
        }
        if(type == "loyal")
        {
            loyal_amt.text = GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty.ToString() + " / " + GameObject.Find("Game_controller").GetComponent<game_manager>().max_loyalty.ToString();

        }

    }

    public void loyal_hover_on()
    {
       
        

    }
    public void loyal_hover_off()
    {
     
    }
}
