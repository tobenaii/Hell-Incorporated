﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private float m_dialogueSpeed = 0;
    [SerializeField]
    private TextMeshPro m_text = null;

    private bool m_isDialoging;
    private bool m_waitingForPage;
    private List<string> m_cutDialogue = new List<string>();
    private int m_currentPage;
    private int m_currentLetter;
    private float m_timer;

    public void StartDialogue(string d)
    {
        m_text.text = "";
        m_cutDialogue.Clear();
        string page = "";
        foreach (string word in d.Split(' '))
        {
            if (word == "/page")
            {
                m_cutDialogue.Add(page);
                m_text.text = "";
                page = "";
                continue;
            }
            m_text.text += word + " ";
            m_text.ForceMeshUpdate();
            if (!m_text.isTextOverflowing)
                page += word + " ";
            else
            {
                m_cutDialogue.Add(page);
                m_text.text = "";
                page = word + " ";
            }
        }
        m_cutDialogue.Add(page);
        m_text.text = "";
        m_isDialoging = true;
        m_currentPage = 0;
        m_currentLetter = 0;
        m_timer = m_dialogueSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isDialoging)
            return;

        if (m_waitingForPage)
            return;

        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            if (m_currentLetter == m_cutDialogue[m_currentPage].Length)
            {
                m_currentLetter = 0;
                m_waitingForPage = true;
                return;
            }
            m_timer = m_dialogueSpeed;
            m_text.text += m_cutDialogue[m_currentPage][m_currentLetter];
            m_currentLetter++;

        }
    }

    private void OnMouseDown()
    {
        if (!m_isDialoging)
            return;
        if (m_waitingForPage)
        {
            m_waitingForPage = false;
            m_text.text = "";
            m_currentPage++;
            m_currentLetter = 0;
            if (m_currentPage == m_cutDialogue.Count)
                m_isDialoging = false;
        }
        else
        {
            m_text.text = m_cutDialogue[m_currentPage];
            m_currentLetter = m_cutDialogue[m_currentPage].Length;
        }
    }
}
