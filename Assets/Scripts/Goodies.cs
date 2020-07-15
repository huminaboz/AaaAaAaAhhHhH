using UnityEngine;

public class Goodies : MonoBehaviour
{
    public GameObject gotItParticle;
    [HideInInspector] public GoodieSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Cuber>() != null)
        {
            GetGot();
        }
    }

    public void GetGot()
    {
        CameraShake.I.StartShake(.2f, .4f);
        spawner.GoodieGot();
        Instantiate(gotItParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void TimeOut()
    {
        Destroy(gameObject);
    }
}
