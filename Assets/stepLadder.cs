using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stepLadder : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Slider ladder;

    [SerializeField]
    private GameObject[] ladderSteps;

    [SerializeField]
    private Transform ladderRef;

    [SerializeField]
    private CanvasGroup finishedButton;

    [Header("Values")]

    [SerializeField]
    private float ladderFillUpSpeed;

    [SerializeField]
    private float ladderSpeed;


    private int _index = 0;
    private float ladderSliderVel;


    private void UpdateLadderSteps()
    {
        ladder.value = Mathf.SmoothDamp(ladder.value, _index * (ladder.maxValue / 100 / ladderSteps.Length) * 100 + 5, ref ladderSliderVel, ladderFillUpSpeed);
        
        if (_index >= ladderSteps.Length + 1 || _index < 0)
        {
            _index = 0;
            return;
        }

        if (_index > ladderSteps.Length - 1)
        {
            finishedButton.alpha = Mathf.Lerp(finishedButton.alpha, 1, Time.deltaTime * ladderSpeed);
        }
        else
        {
            finishedButton.alpha = Mathf.Lerp(finishedButton.alpha, 0, Time.deltaTime * ladderSpeed);
        }

        for(int i = 0; i < ladderSteps.Length; i++)
        {
            CanvasGroup _group = ladderSteps[i].transform.Find("filled").GetComponent<CanvasGroup>();
            Transform _currentStep = ladderSteps[i].transform.Find("current_step");

            if (_index > ladderSteps.Length - 1)
            {
                ladderSteps[ladderSteps.Length - 1].transform.Find("current_step").transform.localScale = Vector3.Lerp(_currentStep.transform.localScale, Vector3.zero, Time.deltaTime * ladderSpeed);
                ladderSteps[ladderSteps.Length - 1].transform.Find("current_step").GetComponent<CanvasGroup>().alpha = Mathf.Lerp(_currentStep.GetComponent<CanvasGroup>().alpha, 0, Time.deltaTime * ladderSpeed);
                return;
            }

            if (i <= _index)
            {
                _group.alpha = Mathf.Lerp(_group.alpha, 1, Time.deltaTime * ladderSpeed);
            }
            else
            {
                _group.alpha = Mathf.Lerp(_group.alpha, 0, Time.deltaTime * ladderSpeed);
            }

            if (i == _index)
            {
                _currentStep.transform.localScale = Vector3.Lerp(_currentStep.transform.localScale, Vector3.one, Time.deltaTime * ladderSpeed);
                _currentStep.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(_currentStep.GetComponent<CanvasGroup>().alpha, 1, Time.deltaTime * ladderSpeed);
            }
            else
            {
                _currentStep.transform.localScale = Vector3.Lerp(_currentStep.transform.localScale, Vector3.zero, Time.deltaTime * ladderSpeed);
                _currentStep.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(_currentStep.GetComponent<CanvasGroup>().alpha, 0, Time.deltaTime * ladderSpeed);
            }
            
        }
    }

    private void Update()
    {
        UpdateLadderSteps();
    }

    public void SetLadderStep(int i)
    {
        _index = i;
    }

    public void IncreaseLadderStep(int i = 1)
    {
        SetLadderStep(_index + i);
    }

    public void DecreaseLadderStep(int i = 1)
    {
        SetLadderStep(_index - i);
    }
}
