﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour
{
    [SerializeField]
    private ProcessorListSet m_procListSet = null;
    [SerializeField]
    private GameObjectPool m_paperPool = null;
    [SerializeField]
    private Transform m_paperLocation = null;
    [SerializeField]
    private FloatEvent m_scoreEvent = null;
    private GameObject m_paperInstance;
    private Soul m_currentSoul;
    [SerializeField]
    private bool m_isPlayerProcessor;
    public bool IsPlayerProcessor => m_isPlayerProcessor;

    private bool m_isProcessing;
    public bool IsProcessing { get { return m_isProcessing; } private set { m_isProcessing = value; } }

    private bool m_hasPaper;
    public bool HasHaper { get { return m_hasPaper; } private set { m_hasPaper = value; } }

    public void StartProcessing(Soul soul)
    {
        m_currentSoul = soul;
        m_paperInstance = m_paperPool.GetObject();
        m_paperInstance.transform.position = m_paperLocation.position;
        m_paperInstance.transform.rotation = m_paperLocation.rotation;
        HasHaper = true;
    }

    public void Lock()
    {
        IsProcessing = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach (Processor proc in m_procListSet.List)
        {
            if (proc.gameObject.transform.position.x > transform.position.x)
                index++;
        }
        m_procListSet.Insert(index, this);
    }

    public void SendToHell()
    {
        m_currentSoul.SendToHell();
        IsProcessing = false;
        m_currentSoul = null;
        HasHaper = false;
        m_scoreEvent.Invoke(1.0f);
    }
    private void Update()
    {
    }
}