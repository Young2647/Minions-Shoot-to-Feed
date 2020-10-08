using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMelonLogic : MonoBehaviour
{
    const float BULLET_SPEED = 10;
    const float BULLET_LIFETIME = 2.5f;
    Rigidbody m_rigidBody;
    DesireCubeLogic m_DesireCubeLogic;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_DesireCubeLogic = FindObjectOfType<DesireCubeLogic>();
        if(m_rigidBody)
        {
            m_rigidBody.velocity = transform.forward * BULLET_SPEED;
        }

        Destroy(gameObject, BULLET_LIFETIME);
    }
   void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        // Destroy bullet
        Destroy(gameObject);
        if(other.tag == "Minion"){
            name = other.name;
            m_DesireCubeLogic.UpdateCube("Watermelon",name);
            Destroy(gameObject);
        }
    }
}