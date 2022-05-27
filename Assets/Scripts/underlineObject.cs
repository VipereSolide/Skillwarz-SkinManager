using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Michsky.UI.ModernUIPack;
using System.Collections.Generic;

public class underlineObject : MonoBehaviour
{
    private float speed;
    private RectTransform target;
    private Vector3 currentUnderlineObjectVelocity;
    private UIGradient m_Image;

    void Start()
    {
        m_Image = transform.GetComponent<UIGradient>();
    }

    public void StartMoving(RectTransform _target, float _speed, UnityEngine.Gradient _targetColor)
    {
        this.target = _target;
        this.speed = _speed;

        StartCoroutine(Fade());

        m_Image.EffectGradient = _targetColor;
    }

    private IEnumerator Fade()
    {
        float startTime = Time.time;
        Vector3 _oldPosition = transform.position;

        while (Time.time < startTime + speed)
        {
            transform.position = Vector3.Lerp(_oldPosition, target.position, (Time.time - startTime) / speed);
            yield return null;
        }

        transform.position = target.position;
    }
}
