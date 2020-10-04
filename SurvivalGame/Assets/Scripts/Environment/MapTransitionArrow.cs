using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class MapTransitionArrow : MonoBehaviour
{
    [SerializeField]
    private Directions direction = Directions.UP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDirection(int newDirection)
    {
        direction = (Directions)newDirection;

        float angle = 0;

        switch (direction)
        {
            case Directions.UP:
                break;
            case Directions.DOWN:
                angle = 180;
                break;
            case Directions.LEFT:
                angle = 90;
                break;
            case Directions.RIGHT:
                angle = 270;
                break;

        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Contains("Player"))
        {
            foreach (var levelSegmentObject in GameObject.FindGameObjectsWithTag("LevelManager"))
            {
                LevelSegment levelSegment = levelSegmentObject.GetComponent<LevelSegment>();

                levelSegment.MoveQuadrant((int)direction);
            }
        }
    }
}
