using System.Collections.Generic;
using UnityEngine.EventSystems;
using Michsky.UI.ModernUIPack;
using System.Collections;
using FeatherLight.Pro;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class invalidCharactersPopup : MonoBehaviour
{
    [SerializeField] private Transform warningWindowContainer;
    [Space()]

    [SerializeField] private char[] invalidCharacters;
    [SerializeField] private CanvasGroup warningWindow;
    [SerializeField] private float warningWindowLastTime = 2;
    [SerializeField] private float warningWindowFadeSpeed = 6;

    private TMP_InputField m_inputField;
    private bool isWindowActive = false;

    private void Start()
    {
        m_inputField = GetComponent<TMP_InputField>();
        m_inputField.onEndEdit.AddListener(delegate { CheckForInvalidCharacters(); });
    }

    private void OnApplicationQuit()
    {
        m_inputField.onEndEdit.RemoveListener(delegate { CheckForInvalidCharacters(); });
    }

    private void CheckForInvalidCharacters()
    {
        foreach(char c in invalidCharacters)
        {
            if (m_inputField.text.Contains(c.ToString()))
            {
                isWindowActive = true;
                StartCoroutine(CanvasGroupHelper.Fade(warningWindow, true, warningWindowFadeSpeed));
                warningWindow.transform.SetParent(warningWindowContainer, true);
                Invoke(nameof(FadeWarningWindow), warningWindowLastTime);

                m_inputField.text = m_inputField.text.Replace(c.ToString(),"");

                // https://stackoverflow.com/questions/56145437/how-to-make-textmesh-pro-input-field-deselect-on-enter-key
                var eventSystem = EventSystem.current;

                if (!eventSystem.alreadySelecting)
                    eventSystem.SetSelectedGameObject (null);
                
                if (string.IsNullOrEmpty(m_inputField.text))
                    m_inputField.transform.GetComponent<CustomInputField>().AnimateOut();
            }
        }
    }

    private void FadeWarningWindow()
    {
        StartCoroutine(CanvasGroupHelper.Fade(warningWindow, false, warningWindowFadeSpeed));
        warningWindow.transform.SetParent(transform, true);
    }
}