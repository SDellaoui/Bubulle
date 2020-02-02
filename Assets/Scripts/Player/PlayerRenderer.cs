using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private float m_speed;
    private int m_blowAnimationId;
    private bool m_isBlowing;

    //public GameObject _renderer;
    public SpriteRenderer _playerHead;
    public SpriteRenderer _playerBody;
    public Animator headAnimator;

    

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_speed = GetComponent<PlayerController>()._speed;
        m_blowAnimationId = Animator.StringToHash("IsBlowing");
}

    // Update is called once per frame
    void Update()
    {
        // Update head & body flip 
        Vector3 aimDirection = GetComponent<PlayerController>()._aimGameObject.transform.right;
        _playerHead.flipY = (aimDirection.x >= 0f) ? false : true;
        _playerBody.flipX = _playerHead.flipY;


        //Update Blow Animation & sound
        if (Input.GetMouseButton(0))
        {
            headAnimator.SetBool(m_blowAnimationId, true);
            if (!m_isBlowing)
                Fabric.EventManager.Instance.PostEvent("Play_Ghost_Blow", gameObject);
            m_isBlowing = true;
        }
        else
        {
            headAnimator.SetBool(m_blowAnimationId, false);
            if (m_isBlowing)
                Fabric.EventManager.Instance.PostEvent("Stop_Ghost_Blow", gameObject);
            m_isBlowing = false;
        }
    }
}
