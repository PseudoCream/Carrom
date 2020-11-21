using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 prevVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        prevVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var speed = prevVelocity.magnitude;
        var direction = Vector3.Reflect(prevVelocity.normalized, other.contacts[0].normal);

        /*Vector3 p1 = transform.position;
        Vector3 p2 = other.transform.position;
        Vector3 v1 = rb.velocity;
        Vector3 v2 = other.rigidbody.velocity;

        float pm1 = rb.mass;
        float pm2 = other.rigidbody.mass;

        // Get distance between object's centres
        float dist = Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));

        // Normal vector values
        float nx = (p2.x - p1.x) / dist;
        float ny = (p2.y - p1.y) / dist;

        // Tangential vector, flip values, invert y
        float tx = -ny;
        float ty = nx;

        // Dot product tangent
        float dpTan1 = v1.x * tx + v1.y * ty;
        float dpTan2 = v2.x * tx + v2.y * ty;

        // Dot product mass
        float dpNorm1 = v1.x * nx + v1.y * ny;
        float dpNorm2 = v2.x * nx + v2.y * ny;

        // Conservation of momentum
        float m1 = (dpNorm1 * (pm1 - pm2) + 2.0f * pm2 * dpNorm2) / (pm1 + pm2);
        float m2 = (dpNorm2 * (pm2 - pm1) + 2.0f * pm1 * dpNorm1) / (pm1 + pm2);

        // Update tangential values
        v1.x = tx * dpTan1 + nx * m1;
        v1.y = ty * dpTan1 + ny * m1;
        v2.x = tx * dpTan2 + nx * m2;
        v2.y = ty * dpTan2 + ny * m2;

        rb.velocity = v1;
        other.rigidbody.velocity = v2;*/

        // Wall collision
        if (other.transform.parent.tag != null)
        {
            if (other.transform.parent.tag == "Wall")
            {
                rb.velocity = direction * Mathf.Max(speed, 0f);
            }
        }
    }
}
