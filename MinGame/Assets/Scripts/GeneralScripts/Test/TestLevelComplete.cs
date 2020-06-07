// fuck file headers copy my code idc
// this class deals with moving a level along in case someone doesnt show up and do anything
// i hope these comments are helpful and not douchey
// idk just kinda feel like it might be seen that way

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelComplete : MonoBehaviour
{
    public float waitTimeToComplete = 3.0f;

    bool called = false;

	void Update ()
    {
        waitTimeToComplete -= Time.deltaTime;

        // just shows what needs to be called to end a level
        // if we call it multiple times the fade manager will continue to fade
        // so try not to call LevelComplete multiple times plz ;)
        // unless you wanna troll
        if(waitTimeToComplete <= 0 && !called)
        {
            called = true;
            
            // all that should happen is that this is called at the end of your game
            GameFlow.Instance.LevelComplete(new List<int>() { 0, 0 });
        }
	}
}
