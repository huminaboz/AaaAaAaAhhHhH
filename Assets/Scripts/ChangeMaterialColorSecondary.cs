using UnityEngine;

public class ChangeMaterialColorSecondary : MonoBehaviour
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

        if (UIManager.I)
        {
            switch (UIManager.I.currentColorTier)
            {
                case 1:
                    if (ColorUtility.TryParseHtmlString("#5CFCFF", out color))
                    {
                        materialRenderer.material.SetColor("_Color", color);
                    }
                    break;
                case 2:
                    if (ColorUtility.TryParseHtmlString("#B719F0", out color))
                    {
                        materialRenderer.material.SetColor("_Color", color);
                    }
                    break;
                case 3:
                    if (ColorUtility.TryParseHtmlString("#FCF39F", out color))
                    { materialRenderer.material.SetColor("_Color", color); }
                    break;
                case 4:
                    if (ColorUtility.TryParseHtmlString("#F00F4B", out color))
                    { materialRenderer.material.SetColor("_Color", color); }
                    break;
                default:
                    if (ColorUtility.TryParseHtmlString("#4EB1B1", out color))
                    { materialRenderer.material.SetColor("_Color", color); }
                    break;
            }
        }
    }
}
