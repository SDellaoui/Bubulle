using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameObject m_currentBall;

    public float _goalRadius = 0.5f;
    public float _centerSpeed = 0.8f;
    public float _ballMaxSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_currentBall != null)
        {
            m_currentBall.transform.position = Vector3.MoveTowards(m_currentBall.transform.position, (Vector2)transform.position, _centerSpeed * Time.deltaTime);
            if(Vector2.Distance(m_currentBall.transform.position, (Vector2)transform.position) < 0.1f)
            {
                GameManager.Instance.Win();
                m_currentBall = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Collider layer is Ball
        if(collision.gameObject.layer == 9)
        {
            Rigidbody2D ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
            BallBehaviour ballBHV = collision.gameObject.GetComponent<BallBehaviour>();

            if (Vector3.Distance(collision.transform.position, transform.position) < _goalRadius && !ballBHV.IsInGoal)
            {
                ballBHV.IsInGoal = true;
                m_currentBall = collision.gameObject;
            }
        }
    }
}
