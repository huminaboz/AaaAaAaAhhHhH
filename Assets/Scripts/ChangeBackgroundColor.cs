using UnityEngine;

public class ChangeBackgroundColor : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void OnEnable()
    {
        UIManager.OnNewColorTier += UpdateColor;
    }


    void OnDisable()
    {
        UIManager.OnNewColorTier -= UpdateColor;
    }

    private void Start()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        Color color;

        switch (UIManager.I.currentColorTier)
        {
            case 1:
                if (ColorUtility.TryParseHtmlString("#C77AC2", out color))
                {
                    cam.backgroundColor = color;
                }
                break;
            case 2:
                if (ColorUtility.TryParseHtmlString("#392654", out color))
                {
                    cam.backgroundColor = color;
                }
                break;
            case 3:
                if (ColorUtility.TryParseHtmlString("#090D1B", out color))
                { cam.backgroundColor = color; }
                break;
            case 4:
                if (ColorUtility.TryParseHtmlString("#EDF4F7", out color))
                { cam.backgroundColor = color; }
                break;
            default:
                if (ColorUtility.TryParseHtmlString("#0F2039", out color))
                { cam.backgroundColor = color; }
                break;
        }
    }
}
