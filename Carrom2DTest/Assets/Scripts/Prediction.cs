using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour
{
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotsPrefab;
    [SerializeField] int dotsNum = 10;
    [SerializeField] float dotSpace = 0.03f;

    Transform[] dotArray;
    Vector2 pos;

    // Time used to calculate dot spacing
    float timeStamp;

    private void Start()
    {
        PrepareDots();
    }

    private void PrepareDots()
    {
        dotArray = new Transform[dotsNum];

        // Fill array & set parent transform
        for(int i = 0; i < dotsNum; i++)
        {
            dotArray[i] = Instantiate(dotsPrefab, null).transform;
            dotArray[i].parent = dotsParent.transform;
        }
    }

    public void UpdateDots(Vector3 piecePos, Vector2 forceApplied)
    {
        // Set how far each dot is from the previous
        timeStamp = dotSpace;

        for(int i = 0; i < dotsNum; i++)
        {
            // Get dot position
            pos.x = piecePos.x + forceApplied.x * timeStamp;
            pos.y = piecePos.y + forceApplied.y * timeStamp;

            // Set dot positions & increment spacing
            dotArray[i].position = pos;
            timeStamp += dotSpace;
        }
    }

    public void DrawDots()
    {
        dotsParent.SetActive(true);
    }

    public void HideDots()
    {
        dotsParent.SetActive(false);
    }
}
