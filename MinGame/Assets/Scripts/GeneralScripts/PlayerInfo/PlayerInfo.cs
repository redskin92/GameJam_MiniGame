// fuck file headers copy my code idc
// this class deals with storing the player info recorded in the CharacterSelection scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public string name;
    public int spriteIndex;
    public int score;

    // the player info saved after CharacterSelection scene
    public PlayerInfo(string _name, int _spriteIndex)
    {
        name = _name;
        spriteIndex = _spriteIndex;
        score = 0;
    }
}
