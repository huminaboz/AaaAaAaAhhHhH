using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {

    public static OptionsController I
    {
        get; private set;
    }

    public Dropdown microphone;
	public Slider sensitivitySlider, thresholdSlider;
	public GameObject settingsPanel;
	public GameObject openButton;

    //private bool panelActive = false;

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
        }
        I = this;
    }

    // Use this for initialization
    void Start () {
		microphone.value = PlayerPrefsManager.GetMicrophone ();
		sensitivitySlider.value = PlayerPrefsManager.GetSensitivity ();
		thresholdSlider.value = PlayerPrefsManager.GetThreshold ();
	}

	public void Save (){
		PlayerPrefsManager.SetMicrophone (microphone.value);
		PlayerPrefsManager.SetSensitivity (sensitivitySlider.value);
		PlayerPrefsManager.SetThreshold (thresholdSlider.value);

		//panelActive = !panelActive;
		//settingsPanel.GetComponent<Animator> ().SetBool ("PanelActive",panelActive);
	}

	public void SetDefaults(){
		microphone.value = 0;
		sensitivitySlider.value = 100f;
		thresholdSlider.value = 0.1f;
	}

	public void OpenSettings(){
		//panelActive = !panelActive;
		//settingsPanel.GetComponent<Animator> ().SetBool ("PanelActive",panelActive);
	}

    public void GoToPlay()
    {
        Save();
        SceneManager.LoadScene("play", LoadSceneMode.Single);
    }
}
