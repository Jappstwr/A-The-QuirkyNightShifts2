using UnityEngine;

public class FerdinandScriptHolder : MonoBehaviour
{
    [SerializeField] public SpriteRenderer ferdinandSr;
    [SerializeField] private NightScript nightscript;

    public float aiTimer; 

    void Start()
    {
        GetNight();  
    }

    // Update is called once per frame
    void Update()
    {
        aiTimer += Time.deltaTime; 

        GetNight();
    }

    public void GetNight() 
    {
        if (nightscript.Night == 4 && aiTimer >= 120)
        {
            ferdinandSr.enabled = false;
        }
        else
        {
            ferdinandSr.enabled = true;
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
