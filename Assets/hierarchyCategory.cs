using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using FeatherLight.Pro;

public class hierarchyCategory : MonoBehaviour
{
    [SerializeField] private Transform m_Arrow;
    [SerializeField] private Button m_Button;
    [SerializeField] private CanvasGroup[] m_Children;

    private bool m_OpenedState;

    public bool IsOpened { get { return m_OpenedState; } }

    private void Start()
    {
        m_Button.onClick.AddListener( delegate { ToggleOpenedState(); } );

        UpdateChildren();
    }

    private void OnApplicationQuit()
    {
        m_Button.onClick.RemoveListener( delegate { ToggleOpenedState(); } );
    }

    public void ToggleOpenedState()
    {
        m_OpenedState = !m_OpenedState;

        UpdateChildren();
    }

    public void UpdateChildren()
    {
        foreach(CanvasGroup _group in m_Children)
        {
            CanvasGroupHelper.SetActive(_group, m_OpenedState);
            _group.gameObject.SetActive(m_OpenedState); 
        }

        m_Arrow.localEulerAngles = new Vector3(0,0, (m_OpenedState) ? -180 : 90 );
    }
}
