﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWorker : MonoBehaviour
{
    [SerializeField]
    protected float m_workSpeed = 0;
    [SerializeField]
    private int m_workerIndex;
    [SerializeField]
    protected ProcState m_procState = null;
    [SerializeField]
    protected GameObjectListSet m_workingImpList;
    [SerializeField]
    protected BoundItem m_boundItem;
    [SerializeField]
    protected Item m_item;
    [SerializeField]
    protected Animator m_animator;

    private bool m_hasImp;
    private bool m_doingAction;
    private Imp m_impObj;

    private void Awake()
    {
        m_animator.enabled = false;
    }

    private void Update()
    {
        if (m_hasImp)
        {
            if (m_workingImpList.List[m_workerIndex] == null)
            {
                m_hasImp = false;
                GiveBackControl();
                return;
            }
                if (!m_doingAction)
                StartCoroutine(GetImp());
            return;
        }
        CheckForImp();
    }

    private void CheckForImp()
    {
        if (m_workingImpList.Count < m_workerIndex + 1)
            m_workingImpList.Add(null);
        else if (m_workingImpList.List[m_workerIndex])
        {
            m_hasImp = true;
            m_impObj = m_workingImpList.List[m_workerIndex].GetComponent<Imp>();
        }
    }

    private IEnumerator GetImp()
    {
        GameObject imp = m_workingImpList.List[m_workerIndex];
        while (Vector3.Distance(imp.transform.position, transform.position) > 0.005f)
        {
            imp.transform.position = Vector3.MoveTowards(imp.transform.position, transform.position, 10 * Time.deltaTime);
            yield return null;
        }
        DoAction();
    }

    private void DoAction()
    {
        if (m_doingAction)
            return;
        if (m_procState.state == (ProcState.ProcessorState)m_workerIndex)
        {
            m_boundItem.enabled = false;
            m_boundItem.GetComponent<Pickup>().enabled = false;
            m_boundItem.GetComponent<Rigidbody>().isKinematic = true;
            m_animator.enabled = true;
            m_animator.SetTrigger("DoAction");
            m_doingAction = true;
        }
    }

    public void TriggerAction()
    {
        m_item.DoAction();
        m_doingAction = false;
    }

    private void GiveBackControl()
    {
        m_boundItem.enabled = true;
        m_boundItem.GetComponent<Pickup>().enabled = true;
        m_boundItem.GetComponent<Rigidbody>().isKinematic = false;
        m_animator.enabled = false;
        m_doingAction = false;
    }
}
