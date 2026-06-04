using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteScript : MonoBehaviour
{
    public float Completetimer;
    public GameObject[] texts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Completetimer = 16;
        texts[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Completetimer -= Time.deltaTime;
        if (Completetimer <= 12)
        {
            texts[1].SetActive(true);
        }
        if (Completetimer <= 8)
        {
            texts[2].SetActive(true);
        }
        if (Completetimer <= 4)
        {
            texts[3].SetActive(true);
        }
        if (Completetimer <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
