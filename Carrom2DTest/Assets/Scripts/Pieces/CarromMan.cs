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

    public AudioClip col_sound;
    public float col_volume;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioSource.PlayClipAtPoint(col_sound, this.gameObject.transform.position, col_volume);
    }

    public void SetColour(Vector4 colour) { rend.color = colour; }
    public void SetPoints(int point) { points = point; }
}
