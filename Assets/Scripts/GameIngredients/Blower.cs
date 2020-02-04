using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    private LayerMask m_ballMask;
    private Vector2 m_blowDirection;

    public bool _isActive = true;
    public float _blowDistance;
    public float _blowForce;

    public GameObject _windSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        m_ballMask = LayerMask.GetMask("Ball");
        float angle = transform.root.eulerAngles.z + 90f;
        m_blowDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        _windSprite.transform.localScale = new Vector3(_windSprite.transform.localScale.x, _blowDistance * 2, _windSprite.transform.localScale.z);
        _windSprite.transform.localPosition = new Vector3(_windSprite.transform.localPosition.x, _blowDistance, _windSprite.transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, m_blowDirection, _blowDistance, m_ballMask, 0f);
        if (hit.collider)
        {
            Vector3 colliderPosition = hit.collider.transform.position;
            Vector2 blowDirection = m_blowDirection;
            float blowFactor = Mathf.Clamp( 1 - (Vector3.Distance(colliderPosition, transform.position) / _blowDistance),0f,1f);

            hit.collider.gameObject.GetComponent<BallBehaviour>().Push(blowDirection * _blowForce * blowFactor, hit.transform.position);
        }
    }

    void OnDrawGizmos()
    {
        if (Camera.main == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)m_blowDirection * _blowDistance);
    }
}
