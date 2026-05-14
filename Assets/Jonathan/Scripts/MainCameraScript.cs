using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class MainCameraScript : MonoBehaviour
{
    [System.Serializable]
    public class CameraRooms
    {
        public GameObject cameraObject;
        public string roomName; 
    }

    [Header("Camera Setup")]
    [SerializeField] private CameraRooms[] Cameras;
    [SerializeField] private int startingCameraIndex = 1;

    public int StartingCameraIndex => startingCameraIndex;

    private int activeCamera = 0;
    public bool camerasOpen = false;

    [Header("UI")]
    [SerializeField] private TMP_Text roomText;  


    void Start()
    {
        camerasOpen = false;  
        
        foreach (var cam in Cameras)
        {
            if (cam.cameraObject != null)
            {
                cam.cameraObject.SetActive(false); 
            }
        }

        //Turn on Stage camera (stage index, 1)
        //if (Cameras != null && startingCameraIndex >= 0 && startingCameraIndex < Cameras.Length)
        //{
        //    Cameras[startingCameraIndex].SetActive(true);
        //    activeCamera = startingCameraIndex;

        //}

        SwitchCamera(startingCameraIndex); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //ben was here... idk
    public void SwitchCamera(int cameraIndex)
    {
        if ( cameraIndex < 0 || cameraIndex >= Cameras.Length)
        {
            return; 
        }

        activeCamera = cameraIndex;
        camerasOpen = true; 
        
        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].cameraObject.SetActive(i == cameraIndex); 
        }


        roomText.text = Cameras[cameraIndex].roomName;
    }

    public void CloseCameras()
    {
        camerasOpen = false; 

        foreach (var cam in Cameras)
        {
            cam.cameraObject.SetActive(false); 
        }

        roomText.text = "";
    }
}
