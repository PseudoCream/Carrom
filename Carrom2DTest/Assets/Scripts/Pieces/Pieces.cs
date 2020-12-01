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
    private Vector4 marrn = new Vector4(1, 0, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < pieces.Length - 1; i++)
        {
            pieces[i] = Instantiate(cm_Prefab, locations[i+1].position, locations[i+1].rotation, parent);
            cm = pieces[i].gameObject.GetComponent<CarromMan>();
            if (i % 2 == 0)
            {
                cm.SetColour(black);
                cm.black = true;
            }
            else
            {
                cm.SetColour(white);
                cm.black = false;
            }
        }
    }

    public void SpawnQueen()
    {
        GameObject queen = Instantiate(cm_Prefab, locations[0].position, locations[0].rotation, parent);
        cm = queen.gameObject.GetComponent<CarromMan>();
        cm.isQueen = true;
    }

    private IEnumerator FindUnoccupied()
    {
        bool unoccupied = false;

        while (!unoccupied)
        {
            unoccupied = true;

            for (int i = 0; i < locations.Length; i++)
            {
                if (Physics2D.OverlapCircle(locations[i].transform.position, 0.157f))
                {
                    unoccupied = false;
                    print("Working...");
                    yield return null;
                    break;
                }

                yield return i;
            }
        }
    }

    public IEnumerator SpawnPiece(int target, bool queen)
    {
        int free = 0;
        bool unoccupied = false;
        GameObject piece;

        /*while (!unoccupied)
        {
            unoccupied = true;

            for (int i = 0; i < locations.Length; i++)
            {
                if (Physics2D.OverlapCircle(locations[i].transform.position, 0.157f))
                {
                    unoccupied = false;
                    print("Working...");
                    yield return null;
                    break;
                }

                free = i;
            }
        }*/

        if (!queen)
        {
            piece = Instantiate(cm_Prefab, locations[free].position, locations[free].rotation, parent);
            cm = piece.gameObject.GetComponent<CarromMan>();
            cm.black = (target == 1 ? true : false);
            Vector4 colour = (target == 1 ? black : white);
            cm.SetColour(colour);
        }
        else if (queen)
        {
            print("SPAWN Q");
            piece = Instantiate(cm_Prefab, locations[free].position, locations[free].rotation, parent);
            cm = piece.gameObject.GetComponent<CarromMan>();
            cm.SetColour(marrn);
            cm.isQueen = true;
        }

        yield return null;
    }
}
