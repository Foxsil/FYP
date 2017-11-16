using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace packt.FoodyGO.UI
{
    [RequireComponent(typeof(RawImage))]
    public class CameraTextureOnRawImage : MonoBehaviour
    {

        private Gyroscope gyro;
        private bool gyroSupported;
        private Quaternion rotFix;

        //public GameObject ARToggle;
        private bool camAvail;


        void Start()
        {
            //CheckARSupported();

            Screen.orientation = ScreenOrientation.Portrait;
        }

        public RawImage rawImage;
        public WebCamTexture webcamTexture;       
        public AspectRatioFitter aspectFitter;
        void Awake()
        {
            StartCam();

            //CheckARSupported();
        }

        private void OnEnable()
        {
            StartCam();
                
               //CheckARSupported();
            
        }

        //void CheckARSupported()
        //{
        //    gyroSupported = SystemInfo.supportsGyroscope;

        //    //if (webcamTexture == null)
        //    //{
        //    //    camAvail = false;
        //    //}
        //    //else {
        //    //    camAvail = true;
        //    //}


        //    if (!gyroSupported )//|| !webcamTexture.isPlaying)
        //    {
        //        ARToggle.SetActive(false);

        //        StartCam();

        //        print("Gyro or Cam not available.");
        //    }
        //    else
        //    {
        //        ARToggle.SetActive(true);

        //        //Screen.orientation = ScreenOrientation.Portrait;

        //        StartCam();

        //        print("Gyro or Cam available.");
        //    }
        //}

        void Update()
        {

            //if (camAvail)
            //{
            if (webcamTexture.isPlaying == false )//|| webcamTexture == null)
            {
                return;
            }
            else
            {
                var camRotation = -webcamTexture.videoRotationAngle;
                if (webcamTexture.videoVerticallyMirrored)
                {
                    camRotation += 180;
                }

                rawImage.transform.localEulerAngles = new Vector3(0f, 0f, camRotation);

                var videoRatio = (float)webcamTexture.width / (float)webcamTexture.height;
                aspectFitter.aspectRatio = videoRatio;

                if (webcamTexture.videoVerticallyMirrored)
                {
                    rawImage.uvRect = new Rect(1, 0, -1, 1);
                }
                else
                {
                    rawImage.uvRect = new Rect(0, 0, 1, 1);
                }
            }
            //}
        }

        void StartCam()
        {
            webcamTexture = new WebCamTexture(Screen.width, Screen.height);
            rawImage = GetComponent<RawImage>();
            aspectFitter = GetComponent<AspectRatioFitter>();

            rawImage.texture = new Texture();
            rawImage.texture = webcamTexture;
            rawImage.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }

    }
}
