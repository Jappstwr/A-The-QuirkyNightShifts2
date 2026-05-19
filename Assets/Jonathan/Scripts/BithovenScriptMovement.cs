using UnityEngine;
using UnityEngine.UI;

public class BithovenScriptMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private WindUpButton windUpSystem;
    [SerializeField] private SpriteRenderer bithovenRenderer;
    [SerializeField] private int roomIndex = 2;
    [SerializeField] private MainCameraScript camSystem;

    [Header("Sprite")]
    [SerializeField] private Sprite sleepSprite;
    [SerializeField] private Sprite awakeSprite;

    private bool awake = false;

    [Header("WayPoint")]
    [SerializeField] private Waypoints waypoints; 

    void Start()
    {
        transform.position = waypoints.transform.position;

        int Layer = LayerMask.NameToLayer(waypoints.roomLayer);

        if (Layer != -1)
        {
            gameObject.layer = Layer;
        }

        //bithovenRenderer.sortingLayerName = waypoints.roomLayer;
        //bithovenRenderer.sortingOrder = 10;

        bithovenRenderer.sprite = sleepSprite; 
    }

    // Update is called once per frame
    void Update()
    {
        bool visible = camSystem.ActiveCamera == roomIndex && camSystem.camerasOpen;

        bithovenRenderer.enabled = visible;

        float fill = windUpSystem.FillPercent; 
        
        if (fill <= 0.3f)
        {
            awake = true;

            bithovenRenderer.sprite = awakeSprite;

            
        }
        else
        {
            if (awake)
            {
                awake = false;

                bithovenRenderer.sprite = sleepSprite;
            }
        }

        if (fill <= 0f)
        {
            Debug.Log("Bithoven attack! Game Over!");

            Jumpscare(); 
        }
    }

    public void Jumpscare()
    {
       //Gaio, here goes the jumpscare       
    }

    public void StartPos(int Index)
    {
        
    }
}
