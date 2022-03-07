using UnityEngine;

public class CloudManager : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rectTransform.localPosition -= new Vector3(0.5f, 0, 0);
        if(_rectTransform.localPosition.x <= -1920) _rectTransform.localPosition = Vector3.zero;
    }
}
