using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class framerateLocker : MonoBehaviour
{
    [SerializeField]
    private sliderOption framerateInputField;

    [SerializeField]
    private int lockedFPS = 60;

    public int LockedFPS { get { return lockedFPS; } }

    void Start()
    {
        QualitySettings.vSyncCount = 0;

        UpdateLockedFPS();
    }

    [ContextMenu("Update Locked FPS")]
    public void UpdateLockedFPS()
    {
        Application.targetFrameRate = lockedFPS;
    }

    private void FixedUpdate()
    {
        UpdateLockedFPSByInputField();
    }

    private void UpdateLockedFPSByInputField()
    {
        lockedFPS = (int)framerateInputField.CurrentValue;

        UpdateLockedFPS();
    }

    public void SetLockedFPS(int i)
    {
        framerateInputField.SetValue((float)i);
    }
}