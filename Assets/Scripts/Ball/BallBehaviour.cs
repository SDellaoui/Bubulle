using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private bool m_isPushed;
    
    public bool IsInGoal = false;
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
        if(IsInGoal)
        {
            m_rb.velocity = Vector2.zero;
        }
        // Force Ballon to avoid going faster than the max speed limit
        if (m_rb.velocity.magnitude > _maxSpeed)
            m_rb.velocity = m_rb.velocity.normalized * _maxSpeed;


        //Decelerate the balloon speed over time when it's not pushed
        if (m_isPushed && m_rb.velocity.magnitude > 0)
        {
            m_rb.velocity -= m_rb.velocity * 0.005f;
            m_rb.angularVelocity -= m_rb.angularVelocity * 0.005f;
        }
        else
            m_isPushed = false;
    }

    public void Push(Vector2 direction, Vector2 position)
    {
        if (IsInGoal)
            return;
        m_rb.AddForceAtPosition(direction, position);
        m_isPushed = true;
    }
    private void OnDestroy()
    {
        //GameManager.Instance.RespawnBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.DestroyBalloon();
    }

}
