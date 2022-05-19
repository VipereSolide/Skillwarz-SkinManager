using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class categoryManager : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.15f;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private int windowSize = 1423;

    [SerializeField]
    private int index = 0;
    
    private Vector3 vel;

    [ContextMenu("Increment")]
    public void Increment()
    {
        index++;
    }

    [ContextMenu("Decrement")]
    public void Decrement()
    {
        index--;
    }

    public void SetIndex(int _index)
    {
        this.index = _index;
    }

    private void Update()
    {
        content.localPosition = Vector3.SmoothDamp(content.localPosition, new Vector3(windowSize * -index,0,0), ref vel, moveSpeed);
    }
}
