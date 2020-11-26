using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Enum for Game States
public enum GameState { START, PLAYERTURN, ENEMYTURN, WIN }
//public enum TurnState { START, SHOT, END }

public class GameManager : MonoBehaviour
{
    // Game States
    public GameState game_state;
    //public TurnState turn_state;

    // Get Player Striker prefab
    public GameObject striker_P1;
    public GameObject striker_P2;

    // Get Position of Parents to place striker
    public Transform par_P1;
    public Transform par_P2;

    // Slider & Camera
    private Slider slider;
    private Camera cam;
    private Quaternion camRotation;

    // Current Player turn & board movement
    public bool notMoving;
    private bool p_turn;

    // Cam rotation
    private bool rotCam;
    private bool newTurn;
    public float cam_rot_speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        game_state = GameState.START;
        //turn_state = TurnState.START;
        slider = FindObjectOfType(typeof(Slider)) as Slider;
        cam = Camera.main;
        StartCoroutine(SetupBoard());
        Quaternion.Euler(0, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if(newTurn)
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camRotation, cam_rot_speed);
    }

    private IEnumerator SetupBoard()
    {
        // Setup Carrom pieces


        // Change to Player turn
        yield return new WaitForSeconds(0.5f);
        game_state = GameState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }


    private IEnumerator PlayerTurn()
    {
        // Set Slider pos to 0
        slider.value = 0;

        // Rotate Camera
        camRotation = Quaternion.Euler(0, 0, 0);
        newTurn = true;

        // Spawn player striker, change state, set turn
        GameObject p1_striker = Instantiate(striker_P1, par_P1.position, par_P1.rotation);
        p1_striker.transform.SetParent(par_P1);
        p_turn = true;

        yield return new WaitForSeconds(0.5f);
        // Transition to end state through "DragNShoot" script
    }

    private IEnumerator EnemyTurn()
    {
        // Set Slider pos to 0
        slider.value = 0;

        // Rotate Camera
        camRotation = Quaternion.Euler(0, 0, 180);
        newTurn = true;

        // Spawn player striker, change state, set turn
        GameObject p2_striker = Instantiate(striker_P2, par_P2.position, par_P2.rotation);
        p2_striker.transform.SetParent(par_P2);
        p_turn = false;

        yield return new WaitForSeconds(0.5f);
        // Transition to end state through "DragNShoot" script
    }

    private void TurnShot(GameObject striker)
    {
        
        /*DragNShoot shoot;
        // Check for the DragNShoot script before assigning the shoot check
        if (striker.GetComponent<DragNShoot>())
        {
            print("Got Drag");
            shoot = striker.GetComponent<DragNShoot>();
            // Check if the player has shot their striker
            if (shoot.canShoot == false)
            {
                turn_state = TurnState.END;
                TurnEnd(striker);
            }
        }*/
    }

    public void TurnEnd(GameObject striker)
    {
        print("TurnEND");
        // Check all pieces have stopped moving
        StartCoroutine(CheckMoving(striker));

        if (p_turn)
            p_turn = false;
        else
            p_turn = true;

        /* TODO */
        // Check for queen, if no consecutive pocket, return to board
        // Check for pocketed pieces, if last piece, set to won
        // If last piece, check pocketed queen, if not return piece to board
        // Set turn to pocketers turn
        // If no pockets, no movement, change turn to next player
        /* TODO */


    }

    private IEnumerator CheckMoving(GameObject striker)
    {
        Rigidbody2D[] pieces = FindObjectsOfType<Rigidbody2D>();
        notMoving = false; 

        while(!notMoving)
        {
            notMoving = true;

            foreach(Rigidbody2D piece in pieces)
            {
                if (!piece.IsSleeping())
                {
                    notMoving = false;
                    yield return null;
                    break;
                }
            }
        }

        print("All Objects Stopped");
        Destroy(striker);

        // Change to next Player's turn
        if (p_turn)
        {
            StartCoroutine(PlayerTurn());
        }
        else
            StartCoroutine(EnemyTurn());
    }
}
