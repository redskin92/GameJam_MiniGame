using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private DisplayPlayerInfo playerInfo;

    [SerializeField]
    private float moveSpeed;

    public const float XBOUNDS = 11;

    // Update is called once per frame
    private void Update()
    {
        if (playerInfo != null)
        {
            if (playerInfo.PlayerIndex == 0)
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
            }
            else
            {
                //Detect when the up arrow key is pressed down
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    MoveLeft();
                }

                //Detect when the up arrow key has been released
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    MoveRight();
                }
            }
        }
    }

    private void MoveLeft()
    {
        float currentX = transform.position.x;

        if (currentX - moveSpeed >= -XBOUNDS)
        {
            currentX -= moveSpeed;
        }
        else
        {
            currentX = -XBOUNDS;
        }

        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }

    private void MoveRight()
    {
        float currentX = transform.position.x;

        if (currentX + moveSpeed <= XBOUNDS)
        {
            currentX += moveSpeed;
        }
        else
        {
            currentX = XBOUNDS;
        }

        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }
}
