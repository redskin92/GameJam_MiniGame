// fuck file headers copy my code idc
// this class deals with selecting chracters in the CharacterSelect scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionScreen : MonoBehaviour
{
    public VirtualKeyboard keyboard;

    // not needed unless back is a thing
    //public GameObject player1Position;

    public GameObject player2Position;

    public Text player1DisplayName;

    public List<Image> characterImages;

    List<string> characterNames;
    List<int> spritesUsed;

    int playerTurn;

    const int NUM_PLAYERS = 2;

    int player1CharacterIndex;

    int player2CharacterIndex;

    private void Awake ()
    {
        characterNames = new List<string>();
        spritesUsed = new List<int>();
        playerTurn = 0;
        player1CharacterIndex = 0;
        player2CharacterIndex = 0;
    }

    // Update the keyboard
    private void Update ()
    {
        string keyboardString = keyboard.UpdateKeyboard();

        if(keyboardString != null)
        {
            SetCharacterName(keyboardString);
        }
    }

    // set the character name and continue if the last player has chosen
    public void SetCharacterName(string name)
    {
        if(name != null && name != "")
        {
            characterNames.Add(name);

            // if player one set up for player two
            if (playerTurn == 0)
            {
                keyboard.transform.position = player2Position.transform.position;

                player1DisplayName.gameObject.SetActive(true);
                player1DisplayName.text = name;

                keyboard.BlankText();

                spritesUsed.Add(player1CharacterIndex);
            }
            else
            {
                spritesUsed.Add(player2CharacterIndex);
            }

            ++playerTurn;
        }

        // if the 2nd player has choseen, set the next state
        if(playerTurn >= NUM_PLAYERS)
        {
            GameFlow.Instance.CharacterSelectionComplete(characterNames, spritesUsed);
        }
    }

    // set player one index to the left
    public void PlayerOneArrowUp()
    {
        ++player1CharacterIndex;

        if(player1CharacterIndex >= GameFlow.Instance.CharacterSprites.Count)
        {
            player1CharacterIndex = 0;
        }

        DisplayCurrentImage();
    }

    // set player two index to the left
    public void PlayerTwoArrowUp()
    {
        ++player2CharacterIndex;

        if (player2CharacterIndex >= GameFlow.Instance.CharacterSprites.Count)
        {
            player2CharacterIndex = 0;
        }

        DisplayCurrentImage();
    }

    // set player one index to the right
    public void PlayerOneArrowDown()
    {
        --player1CharacterIndex;

        if (player1CharacterIndex < 0)
        {
            player1CharacterIndex = GameFlow.Instance.CharacterSprites.Count - 1;
        }

        DisplayCurrentImage();
    }

    // set player two index to the right
    public void PlayerTwoArrowDown()
    {
        --player2CharacterIndex;

        if (player2CharacterIndex < 0)
        {
            player2CharacterIndex = GameFlow.Instance.CharacterSprites.Count - 1;
        }

        DisplayCurrentImage();
    }

    // display the current sprite for each player
    public void DisplayCurrentImage()
    {
        if(characterImages.Count >= 1 && GameFlow.Instance.CharacterSprites.Count > player1CharacterIndex)
        {
            characterImages[0].sprite = GameFlow.Instance.CharacterSprites[player1CharacterIndex];
        }

        if (characterImages.Count >= 2 && GameFlow.Instance.CharacterSprites.Count > player2CharacterIndex)
        {
            characterImages[1].sprite = GameFlow.Instance.CharacterSprites[player2CharacterIndex];
        }
    }
}
