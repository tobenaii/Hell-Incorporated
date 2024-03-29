﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilSpawner : MonoBehaviour
{
    [SerializeField]
    private float m_maxTime;
    [SerializeField]
    private float m_spawnInterval;
    private float m_spawnChance;
    private bool m_devilInScene;
    private float m_timer;
    private bool m_canSpawn;
    private TimeLerper m_lerper;
    private bool m_wait;
    private float m_waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = 1;
        m_canSpawn = false;
        m_lerper = new TimeLerper();
    }

    public void StartDevil()
    {
        m_canSpawn = true;
        m_waitTimer = m_spawnInterval;
        m_wait = true;
    }
    void SpawnDevil()
    {
        m_devilInScene = true;
        GetComponent<Devil>().Init();
        m_timer = 1;
        m_wait = true;
    }

    public void DevilFinished()
    {
        m_devilInScene = false;
        m_waitTimer = m_spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_canSpawn)
            return;

        if (m_devilInScene)
            return;

        //Wait for specified seconds before starting random spawn again
        if (m_wait)
        {
            m_waitTimer -= Time.deltaTime;
            if (m_waitTimer <= 0)
            {
                m_wait = false;
            }
            return;
        }

        //Every second, check if random number is less than spawnChance
        m_timer -= Time.deltaTime;
        m_spawnChance = m_lerper.Lerp(0, 1000, m_maxTime);
        if (m_timer <= 0)
        {
            float spawn = Random.Range(0, 1000);
            if (spawn < m_spawnChance)
            {
                //Spawn the devil
                m_lerper.Reset();
                SpawnDevil();
            }
            m_timer = 1;
        }
    }
}
