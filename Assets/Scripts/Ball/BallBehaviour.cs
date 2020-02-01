using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody2D m_rb;
    // Start is called before the first frame update

    [Range(1, 10)]
    public float _maxSpeed = 10;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rb.velocity.magnitude > _maxSpeed)
            m_rb.velocity = m_rb.velocity.normalized * _maxSpeed;
    }

    public void Push(Vector2 direction, Vector2 position)
    {
        m_rb.AddForceAtPosition(direction, position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.RespawnBall();
        Destroy(gameObject);
    }

}
