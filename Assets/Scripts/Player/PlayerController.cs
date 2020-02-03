using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Vector2 m_currentLookDirection;
    private float m_currentBlowTime;
    
    [HideInInspector]
    public bool m_isRecoveringFullBlow;
    [HideInInspector]
    public float m_currentBlowPercentTime;

    public GameObject _aimGameObject;

    [Range(1, 1000)]
    public float _speed = 500;

    [Range(1, 20)]
    public float _blowForce = 5;

    [Range(1, 20)]
    public float _blowMaxTime = 5f;

    [Range(1, 20)]
    public float _blowRecoverTime = 3f;


    [Range(10,90)]
    public float _blowAngle = 10;

    [Range(1, 6)]
    public float _blowMaxDistance = 2f;

    public LayerMask mask;
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateControls();
        UpdateDirection();
        UpdateBlowBall();
        UpdateBlowStatus();
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
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
        m_currentLookDirection = (Vector2)mousePos;
        _aimGameObject.transform.right = -((Vector3)m_currentLookDirection - transform.position).normalized;
    }
    private void UpdateBlowBall()
    {
        if (!Input.GetMouseButton(0) || m_isRecoveringFullBlow)
            return;

        Vector3 position = transform.position;
            
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(mask);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)position, -_aimGameObject.transform.right, _blowMaxDistance, mask, 0f);
            
        if (!hit.collider)
        {
            return;
        }
            
        Vector3 colliderPosition = hit.collider.transform.position;
                
        Vector2 blowDirection = (((Vector2)colliderPosition - hit.point) + (Vector2)(colliderPosition - position)).normalized;
        float blowFactor = 1 - (Vector3.Distance(colliderPosition, position) / _blowMaxDistance);
        hit.collider.gameObject.GetComponent<BallBehaviour>().Push(blowDirection * _blowForce, hit.point);
    }

    private void UpdateBlowStatus()
    {
        //float blowTime = m_currentBlowTime;
        if (!Input.GetMouseButton(0) || m_isRecoveringFullBlow)
        {
            if(m_isRecoveringFullBlow)
                m_currentBlowTime = Mathf.Clamp(m_currentBlowTime - ((_blowMaxTime / _blowRecoverTime) * Time.deltaTime), 0f, _blowMaxTime);
            else
                m_currentBlowTime = Mathf.Clamp(m_currentBlowTime - Time.deltaTime, 0f, _blowMaxTime);
        }
        else
        {
            m_currentBlowTime = Mathf.Clamp(m_currentBlowTime + Time.deltaTime, 0f, _blowMaxTime);
        }
        m_currentBlowPercentTime = 1-(m_currentBlowTime / _blowMaxTime);
        if (m_currentBlowTime == _blowMaxTime)
            m_isRecoveringFullBlow = true;
        else if (m_currentBlowTime == 0)
            m_isRecoveringFullBlow = false;
    }

    void OnDrawGizmos()
    {
        if (_mainCamera == null)
        {
            return;
        }

        Color refColor = Gizmos.color;
        Gizmos.color = Color.blue;
        
        Vector3 position = this.transform.position;
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
        mousePos.z = position.z;
        
        Gizmos.DrawLine(position, position + ((mousePos - position).normalized * _blowMaxDistance));
        Gizmos.color = refColor;
    }
}
