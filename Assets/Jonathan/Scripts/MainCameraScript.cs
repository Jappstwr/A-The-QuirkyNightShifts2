using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class MainCameraScript : MonoBehaviour
{
    [Header("Camera Setup")]
    [SerializeField] private GameObject[] Cameras;
    [SerializeField] private int startingCameraIndex = 1;
    [SerializeField] private GameObject[] Text;
    [SerializeField] private int startingTextIndex = 1; 
    public int StartingCameraIndex => startingCameraIndex;
    public int StartingTextIndex => startingTextIndex;

    private int activeCamera = 0;
    public bool camerasOpen = false;

    private int activeText = 0; 


    
    void Start()
    {
        camerasOpen = false;  
        
        foreach (var cam in Cameras)
        {
            if (cam != null)
            {
                cam.SetActive(false); 
            }
        }

        if (Cameras != null && startingCameraIndex >= 0 && startingCameraIndex < Cameras.Length)
        {
            Cameras[startingCameraIndex].SetActive(true);
            activeCamera = startingCameraIndex; 

        }

        if (Text != null && startingCameraIndex >= 0 && startingCameraIndex < Text.Length)
        {
            Text[startingTextIndex].SetActive(true);
            activeCamera = startingTextIndex;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCamera(int camera, int text)
    {
        activeText = text; 
        activeCamera = camera;
        camerasOpen = true; 



        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].SetActive(i == camera); 
        }
        for (int i = 0; i < Text.Length; i++)
        {
            Text[i].SetActive(i == text); 
        }
    }

    public void CloseCameras()
    {
        camerasOpen = false; 

        foreach (var cam in Cameras)
        {
            cam.SetActive(false); 
        }
        foreach (var text in Text)
        {
            text.SetActive(false); 
        }
    }
}
