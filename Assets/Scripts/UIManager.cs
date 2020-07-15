using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager I
    {
        get; private set;
    }

    public UILabel scoreLabel, timeLabel, timeOverLabel, yellAlot;
    public GameObject timeOverScreen, introMessage, settings;
    public bool debugMode = false;

    [HideInInspector] public float t;
    [HideInInspector] public bool reloadable = false;
    [HideInInspector] public int winScore = 20;
    [HideInInspector] public int score;
    [HideInInspector] public int[] colorTiers = new int[4];
    [HideInInspector] public int currentColorTier = 0;
    //private readonly float time = 180f;

    public delegate void UpgradeColorTier();
    public static event UpgradeColorTier OnNewColorTier;


    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
        }
        I = this;
    }

    private void Start()
    {
        UpdateScore();
        UpdateTime(0f);
        StartCoroutine(TimerTimer());
        StartCoroutine(IntroMessageTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
        if (Input.GetKeyDown(KeyCode.R) && debugMode)
        {
            ReloadLevel();
        }
    }

    public void ToggleSettings()
    {
        if (settings.activeSelf)
        {
            OptionsController.I.Save();
            settings.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
        }
    }

    public void GetPoint()
    {
        if (t > 0)
        {
            score++;
            //Debug.Log(score % 5);
            if (score % 5 == 0)
            {
                currentColorTier++;
                OnNewColorTier?.Invoke();
                UpdateColors();
            }
            UpdateScore();
        }
    }

    private IEnumerator IntroMessageTimer()
    {
        yield return new WaitForSeconds(5f);
        introMessage.gameObject.SetActive(false);
    }

    private IEnumerator TimerTimer()
    {
        //t = time;

        while (score < winScore)
        {
            t += Time.deltaTime;
            // t = Mathf.Clamp(t, 0f, time);
            UpdateTime(t);
            yield return null;
        }

        if (score >= winScore)
        {
            TimeOver();
        }
    }

    private void UpdateTime(float t)
    {
        string minutes = Mathf.Floor(t / 60).ToString("0");
        string seconds = (t % 60).ToString("00");

        timeLabel.text = string.Format("{0}:{1}", minutes, seconds);

        //timeLabel.text = t.ToString("F0");
    }

    private void UpdateScore()
    {
        scoreLabel.text = score.ToString() + " / " + winScore;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TimeOver()
    {
        //GoodieSpawner.I.DespawnGoodie();
        //timeOverLabel.text = t.ToString("F0") + " win time!";
        timeOverLabel.text = timeLabel.text + " win time!";
        timeOverScreen.SetActive(true);
        StartCoroutine(ReloadableTimer());
    }

    private IEnumerator ReloadableTimer()
    {
        yield return new WaitForSeconds(2.5f);
        yellAlot.gameObject.SetActive(true);
        reloadable = true;
    }

    private void UpdateColors()
    {
        Color color;
        Color secondaryColor = Color.white;

        switch (currentColorTier)
        {
            case 1:
                if (ColorUtility.TryParseHtmlString("#5CFCFF", out color))
                {
                    secondaryColor = color;
                }
                break;
            case 2:
                if (ColorUtility.TryParseHtmlString("#B719F0", out color))
                {
                    secondaryColor = color;
                }
                break;
            case 3:
                if (ColorUtility.TryParseHtmlString("#FCF39F", out color))
                { secondaryColor = color; }
                break;
            case 4:
                if (ColorUtility.TryParseHtmlString("#F00F4B", out color))
                { secondaryColor = color; }
                break;
            default:
                if (ColorUtility.TryParseHtmlString("#4EB1B1", out color))
                { secondaryColor = color; }
                break;
        }
        timeLabel.color = secondaryColor;
        scoreLabel.color = secondaryColor;
    }

}
