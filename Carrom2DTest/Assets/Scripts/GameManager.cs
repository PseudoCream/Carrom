using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Enum for Game States
public enum GameState { START, PLAYERTURN, ENEMYTURN, WIN }
//public enum TurnState { START, SHOT, END }
public struct Player { public int tar_count; public int points; }

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

    // Current Player
    private int curr_player;

    // Slider & Camera
    private Slider slider;
    private Camera cam;
    private Quaternion camRotation;

    // Current Player turn & board movement
    public bool notMoving;
    private bool p_turn;
    Player p1 = new Player();
    Player p2 = new Player();

    // PLayer target
    public int target;

    // Pieces
    private Pieces pieces;
    private Queue<int> carromQ;
    private bool non_target = false;
    private bool pock_queen = false;
    private bool queen_cover = false;

    // Cam & slider rotation
    private bool rotCam;
    private bool newTurn;
    private bool sliderFlip;
    public float cam_rot_speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        game_state = GameState.START;
        //turn_state = TurnState.START;
        slider = FindObjectOfType(typeof(Slider)) as Slider;
        pieces = FindObjectOfType(typeof(Pieces)) as Pieces;
        cam = Camera.main;
        StartCoroutine(SetupBoard());
        //Quaternion.Euler(0, 0, 0);
        carromQ = new Queue<int>();
        sliderFlip = false;

        p1.tar_count = 9;
        p2.tar_count = 9;
        p1.points = 0;
        p2.points = 0;
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
        PlayerTurn();
    }


    private void PlayerTurn()
    {
        //Set State & target white
        game_state = GameState.PLAYERTURN;
        target = 1;
        non_target = false;

        // Set Slider pos to 0, flip slider
        if (sliderFlip)
        {
            slider.transform.Rotate(new Vector3(0, 0, 180));
            sliderFlip = false;
        }
        slider.value = 0;

        // Rotate Camera
        camRotation = Quaternion.Euler(0, 0, 0);
        newTurn = true;

        // Spawn player striker, change state, set turn
        GameObject p1_striker = Instantiate(striker_P1, par_P1.position, par_P1.rotation);
        p1_striker.transform.SetParent(par_P1);
        p_turn = true;

        // Transition to end state through "DragNShoot" script
    }

    private void EnemyTurn()
    {
        //Set State & target black
        game_state = GameState.ENEMYTURN;
        target = 0;
        non_target = false;

        // Set Slider pos to 0, flip slider
        if (!sliderFlip)
        {
            slider.transform.Rotate(new Vector3(0, 0, 180));
            sliderFlip = true;
        }
        slider.value = 0;

        // Rotate Camera
        camRotation = Quaternion.Euler(0, 0, 180);
        newTurn = true;

        // Spawn player striker, change state, set turn
        GameObject p2_striker = Instantiate(striker_P2, par_P2.position, par_P2.rotation);
        p2_striker.transform.SetParent(par_P2);
        p_turn = false;

        // Transition to end state through "DragNShoot" script
    }

    public void TurnEnd(GameObject striker)
    {
        print("TurnEND");
        // Check all pieces have stopped moving
        StartCoroutine(CheckMoving(striker));

        /* TODO */
        // Check for queen, if no consecutive pocket, return to board
        // Check for pocketed pieces, if last piece, set to won
        // If last piece, check pocketed queen, if not return piece to board
        // Set turn to pocketers turn
        // If no pockets, no movement, change turn to next player
        /* TODO */


    }

    public void Pocketed(bool black, bool isQueen)
    {
        int carromType;

        if(isQueen)
        {
            print("Queen pock");
            pock_queen = true;

            if (game_state == GameState.PLAYERTURN)
                p_turn = false;
            else
                p_turn = true;
        }

        if(!isQueen && !non_target)
        {
            carromType = black ? 0 : 1;
            if(target == carromType)
            {
                if(game_state == GameState.PLAYERTURN)
                {
                    if(p1.tar_count > 1)
                    {
                        p1.tar_count -= 1;
                        print("TARGET" + p1.tar_count);
                        p_turn = false;
                    }
                    else if (p1.tar_count <= 1)
                    {
                        // PLayer loses
                    }
                }
                else
                {
                    if(p2.tar_count > 1)
                    {
                        p2.tar_count -= 1;
                        print("TARGET" + p2.tar_count);
                        p_turn = true;
                    }
                    else if(p2.tar_count <= 1)
                    {
                        // PLayer loses
                    }
                }

                if (pock_queen)
                {
                    queen_cover = true;
                    print("COVER QUEEN");
                }
            }
            else if(!(target == carromType))
            {
                non_target = true;

                if (game_state == GameState.PLAYERTURN)
                {
                    p2.tar_count -= 1;
                    print("TARGET" + p2.tar_count);
                    p_turn = true;
                }
                else
                {
                    p1.tar_count -= 1;
                    print("TARGET" + p1.tar_count);
                    p_turn = false;
                }

                if (pock_queen)
                {
                    print("FOUL");
                    pieces.SpawnQueen();
                }
            }
            carromQ.Enqueue(carromType);
            print(p_turn);
        }
    }

    private IEnumerator CheckMoving(GameObject striker)
    {
        //yield return new WaitForSeconds(2.0f);
        Rigidbody2D[] pieces = FindObjectsOfType<Rigidbody2D>();
        notMoving = false; 

        while(!notMoving)
        {
            notMoving = true;

            foreach(Rigidbody2D piece in pieces)
            {
                if(piece)
                {
                    if (!piece.IsSleeping())
                    {
                        notMoving = false;
                        yield return null;
                        break;
                    }
                }
            }
        }

        print("All Objects Stopped");
        if (striker)
            Destroy(striker);

        if (p_turn)
        {
            print("ENEMY TURN");
            EnemyTurn();
        }
        else
        {
            print(p_turn);
            PlayerTurn();
        }
    }
}
