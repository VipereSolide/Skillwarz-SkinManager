using System.Collections;
using System.Collections.Generic;

using FeatherLight.Pro;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    public static Console Instance;

    [Header("Active State")]
    [SerializeField] private KeyCode m_toggleConsoleKeyCode = KeyCode.Minus;
    [SerializeField] private CanvasGroup m_backgroundCanvasGroup;
    [SerializeField] private float m_backgroundFadeTime;
    [SerializeField] private bool m_isActive = false;

    [Header("Global References")]
    [SerializeField] private TMP_Text m_consoleOutputText;

    #region Default Methods

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.SetActive(m_isActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_toggleConsoleKeyCode))
        {
            this.Toggle();
        }
    }

    #endregion
    #region Public Getters
    public bool IsActive
    {
        get { return m_isActive; }
    }
    #endregion
    #region Active State
    private void UpdateActivity()
    {
        StartCoroutine(CanvasGroupHelper.Fade(m_backgroundCanvasGroup, m_isActive, m_backgroundFadeTime));
    }

    public void SetActive(bool _Value)
    {
        m_isActive = _Value;

        UpdateActivity();
    }

    public void Toggle()
    {
        m_isActive = !m_isActive;

        UpdateActivity();
    }
    #endregion
    #region Public Methods

    /// <summary>
    /// Outputs a new message in the console.
    /// </summary>
    public new void SendMessage(string _MessageContent)
    {
        m_consoleOutputText.text += _MessageContent + "\n";
    }

    #endregion
}