using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    [SerializeField] private bool _showSettings;
    public GameObject settingsMenu;
    public Slider audioSlider;

    public AudioClip ToggleSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LoadGame()
    {
        SoundEffectScript.Instance.officeSource.volume = audioSlider.value * 0.165f;
        SoundEffectScript.Instance.ambienceSource.volume = audioSlider.value;
        SoundEffectScript.Instance.flashlightSource.volume = audioSlider.value * 0.2f;
        SoundEffectScript.Instance.breathingSource.volume = audioSlider.value;

        SceneManager.LoadScene(2);
    }
    public void ToggleSettings()
    {
        _showSettings = !_showSettings;

        if (_showSettings)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }

        SoundEffectScript.Instance.PlaySoundEffect(ToggleSound, 1f);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    private void SettingsUpdate()
    {
        SoundEffectScript.Instance.Volume = audioSlider.value;
    }
    void Update()
    {
        if (_showSettings)
        {
            SettingsUpdate();
        }
    }
}
