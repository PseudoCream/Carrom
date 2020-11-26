using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarromMan : MonoBehaviour
{
    public int points;
    public bool isQueen;
    public bool black;

    private SpriteRenderer rend;
    private Vector4 defaultColour;

    private void Awake()
    {
        rend = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPoints(3);
        black = true;
        isQueen = false;
    }

    public void SetColour(Vector4 colour) { rend.color = colour; print("Set"); }
    public void SetPoints(int point) { points = point; }
}
