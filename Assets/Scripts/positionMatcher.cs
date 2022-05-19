using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class positionMatcher : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private bool m_IsActive = true;

    public bool IsActive { get { return m_IsActive; } }

    public void SetActive(bool _Value)
    {
        m_IsActive = _Value;
    }

    private void Update()
    {
        if (m_IsActive)
            transform.position = m_Target.position;
    }
}