using UnityEngine;

public class FerdinandScriptHolder : MonoBehaviour
{
    [SerializeField] public SpriteRenderer ferdinandSr;
    [SerializeField] private NightScript nightscript; 

    void Start()
    {
        GetNight();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetNight() 
    {
        if (nightscript.Night == 4)
        {
            ferdinandSr.enabled = false;
        }
        if (nightscript.Night >= 5)
        {
            ferdinandSr.enabled = false; 
        }
        else
        {
            ferdinandSr.enabled = true; 
        }

    }
}
