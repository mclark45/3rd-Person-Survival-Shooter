﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
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

            if (Physics.Raycast(rayOrigin, out hitInfo))
                Debug.Log(hitInfo.transform.name);
        }
    }
}
