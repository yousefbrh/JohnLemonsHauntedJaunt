using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    private PlayerMovement m_playerMovement;
    private bool hasJumped;
    private float damage;
    public float healthBar = 2000;
    public float fallDamage;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_playerMovement.grounded && m_Rigidbody.velocity.y < -7 )
        {
            damage = -(m_Rigidbody.velocity.y * fallDamage);
            hasJumped = true;
        }
        HitGround();
    }

    void HitGround()
    {
        if (m_playerMovement.grounded && hasJumped)
        {
            healthBar -= damage;
            Debug.Log("took damage");
            hasJumped = false;
        }
    }
}
