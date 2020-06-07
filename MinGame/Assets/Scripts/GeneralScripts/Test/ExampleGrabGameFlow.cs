// fuck file headers copy my code idc
// this class deals with an example to show how to use GameFlow (the main class to interact in our scenes)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGrapGameFlow : MonoBehaviour {

	// Just shows some ways to interact with GameFlow
	void Start ()
    {
        UnityEngine.Debug.Log("Name: " + GameFlow.Instance.PlayerInfo[0].name);
        UnityEngine.Debug.Log("Score: " + GameFlow.Instance.PlayerInfo[0].score);
        UnityEngine.Debug.Log("ImageName: " + GameFlow.Instance.GetPlayerSprite(0).name);
    }
}
