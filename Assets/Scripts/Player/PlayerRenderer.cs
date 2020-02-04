using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    private PlayerController m_playerController;
    private int m_blowAnimationId;
    private bool m_isBlowing;
    private float m_blowBarScale;

    //public GameObject _renderer;
    public SpriteRenderer _playerHead;
    public SpriteRenderer _playerBody;
    public Animator _headAnimator;

    public SpriteRenderer _blowBar;

    

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
        m_blowAnimationId = Animator.StringToHash("IsBlowing");

        m_blowBarScale = _blowBar.transform.localScale.x;
        
}

    // Update is called once per frame
    void Update()
    {
        // Update head & body flip 
        Vector3 aimDirection = GetComponent<PlayerController>()._aimGameObject.transform.right;
        _playerHead.flipY = (aimDirection.x >= 0f) ? false : true;
        _playerBody.flipX = _playerHead.flipY;


        //Update Blow Animation & sound
        if (Input.GetMouseButton(0) && !m_playerController.m_isRecoveringFullBlow)
        {
            _headAnimator.SetBool(m_blowAnimationId, true);
            if (!m_isBlowing)
                Fabric.EventManager.Instance.PostEvent("Play_Ghost_Blow", gameObject);
            m_isBlowing = true;

            UpdateBlowBar(true);
        }
        else
        {
            _headAnimator.SetBool(m_blowAnimationId, false);
            if (m_isBlowing)
                Fabric.EventManager.Instance.PostEvent("Stop_Ghost_Blow", gameObject);

            m_isBlowing = false;

            UpdateBlowBar(false);
        }
    }
    void UpdateBlowBar(bool blow = false)
    {
        _blowBar.transform.localScale = new Vector3(m_blowBarScale * m_playerController.m_currentBlowPercentTime, _blowBar.transform.localScale.y, _blowBar.transform.localScale.z);

        float colorAlpha = (m_playerController.m_currentBlowPercentTime == 1)? 0 : 1;
        _blowBar.color = new Color(_blowBar.color.r, _blowBar.color.g, _blowBar.color.b, colorAlpha);
    }
}
