using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    // pieces (excluding Strikers)
    public Transform parent;
    public GameObject cm_Prefab;
    public Transform[] locations = new Transform[19];

    private GameObject[] pieces = new GameObject[19];
    private CarromMan cm;
    private Vector4 black = new Vector4(0, 0, 0, 1);
    private Vector4 white = new Vector4(1, 1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < pieces.Length - 1; i++)
        {
            pieces[i] = Instantiate(cm_Prefab, locations[i+1].position, locations[i+1].rotation, parent);
            cm = pieces[i].gameObject.GetComponent<CarromMan>();
            if (i % 2 == 0)
                cm.SetColour(black);
            else
            {
                cm.SetColour(white);
                cm.black = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
