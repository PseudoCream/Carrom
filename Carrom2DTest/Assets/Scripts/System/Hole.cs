using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<CarromMan>())
        {
            CarromMan carrom = other.GetComponent<CarromMan>();
            manager.Pocketed(carrom.black, carrom.isQueen);
            Destroy(other.gameObject);
        }

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        rb.Sleep();
    }


}
