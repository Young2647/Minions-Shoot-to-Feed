using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaLogic : MonoBehaviour
{
    const float BULLET_SPEED = 15;
    const float BULLET_LIFETIME = 2.0f;
    Rigidbody m_rigidBody;
    DesireCubeLogic m_desireCubeLogic;
    // Start is called before the first frame update
    void Start()
    {
        m_desireCubeLogic = FindObjectOfType<DesireCubeLogic>();
        m_rigidBody = GetComponent<Rigidbody>();
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
            string name = other.name;
            m_desireCubeLogic.UpdateCube("Banana",name);
            Destroy(gameObject);
        }
    }
}
