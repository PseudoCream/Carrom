using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStrikerSlider : MonoBehaviour
{
    public float xPos;
    private Vector3 pos;

    public void MoveXPos(float newXPos)
    {
        // Move x pos of striker with slider
        xPos = newXPos;
        pos = transform.position;
        pos.x = xPos;
        transform.position = pos;
    }
}
