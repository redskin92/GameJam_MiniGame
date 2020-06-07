using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    [SerializeField]
    private float spawnRateStart = 1.0f;

    [SerializeField]
    private float spawnRateDecreaseTime = 5.0f;

    [SerializeField]
    private float spawnRateDecrease = 0.1f;

    [SerializeField]
    private GameObject spike;

    private float spawnRateCurrent;

    private float spawnRateDecreaseCurrent;

    // Start is called before the first frame update
    void Start()
    {
        spawnRateCurrent = spawnRateStart;
        spawnRateDecreaseCurrent = spawnRateDecreaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnRateCurrent -= Time.deltaTime;

        spawnRateDecreaseCurrent -= Time.deltaTime;

        if (spawnRateCurrent <= 0)
        {
            // spawn a spike

            if(spike != null)
            {
                GameObject newObject = Instantiate(spike);

                float xPos = Random.Range(-PlayerMovement.XBOUNDS, PlayerMovement.XBOUNDS);

                newObject.transform.position = new Vector3(xPos, newObject.transform.position.y, newObject.transform.position.z);

                newObject.transform.parent = transform;
            }

            if(spawnRateDecreaseCurrent <= 0 && spawnRateStart >= spawnRateDecrease)
            {
                spawnRateStart -= spawnRateDecrease;
                spawnRateDecreaseCurrent = spawnRateDecreaseTime;
            }

            spawnRateCurrent = spawnRateStart;
        }
    }
}
