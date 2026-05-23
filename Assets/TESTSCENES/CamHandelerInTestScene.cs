using UnityEngine;

public class CamHandelerInTestScene : MonoBehaviour
{
    [SerializeField] private GameObject CAMS;
    [SerializeField] private GameObject CAMSImage;
    [SerializeField] private GameObject Office;
    [SerializeField] private GameObject MonitorView;

    [SerializeField] private NightScript nightscript; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToOffice()
    {
        CAMS.SetActive(false);
        CAMSImage.SetActive(false);
        Office.SetActive(true);
        MonitorView.gameObject.SetActive(false); 
    }
    public void SwitchToCams()
    {
        if (!nightscript._powerOutage)
        {
            //nightscript._monitorOpen = true; 
            CAMS.SetActive(true);
            CAMSImage.SetActive(true);
            MonitorView.gameObject.SetActive(false);
            Office.SetActive(false);
        }
        else if (nightscript._powerOutage)
        {
            CAMS.SetActive(false);
            CAMSImage.SetActive(false);
            MonitorView.gameObject.SetActive(true);
            Office.SetActive(true);
        }
    }


}
