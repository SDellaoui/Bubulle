using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Vector2 m_currentLookDirection;

    public GameObject _aimGameObject;

    [Range(1, 1000)]
    public float _speed = 500;

    [Range(10, 100)]
    public float _blowForce = 50;

    [Range(10,90)]
    public float _blowAngle = 10;
    [Range(5, 10)]
    public float _blowMaxDistance = 5f;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateControls();
        UpdateDirection();
        UpdateBlowBall();
    }

    private void UpdateControls()
    {
        Vector3 newPosition = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Z))
            newPosition += new Vector3(0, 1, 0);
        if(Input.GetKey(KeyCode.Q))
            newPosition += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.S))
            newPosition += new Vector3(0, -1, 0);
        if (Input.GetKey(KeyCode.D))
            newPosition += new Vector3(1, 0, 0);

        m_rb.velocity = newPosition * _speed * Time.fixedDeltaTime;
    }

    private void UpdateDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
        m_currentLookDirection = (Vector2)mousePos;
        _aimGameObject.transform.right = ((Vector3)m_currentLookDirection - transform.position).normalized;
    }

    private void UpdateBlowBall()
    {
        if(Input.GetMouseButton(0))
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(mask);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, _aimGameObject.transform.right, _blowMaxDistance, mask, 0f);
            if(hit.collider != null)
            {
                
                Vector2 blowDirection = (((Vector2)hit.collider.transform.position - hit.point) + (Vector2)(hit.collider.transform.position - transform.position)).normalized;
                float blowFactor = 1 - (Vector3.Distance(hit.collider.transform.position, transform.position) / _blowMaxDistance);
                Debug.Log($"Hitting {hit.collider.gameObject.name} at {hit.point}. blow force : {blowFactor}");
                hit.collider.gameObject.GetComponent<BallBehaviour>().Push(blowDirection * _blowForce, hit.point);
            }
        }
    }
}
