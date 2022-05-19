using System.Collections;
using System.Collections.Generic;
using FeatherLight.Pro;
using UnityEngine;

public class stepManager : MonoBehaviour
{
    [SerializeField] private stepLadder m_StepLadder;
    [SerializeField] private CanvasGroup[] m_Steps;

    [SerializeField] private float m_StepFadeTime = 0.15f;

    private int _currentIndex;

    private void UpdateSteps()
    {
        
        for (int i = 0; i < m_Steps.Length; i++)
        {
            if (i == _currentIndex)
            {
                if(m_Steps[i].alpha != 1)
                    StartCoroutine(CanvasGroupHelper.Fade(m_Steps[i], true, m_StepFadeTime));
            }
            else
            {
                if (m_Steps[i].alpha == 1)
                    StartCoroutine(CanvasGroupHelper.Fade(m_Steps[i], false, m_StepFadeTime));
            }
        }
    }

    public void IncreaseStep()
    {
        _currentIndex++;

        if (_currentIndex >= m_Steps.Length)
            _currentIndex = 0;

        m_StepLadder.SetLadderStep(_currentIndex);

        UpdateSteps();
    }

    public void DecreaseStep()
    {
        _currentIndex--;

        if (_currentIndex <= -1)
            _currentIndex = 0;

        m_StepLadder.SetLadderStep(_currentIndex);

        UpdateSteps();
    }
}
