﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 m_cameraPosition;
    private GameObject m_target;

    public bool _lockX;
    public bool _lockY;
    public float _cameraFollowSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        m_cameraPosition = transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_target != null)
        {
            Vector3 target = new Vector3(m_target.transform.position.x, m_target.transform.position.y, transform.position.z);
            float camSmoothX;
            float camSmoothY;
            //float camSmoothX = (0.9f * transform.position.x) + (0.1f * target.x);
            //float camSmoothY = (0.9f * transform.position.y) + (0.1f * target.y);

            if (!_lockX)
                camSmoothX = transform.position.x + ((target.x - transform.position.x) * 0.3f) * _cameraFollowSpeed * Time.fixedDeltaTime;
            else
                camSmoothX = transform.position.x;

            if (!_lockY)
                camSmoothY = transform.position.y + ((target.y - transform.position.y) * 0.6f) * _cameraFollowSpeed * Time.fixedDeltaTime;
            else
                camSmoothY = transform.position.y;
            
            Vector3 finalCamPos = new Vector3(camSmoothX,camSmoothY,transform.position.z);
            transform.position = finalCamPos;

        }
    }
    public void SetTarget(GameObject t, bool snap = false)
    {
        this.m_target = t;
        if (snap)
            transform.position = new Vector3(t.transform.position.x, t.transform.position.y, transform.position.z);
    }
}
