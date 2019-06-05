﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool m_impPool = null;
    [SerializeField]
    private float m_spawnChance = 0;
    [SerializeField]
    private float m_spawnTime = 0;
    private float m_spawnTimer;

    private void Start()
    {
        m_spawnTimer = m_spawnTime;
    }

    private void Update()
    {
        m_spawnTimer -= Time.deltaTime;
        if (m_spawnTimer <= 0)
        {
            float toSpawnOrNotToSpawnThatIsTheQuestion = Random.Range(0, 100);
            if (toSpawnOrNotToSpawnThatIsTheQuestion <= m_spawnChance)
            {
                GameObject imp = m_impPool.GetObject();
                imp.transform.position = transform.position;
            }
            m_spawnTimer = m_spawnTime;
        }


    }
}