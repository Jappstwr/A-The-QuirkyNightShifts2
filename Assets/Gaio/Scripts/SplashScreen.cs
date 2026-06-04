using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float Splashtimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Splashtimer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Splashtimer -= Time.deltaTime;
        if (Splashtimer <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
