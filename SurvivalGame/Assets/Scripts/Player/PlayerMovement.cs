using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LevelSegment levelSegment;

    [SerializeField]
    private float xBounds;

    [SerializeField]
    private float yBounds;

    [SerializeField]
    private float moveSpeed;

    // Update is called once per frame
    private void Update()
    {
        //if (GameFlow.Instance != null && !GameFlow.Instance.paused)
        //{
            float x = 0;
            float y = 0;

            //Detect when the up arrow key is pressed down
            if (Input.GetKey(KeyCode.A))
            {
                x = -1;
            }

            //Detect when the up arrow key has been released
            if (Input.GetKey(KeyCode.D))
            {
                x = 1;
            }

            //Detect when the up arrow key is pressed down
            if (Input.GetKey(KeyCode.W))
            {
                y = 1;
            }

            //Detect when the up arrow key is pressed down
            if (Input.GetKey(KeyCode.S))
            {
                y = -1;
            }

            ApplyMovement(x, y);
        //}
    }

    public void ApplyMovement(float x, float y)
    {
        Vector3 normalizedMovement = new Vector3(x, y, 0);

        normalizedMovement.Normalize();

        if (levelSegment != null)
        {
            normalizedMovement = levelSegment.SetPlayerBoundsPosition(transform.position, normalizedMovement, transform.localScale.x / 2, transform.localScale.y / 2);
        }
        else
        {
            UnityEngine.Debug.LogError("Map bounds not set!");
        }

        normalizedMovement.x *= moveSpeed * Time.deltaTime;

        normalizedMovement.y *= moveSpeed * Time.deltaTime;

        transform.position += normalizedMovement;
    }
}
