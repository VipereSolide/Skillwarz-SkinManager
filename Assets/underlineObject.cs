using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;

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

        m_Image.EffectGradient = _targetColor;
    }

    private void Update()
    {
        if(target != null)
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentUnderlineObjectVelocity, speed);
    }
}
