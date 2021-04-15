using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cut_wood : MonoBehaviour
{
    int max_num_of_trees = 5;
    public int count;
    float tick = 0f;
    //float delay = 10f;
    public LayerMask tree_mask;
    public GameObject the_tree;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
        Collider2D tree_detection = gameObject.GetComponent<Collider2D>();
        Collider2D[] treelist = new Collider2D[max_num_of_trees];
        ContactFilter2D contactFilter = new ContactFilter2D();
        //contactFilter.useLayerMask = true;
        //contactFilter.IsFilteringLayerMask(the_tree);
        count = Physics2D.OverlapCollider(tree_detection, contactFilter, treelist);
        tick += Time.deltaTime;
        

    }

 
}
