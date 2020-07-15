using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellToRestart : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubePiston>() != null)
        {
            if (CameraShake.I)  CameraShake.I.StartShake(.1f, .1f);

            if (UIManager.I)
            {
                if (UIManager.I.reloadable)
                {
                    UIManager.I.ReloadLevel();
                }
            }
        }

    }

}
