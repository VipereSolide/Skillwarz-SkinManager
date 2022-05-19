using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class customCursorObject : MonoBehaviour
{
    [SerializeField]
    private float activeSwapTime = 0.15f;

    [SerializeField] private float m_MoveSpeed = 7f;
    [SerializeField] private Transform m_LerpElement;

    public bool activeSelf { get; private set; }
    public float MoveSpeed { get { return m_MoveSpeed; } }
    private CanvasGroup m_CanvasGroup;

    private Vector3 localScaleVelocity;
    private float canvasGroupVelocity;

    void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(m_LerpElement != null) m_LerpElement.transform.position = Vector3.Lerp(m_LerpElement.transform.position, transform.position, Time.deltaTime * m_MoveSpeed);

        transform.localScale = Vector3.SmoothDamp(
            transform.localScale,
            Vector3.one * ((activeSelf) ? 1 : 0),
            ref localScaleVelocity,
            activeSwapTime
        );

        m_CanvasGroup.alpha = Mathf.SmoothDamp(
            m_CanvasGroup.alpha,
            (activeSelf) ? 1 : 0,
            ref canvasGroupVelocity,
            activeSwapTime
        );
    }

    public void SetActive(bool value)
    {
        if (m_CanvasGroup == null)
            return;

        activeSelf = value;
    }
}