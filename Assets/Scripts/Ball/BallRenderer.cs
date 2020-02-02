using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRenderer : MonoBehaviour
{
    private Animator m_ballAnimator;
    private int m_isExploding;

    // Start is called before the first frame update
    void Start()
    {
        m_ballAnimator = GetComponent<Animator>();
        m_isExploding = Animator.StringToHash("IsExploding");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explode()
    {
        m_ballAnimator.SetBool(m_isExploding, true);
        Fabric.EventManager.Instance.PostEvent("Play_Balloon_Explode", gameObject);
    }
    public void DestroyAfterExplode()
    {
        GameManager.Instance.DestroyBalloon();
    }
}
