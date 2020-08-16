using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float xBounds;

    [SerializeField]
    private float yBounds;

    [SerializeField]
    private float moveSpeed;

    // Update is called once per frame
    private void Update()
    {
        //Detect when the up arrow key is pressed down
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }

        //Detect when the up arrow key has been released
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        //Detect when the up arrow key is pressed down
        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }

        //Detect when the up arrow key is pressed down
        if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
    }

    private void MoveLeft()
    {
        float currentX = transform.position.x;

        if (currentX - moveSpeed >= -xBounds)
        {
            currentX -= moveSpeed;
        }
        else
        {
            currentX = -xBounds;
        }

        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }

    private void MoveRight()
    {
        float currentX = transform.position.x;

        if (currentX + moveSpeed <= xBounds)
        {
            currentX += moveSpeed;
        }
        else
        {
            currentX = xBounds;
        }

        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }

    private void MoveDown()
    {
        float currentY = transform.position.y;

        if (currentY - moveSpeed >= -yBounds)
        {
            currentY -= moveSpeed;
        }
        else
        {
            currentY = -yBounds;
        }

        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }

    private void MoveUp()
    {
        float currentY = transform.position.y;

        if (currentY + moveSpeed <= yBounds)
        {
            currentY += moveSpeed;
        }
        else
        {
            currentY = yBounds;
        }

        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}
