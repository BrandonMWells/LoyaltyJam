using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Build_menu : MonoBehaviour
{


    public Grid grid;
    public Prince the_prince;
    //tooltip


    //build_button_References
    public GameObject House_build_B;
    public GameObject Farm_build_B;
    public GameObject Lumberyard_build_B;
    public GameObject Tavern_Build_B;
    public GameObject Church_Build_B;
    public GameObject BlackSmith_Build_B;
    public GameObject Marketplace_Build_B;
    public GameObject Park_Build_B;
    public GameObject GuardTower_Build_B;

    public GameObject Repair_button;
    public GameObject Donate_button;

    //windows//
    //public GameObject Window_Pane;
    [SerializeField] flashImage _flashImage = null;
    [SerializeField] Color _newColor = Color.white;
    public Text Status_txt;
    public Text add_txt;
    public int number_of_stars;
    public Image[] stars;
    public Sprite empty_star;
    public Sprite full_star;
    public Sprite org_sprite;
    public Image Status_image;
    public GameObject Window_object;
    //-Building_Window-
    //public GameObject building_status;
    public GameObject Building_window;
    public GameObject Building_window_tooltip;
    
    public GameObject Information_pane;

    public Text building_tool_tip_text;
    public string building_tool_tip;
    //-Status_Window-
    public GameObject Status_Window;
    //public GameObject Upgrade_pane;
    public GameObject sell_button;
    public GameObject button_pane;
    public GameObject Status_Text;
    //public GameObject Window_Pane;

    //-Status/Upgrade_window_pane-

    //Menu_Window
    public GameObject Menu_Window;
    //public GameObject Sell_menu_button;
    //Raycast 
    private Vector3 target_pos;

    //Building Costs
    private int wood_cost;
    private int food_cost;
  //  private int treasure_cost;
    private int loyalty_cost;
    private int population_cost;



    //Building Objects Reference
    public GameObject Castle;
    public GameObject farm;
    public GameObject house;
    public GameObject lumberyard;
    public GameObject Tavern;


    public GameObject Park;
    public GameObject Chapel;
    public GameObject Market;
    public GameObject blacksmith;
    public GameObject tower;
    //public GameObject sell;
    public GameObject building_plot;
    public Sprite exploded_plot;
    //tooltips list

    //Object references
    [SerializeField] float org_x;
    [SerializeField] float org_y;
    [SerializeField] GameObject building_Selected;
    [SerializeField] public GameObject org_obj;
    [SerializeField] Vector3 org_pos;



    [SerializeField] Transform original_plot_position;
    public Sprite selected_mouse_obj;
    [SerializeField]private LayerMask tile_filter;

    //Bools
    private bool can_place_down;
    private bool building_mode;
    private bool sellable;
    private bool selected;
    private bool can_click;
    private bool for_status;

    //sounds
    public AudioClip explosion;
    public AudioClip cancel;
    public AudioClip blip;
    public AudioClip build_noise;


    //System
    private SpriteRenderer spriteRenderer;
    public AudioSource my_audio;

    // Start is called before the first frame update
    void Start()
    {

        Repair_button.GetComponent<Button>().interactable = false;
        Donate_button.SetActive(false);
        House_build_B.SetActive(false);
         Farm_build_B.SetActive(false);
        Tavern_Build_B.SetActive(false);
        Church_Build_B.SetActive(false);
        BlackSmith_Build_B.SetActive(false);
        Marketplace_Build_B.SetActive(false);
        Park_Build_B.SetActive(false);
        GuardTower_Build_B.SetActive(false);
        //Set my bools
        can_click = true;
        Menu_Window.SetActive(false);
        Status_Window.SetActive(false);
        Building_window.SetActive(false);
        Building_window_tooltip.SetActive(false);
       // Status_image = GetComponent<Image>();
        //Call Hover_off
        hover_off();

        //Get components
        original_plot_position = gameObject.GetComponent<Transform>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        //Find my game_manager
        GameObject.Find("Game_controller").GetComponent<game_manager>();
        
    }


    void Update()
    {
        if (GameObject.Find("Game_controller").GetComponent<game_manager>().castle_LV > 1)
        {
            if (BlackSmith_Build_B.activeSelf == false)
            {
                BlackSmith_Build_B.SetActive(true);
            }
            if (Church_Build_B.activeSelf == false)
            {
                Church_Build_B.SetActive(true);
            }
        }
        if (GameObject.Find("Game_controller").GetComponent<game_manager>().castle_LV > 2)
        {
            if (Marketplace_Build_B.activeSelf == false)
            {
                Marketplace_Build_B.SetActive(true);
            }
            
        }
        if (GameObject.Find("Game_controller").GetComponent<game_manager>().castle_LV > 3)
        {
            Debug.Log("Why are you doing this to me");
            if (Park_Build_B.activeSelf == false)
            {
                Park_Build_B.SetActive(true);
            }

        }
        if (GameObject.Find("Game_controller").GetComponent<game_manager>().lumberyards >=1)
        {
            if (Farm_build_B.activeSelf == false)
            {
                Farm_build_B.SetActive(true);
            }
            if (House_build_B.activeSelf == false)
            {
                House_build_B.SetActive(true);
            }
            if (Tavern_Build_B.activeSelf == false)
            {
                Tavern_Build_B.SetActive(true);
            }
        }
        if (GameObject.Find("Game_controller").GetComponent<game_manager>().blacksmiths > 0)
        {

            //Debug.Log("stop");
            Repair_button.SetActive(true);
            


        }
        if (GameObject.Find("Game_controller").GetComponent<game_manager>().houses > 2)
        {

            //Debug.Log("stop");
            if (GuardTower_Build_B.activeSelf == false)
            {
                GuardTower_Build_B.SetActive(true);
            }



        }


        if (Input.GetMouseButtonDown(0))
        {

            if (can_click == true)
            {
                if (selected == false)
                {
                    Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mouse_position.x = Mathf.Round(mouse_position.x + 0.5f) - 0.5f;
                    mouse_position.y = Mathf.Round(mouse_position.y + 0.5f) - 0.5f;
                    Vector2 screen_pos = new Vector2(mouse_position.x, mouse_position.y);
                    RaycastHit2D hit = Physics2D.Raycast(screen_pos, Vector2.zero);
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        if (hit)
                        {
                            //Debug.Log("Object hit:" + hit.collider.gameObject);
                            org_obj = hit.collider.gameObject;
                            org_pos = hit.collider.gameObject.transform.localPosition;
                            
                            if(hit.collider.gameObject.tag == "Status")
                            {
                                org_obj = hit.collider.gameObject;
                                org_pos = hit.collider.gameObject.transform.localPosition;
                                org_sprite = org_obj.GetComponent<SpriteRenderer>().sprite;
                                Window_object.GetComponent<Image>().sprite = org_sprite;
                                add_txt.text = org_obj.GetComponent<building_status>().upgrade_descriptions[org_obj.GetComponent<building_status>().upgrade_level];
                                //Menu_Window.SetActive(true);
                                status_on();
                                
                                Status_txt.text = org_obj.GetComponent<building_status>().building_type + "\n" + "HP:" +
                                org_obj.GetComponent<building_status>().HP + "/" +
                                org_obj.GetComponent<building_status>().MHP;
                                can_click = false;
                                if (GameObject.Find("Game_controller").GetComponent<game_manager>().blacksmiths > 0)
                                {
                                    Repair_button.GetComponent<Button>().interactable = true;
                                }
                                if (org_obj.GetComponent<building_status>().building_type== "Castle")
                                {
                                    Status_txt.text = org_obj.GetComponent<building_status>().building_type;
                                    sell_button.GetComponent<Button>().interactable = false;
                                    Repair_button.GetComponent<Button>().interactable = false;
                                    Donate_button.SetActive(false);
                                }
                                else
                                {
                                    
                                    Donate_button.SetActive(false);
                                    sell_button.GetComponent<Button>().interactable = true;
                                    
                                }
                                
                                if (org_obj.GetComponent<building_status>().building_type == "Church")
                                {
                                    Donate_button.SetActive(true);
                                    Donate_button.GetComponent<Button>().interactable = true;
                                }
                                else
                                {
                                    Donate_button.GetComponent<Button>().interactable = false;
                                    Donate_button.SetActive(false);
                                   

                                }
                                
                                


                                for_status = true;
                                Building_Select(org_obj);
                                for_status = false;
                            }
                            if (hit.collider.gameObject.tag == "placeable")
                            {
                                the_prince.hammertime = true;
                                the_prince.set_move(false);
                                org_obj = hit.collider.gameObject;
                                org_pos = hit.collider.gameObject.transform.localPosition;
                                org_obj.GetComponent<SpriteRenderer>().material.color = Color.green;
                                //Destroy(hit.collider.gameObject);

                                //Instantiate(farm, screen_pos,Quaternion.identity);
                                Build_Menu_on();
                                selected = true;

                            }




                        }
                    }


                }
            }


        }
        



        }


    public void build(GameObject type)
    {
        int x = 1;
        //Building 2.0
        if (building_Selected == null)
        {
            
        }
        else
        {
            if (building_Selected == house)
            {
                if (GameObject.Find("Game_controller").GetComponent<game_manager>().wood < building_Selected.GetComponent<building_status>().wood_value ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().food < (building_Selected.GetComponent<building_status>().food_value) ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().treasure < building_Selected.GetComponent<building_status>().treasure_value ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty < building_Selected.GetComponent<building_status>().loyalty_value)
                {
                    Debug.Log("You do not have enough resources!");
                    my_audio.clip = cancel;
                    my_audio.Play();
                    x = 0;
                }
            }
            else
            {
                if (GameObject.Find("Game_controller").GetComponent<game_manager>().wood < building_Selected.GetComponent<building_status>().wood_value ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().food < (building_Selected.GetComponent<building_status>().food_value) ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().treasure < building_Selected.GetComponent<building_status>().treasure_value ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty < building_Selected.GetComponent<building_status>().loyalty_value ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().population >= GameObject.Find("Game_controller").GetComponent<game_manager>().population_max)
                {
                    Debug.Log("You do not have enough resources!");
                    my_audio.clip = cancel;
                    my_audio.Play();
                    x = 0;
                }
            }

            if (x == 1)
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().wood_change(-1 * (building_Selected.GetComponent<building_status>().wood_value));
                GameObject.Find("Game_controller").GetComponent<game_manager>().food_change(-1 * (building_Selected.GetComponent<building_status>().food_value));
                GameObject.Find("Game_controller").GetComponent<game_manager>().treasure_change(-1 * (building_Selected.GetComponent<building_status>().treasure_value));
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure -= building_Selected.GetComponent<building_status>().treasure_value;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_food -= building_Selected.GetComponent<building_status>().food_value;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood -= building_Selected.GetComponent<building_status>().wood_value;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= building_Selected.GetComponent<building_status>().loyalty_value;
                GameObject.Find("Game_controller").GetComponent<game_manager>().population += building_Selected.GetComponent<building_status>().population_value;
                //Delete and place down new building
                Destroy(org_obj);
                Instantiate(building_Selected, org_pos, Quaternion.identity);

                //Play building noise
                my_audio.clip = build_noise;
                my_audio.Play();

                //shut build menu off
                Build_Menu_off();

                //play hammer animation
                the_prince.hammertime = false;

                //reset mouse pointer
                selected = false;
                building_Selected = null;

                //allow player to move again
                the_prince.set_move(true);
                //Apply effects
            }
        }
    }


    //public bool population_check()
    //{
    //    //if population is greater than population max return false

    //    if (GameObject.Find("Game_controller").GetComponent<game_manager>().population >= GameObject.Find("Game_controller").GetComponent<game_manager>().population_max)
    //    {
    //        Debug.Log("population check failed");
    //        if(building_Selected.GetComponent<building_status>().population_value == 0)
    //        {
    //            return true;
    //        }
    //        my_audio.clip = cancel;
    //        my_audio.Play();
    //        return false;

    //    }

    //    else
    //    {
    //        //Debug.Log("population check succeeeded");
    //        //return true and pay the population cost
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().population += building_Selected.GetComponent<building_status>().population_value;
    //        return true;
    //    }
    //   // Debug.Log("Population:" + GameObject.Find("Game_controller").GetComponent<game_manager>().population + "/" + GameObject.Find("Game_controller").GetComponent<game_manager>().population_max);
    //    //Debug.Log("I hit the cconditional and do nothing");
    //}

    //looks for the gameobject and compares the cost to the values from building selected.
    //public bool payment_check(){

    //    // check if any of the resources are non existant
    //    if (GameObject.Find("Game_controller").GetComponent<game_manager>().wood < building_Selected.GetComponent<building_status>().wood_value||
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().food < (building_Selected.GetComponent<building_status>().food_value) ||
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().treasure < building_Selected.GetComponent<building_status>().treasure_value ||
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty < building_Selected.GetComponent<building_status>().loyalty_value
    //        )
    //    {

    //        //return false if not
    //        Debug.Log("You do not have enough resources!");
    //        my_audio.clip = cancel;
    //        my_audio.Play();
    //        return false;

    //    } else {
    //        //else pay for the cost and return true
    //        //Debug.Log("Paying for building...");

    //        GameObject.Find("Game_controller").GetComponent<game_manager>().wood_change(-1*(building_Selected.GetComponent<building_status>().wood_value));

    //        GameObject.Find("Game_controller").GetComponent<game_manager>().food_change(-1 * (building_Selected.GetComponent<building_status>().food_value));
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().treasure_change(-1*(building_Selected.GetComponent<building_status>().treasure_value));

    //        GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure -= building_Selected.GetComponent<building_status>().treasure_value;
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().qued_food -= building_Selected.GetComponent<building_status>().food_value;
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood -= building_Selected.GetComponent<building_status>().wood_value;
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty  -= building_Selected.GetComponent<building_status>().loyalty_value;



    //        return true;

    //    }
    //}
    //public bool upgrade_pay()
    //{

    //    // check if any of the resources are non existant
    //    if (GameObject.Find("Game_controller").GetComponent<game_manager>().wood < building_Selected.GetComponent<building_status>().wood_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level] ||
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().food < building_Selected.GetComponent<building_status>().food_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level] ||
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().treasure < building_Selected.GetComponent<building_status>().treasure_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level] ||
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty < building_Selected.GetComponent<building_status>().loyalty_cost[building_Selected.GetComponent<building_status>().upgrade_level] 
    //        )
    //    {

    //        //return false if not
    //        Debug.Log("You do not have enough resources!");
    //        my_audio.clip = cancel;
    //        my_audio.Play();
    //        return false;

    //    }
    //    else
    //    {
    //        //else pay for the cost and return true
    //        Debug.Log("Paying for building...");
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().wood_change(-1*( building_Selected.GetComponent<building_status>().wood_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level]));
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().food_change(-1* ( building_Selected.GetComponent<building_status>().food_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level]));
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().treasure_change (-1*( building_Selected.GetComponent<building_status>().treasure_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level]));
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= building_Selected.GetComponent<building_status>().loyalty_cost[building_Selected.GetComponent<building_status>().upgrade_level];
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure -= building_Selected.GetComponent<building_status>().treasure_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().qued_food -= building_Selected.GetComponent<building_status>().food_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood -= building_Selected.GetComponent<building_status>().wood_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
    //        building_Selected.GetComponent<building_status>().food_value += building_Selected.GetComponent<building_status>().food_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
    //        building_Selected.GetComponent<building_status>().wood_value += building_Selected.GetComponent<building_status>().wood_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
    //        building_Selected.GetComponent<building_status>().treasure_value += building_Selected.GetComponent<building_status>().treasure_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
    //        GameObject.Find("Game_controller").GetComponent<game_manager>().population_max += this.gameObject.GetComponent<building_status>().house_pop_value;
    //        return true;

    //    }
    //}
    public void sell_back()
    {
        if (org_obj.GetComponent<Construction>().upgrading == true)
        {

        }
        else
        {
            if (org_obj.GetComponent<Construction>().is_constructed())
            {
                org_obj.GetComponent<building_status>().building_count(-1);
            }


            GameObject new_plot = Instantiate(building_plot, org_pos, Quaternion.identity);
            new_plot.GetComponent<SpriteRenderer>().sprite = exploded_plot;

            _flashImage.Startflash(1f, .8f, Color.white);
            my_audio.clip = explosion;
            my_audio.Play();
            //

            Build_Menu_off();
            the_prince.hammertime = false;
            selected = false;
            building_Selected = null;
            the_prince.set_move(true);

            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_food += (org_obj.GetComponent<building_status>().food_value / 2);
            GameObject.Find("Game_controller").GetComponent<game_manager>().food_change((org_obj.GetComponent<building_status>().food_value) / 2);
            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure += (org_obj.GetComponent<building_status>().treasure_value / 2);
            GameObject.Find("Game_controller").GetComponent<game_manager>().treasure_change((org_obj.GetComponent<building_status>().treasure_value) / 2);
            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood += (org_obj.GetComponent<building_status>().wood_value / 2);
            GameObject.Find("Game_controller").GetComponent<game_manager>().wood_change((org_obj.GetComponent<building_status>().wood_value) / 2);


            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= 5;
            menu_off();

            // Debug.Log("Max_Population_value"+org_obj.GetComponent<building_status>().max_population_value);


            GameObject.Find("Game_controller").GetComponent<game_manager>().population -= org_obj.GetComponent<building_status>().population_value;
            GameObject.Find("Game_controller").GetComponent<game_manager>().population_max -= org_obj.GetComponent<building_status>().house_pop_value;
            Destroy(org_obj);
        }

    }

    public void Building_Info(int select)
    {
        Building_window_tooltip.SetActive(true);

        switch (select)
        {
            //farm
            case 1:
                building_tool_tip_text.text = " Farm \n 50 Wood \n 25 treasure \n Description:  Produces food with each upgrade giving more yield, this building takes up 2 population slots.";
                break;

            //House
            case 2:
                building_tool_tip_text.text = "House \n 25 Wood \n 25 food \n Description:  Produces 2 Population and can upgrade to a total of 6 population.  Requires food to maintain this building.";
                break;

            //Lumberyard
            case 3:
                building_tool_tip_text.text = "Lumberyard \n 25 wood \n 50 treasure \nDescription:  Produces wood, very durable building will  stick around for a while even if food supplies are low. \n this building takes up 2 population slots.";
                break;

            //Tavern
            case 4:
                building_tool_tip_text.text = "Tavern \n 75 wood \n 100 food \n " +
                "Description: Will slowly produce treasure at a slow rate upgrades cost a fortune " +
                "\n this building takes up 2 population slots.";
                break;
            //Blacksmith
            case 5:
                building_tool_tip_text.text = "Blacksmith " +
                "\n 100 wood \n 50 treasure \n 250 food \n " +
                "Description: Will produce treasure quickly but consume food really quickly, also grants access to the ability to repair damaged buildings" +
                "\n this building takes up 2 population slots.";
                break;
          //Churches
            case 6:
                building_tool_tip_text.text = "Church \n 100 wood \n 100 Treasure \n 250 food \n " +
                "Description: Allows you to donate treasure for loyalty, helps keep people happy produces loyality extremely slowly " +
                "\n this building takes up 1 population slots.";
                break;
          //Markets
            case 7:
                building_tool_tip_text.text = "Market \n 150 wood \n 150 Treasure \n 150 Food \n " +
                "Description: Produces resources at random and helps keep people happy, however, building too many can be ineffecient." +
                "\n this building takes up 2 population slots.";
                break;
            //Park
            case 8:
                building_tool_tip_text.text = "GuardTower \n 250 wood \n " +
                "Slowly generates loyalty has no upgrades. \n this building takes up 2 population slots.";
                break;
            case 9:
                building_tool_tip_text.text = "Park \n 1000 wood \n " +
                "Description: Takes up no building slots and upgrading drastically increases your loyalty, it also helps keep people really happy. \n takes no population";
                break;
            //Park
            default:
                Debug.Log("Error");
                break;
            




        }
        }
    

    public void Building_Select(GameObject select)
    {



        
        // Debug.Log("Select"+select);
        building_Selected = select;
        
        if (for_status == false) {
            selected = true;
            Building_window_tooltip.SetActive(true);
        }
        if(select == farm)
        {
            Building_Info(1);
        }
        if (select == house)
        {
            Building_Info(2);
        }
        if (select == lumberyard)
        {
            Building_Info(3);
        }
        if (select == Tavern)
        {
            Building_Info(4);
        }
        if (select == blacksmith)
        {
            Building_Info(5);
        }
        if(select == Chapel)
        {
            Building_Info(6);
        }
        if (select == Market)
        {
            Building_Info(7);
        }
        if (select == tower)
        {
            Building_Info(8);
        }
        if (select == Park)
        {
            Building_Info(9);
        }




    }

    //Repair Function
    public void Repair()
    {
        if (org_obj.GetComponent<building_status>().building_type == "Castle")
        {
            add_txt.text = "This building cannot be repaired";
            Status_txt.text = org_obj.GetComponent<building_status>().building_type + "\n" + "HP:" +
                                org_obj.GetComponent<building_status>().HP + "/" +
                                org_obj.GetComponent<building_status>().MHP;
        }
        else
        {
            if (org_obj.GetComponent<building_status>().HP >= org_obj.GetComponent<building_status>().MHP)
            {
                Status_txt.text = org_obj.GetComponent<building_status>().building_type + "\n" + "HP:" +
                                org_obj.GetComponent<building_status>().HP + "/" +
                                org_obj.GetComponent<building_status>().MHP;
                add_txt.text = "This building is not damaged your majesty... ";
                    }
            else
            {

                int cost = (org_obj.GetComponent<building_status>().MHP - org_obj.GetComponent<building_status>().HP);
                if (GameObject.Find("Game_controller").GetComponent<game_manager>().wood > cost && cost > 0)
                {
                    GameObject.Find("Game_controller").GetComponent<game_manager>().wood -= cost;
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood -= cost;
                    org_obj.GetComponent<building_status>().HP = org_obj.GetComponent<building_status>().MHP;
                    Status_txt.text = org_obj.GetComponent<building_status>().building_type + "\n" + "HP:" +
                                org_obj.GetComponent<building_status>().HP + "/" +
                                org_obj.GetComponent<building_status>().MHP;
                    status_window_off();
                    
                }
                else
                {
                    Status_txt.text = org_obj.GetComponent<building_status>().building_type + "\n" + "HP:" +
                                org_obj.GetComponent<building_status>().HP + "/" +
                                org_obj.GetComponent<building_status>().MHP;
                    add_txt.text = "You do not have enough resources to repair this building. \n  Total Repair cost: " + (org_obj.GetComponent<building_status>().HP - (5 * GameObject.Find("Game_controller").GetComponent<game_manager>().blacksmiths) - 30);
                }

                if(cost < 0)
                {
                    add_txt.text = "This building is free to repair but you shouldn't be able to really see this message, please report to developer";
                    org_obj.GetComponent<building_status>().HP = org_obj.GetComponent<building_status>().MHP;

                }
                //Play hammer sound
            }
        }
        
            
        //Repair buildings health using wood.
        //get the selected objects HP 
        //Get difference in wood
        //Flat price of 1HP <-> 1 Wood  + flat rate that is lower per level of blacksmith.
        //Castle cannot be repaired unless in Arcade mode
        //close menu can click etc
    }
    public void Repair_info()
    {
        if (org_obj.GetComponent<building_status>().building_type == "Castle")
        {
            Status_txt.text = org_obj.GetComponent<building_status>().building_type;
        }
        else
        {
            Status_txt.text = org_obj.GetComponent<building_status>().building_type + "\n" + "HP:" +
                                org_obj.GetComponent<building_status>().HP + "/" +
                                org_obj.GetComponent<building_status>().MHP;
            add_txt.text = "Total Repair cost = " + (org_obj.GetComponent<building_status>().MHP - org_obj.GetComponent<building_status>().HP) ;
        }
    }
    public void Donate()
    {

        if (GameObject.Find("Game_controller").GetComponent<game_manager>().treasure < 100)
        {
            add_txt.text = "You do not seem to have the funds on you... maybe next time.";
        }  else
        {
            add_txt.text = "Praise the goddess!";
            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 15 +  (org_obj.GetComponent<building_status>().upgrade_level + 5);
            GameObject.Find("Game_controller").GetComponent<game_manager>().num_of_donations += 1;
            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure -= 100;
            GameObject.Find("Game_controller").GetComponent<game_manager>().treasure -= 100;
            if(GameObject.Find("Game_controller").GetComponent<game_manager>().num_of_donations > 3)
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
            }
            if (GameObject.Find("Game_controller").GetComponent<game_manager>().num_of_donations > 5)
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 2;
                GameObject.Find("Game_controller").GetComponent<game_manager>().happiness += 1;
            }
            if (GameObject.Find("Game_controller").GetComponent<game_manager>().num_of_donations > 15)
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().happiness += 2;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 5;
            }
        }
        
        //Spend Treasure to buy loyalty
            //100 Treasure for 100 loyalty
            // 80 treasure for 100 loyalty
            // 70 for 100 loyalty

            //close menu can click etc
    }
    public void Donate_info()
    {
        add_txt.text = " We would appreciate any donations you can give your majesty! Could you spare 100 gold from your treasury?";
        if(org_obj.GetComponent<building_status>().upgrade_level >=3)
        {

            add_txt.text = "We would appreciate any donations you can give your majesty! Could you spare 100 gold from your treasury? " +
                "\n She has also said you have made "+ GameObject.Find("Game_controller").GetComponent<game_manager>().num_of_donations + " Number of donations. " +
                "\n " +
                "\n The great water goddess wanted  you to know that your loyalty to her  and the kingdom is " +  GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty;
        }
        //Spend Treasure to buy loyalty
        //100 Treasure for 100 loyalty
        // 80 treasure for 100 loyalty
        // 70 for 100 loyalty

        //close menu can click etc
    }

    //GUI
    public void building_off()
    {
        building_Selected = null;
    }

    //    public void build(GameObject type)
    //    {

    //        //check if you have enough funds..
    //       // Debug.Log("x"); 
    //        if (building_Selected == null)
    //        {

    //}
    //        else
    //        {
    //            //if population is capped and building selected is not a house.
    //            if(building_Selected != house)
    //            {
    //                //check for population and payment.
    //                if(population_check())
    //                {
    //                    //Debug.Log("I Passed my pop check");

    //                    if(payment_check())
    //                    {
    //                        Destroy(org_obj);

    //                        //GameObject.Find("confirm_construction").GetComponent<>


    //                        Instantiate(building_Selected, org_pos, Quaternion.identity);
    //                        Build_Menu_off();
    //                        the_prince.hammertime = false;
    //                        selected = false;
    //                        building_Selected = null;
    //                        the_prince.set_move(true);


    //                    }
    //                    else
    //                    {
    //                        //refund the population.
    //                        my_audio.clip = cancel;
    //                        my_audio.Play();
    //                        //GameObject.Find("Game_controller").GetComponent<game_manager>().population -= building_Selected.GetComponent<building_status>().population_value;
    //                        the_prince.set_move(false);
    //                    }

    //                }


    //            }else
    //            {

    //                //pay for the house.
    //                if (payment_check())
    //                {
    //                    Destroy(org_obj);
    //                    Instantiate(building_Selected, org_pos, Quaternion.identity);
    //                    my_audio.clip = build_noise;
    //                    my_audio.Play();
    //                    Build_Menu_off();
    //                    the_prince.hammertime = false;
    //                    selected = false;
    //                    building_Selected = null;
    //                    the_prince.set_move(true);

    //                }
    //            }





    //        }


    //    }



    public void hover_off()
    {
        if (selected == true)
        {
            
        }
        else
        {
            Building_window_tooltip.SetActive(false);
        }
    }
    public void hover()
    {

        building_tool_tip_text.text = "ooo nothing selected...";
        Building_window_tooltip.SetActive(true);

    }
    public void Build_Menu_on()
    {
        Building_window.SetActive(true);
        Building_window_tooltip.SetActive(false);
        selected = false;

    }


    public void upgrade()
    {

        if (org_obj.GetComponent<building_status>().upgrade_level < org_obj.GetComponent<building_status>().max_upgrade_level)
        {
            //&& org_obj.GetComponent<Construction>().upgrading == false && org_obj.GetComponent<Construction>().is_constructed() == true)
            if (org_obj.GetComponent<Construction>().is_constructed() == true && org_obj.GetComponent<Construction>().upgrading == false)
            {
                if (GameObject.Find("Game_controller").GetComponent<game_manager>().wood < building_Selected.GetComponent<building_status>().wood_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level] ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().food < building_Selected.GetComponent<building_status>().food_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level] ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().treasure < building_Selected.GetComponent<building_status>().treasure_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level] ||
                    GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty < building_Selected.GetComponent<building_status>().loyalty_cost[building_Selected.GetComponent<building_status>().upgrade_level]) { 
                }else
                {
                    //Pay for building
                    GameObject.Find("Game_controller").GetComponent<game_manager>().wood_change(-1 * (building_Selected.GetComponent<building_status>().wood_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level]));
                    GameObject.Find("Game_controller").GetComponent<game_manager>().food_change(-1 * (building_Selected.GetComponent<building_status>().food_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level]));
                    GameObject.Find("Game_controller").GetComponent<game_manager>().treasure_change(-1 * (building_Selected.GetComponent<building_status>().treasure_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level]));
                    GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty -= building_Selected.GetComponent<building_status>().loyalty_cost[building_Selected.GetComponent<building_status>().upgrade_level];
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure -= building_Selected.GetComponent<building_status>().treasure_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_food -= building_Selected.GetComponent<building_status>().food_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood -= building_Selected.GetComponent<building_status>().wood_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
                    building_Selected.GetComponent<building_status>().food_value += building_Selected.GetComponent<building_status>().food_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
                    building_Selected.GetComponent<building_status>().wood_value += building_Selected.GetComponent<building_status>().wood_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
                    building_Selected.GetComponent<building_status>().treasure_value += building_Selected.GetComponent<building_status>().treasure_upg_cost[building_Selected.GetComponent<building_status>().upgrade_level];
                    //GameObject.Find("Game_controller").GetComponent<game_manager>().population_max += org_obj.gameObject.GetComponent<building_status>().house_pop_value;
                    if (org_obj.GetComponent<building_status>().building_type == "Castle")
                    {
                        _flashImage.Startflash(6f, 1f, Color.white);
                    }
                    org_obj.GetComponent<Construction>().upgrading = true;
                    org_obj.GetComponent<SpriteRenderer>().sprite = org_obj.GetComponent<Construction>().levels[0];
                    org_obj.GetComponent<Construction>().is_sound_played = false;
                    status_window_off();
                }

            }

        }else
        {
            my_audio.clip = cancel;
            my_audio.Play();
        }
        

    }
    public void Build_Menu_off()
    {
        selected = false;
        Building_window_tooltip.SetActive(false);
        Building_window.SetActive(false);
        the_prince.set_move(true);
        the_prince.hammertime = false;
        building_Selected = null;
        org_obj.GetComponent<SpriteRenderer>().material.color = Color.white;


    }
    //Status window States
    public void status_on()
    {
        selected = false;
        Status_Window.SetActive(true);
        button_pane.SetActive(true);
        check_stars();
        //org_obj.GetComponent<SpriteRenderer>();

        // Status_image.sprite = org_obj.GetComponent<Sprite>();

        
        
        //add_txt.text = "test";


        
    }
    
    public void check_stars()
    {

        // status_image = org_obj.GetComponent<Image>();
        
        int upgrade_level = org_obj.GetComponent<building_status>().upgrade_level;
       

        //normalizes the number
        number_of_stars = org_obj.GetComponent<building_status>().max_upgrade_level;
        //int building_id = org_obj.GetComponent<building_status>().building_ID;
        //reset the starlength
        for (int x = 0; x < stars.Length; x++)
        {
            stars[x].enabled = true;
        }

        for (int x = 0; x < stars.Length; x++)
        {
            if (x < number_of_stars)
            {
                stars[x].sprite = empty_star;
            }
            else
            {
                stars[x].enabled = false;
            }


        }
        for (int y = 0; y < upgrade_level; y++)
        {

            if (org_obj.GetComponent<building_status>().upgrade_level >= org_obj.GetComponent<building_status>().max_upgrade_level)
            {
                Debug.Log("ZX");
                //building_id = 0;

                
                //upg_wood_cost.text = org_obj.GetComponent<building_status>().wood_upg_cost[building_id].ToString();
                //upg_treasure_cost.text = org_obj.GetComponent<building_status>().treasure_upg_cost[building_id].ToString();


                stars[y].sprite = full_star;
            }
            else
            {

                
                stars[y].sprite = full_star;

                if (org_obj.GetComponent<building_status>().building_type == "Castle")
                {
                    //add_txt.text = org_obj.GetComponent<building_status>().upgrade_descriptions[org_obj.GetComponent<building_status>().building_ID + y];
                    //building ID + Y
                   // upg_wood_cost.text = org_obj.GetComponent<building_status>().wood_upg_cost[building_id + y].ToString();
                    //upg_treasure_cost.text = org_obj.GetComponent<building_status>().treasure_upg_cost[building_id + y].ToString();

                }
                else
                {

                    //upgrade_desc.text = org_obj.GetComponent<building_status>().upgrade_descriptions[building_id + y];
                    //building ID + Y
                    //upg_wood_cost.text = org_obj.GetComponent<building_status>().wood_upg_cost[building_id + y].ToString();
                   // upg_treasure_cost.text = org_obj.GetComponent<building_status>().treasure_upg_cost[building_id + y].ToString();
                    //y could = the number in list for upgrade
                }
            }

        }
    }
    public void info_text()
    {

        if (org_obj.GetComponent<building_status>().building_type == "Castle")
        {
            add_txt.text = "Ah yes, your magnificiant Castle if only your subjects were loyal to you...";
        }
        else
        {
            add_txt.text = "Total Property Value: \n" +
                       "Wood Value:" + building_Selected.GetComponent<building_status>().wood_value.ToString() + "\n" +
                       "Food Value:" + building_Selected.GetComponent<building_status>().food_value.ToString() + "\n" +
                       "Treasure Value:" + building_Selected.GetComponent<building_status>().treasure_value.ToString() + "\n" + "\n" 
                       ;
        }
                       

        
    }
    public void sell_info()
    {


        if (sell_button.GetComponent<Button>().interactable == false)
        {
            add_txt.text = "This building cannot be sold.";
        }
        else {
            add_txt.text = "Sell back Value \n" +
                           "Wood gain:" + (building_Selected.GetComponent<building_status>().wood_value / 2).ToString() + "\n" +
                           "Food gain:" + (building_Selected.GetComponent<building_status>().food_value / 2).ToString() + "\n" +
                           "Treasure gain:" + (building_Selected.GetComponent<building_status>().treasure_value / 2).ToString() + "\n" + "\n";
        }


    }




    public void status_window_off()
    {
        button_pane.SetActive(false);
        Status_Window.SetActive(false);
        //Information_pane.SetActive(false);
        // Upgrade_pane.SetActive(false);
        //Sell_pane.SetActive(false);
        can_click = true;
    }


    public void sell_on()
    {
        selected = false;
        Status_Window.SetActive(true);
        Information_pane.SetActive(false);
        //Upgrade_pane.SetActive(false);
        //Sell_pane.SetActive(true);
       
    }
    public void upgrade_window_on()
    {
        selected = false;
        Status_Window.SetActive(true);
        //Upgrade_pane.SetActive(true);
        Information_pane.SetActive(false);
        //Sell_pane.SetActive(false);
    }
    public void menu_off()
    {
        

        Menu_Window.SetActive(false);
        Status_Window.SetActive(false);
        can_click = true;
    }

    
    public void close_upgrade_window()
    {

    }

    public void hide_building_buttons()
    {
       // this.gameObject.GetComponentsInChildren<farm_button>().enabled = false;
    }
   
    public void update_button()
    {
        if(org_obj.GetComponent<building_status>().upgrade_level >= org_obj.GetComponent<building_status>().max_upgrade_level)
        {
            add_txt.text = "You cannot upgrade any further";
            
        }
        else
        {
            if (org_obj.GetComponent<building_status>().building_type == "Castle")
            {
                add_txt.text = "Upgrade Materials Needed \n" +
                                   "Wood Cost:" + org_obj.GetComponent<building_status>().wood_upg_cost[org_obj.GetComponent<building_status>().upgrade_level].ToString() + "\n" +
                                   "Food Cost:" + org_obj.GetComponent<building_status>().food_upg_cost[org_obj.GetComponent<building_status>().upgrade_level].ToString() + "\n" +
                                   "Treasure Cost:" + org_obj.GetComponent<building_status>().treasure_upg_cost[org_obj.GetComponent<building_status>().upgrade_level].ToString() + "\n" +
                                   "Loyalty Cost:" + org_obj.GetComponent<building_status>().loyalty_cost[org_obj.GetComponent<building_status>().upgrade_level].ToString() + "\n" + "\n";
                //"Special Effect:" + org_obj.GetComponent<building_status>().upgrade_descriptions[org_obj.GetComponent<building_status>().upgrade_level];
            }
            else
            {
                add_txt.text = "Upgrade Materials Needed \n" +
                                   "Wood Cost:" + org_obj.GetComponent<building_status>().wood_upg_cost[org_obj.GetComponent<building_status>().upgrade_level].ToString() + "\n" +
                                   "Food Cost:" + org_obj.GetComponent<building_status>().food_upg_cost[org_obj.GetComponent<building_status>().upgrade_level].ToString() + "\n" +
                                   "Treasure Cost:" + org_obj.GetComponent<building_status>().treasure_upg_cost[org_obj.GetComponent<building_status>().upgrade_level].ToString() + "\n" + "\n"
                                    + org_obj.GetComponent<building_status>().upgrade_descriptions[org_obj.GetComponent<building_status>().upgrade_level];
            }
        }

        
        
        //upgrade_desc.text = org_obj.GetComponent<building_status>().upgrade_descriptions[building_id + y];
        //building ID + Y
        //upg_wood_cost.text = org_obj.GetComponent<building_status>().wood_upg_cost[building_id + y].ToString();
        // upg_treasure_cost.text = org_obj.GetComponent<building_status>().treasure_upg_cost[building_id + y].ToString();

    }
    public void Special_button()
    {
        add_txt.text = "This building does nothing.";
    }
    public void Special_info()
    {
        add_txt.text = "This building does nothing.";
    }

    //public void swap_pages()
    //{
    //    tool_tip.SetActive(false);
    //    if(building_buttons.activeInHierarchy)
    //    {
    //        pathway_buttons.SetActive(true);
    //        building_buttons.SetActive(false);
    //    }
    //    else if(pathway_buttons.activeInHierarchy)
    //    {
    //        building_buttons.SetActive(true);
    //        pathway_buttons.SetActive(false);
    //    }



    //}

    //public void building_mode_toggle(int x)
    //{

    //    if (x == 1)
    //    {
    //        Building_tree.SetActive(true);
    //        Debug.Log("1");
    //    }

    //    if(x == 0)
    //    {
    //        Building_tree.SetActive(false);
    //        Debug.Log("2");
    //    }

    //}


    private void FixedUpdate()
    {
        
    }
}
