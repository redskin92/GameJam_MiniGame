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
        float x = 0;
        float y = 0;

        //Detect when the up arrow key is pressed down
        if (Input.GetKey(KeyCode.A))
        {
            x = -moveSpeed;
        }

        //Detect when the up arrow key has been released
        if (Input.GetKey(KeyCode.D))
        {
            x = moveSpeed;
        }

        //Detect when the up arrow key is pressed down
        if (Input.GetKey(KeyCode.W))
        {
            y = moveSpeed;
        }

        //Detect when the up arrow key is pressed down
        if (Input.GetKey(KeyCode.S))
        {
            y = -moveSpeed;
        }

        ApplyMovement(x,y);
    }

    public void ApplyMovement(float x, float y)
    {
        Vector3 normalizedMovement = new Vector3(x, y, 0);

        normalizedMovement.Normalize();

        normalizedMovement.x *= moveSpeed;

        normalizedMovement.y *= moveSpeed;

        transform.position += normalizedMovement;
    }
}
