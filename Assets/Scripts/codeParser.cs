using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class codeParser : MonoBehaviour
{
    [SerializeField][TextArea()] private string m_Code;
    [SerializeField] private ParseAgent[] m_Agents;
    [SerializeField] private Color m_NumberColor;

    [ContextMenu("Parse")]
    private void Parse()
    {
        m_Code = m_Code.Replace("/","<#" + ColorToHex(m_Agents[7].m_Color) + ">/</color>");

        for(int i = 0; i < m_Agents.Length; i++)
        {
            for(int c = 0; c < m_Agents[i].m_From.Length; c++)
            {
                m_Code = m_Code.Replace(m_Agents[i].m_From[c], "<#" + ColorToHex(m_Agents[i].m_Color) + ">" + m_Agents[i].m_From[c] + "</color>");
            }
        }
    }

    private string ColorToHex(Color color)
    {
        int r = (int)(color.r * 255);
        int g = (int)(color.g * 255);
        int b = (int)(color.b * 255);

        string _color = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        return _color;
    }
}
[Serializable]
public class ParseAgent
{
    public string[] m_From;
    public Color m_Color;
}