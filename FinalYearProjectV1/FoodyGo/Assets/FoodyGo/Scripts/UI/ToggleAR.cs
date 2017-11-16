using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAR : MonoBehaviour {

    private Gyroscope gyro;
    private bool gyroSupported;
    private Quaternion rotFix;

    public GameObject ARToggle; 

    // Use this for initialization
    void Start () {

        gyroSupported = SystemInfo.supportsGyroscope;

        CheckARSupported();

    }

    private void OnEnable()
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        CheckARSupported();
    }

    void CheckARSupported()
    {
        if (gyroSupported)
        {
            ARToggle.SetActive(true);
        }
        else
        {
            ARToggle.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
