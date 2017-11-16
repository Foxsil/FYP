using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInput : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;
    private Camera cam;
    float distance = 10.0f;
    private float currentX, currentY = 15.0f;
    public float perspectiveZoomSpeed = 0.5f, yZoomSpeed = 1.0f;
    private Vector2 start;

    public Toggle toggle;

    public bool AROn;

    private void Start()
    {
        AROn = false;

        camTransform = transform;
        cam = Camera.main;

        
    }

    public void TurnAROn ()
    {
        AROn = true;

        Debug.Log("AR is on");
    }

    public void TurnAROff()
    {
        AROn = false;

        Debug.Log("AR is off");
    }

   
public void MyListener(bool value)
{
    if (value)
    {
            TurnAROn();
    }
    else
    {
            TurnAROff();
    }

}

private void Update()
    {
        //toggle.onValueChanged.AddListener((AROn) => {
        //    MyListener(AROn);
        //});


        if (Input.touchCount == 1 )//&& toggle == false)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                start = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                currentX += (touch.position.x - start.x);

                start = touch.position;
            }
        }

        //if (toggle)
        //{
        //    transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
        //}

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            //currentX += prevTouchDeltaMag; //Input.GetAxis("Mouse X");
            currentY += deltaMagnitudeDiff * perspectiveZoomSpeed; //Input.GetAxis("Mouse Y");

            currentY = Mathf.Clamp(currentY, 20.0f, 60.0f);

            // Otherwise change the field of view based on the change in distance between the touches.
            Camera.current.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

            // Clamp the field of view to make sure it's between 0 and 180.
            Camera.current.fieldOfView = Mathf.Clamp(Camera.current.fieldOfView, 10.0f, 50.0f);
        }
    }


    private void LateUpdate()
    {

        Vector3 dir = new Vector3(0, 0, -distance);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        camTransform.position = lookAt.position + rotation * dir;

        camTransform.LookAt(lookAt.position);

    }
}
