using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float passiveNoiseDistance;
    public float noiseDepreciationRate;
    public float noiseDistance;
    private float noiseDepreciation;

    public void Update()
    {

        if (noiseDistance > passiveNoiseDistance) //so long as noise is above the passive noise
        {
            noiseDistance -= noiseDepreciation; //depreciate the noise

        }
        else noiseDistance = passiveNoiseDistance; //set to min


        noiseDepreciation = (noiseDepreciationRate * Time.deltaTime) / noiseDistance; //slower the louder it is
    }

    //update the noise if louder than current noise
    public void updateNoise(float newNoise)
    {
        if (newNoise > noiseDistance)
        {
            //set the new noise
            noiseDistance = newNoise;
        }
    }


}
