using UnityEngine;
using UnityEngine.UI;

public class AudioVisualizer : MonoBehaviour
{
    public Transform spectrumObjectsParent;
    public GameObject cubeSpectrumPrefab;
    [Range(1, 2000)] public float heightMultiplier = 100;
    [Range(64, 8192)] public int numberOfSamples = 1024; //step by 2
    public FFTWindow fftWindow;
    public Slider sensitivitySlider;
    public Slider thresholdSlider;
    public float threshold = .1f;
    public float xScale = 1f;
    public int cubesToMake = 16;

    private GameObject[] audioSpectrumObjects;
    private Vector3 velocity = Vector3.zero;
    private AudioSource audioSource;
    private Rigidbody[] spectrumRigis;

    /*
	 * The intensity of the frequencies found between 0 and 44100 will be
	 * grouped into 1024 elements. So each element will contain a range of about 43.06 Hz.
	 * The average human voice spans from about 60 hz to 9k Hz
	 * we need a way to assign a range to each object that gets animated. that would be the best way to control and modify animatons.
	*/

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (PlayerPrefsManager.GetSensitivity() != 0)
        {
            heightMultiplier = PlayerPrefsManager.GetSensitivity();
        }
        if (PlayerPrefsManager.GetThreshold() != 0)
        {
            threshold = PlayerPrefsManager.GetThreshold();
        }

        sensitivitySlider.onValueChanged.AddListener(delegate
        {
            SensitivityValueChangedHandler(sensitivitySlider);
        });

        thresholdSlider.onValueChanged.AddListener(delegate
        {
            ThresholdValueChangedHandler(thresholdSlider);
        });

        for (int i = 0; i < cubesToMake; i++)
        {
            GameObject newCube = Instantiate(cubeSpectrumPrefab, spectrumObjectsParent);
            newCube.name = "Spectrum Cube " + i;
            //Debug.Log(newCube);
        }

        audioSpectrumObjects = new GameObject[cubesToMake];
        spectrumRigis = new Rigidbody[cubesToMake];

        Transform[] cubes = spectrumObjectsParent.GetComponentsInChildren<Transform>();
        int asdf = 0;
        foreach (Transform cube in cubes)
        {
            if (cube.transform.parent != null)
            {
                audioSpectrumObjects[asdf] = cube.gameObject;
                audioSpectrumObjects[asdf].transform.position = Vector3.up * -4.695f;
                asdf++;
            }
        }
        for (int i = 0; i < audioSpectrumObjects.Length; i++)
        {
            spectrumRigis[i] = audioSpectrumObjects[i].GetComponent<Rigidbody>();
        }
        AdjustRelativePositioning();
        //Debug.Log("AudioSpectrumObjects: " + audioSpectrumObjects);
    }

    void FixedUpdate()
    {

        // initialize our float array
        float[] spectrum = new float[numberOfSamples];

        // populate array with fequency spectrum data
        audioSource.GetSpectrumData(spectrum, 0, fftWindow);


        // loop over audioSpectrumObjects and modify according to fequency spectrum data
        // this loop matches the Array element to an object on a One-to-One basis.
        for (int i = 0; i < audioSpectrumObjects.Length; i++)
        {

            // apply height multiplier to intensity
            float intensity = spectrum[i] * heightMultiplier;

            // calculate object's scale
            //float lerpY = Mathf.Lerp(audioSpectrumObjects[i].localScale.y, intensity, lerpTime);
            //Vector3 newScale = new Vector3(audioSpectrumObjects[i].localScale.x, Mathf.Clamp(lerpY, .25f, 9999999), audioSpectrumObjects[i].localScale.z);
            //Vector3 newPos = new Vector3(audioSpectrumObjects[i].localPosition.x, intensity, audioSpectrumObjects[i].localPosition.z);
            //Vector3 newPos = new Vector3(audioSpectrumObjects[i].localPosition.x, lerpY, audioSpectrumObjects[i].localPosition.z);
            //audioSpectrumObjects[i].localPosition = Vector3.SmoothDamp(audioSpectrumObjects[i].localPosition, newPos, ref velocity, .2f, 1f, Time.deltaTime);
            // appply new scale to object
            //audioSpectrumObjects[i].localScale = newScale;
            //audioSpectrumObjects[i].localPosition = newPos;
            //spectrumRigis[i].AddForce(new Vector3(0, intensity*50f));
           // Debug.Log(intensity);
            if (intensity > threshold)
            {
                spectrumRigis[i].velocity = new Vector2(0, intensity);
            }
            else
            {
                spectrumRigis[i].velocity = new Vector2(0, -50f);
            }
        }

       AdjustRelativePositioning();
    }

    private void AdjustRelativePositioning()
    {
        for (int i = 0; i < audioSpectrumObjects.Length; i++)
        {
            //Debug.Log("i is: " + i);
            //Debug.Log(i * xScale + (i * xScale * .5f));
            Vector3 newScale = new Vector3(xScale, audioSpectrumObjects[i].transform.localScale.y, audioSpectrumObjects[i].transform.localScale.z);
            audioSpectrumObjects[i].transform.localScale = newScale;
            float adjustedXScale = xScale + .1f; 
            audioSpectrumObjects[i].transform.localPosition = new Vector3((i * adjustedXScale + (adjustedXScale * .5f)), audioSpectrumObjects[i].transform.localPosition.y, 0);
        }
    }

    public void SensitivityValueChangedHandler(Slider sensitivitySlider)
    {
        heightMultiplier = sensitivitySlider.value;
        PlayerPrefsManager.SetSensitivity(heightMultiplier);
    }

    public void ThresholdValueChangedHandler(Slider thresholdSlider)
    {
        threshold = thresholdSlider.value;
        PlayerPrefsManager.SetThreshold(threshold);
    }

}
