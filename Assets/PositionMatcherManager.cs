using System;

using UnityEngine;

public class PositionMatcherManager : MonoBehaviour
{
    [SerializeField] private MatchedPosition[] m_positions;

    private void Update()
    {
        foreach(MatchedPosition _position in m_positions)
        {
            if (_position.Matched.gameObject.activeInHierarchy)
                _position.Matched.position = _position.To.position;
        }
    }

    [Serializable]
    public class MatchedPosition
    {
        [SerializeField] private Transform m_matched;
        [SerializeField] private Transform m_onto;

        public Transform Matched
        {
            get
            {
                return m_matched;
            }
        }

        public Transform To
        {
            get
            {
                return m_onto;
            }
        }
    }
}