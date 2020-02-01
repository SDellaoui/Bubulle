using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private GameObject m_target;

    public float _cameraFollowSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_target != null)
        {
            Vector3 target = new Vector3(m_target.transform.position.x, m_target.transform.position.y, transform.position.z);

            //float camSmoothX = (0.9f * transform.position.x) + (0.1f * target.x);
            //float camSmoothY = (0.9f * transform.position.y) + (0.1f * target.y);
            float camSmoothX = transform.position.x + ((target.x - transform.position.x) * 0.1f) * _cameraFollowSpeed * Time.fixedDeltaTime;
            float camSmoothY = transform.position.y + ((target.y - transform.position.y) * 0.1f) * _cameraFollowSpeed * Time.fixedDeltaTime;
            Vector3 finalCamPos = new Vector3(camSmoothX,camSmoothY,transform.position.z);
            transform.position = finalCamPos;

        }
    }

    public void SetTarget(GameObject t)
    {
        this.m_target = t;
    }
}
