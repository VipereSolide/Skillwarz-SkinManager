using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FeatherLight.Pro;

public class helpPages : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] m_Pages;
    
    private int m_Index = 0;

    public int Index { get { return m_Index; } }

    public void SetIndex(int _Index)
    {
        for (int i = 0; i < m_Pages.Length; i++)
        {
            CanvasGroupHelper.SetActive(m_Pages[i], (i == _Index) );
            m_Pages[i].transform.gameObject.SetActive( (i == _Index) );
        }
    }
}
