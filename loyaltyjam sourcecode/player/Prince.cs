using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Prince : MonoBehaviour
{
    int x = 0;
    public Transform trg;
    private Camera cam;
    float move_speed = 2f;

    //check if i am moving in that direction\
    float Horizontal;
    float Vertical;
    //give me a float value in that direction
    float look_x;
    float look_y;
    public bool hammertime = false;

    //public bool can_move;
    Rigidbody2D rigidbody2d;
    Vector2 lookDirection = new Vector2(1, 0);
    Animator animator;
    private bool can_move;
    public AudioSource my_audio;
    public AudioClip hammer;

    // Start is called before the first frame update
    void Start()
    {
        //can_move = true;
        //Grid grid = transform.parent.GetComponent<Grid>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
        can_move = true;
        my_audio = GetComponent<AudioSource>();

        // Original_cell = grid.WorldToCell(transform.position);


    }
    private void Awake()
    {
        
    }


    void Update()
    {

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(Horizontal, Vertical);


        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }


        if (hammertime == false)
        {
            my_audio.Play();
            animator.SetBool("hammertime", false);
            animator.SetFloat("look_x", lookDirection.x);
            animator.SetFloat("look_y", lookDirection.y);
            animator.SetFloat("move_speed", move.magnitude);

        }else
        {
            
            animator.SetBool("hammertime", hammertime);
        }

        if (Input.GetKeyDown("e"))
        {
            Debug.Log("e");

            //RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("plot"));
            //if(hit.collider != null)
           // {
                
           // }
        }
        //Vector3Int cellPosition = grid.WorldToCell(transform.position);
        //Debug.Log("cell: " + cellPosition + "Orig: " + Original_cell+ "times swapped:"+ x);
        //if (cellPosition == Original_cell)
        //{
         //   cellPosition = Original_cell;
     
      
           // if (Vector2.Distance(trg.position, transform.position) > 0f)
          //  {
          //      trg.position = Vector2.MoveTowards( trg.transform.position, transform.position, move_speed * Time.deltaTime);
          // }
      //  }

        //Debug.Log(grid.GetCellCenterWorld(cellPosition));
    }

    void FixedUpdate()
    {
        if (can_move == true)
        {
            Vector2 position = rigidbody2d.position;
            position.x = position.x + move_speed * Horizontal * Time.deltaTime;
            position.y = position.y + move_speed * Vertical * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }
        
    }

    public void set_move(bool x)
        
    {
        can_move = x;


    }



}
