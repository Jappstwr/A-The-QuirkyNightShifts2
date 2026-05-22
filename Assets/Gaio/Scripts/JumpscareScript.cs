using Unity.VisualScripting;
using UnityEngine;

public class JumpscareScript : MonoBehaviour
{
    public NightScript nightScript;
    public float time;
    public float staticTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            staticTime -= Time.deltaTime;
            if (staticTime <= 0)
            {
                nightScript._isDead = true;
            }
        }
    }
}
