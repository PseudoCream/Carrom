using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNShoot : MonoBehaviour
{
    Camera cam;

    public float power = 2f;
    public Rigidbody2D rb;
    public Prediction prediction;
    public float xPos = 0.0f;

    // Min & Max power of pull (negatives allow backwards movement)
    public Vector2 minPower;
    public Vector2 maxPower;

    // Draw Lines
    private DrawLine powerLine;

    // Force to add & the start and end point of shoot
    private Vector2 force;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private bool canShoot;
    private bool onStriker;
    private Vector3 pos;

    private void Start()
    {
        // Set default values
        cam = Camera.main;
        powerLine = GetComponent<DrawLine>();
        canShoot = true;
    }

    public void MoveXPos(float newXPos)
    {
        // Move x pos of striker with slider
        xPos = newXPos;
        pos = transform.position;
        pos.x = xPos;
        transform.position = pos;
    }

    private void Update()
    {
        // Predicted path raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Input.mousePosition);
        Debug.DrawRay(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), Color.white);

        if (canShoot && onStriker)
        {
            // Start pull at mouse position click
            if (Input.GetMouseButtonDown(0))
            {
                // Set start point as where the mouse is clicked and keep it in a visable z layer
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }
            // As the mouse button is held...
            if (Input.GetMouseButton(0))
            {
                // Set the current point to mouse position & draw line to there
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = -15;
                powerLine.RenderLine(currentPoint);

                // Set force
                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                    Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            }
            // End pull and add velocity to shoot object
            if (Input.GetMouseButtonUp(0))
            {
                // Set end point as where the mouse is released and keep it in a visable z layer
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                // Set force
                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                    Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                // Apply force & remove line
                rb.AddForce(power * force, ForceMode2D.Impulse);
                powerLine.EndRender();

                // Set ability to shoot again
                //canShoot = false;
                onStriker = false;
            }
        }
    }

    private void OnMouseEnter()
    {
        onStriker = true;
    }
}
