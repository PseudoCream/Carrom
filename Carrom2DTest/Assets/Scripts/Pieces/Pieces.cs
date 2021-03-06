﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    // pieces (excluding Strikers)
    public Transform parent;
    public GameObject cm_Prefab;
    public SpriteRenderer sRenderer;
    public Sprite[] sprite = new Sprite[2];
    public Transform[] locations = new Transform[19];

    private GameObject[] pieces = new GameObject[19];
    private CarromMan cm;
    private Vector4 black = new Vector4(0, 0, 0, 1);
    private Vector4 white = new Vector4(1, 1, 1, 1);
    private Vector4 marrn = new Vector4(1, 0, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < pieces.Length - 1; i++)
        {
            pieces[i] = Instantiate(cm_Prefab, locations[i+1].position, locations[i+1].rotation, parent);
            cm = pieces[i].gameObject.GetComponent<CarromMan>();
            sRenderer = pieces[i].gameObject.GetComponent<SpriteRenderer>();
            if (i % 2 == 0)
            {
                //cm.SetColour(black);
                sRenderer.sprite = sprite[1];
                cm.black = true;
                if (i > 2)
                    Destroy(pieces[i].gameObject);
            }
            else
            {
                //cm.SetColour(white);
                sRenderer.sprite = sprite[0];
                cm.black = false;
                if(i > 2)
                    Destroy(pieces[i].gameObject);
            }
        }
    }

    public void SpawnQueen()
    {
        /* TODO */
        // Find unoccupied spot on board to spawn to

        GameObject queen = Instantiate(cm_Prefab, locations[0].position, locations[0].rotation, parent);
        cm = queen.gameObject.GetComponent<CarromMan>();
        cm.isQueen = true;
    }
}
