using UnityEngine;
using UnityEngine.Rendering;

public class ScanMarkerScript : MonoBehaviour
{
    public float activeTime;
    [SerializeField] private float currentTime;

    public Vector3 scanPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    

    public void Activate()
    {
        currentTime = activeTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 4 && currentTime > 3.5 ||
            currentTime < 3 && currentTime > 2.5 ||
            currentTime < 2 && currentTime > 1.5 ||
            currentTime < 1 && currentTime > 0.5)
        {
            gameObject.transform.localPosition = scanPosition;
        }
        else if (currentTime <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(0, 500);
        }

       
    }
}
