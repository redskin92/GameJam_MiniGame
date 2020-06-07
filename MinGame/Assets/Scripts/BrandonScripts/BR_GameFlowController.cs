using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BR_GameFlowController : MonoBehaviour
{
    public Camera mainCamera;

    enum PlayerTurn { NONE, PLAYER1, PLAYER2 };

    private PlayerTurn currentPlayersTurn;

    [SerializeField]
    private GameObject bombObject;


    [SerializeField]
    private TextMesh dragText;

    private BR_Bomb currentBomb;

    [SerializeField]
    private BR_Player player1, player2;

    private bool trackMouse = false;
    private bool trackbomb = false;

    Vector3 originalMousePosition;
    Vector3 dragVector;
    private BoxCollider2D clickCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayersTurn = UnityEngine.Random.Range(0, 1) == 1 ? PlayerTurn.PLAYER1 : PlayerTurn.PLAYER2;

        clickCollider = GetComponent<BoxCollider2D>();

        SetUpPlayer(false);
        dragText.gameObject.SetActive(false);

        player1.SetSprite(GameFlow.Instance.GetPlayerSprite(0));
        player2.SetSprite(GameFlow.Instance.GetPlayerSprite(1));
    }

    private void SetUpPlayer(bool moveNext = false)
    {
        if (moveNext)
        {
            currentPlayersTurn = currentPlayersTurn == PlayerTurn.PLAYER1 ? PlayerTurn.PLAYER2 : PlayerTurn.PLAYER1;
        }

        mainCamera.transform.position = GetPlayer().GetCameraStartOffset();
        trackMouse = true;
        clickCollider.enabled = true;
    }


    private void OnMouseDown()
    {
        if (!trackMouse)
            return;
        Debug.Log("OnMouseDown");

        dragVector = Vector3.zero;
        originalMousePosition = Input.mousePosition;

        GameObject newBomb = GameObject.Instantiate(bombObject);

        currentBomb = newBomb.GetComponent<BR_Bomb>();
        currentBomb.transform.position = GetPlayerPositions();
        currentBomb.ArmBomb();
        currentBomb.bombExploded += BombFinished;

        dragText.gameObject.SetActive(true);

    }


    private void BombFinished(object sender, EventArgs e)
    {
        dragText.gameObject.SetActive(false);
        trackMouse = false;
        currentBomb.bombExploded -= BombFinished;

        trackbomb = false;
        Destroy(currentBomb.gameObject);
        currentBomb = null;

        if (CheckEnd())
        {
            int player1Score = player2.GetScore();
            int player2Score = player1.GetScore();

            if(player1.IsDead())
            {
                player2Score += 100;
            }
            else if(player2.IsDead())
            {
                player1Score += 100;
            }
            
            GameFlow.Instance.LevelComplete(new List<int>() { player1Score, player2Score });
        }
        else
        {
            SetUpPlayer(true);
        }

    }

    private void OnMouseUp()
    {
        if (!trackMouse || currentBomb == null)
            return;

        trackMouse = false;
        clickCollider.enabled = false;

        Debug.Log(dragVector.magnitude);


        float throwVelocity = Mathf.Min(dragVector.magnitude, 500.0f) / 500.0f;
        throwVelocity = 100 * throwVelocity;

        Vector2 directionVec = dragVector.normalized * throwVelocity;

        currentBomb.Throw(directionVec);
        trackbomb = true;
        Debug.Log("Throwing at " + directionVec);
        dragText.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (trackMouse)
        {
            dragVector = originalMousePosition - Input.mousePosition;

            float throwVelocity = Mathf.Min(dragVector.magnitude, 500.0f) / 500.0f;
            throwVelocity = 100 * throwVelocity;

            float angle = Vector3.Angle(dragVector, currentPlayersTurn == PlayerTurn.PLAYER1 ? Vector3.right : Vector3.left);

            dragText.text = throwVelocity.ToString() + "%\nAngle = " + angle +" deg";

        }
        if (trackbomb)
        {
            mainCamera.transform.position = new Vector3(currentBomb.transform.position.x, currentBomb.transform.position.y, -10);
        }
    }


    private BR_Player GetPlayer()
    {
        if (currentPlayersTurn == PlayerTurn.PLAYER1)
            return player1;
        else if (currentPlayersTurn == PlayerTurn.PLAYER2)
            return player2;

        return null;
    }

    private Vector2 GetPlayerPositions()
    {
        if (currentPlayersTurn == PlayerTurn.PLAYER1)
            return player1.GetBombStartPosition();
        else if (currentPlayersTurn == PlayerTurn.PLAYER2)
            return player2.GetBombStartPosition();
        return Vector2.zero;
    }

    private bool CheckEnd()
    {
        if (player1.IsDead())
        {
            return true;
        }
        if (player2.IsDead())
        {
            return true;
        }
        return false;
    }
}
