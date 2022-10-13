using UnityEngine;
using DG.Tweening;

public class ExitAlertUI : MonoBehaviour
{
    [Header("Animated elements")]
    [SerializeField] private RectTransform _exitBox;

    [Header("Easing")]
    [SerializeField] private Ease _defaultEase;

    private bool _active = false;
    private Vector2 _exitBoxFinalPosition;

    private void Start() 
    {
        _exitBoxFinalPosition = _exitBox.anchoredPosition;
        _exitBox.anchoredPosition = new Vector2(0,-500);
    }

    private void AppearAnimation()
    {
        _exitBox.DOAnchorPos(_exitBoxFinalPosition, 1.0f);
        _exitBox.GetComponent<CanvasGroup>().DOFade(1, 1.0f);
    }

    private void DisappearAnimation()
    {
        _exitBox.DOAnchorPos(new Vector2(0,-500), 1.0f);
        _exitBox.GetComponent<CanvasGroup>().DOFade(0, 0.8f);
    }

    public void Open()
    {
        _active = true;

        GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        AppearAnimation();
    }

    public void Close()
    {
        _active = false;
        DisappearAnimation();
        GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}
