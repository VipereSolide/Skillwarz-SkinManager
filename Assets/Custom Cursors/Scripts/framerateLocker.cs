using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class framerateLocker : MonoBehaviour
{
    [SerializeField] private sliderOption m_framerateSlider;
    [SerializeField] private TMP_Text m_sliderText;
    [SerializeField] private int m_lockedFPS = 60;
    [SerializeField] private int[] m_lockedFpsLevels = new int[5]{30,60,120,144,200};

    public int LockedFPS { get { return m_lockedFPS; } }

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        m_framerateSlider.Slider.onValueChanged.AddListener(OnValueChanged);

        UpdateLockedFPS();
    }

    void OnApplicationQuit()
    {
        m_framerateSlider.Slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    [ContextMenu("Update Locked FPS")]
    public void UpdateLockedFPS()
    {
        int _targetFps = m_lockedFpsLevels[(int)m_framerateSlider.Slider.value - 1];
        Application.targetFrameRate = _targetFps;
        m_sliderText.text = _targetFps.ToString();
        m_lockedFPS = (int)m_framerateSlider.Slider.value;
    }

    protected void OnValueChanged(float _Value)
    {
        UpdateLockedFPS();
    }

    public void SetLockedFPS(int i)
    {
        m_framerateSlider.SetValue(i);
        UpdateLockedFPS();
    }
}