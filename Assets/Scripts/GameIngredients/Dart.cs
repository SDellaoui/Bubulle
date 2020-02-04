using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private float m_lifeTime = 3f;

    public float _speed;

    // Start is called before the first frame update
    void Start()
    {
        m_lifeTime += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
        transform.position += transform.up * _speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Fabric.EventManager.Instance.PostEvent("Play_Dart_Impact", gameObject);
        Destroy(gameObject);
    }
}
