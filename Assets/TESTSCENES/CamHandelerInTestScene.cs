using UnityEngine;

public class CamHandelerInTestScene : MonoBehaviour
{
    [SerializeField] private GameObject CAMS;
    [SerializeField] private GameObject CAMSImage;
    [SerializeField] private GameObject Office;
    [SerializeField] private GameObject MonitorView; 

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
        CAMS.SetActive(false);
        Office.SetActive(true);
        MonitorView.gameObject.SetActive(false); 
    }
    public void SwitchToCams()
    {
        CAMS.SetActive(true);
        CAMS.SetActive(true);
        MonitorView.gameObject.SetActive(false);
        Office.SetActive(false);
    }


}
