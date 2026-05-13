using UnityEngine;
using System.Collections.Generic;

public class MainCameraScript : MonoBehaviour
{
    [Header("Camera Setup")]
    [SerializeField] private GameObject[] Cameras;
    [SerializeField] private int startingCameraIndex = 1;

    public int StartingCameraIndex => startingCameraIndex;

    private int activeCamera = 0;
    public bool camerasOpen = false; 


    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCamera(int camera)
    {
        activeCamera = camera;
        camerasOpen = true; 

        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].SetActive(i == camera); 
        }
    }

    public void CloseCameras()
    {
        camerasOpen = false; 

        foreach (var cam in Cameras)
        {
            cam.SetActive(false); 
        }
    }
}
