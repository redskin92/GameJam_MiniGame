using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.zero;
    public float speed = 5.0f;
    public KeyCode rotateClockwise;
    public KeyCode rotateCounterClockwise;
    public KeyCode charge;
    public float baseCharge = .5f;
    public float maxCharge = 10.0f;
    public float chargeMultiplier = 25.0f;
    public float angleChange = 5.0f;
    public int id = 0;


    public float chargeAmount = 2.0f;

    public float lineLength = 2.0f;
    private Rigidbody2D rigidBody2D;
    private LineRenderer lineRenderer;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;

        spriteRenderer.sprite = GameFlow.Instance.GetPlayerSprite(id);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + (moveDirection * (chargeAmount / maxCharge) * lineLength));

        if (Input.GetKey(charge))
        {
            chargeAmount += .1f;
            chargeAmount = Mathf.Clamp(chargeAmount, baseCharge, maxCharge);
        }
        if(Input.GetKeyUp(charge))
        {
            rigidBody2D.velocity = Vector3.zero;
            rigidBody2D.angularVelocity = 0.0f;
            rigidBody2D.AddForce(new Vector2(0, 0));
            rigidBody2D.AddForce(new Vector2(moveDirection.x, moveDirection.y) * chargeAmount * chargeMultiplier);
            chargeAmount = baseCharge;

        }
        if (Input.GetKey(rotateClockwise))
        {
            moveDirection = Quaternion.AngleAxis(-angleChange * Time.deltaTime, Vector3.forward) * moveDirection;
        }
        if (Input.GetKey(rotateCounterClockwise))
        {
            moveDirection = Quaternion.AngleAxis(angleChange * Time.deltaTime, Vector3.forward) * moveDirection;
        }
    }
}
