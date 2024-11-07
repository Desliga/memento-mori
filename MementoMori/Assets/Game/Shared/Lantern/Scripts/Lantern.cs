using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Lantern : MonoBehaviour
{
    private LanternCollect _lanternCollect;
    private Camera _mainCamera;

    public LayerMask itemLayer;
    public Light lanternLight;

    void Start()
    {
        _lanternCollect = GetComponent<LanternCollect>();

        _mainCamera = Camera.main;
    }

    void Update()
    {
        _lanternCollect.DetectObjects(_mainCamera, itemLayer);

        if (_lanternCollect.isCollecting)
        {
            lanternLight.intensity += 10 * Time.deltaTime;
            //lanternLight.innerSpotAngle -= 1;
            lanternLight.spotAngle -= 10f * Time.deltaTime;
        } else
        {
            lanternLight.intensity = 5;
            lanternLight.spotAngle = 80;
            lanternLight.innerSpotAngle = 60;
        }
    }
}
