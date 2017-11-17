using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScreenSizeController : MonoBehaviour {

    private int height, width;

    // Use this for initialization
    void Start () {

        height = Screen.height;
        width = Screen.width;

        var rectH = GetComponent<RectTransform>();

        rectH.sizeDelta = new Vector2(width, height/2);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
