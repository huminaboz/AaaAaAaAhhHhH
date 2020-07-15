using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake I
    {
        get; private set;
    }

    [HideInInspector] public Vector3 originPosition;

    public bool shakeOnEnable = false;

    private bool shaking;
    private float shakeIntensity = 0f;
    private float shakeMultiplier = 60f; //Need this because the build version seems to have a weaker shake for some reason

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
        }
        I = this;

        if(Application.isEditor)
        {
            shakeMultiplier = 60f;
        }
    }

    private void Start()
    {
        originPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        if (shakeOnEnable)
        {
            StartShake(1f, 2f);
        }
    }

    public void StartShake(float time, float intensity)
    {
        //NOTE:: .2f intensity is about a good place to start
        shakeIntensity = intensity;
        
        if (shakeCo != null) StopCoroutine(shakeCo);
        shakeCo = StartCoroutine(DoShake(time));
    }

    private Coroutine shakeCo;
    IEnumerator DoShake(float time)
    {
        shaking = true;
        yield return new WaitForSeconds(time);
        shaking = false;
        //transform.localPosition = originPosition;
        //CameraFollower.I.UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (shaking)
        {
            transform.localPosition = new Vector3(
        originPosition.x + Random.insideUnitSphere.x * shakeIntensity * Time.deltaTime * shakeMultiplier,
        originPosition.y + Random.insideUnitSphere.y * shakeIntensity * Time.deltaTime * shakeMultiplier,
        originPosition.z);
        }
    }

}//Class
