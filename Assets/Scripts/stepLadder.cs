using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FeatherLight.Pro;

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

    private IEnumerator SmoothLadderValue()
    {
        float startTime = Time.time;
        float _oldValue = ladder.value;
        float _finalValue = _index * (ladder.maxValue / 100 / ladderSteps.Length) * 100 + 5;

        while (Time.time < startTime + ladderFillUpSpeed)
        {
            ladder.value = Mathf.Lerp(_oldValue, _finalValue, (Time.time - startTime) / ladderFillUpSpeed);
            yield return null;
        }

        ladder.value = _finalValue;
    }

    private IEnumerator SmoothCurrentStep(bool _Value, Transform _step)
    {
        float startTime = Time.time;
        Vector3 _oldPosition = _step.transform.localScale;

        while (Time.time < startTime + ladderSpeed)
        {
            _step.transform.localScale = Vector3.Lerp(_oldPosition, (_Value) ? Vector3.one : Vector3.zero, (Time.time - startTime) / ladderSpeed);
            yield return null;
        }

        _step.transform.localScale = Vector3.one;
    }


    private void UpdateLadderSteps()
    {
        StartCoroutine(SmoothLadderValue());

        ladder.value = Mathf.SmoothDamp(ladder.value, _index * (ladder.maxValue / 100 / ladderSteps.Length) * 100 + 5, ref ladderSliderVel, ladderFillUpSpeed);

        if (_index >= ladderSteps.Length + 1 || _index < 0)
        {
            _index = 0;
            return;
        }

        StartCoroutine(CanvasGroupHelper.Fade(finishedButton, (_index > ladderSteps.Length - 1), ladderSpeed));

        for (int i = 0; i < ladderSteps.Length; i++)
        {
            CanvasGroup _group = ladderSteps[i].transform.Find("filled").GetComponent<CanvasGroup>();
            Transform _currentStep = ladderSteps[i].transform.Find("current_step");

            if (_index > ladderSteps.Length - 1)
            {
                StartCoroutine(SmoothCurrentStep(false, ladderSteps[ladderSteps.Length - 1].transform));
                StartCoroutine(CanvasGroupHelper.Fade(ladderSteps[ladderSteps.Length - 1].transform.Find("current_step").GetComponent<CanvasGroup>(), false, ladderSpeed));
                return;
            }

            if (_group.alpha != 1 && i <= _index)
            {
                StartCoroutine(CanvasGroupHelper.Fade(_group, true, ladderSpeed));
            }
            else if (_group.alpha != 0 && i > _index)
            {
                StartCoroutine(CanvasGroupHelper.Fade(_group, false, ladderSpeed));
            }

            if (_currentStep.transform.localScale != Vector3.one && i == _index)
            {
                StartCoroutine(SmoothCurrentStep(true, _currentStep.transform));
            }
            else if (_currentStep.transform.localScale != Vector3.zero && i != _index)
            {
                StartCoroutine(SmoothCurrentStep(false, _currentStep.transform));
            }

            CanvasGroup _currentStepCanvasGroup = _currentStep.GetComponent<CanvasGroup>();

            if (_currentStepCanvasGroup.alpha != 1 && i == _index)
            {
                StartCoroutine(CanvasGroupHelper.Fade(_currentStepCanvasGroup, true, ladderSpeed));
            }
            else if (_currentStepCanvasGroup.alpha != 0 && i != _index)
            {
                StartCoroutine(CanvasGroupHelper.Fade(_currentStepCanvasGroup, false, ladderSpeed));
            }
        }
    }

    private void Start()
    {
        UpdateLadderSteps();
    }

    public void SetLadderStep(int i)
    {
        _index = i;

        UpdateLadderSteps();
    }

    public void IncreaseLadderStep(int i = 1)
    {
        SetLadderStep(_index + i);

        UpdateLadderSteps();
    }

    public void DecreaseLadderStep(int i = 1)
    {
        SetLadderStep(_index - i);

        UpdateLadderSteps();
    }
}
