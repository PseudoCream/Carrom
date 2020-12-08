using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNShoot : MonoBehaviour
{
    Camera cam;

    public float power = 2f;
    public Rigidbody2D rb;
    public bool canShoot;
    public GameObject manager;

    // Touch control
    private Touch touch;

    // Min & Max power of pull (negatives allow backwards movement)
    public Vector2 minPower;
    public Vector2 maxPower;

    // Draw Lines
    private DrawLine powerLine;

    // Force to add & the start and end point of shoot
    private Vector2 force;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private bool onStriker;
    private GameManager gameManager;

    private void Start()
    {
        // Set Components
        cam = Camera.main;
        powerLine = GetComponent<DrawLine>();
        manager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = manager.GetComponent<GameManager>();
        
        // Set default values
        canShoot = true;
    }

    private void Update()
    {
        // Predicted path raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Input.mousePosition);
        Debug.DrawRay(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), Color.white);

        if (canShoot && onStriker)
        {
            /* #################### Touch Controls #################### */
            // Get Touch 
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                // Start of touch drag...
                if(touch.phase == TouchPhase.Began)
                {
                    DragStart();
                }
                // While dragging across screen...
                if(touch.phase == TouchPhase.Moved)
                {
                    Drag();
                }
                // When released.
                if(touch.phase == TouchPhase.Ended)
                {
                    DragEnd();
                }
            }
            /* #################### Touch Controls #################### */

            /* #################### Mouse Controls #################### */
            // Start pull at mouse position click
            if (Input.GetMouseButtonDown(0))
            {
                DragStart();
            }
            // As the mouse button is held...
            if (Input.GetMouseButton(0))
            {
                Drag();
            }
            // End pull and add velocity to shoot object
            if (Input.GetMouseButtonUp(0))
            {
                DragEnd();
            }
            /* #################### Mouse Controls #################### */
        }

        void DragStart()
        {
            // Set start point as where the mouse is clicked and keep it in a visable z layer
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
        }

        void Drag()
        {
            // Set the current point to mouse position & draw line to there
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = -15;
            powerLine.RenderLine(currentPoint);

            // Set force
            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
        }

        void DragEnd()
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

            // Set ability to shoot again, end turn
            canShoot = false;
            gameManager.TurnEnd(this.gameObject);
            onStriker = false;
        }
    }

    private void OnMouseEnter()
    {
        onStriker = true;
    }

    private void OnMouseExit()
    {
        if(!Input.GetMouseButton(0))
            onStriker = false;
    }
}
