﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ : MonoBehaviour
{
    private Rigidbody m_rb;
    [SerializeField]
    private GameObjectListSet m_imps = null;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject imp in m_imps.List)
        {
            float speed = Vector3.Magnitude(m_rb.velocity);
            if (speed > 5)
            {
                Vector3 dirToImp = imp.transform.position - transform.position;
                if (Vector3.Dot(dirToImp.normalized, m_rb.velocity.normalized) > 0.9f)
                {
                    m_rb.velocity = dirToImp.normalized * speed;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Imp"))
        {
            collision.transform.GetComponent<Imp>().Fall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("AiProcessor"))
            other.transform.GetComponent<AiProcessor>().Distract();
    }
}
