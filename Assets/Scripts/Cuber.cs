using UnityEngine;

public class Cuber : MonoBehaviour
{
    Camera cam;
    readonly float buffer = 1.0f;

    float screenLeft, screenRight, screenTop, screenBottom;

    private void Start()
    {
        cam = Camera.main;
        //float distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        screenLeft = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0)).x;
        screenRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0)).x;
        screenTop = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0)).y;
        screenBottom = cam.ScreenToWorldPoint(new Vector3(0f, 0f, 0)).y;
    }

    void Update()
    {
        if (transform.position.x < screenLeft - buffer)
        {
            transform.position = new Vector2(screenRight - 0.5f, transform.position.y);
        }
        if (transform.position.x > screenRight)
        {
            transform.position = new Vector2(screenLeft - 0.5f, transform.position.y);
        }
        if (transform.position.y < screenBottom)
        {
            transform.position = new Vector2(transform.position.x, screenTop - 1f);
        }
    }




}
