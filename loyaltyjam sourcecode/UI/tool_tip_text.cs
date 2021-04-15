using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tool_tip_text : MonoBehaviour
{
    public Text t;
    
    // Start is called before the first frame update
    void Start()
    {
        t.GetComponent<Text>();
        //Debug.Log(t.text);
    }


    public string get_text()
    {

        return t.text;
    }

    public void set_text(string x)
    {
        t.text = x;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
