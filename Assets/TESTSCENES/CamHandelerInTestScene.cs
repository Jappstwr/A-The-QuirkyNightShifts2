using Unity.VisualScripting;
using UnityEngine;

public class CamHandelerInTestScene : MonoBehaviour
{
    [SerializeField] private GameObject CAMS;
    [SerializeField] private GameObject CAMSImage;
    [SerializeField] private GameObject Office;

    [SerializeField] private NightScript nightscript;

    private bool hasClosedCameras; 
 

    void Start()
    {
        CAMSImage.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToOffice()
    {
        
        if (nightscript.scanTimer <= 0f)
        {
            hasClosedCameras = true; 
            nightscript._monitorOpen = false; 
            CAMS.SetActive(false);
            CAMSImage.SetActive(false);
            Office.SetActive(true);
        }
        else if (nightscript.scanTimer > 0f) 
        {
            hasClosedCameras = false; 
            nightscript._monitorOpen = true; 
            Debug.Log("Cannot close whilst scanning!"); 
        }
        
        if (!hasClosedCameras && nightscript.scanTimer > 0f)
        {
            CAMS.SetActive(false);
            CAMSImage.SetActive(false);
            Office.SetActive(false);
        }
        
    }
    public void SwitchToCams()
    {
        if (!nightscript._powerOutage)
        {
            //nightscript._monitorOpen = true;
            CAMS.SetActive(true);
            CAMSImage.SetActive(true);
            Office.SetActive(false);
        }
        else if (nightscript._powerOutage)
        {
            CAMS.SetActive(false);
            CAMSImage.SetActive(false);
            Office.SetActive(true);
        }
    }


}
