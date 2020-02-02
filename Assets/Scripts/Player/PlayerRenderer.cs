using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private float m_speed;
    public GameObject _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_speed = GetComponent<PlayerController>()._speed;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (m_rb.velocity.magnitude > 0.1f)
        {
            float speed = Mathf.Clamp(m_rb.velocity.magnitude, 0f, m_speed);
            _renderer.transform.localPosition = Vector3.zero;
            return;
        }*/

        float speed = Mathf.Clamp(m_rb.velocity.magnitude, 0f, 13);
        speed = 1 - (m_rb.velocity.magnitude / 13);
        //Debug.Log($"CurrentSpeed : {speed}");
        //_renderer.transform.localPosition = new Vector3(0f, Mathf.Sin(Time.time * 2) * speed, 0.0f);

    }
}
