
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
/*
public class QRCodeScanner : MonoBehaviour
{
    [SerializedField]
    private RawImage _rawImageBackground;
    [SerializedField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializedField]
    private TextMeshProUGUI _textOut;
    [SerializedField]
    private RectTransform _scanZone;

    private bool _isCamAvailable;
    private WebCamTexture _cameraTexture;


    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera(); 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
    }

    //Access camera on device, no need to go further if we dont have a camera on device
    private void SetUpCamera()
    {
        //Array of devices
        WebCamDevice[] devices WebCamTexture.devices;

        if (devices.Length == 0)
        {
            //Create
            _isCamAvailable = false;
            return;
        }

        //Handle the other case
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                //set up texture (needs a name) (Height,width)
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);

            }
        }

        //Display render
        _cameraTexture.play();
        _rawImageBackground.texture = _cameraTexture;

        _isCamAvailable = true;

    }

    private void UpdateCameraRender()
    {
        if(_isCamAvailable == false)
        {
            return;
        }

        //Cast camera texture for the full screen
        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio; //Take everything on the screen, and not adjust a small part of our device

        //handle rotation of raw image
        int orientation = -_cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }
    public void OnClickScan()
        {
            //call below function
            Scan();
        }
    //This function does the scan of the QR code
    private void Scan()
        {
            try
            {
                IBarcodeReader barcodeReader = new IBarcodeReader();

                //Take the pixel of 32 bytes
                Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);
                if(result != null)
                {
                    //show what QRcode is scanning
                    _textOut.text = result.text;
                }
                else
                {
                    _textOut.text = "FAILED TO READ QR CODE";
                }
            }
            catch
            {
                _textOut.text = "FAILED IN TRY";
            }
        }
    
}*/
