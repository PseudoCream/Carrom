using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarromMan : MonoBehaviour
{
    public int points;
    public bool isQueen = false;
    public bool black = true;
    public float deceleration = 10.0f;

    private SpriteRenderer rend;
    private Rigidbody2D rb;
    private Vector4 defaultColour;
    private float speed;

    private void Awake()
    {
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPoints(3);
        if (isQueen)
            SetColour(new Vector4(1, 0, 0, 1));
    }

    /*private void Update()
    {
        speed = rb.velocity.magnitude;
        if(speed > (deceleration * Time.deltaTime))
        {
            speed = speed - (deceleration * Time.deltaTime);
        }
        else if(speed < -deceleration * Time.deltaTime)
            speed = speed + deceleration * Time.deltaTime;
        else
            speed = 0;

        rb.velocity = new Vector3(rb.velocity.x + speed, rb.velocity.y + speed);
    }*/

    public void SetColour(Vector4 colour) { rend.color = colour; }
    public void SetPoints(int point) { points = point; }
}
