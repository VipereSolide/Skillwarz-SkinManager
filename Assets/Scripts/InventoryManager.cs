using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup m_gridLayout;
    [SerializeField] private float[] m_size;
    [SerializeField] private float[] m_margin;

    public void SetIconSize(int _Level)
    {
        m_gridLayout.cellSize = new Vector3(m_size[_Level], m_size[_Level], 0);
        m_gridLayout.spacing = new Vector3(m_margin[_Level], m_margin[_Level], 0);
    }
}
