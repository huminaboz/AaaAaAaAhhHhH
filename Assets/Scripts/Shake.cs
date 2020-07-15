using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{
    [HideInInspector] public Vector3 originPosition;

    [Header("Shake Always")]
    public bool shakeOnEnable = false;
    [Tooltip("Only works if Shake on Enable is true")]
    public float shakeOnEnableIntensity = .2f;

    private float shakeIntensity = 0f;
    private bool shaking;

    private void OnEnable()
    {
        if(shakeOnEnable)
        {
            shaking = true;
            shakeIntensity = shakeOnEnableIntensity;
        }

        originPosition = transform.localPosition;
    }

    public void StartShake(float time, float intensity)
    {
        //NOTE:: .2f intensity is about a good place to start
        shakeIntensity = intensity;
        if(!shaking) originPosition = transform.localPosition;
        //Debug.Log(gameObject.name + " START:" + transform.localPosition);
        StartCoroutine(DoShake(time));
    }

    public void StopShaking()
    {
        shaking = false;
        transform.localPosition = originPosition;
    }

    IEnumerator DoShake(float time)
    {
        shaking = true;
        yield return new WaitForSeconds(time);
        shaking = false;
        transform.localPosition = originPosition;
        //Debug.Log(gameObject.name + " END:" + transform.localPosition);
    }

    void Update()
    {
        if (shaking)
        {
            transform.localPosition = new Vector3(
                originPosition.x + Random.insideUnitCircle.x * shakeIntensity * Time.deltaTime * 120f,
                originPosition.y + Random.insideUnitCircle.y * shakeIntensity * Time.deltaTime * 120f,
                originPosition.z);
        }
    }

}//Class
