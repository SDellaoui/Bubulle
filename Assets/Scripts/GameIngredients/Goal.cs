using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Collider layer is Ball
        if(collision.gameObject.layer == 9)
        {
            if (Vector3.Distance(collision.transform.position, transform.position) < 0.2f)
                GameManager.Instance.Win();
        }
    }
}
