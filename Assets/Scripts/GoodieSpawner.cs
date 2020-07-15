using UnityEngine;

public class GoodieSpawner : MonoBehaviour
{
    public static GoodieSpawner I
    {
        get; private set;
    }

    public GameObject goodiePrefab;
    private Goodies currentGoodie;
    public float bottomOfScreenBuffer = 1f;

    Camera cam;

    float screenLeft, screenRight, screenTop, screenBottom;

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
        cam = Camera.main;
        //float distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        screenLeft = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0)).x;
        screenRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0)).x;
        screenTop = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0)).y;
        screenBottom = cam.ScreenToWorldPoint(new Vector3(0f, 0f, 0)).y;
        SpawnGoodie();

//        TestScreenArea();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && UIManager.I.debugMode)
        {
            currentGoodie.GetGot();
        }
    }

    private void TestScreenArea()
    {
        for(int i = 0; i < 100; i++)
        {
            SpawnGoodie();
        }
    }

    public void GoodieGot()
    {
        UIManager.I.GetPoint();
        SpawnGoodie();
    }

    private void SpawnGoodie()
    {
        if (UIManager.I.score >= UIManager.I.winScore) return;
        currentGoodie = Instantiate(goodiePrefab, GetRandomScreenPoint(), Quaternion.identity).GetComponent<Goodies>() ;
        currentGoodie.spawner = this;
    }

    public void DespawnGoodie()
    {
        currentGoodie.TimeOut();
    }

    private Vector2 GetRandomScreenPoint()
    {
        float x = Random.Range(screenLeft, screenRight);
        float y = Random.Range(bottomOfScreenBuffer, screenTop-1f);
        //Debug.Log(new Vector2(x, y));
        return new Vector2(x, y);
    }
}
