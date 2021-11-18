using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bloodSplatter;
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 centerOfScreen = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(centerOfScreen);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 9))
            {
                if (hitInfo.transform.tag == "Enemy")
                {
                    IDamageable hit = hitInfo.transform.GetComponent<IDamageable>();

                    if (hit != null)
                    {
                        hit.Damage();
                        GameObject blood = Instantiate(_bloodSplatter, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        Destroy(blood, 0.05f);
                        //create an object pool for the bloodsplatter animation
                        Debug.Log("Enemy Health: " + hit.Health);
                    }
                }
            }       
        }
    }
}
