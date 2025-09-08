using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager instance;    

    private Camera cam;

    [SerializeField]
    private Camera screenshotCam;
    private Camera parentCamContainer;
    public RenderTexture lastScreenshotTaken;
    public static Texture2D screenshotTexture;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    IEnumerator TakeScreenshot(float delay=0.1f)
    {    
        screenshotCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        yield return new WaitForEndOfFrame();

        RenderTexture scaledTexture = RenderTexture.GetTemporary(1080, 1080);
        Graphics.Blit(lastScreenshotTaken, scaledTexture);
        screenshotTexture = new Texture2D(scaledTexture.width, scaledTexture.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, scaledTexture.width, scaledTexture.height), 0, 0);
        screenshotTexture.Apply();
        RenderTexture.ReleaseTemporary(scaledTexture);
        screenshotCam.gameObject.SetActive(false);

        this.UploadScreenshot(screenshotTexture);
    }

    private void UploadScreenshot(Texture2D screenshotTexture)
    {
        UploadThumbnailAsyncRequest request = new UploadThumbnailAsyncRequest(PlayerPrefs.GetString("username", "test"), PlayerPrefs.GetInt("videoCount"), screenshotTexture);
        request.Send();
    }

    public void TakeImmediateScreenshot()
    {
        StartCoroutine(TakeScreenshot());
    }
}
