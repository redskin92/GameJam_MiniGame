using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    public int x = 0;
    public int y = 0;

    public bool locked = true;

    public Segment(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}

public class LevelSegment : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private int xSegments = 3;

    [SerializeField]
    private int ySegments = 3;

    [SerializeField]
    private float transitionTime = 3.0f;

    [SerializeField]
    private float HUDDispplaySize = 100.0f;

    [SerializeField]
    private int xArrowOffset = 370;

    [SerializeField]
    private int yArrowOffset = 270;

    private float mapBoundX = 800.0f;
    private float mapBoundY = 600.0f;

    private List<Segment> segments = new List<Segment>();

    private int currentX = 0;
    private int currentY = 0;

    private int nextX = 0;
    private int nextY = 0;

    private float currentTime = 0;

    private int currentDirection = 0;

    public bool Transitioning { get; set; }

    List<KeyValuePair<int, int>> directions = new List<KeyValuePair<int, int>>()
    {
        new KeyValuePair<int, int>( 0, 1),
        new KeyValuePair<int, int>( 0, -1),
        new KeyValuePair<int, int>( -1, 0),
        new KeyValuePair<int, int>( 1, 0)
    };

    // Start is called before the first frame update
    void Start()
    {
        int numSegments = xSegments * ySegments;

        for(int i = 0; i < numSegments; ++i)
        {
            segments.Add(new Segment(i / xSegments, i % ySegments));
        }

        Transitioning = false;

        GenerateArrows();
    }

    public void MoveQuadrant(int direction)
    {
        if (!Transitioning)
        {
            KeyValuePair<int, int> move = directions[direction];

            //UnityEngine.Debug.Log("X moved to: " + (currentX + move.Key));
            //UnityEngine.Debug.Log("Y moved to: " + (currentY + move.Value));

            if (currentX + move.Key >= 0 && currentX + move.Key <= xSegments
                && currentY + move.Value >= 0 && currentY + move.Value <= xSegments)
            {
                nextX = currentX + move.Key;
                nextY = currentY + move.Value;

                currentDirection = direction;

                currentTime = 0;

                Transitioning = true;
            }
        }
    }

    public void Transition()
    {
        currentTime += Time.deltaTime;

        if (currentTime > transitionTime)
        {
            camera.transform.position = new Vector3(nextX * 800, nextY * 600, camera.transform.position.z);
            FinishedTransition();
        }
        else
        {
            Vector3 position = camera.transform.position;

            //moving left
            if(currentX > nextX)
            {
                position.x = (currentX - (currentTime / transitionTime)) * 800;
            }
            //moving right
            else if (currentX < nextX)
            {
                position.x = (currentX + (currentTime / transitionTime)) * 800;

            }
            // moving up
            else if (currentY > nextY)
            {
                position.y = (currentY - (currentTime / transitionTime)) * 600;
            }
            // moving down
            else if (currentY < nextY)
            {
                position.y = (currentY + (currentTime / transitionTime)) * 600;
            }

            Vector3 difference = position - camera.transform.position;

            camera.transform.position = position;

            player.transform.position += (difference / 10);
        }
    }

    public void FinishedTransition()
    {
        currentX = nextX;
        currentY = nextY;

        Transitioning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Transitioning)
        {
            Transition();
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveQuadrant(0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveQuadrant(1);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveQuadrant(2);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveQuadrant(3);
            }
        }
    }

    public bool IsInBounds(Vector3 position, float xBoundOffset = 0, float yBoundOffset = 0)
    {
        float positionX = position.x % ((currentX + 1) * mapBoundX);
        float positionY = position.y % ((currentY + 1) * mapBoundY);

        if (positionX > (-mapBoundX + xBoundOffset) / 2 && positionX < (mapBoundX - xBoundOffset) / 2
            && positionY > ((-mapBoundY + yBoundOffset) / 2) + HUDDispplaySize && positionY < (mapBoundY - yBoundOffset) / 2)
        {
            return true;
        }

        return false;
    }

    public Vector3 SetPlayerBoundsPosition(Vector3 position, Vector3 move, float xBoundOffset = 0, float yBoundOffset = 0)
    {
        float positionX = position.x - ((currentX) * mapBoundX);
        float positionY = position.y - ((currentY) * mapBoundY);

        if (positionX + move.x < (-mapBoundX + xBoundOffset) / 2)
        {
            if (move.x < 0)
            {
                move.x = 0;
            }
        }
        else if (positionX + move.x > (mapBoundX - xBoundOffset) / 2)
        {
            if (move.x > 0)
            {
                move.x = 0;
            }
        }

        if (positionY + move.y < ((-mapBoundY + yBoundOffset) / 2) + HUDDispplaySize)
        {
            if (move.y < 0)
            {
                move.y = 0;
            }
        }
        else if (positionY + move.y > (mapBoundY - yBoundOffset) / 2)
        {
            if (move.y > 0)
            {
                move.y = 0;
            }
        }

        move.Normalize();

        return move;
    }

    protected void GenerateArrows()
    {
        for (int i = 0; i < segments.Count; ++i)
        {
            int x = segments[i].x;
            int y = segments[i].y;

            if(x != 0)
            {
                Vector3 position = new Vector3(x * mapBoundX, y * mapBoundY, 0);
                position.x -= xArrowOffset;
                CreateArrow(2, position);
            }

            if (x < xSegments - 1)
            {
                Vector3 position = new Vector3(x * mapBoundX, y * mapBoundY, 0);
                position.x += xArrowOffset;
                CreateArrow(3, position);
            }

            if (y != 0)
            {
                Vector3 position = new Vector3(x * mapBoundX, y * mapBoundY, 0);
                position.y -= yArrowOffset;
                CreateArrow(1, position);
            }

            if (y < ySegments - 1)
            {
                Vector3 position = new Vector3(x * mapBoundX, y * mapBoundY, 0);
                position.y += yArrowOffset;
                CreateArrow(0, position);
            }
        }
    }

    protected void CreateArrow(int direction, Vector3 position)
    {
        GameObject arrow = Instantiate(arrowPrefab, position, Quaternion.identity);

        MapTransitionArrow mapArrow = arrow.GetComponent<MapTransitionArrow>();

        if(mapArrow != null)
        {
            mapArrow.UpdateDirection(direction);
        }
    }
}
