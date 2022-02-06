using System.Collections;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _transitionTime = 0.5f;

    private Coroutine _fadeCoroutine;

    private void OnValidate()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void SetAlpha(float alpha)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(TransitionColor(new Color(1, 1, 1, alpha)));
    }

    public void SetAlphaInstant(float pAlpha)
    {
        StopAllCoroutines();
        _spriteRenderer.color = new Color(1, 1, 1, pAlpha);
    }

    IEnumerator TransitionColor(Color pTargetColor)
    {
        float t = 0;

        Color currentColor = _spriteRenderer.color;

        while (t != _transitionTime)
        {
            _spriteRenderer.color = Color.Lerp(currentColor, pTargetColor, t / _transitionTime);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
