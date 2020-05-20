using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    private bool _dangerousFall;
    
    private float damage;
    public float healthBar = 2000;
    public float fallDamage;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Rigidbody.velocity.y < -7 )
        {
            damage = -(m_Rigidbody.velocity.y * fallDamage);
            _dangerousFall = true;
        }
    }

    void HitGround()
    {
        if (_dangerousFall)
        {
            healthBar -= damage;
            _dangerousFall = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        HitGround();
    }
}
