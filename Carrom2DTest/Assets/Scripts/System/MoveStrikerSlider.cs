using System.Collections;
using UnityEngine;

public class MoveStrikerSlider : MonoBehaviour
{
    public float xPos;
    private Vector3 pos;
    private DragNShoot striker;

    public void MoveXPos(float newXPos)
    {
        striker = FindObjectOfType<DragNShoot>();

        // Move x pos of striker with slider
        xPos = newXPos;
        pos = transform.position;
        pos.x = xPos;
        transform.position = pos;

        if(striker)
            striker.onSlider = true;
    }
}
