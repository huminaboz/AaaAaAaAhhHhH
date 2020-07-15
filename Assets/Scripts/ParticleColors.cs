using UnityEngine;

public class ParticleColors : MonoBehaviour
{

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
        ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;

        switch (UIManager.I.currentColorTier)
        {
            case 1:
                if (ColorUtility.TryParseHtmlString("#D4C973", out color))
                { settings.startColor = new ParticleSystem.MinMaxGradient(color); }
                break;
            case 2:
                if (ColorUtility.TryParseHtmlString("#2ADF66", out color))
                { settings.startColor = new ParticleSystem.MinMaxGradient(color); }
                break;
            case 3:
                if (ColorUtility.TryParseHtmlString("#CC3354", out color))
                { settings.startColor = new ParticleSystem.MinMaxGradient(color); }
                break;
            case 4:
                if (ColorUtility.TryParseHtmlString("#219FDE", out color))
                { settings.startColor = new ParticleSystem.MinMaxGradient(color); }
                break;
            default:
                if (ColorUtility.TryParseHtmlString("#FF45C5", out color))
                { settings.startColor = new ParticleSystem.MinMaxGradient(color); }
                break;
        }
    }

}
