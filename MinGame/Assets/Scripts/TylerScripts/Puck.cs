using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puck : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    public Text player1Score;
    public Text player2Score;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.position = Vector3.zero;
        rigidBody2D.velocity = Vector3.zero;
        rigidBody2D.angularVelocity = 0.0f;
        if (other.gameObject.tag == "GoalLeft")
        {
            ++TylerLevelComplete.player2Score;
            player2Score.text = TylerLevelComplete.player2Score.ToString();

        }
        else if (other.gameObject.tag == "GoalRight")
        {

            ++TylerLevelComplete.player1Score;
            player1Score.text = TylerLevelComplete.player1Score.ToString();
        }
    }
}
