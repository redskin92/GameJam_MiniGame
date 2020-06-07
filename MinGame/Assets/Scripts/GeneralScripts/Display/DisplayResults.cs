// fuck file headers copy my code idc
// this class deals with diaplying the results in the ShowResults scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayResults : MonoBehaviour
{
    public Text player1Name;

    public Text player2Name;

    public Text player1Score;

    public Text player2Score;

    public Image player1Image;

    public Image player2Image;

    // Use this for initialization
    void Start ()
    {
        SetScores();
	}
	
    // set the scores of each player in the results screen
    // kinda brute force but meh im lazy
	public void SetScores()
    {
        if (GameFlow.Instance.PlayerInfo.Count >= 1 && GameFlow.Instance.CharacterSprites.Count > 1)
        {
            player1Name.text = "Name: " + GameFlow.Instance.PlayerInfo[0].name;
            player1Score.text = "Score: " + GameFlow.Instance.PlayerInfo[0].score;
            player1Image.sprite = GameFlow.Instance.CharacterSprites[GameFlow.Instance.PlayerInfo[0].spriteIndex];
        }

        if (GameFlow.Instance.PlayerInfo.Count >= 2 && GameFlow.Instance.CharacterSprites.Count > 2)
        {
            player2Name.text = "Name: " + GameFlow.Instance.PlayerInfo[1].name;
            player2Score.text = "Score: " + GameFlow.Instance.PlayerInfo[1].score;
            player2Image.sprite = GameFlow.Instance.CharacterSprites[GameFlow.Instance.PlayerInfo[1].spriteIndex];
        }
    }
}
