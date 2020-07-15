using UnityEngine;

public class ChangeMaterialColorAccent : MonoBehaviour
{
    Renderer materialRenderer;

    private void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
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
                if (ColorUtility.TryParseHtmlString("#D4C973", out color))
                { materialRenderer.material.SetColor("_Color", color);
                }
                break;
            case 2:
                if (ColorUtility.TryParseHtmlString("#2ADF66", out color))
                { materialRenderer.material.SetColor("_Color", color);
                }
                break;
            case 3:
                if (ColorUtility.TryParseHtmlString("#CC3354", out color))
                { materialRenderer.material.SetColor("_Color", color); }
                break;
            case 4:
                if (ColorUtility.TryParseHtmlString("#219FDE", out color))
                { materialRenderer.material.SetColor("_Color", color); }
                break;
            default:
                if (ColorUtility.TryParseHtmlString("#FF45C5", out color))
                { materialRenderer.material.SetColor("_Color", color); }
                break;
        }
    }
}
