using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{

    private float tick;
    private float delay = 1;
    //build_button_References
    


    public Sprite Object_type;
    public Sprite[] levels;
    private SpriteRenderer spriteRenderer;
    private bool constructed = false;
    public AudioSource my_sudio_source;
    public AudioClip my_clip;
    public bool is_sound_played;
    public bool upgrading = false;
    private void Awake()
    {
        is_sound_played = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        
        //if constructed is false
        if (constructed == false)
        {
            
            construct_building();
            
        }

        if(upgrading == true && constructed == true)
        {
            
            upgrade_construct();
        }
        
    }



    private void construct_building()
    {
        tick += Time.deltaTime;
        if(is_sound_played == false)
        {
            play_clip();
            is_sound_played = true;
            my_sudio_source.loop = true;
            
        }
        
        if (tick > this.gameObject.GetComponent<building_status>().construction_times[0])
        {
            //    Debug.Log("poof");
            tick = 0f;
            change_sprite();
            constructed = true;
            my_sudio_source.Stop();
        }
    }
    public void upgrade_construct()
    {
        Debug.Log("delay"+ delay);
        tick += Time.deltaTime;
        
        if (is_sound_played == false)
        {
            
            play_clip();
            is_sound_played = true;
            my_sudio_source.loop = true;
        }
        //Debug.Log("upgrading to level :"+ this.gameObject.GetComponent<building_status>().upgrade_level +"Time_remaining:"+ tick+"building status delay"+ this.gameObject.GetComponent<building_status>().upgrade_delays[this.gameObject.GetComponent<building_status>().upgrade_level]);
        if (tick > this.gameObject.GetComponent<building_status>().construction_times[this.gameObject.GetComponent<building_status>().upgrade_level])
        {
            //this.gameObject.GetComponent<building_status>().upgrade_level
            tick = 0f;
            spriteRenderer.sprite = levels[this.gameObject.GetComponent<building_status>().upgrade_level];
            this.GetComponent<building_status>().upgrade_level++;
            this.GetComponent<building_status>().upgrades[this.GetComponent<building_status>().upgrade_level] = true;
            if(this.GetComponent<building_status>().building_type == "Castle")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().castle_LV += 1;
            }
            upgrading = false;
            my_sudio_source.Stop();
            
        }
    }
    public bool is_constructed()
    {
        return constructed;
    }
    public void change_sprite()
    {


        spriteRenderer.sprite = Object_type;
    }
    public void change_delay(float x)
    {
        delay = x;
        
    }

    public void play_clip()
    {
        my_sudio_source.clip = my_clip;
        my_sudio_source.Play();
    }


}
