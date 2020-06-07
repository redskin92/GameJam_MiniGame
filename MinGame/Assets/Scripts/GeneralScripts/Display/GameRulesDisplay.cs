// fuck file headers copy my code idc
// this class deals with displaying the game rules image 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRulesDisplay : MonoBehaviour
{
    public List<Sprite> gameRulesImages;

    public Image gameRulesImage;

    void Start ()
    {
        // set the game rules image when loaded in
        // pretty simple ;)
        // i think awake is called first so this should be safe
        int level = GameFlow.Instance.CurrentLevel - 1;
        if (level < gameRulesImages.Count)
        {
            gameRulesImage.sprite = gameRulesImages[level];
        }
    }
}
