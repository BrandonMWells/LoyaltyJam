using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class generation : MonoBehaviour
{

    public int width;
    public int height;
    public string seed;
    public bool random_seed;
    [Range(0, 100)]
    public int random_fill;

    int[,] map;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void map_generation()
    {
        map = new int[width, height];


    }


    void random_fill_map()
    {
        if(random_seed)
        {
            seed = Time.time.ToString();
        }

        System.Random psudoRandom = new System.Random(seed.GetHashCode());

        for(int x = 0; x < width; x++)
        {
        for(int y = 0; y < height; y++)
            {
                if(x == 0|| x == width - 1 || y == 0 || y == height -1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (psudoRandom.Next(0, 100) < random_fill) ? 1 : 0;
                }
            }
        }

    }

    void smooth_map()
    {
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
               
            }
        }

    }

}