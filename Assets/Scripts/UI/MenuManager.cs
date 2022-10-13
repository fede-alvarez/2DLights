using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Main Refs")]
    [SerializeField] private ExitAlertUI _exitUI;

    [Header("Animated elements")]
    [SerializeField] private RectTransform _title;
    [SerializeField] private RectTransform _buttonsContainer;
    [SerializeField] private RectTransform _settingsContainer;

    [Header("Times")]
    [Range(1.0f,3.0f)]
    [SerializeField] private float _titleAppearTime = 3.0f;
    [Range(1.0f,3.0f)]
    [SerializeField] private float _buttonsAppearTime = 2.5f;

    [Header("Easing")]
    [SerializeField] private Ease _defaultEase;

    private Vector3 _titleFinalPosition;
    private Vector3 _buttonsFinalPosition;

    private CanvasGroup _titleGroup, _buttonsGroup, _settingsGroup;

    private void Start() 
    {
        _titleGroup = _title.GetComponent<CanvasGroup>();
        _buttonsGroup = _buttonsContainer.GetComponent<CanvasGroup>();
        _settingsGroup = _settingsContainer.GetComponent<CanvasGroup>();

        _settingsContainer.gameObject.SetActive(false);
        
        _titleGroup.alpha = 0;
        _buttonsGroup.alpha = 0;

        _titleFinalPosition = _title.anchoredPosition;
        _buttonsFinalPosition = _buttonsContainer.anchoredPosition;

        _title.anchoredPosition += new Vector2(0, 200);
        _buttonsContainer.anchoredPosition -= new Vector2(0, 250);

        InitializeAnimation();
    }

    public void StartGame()
    {

    }

    public void GoSettings()
    {
        _buttonsGroup.DOFade(0, 1.0f).OnComplete(() => 
        {
            // Desactivo los botones y activo los settings
            _buttonsContainer.gameObject.SetActive(false);
            _settingsContainer.gameObject.SetActive(true);

            // Hago Fade de 0 a 1 del grupo de los settings
            _settingsGroup.DOFade(1, 0.5f);

            // Guardo la posición actual del settings container
            // luego le sumo un offset para que aparezca de la derecha
            Vector2 initialPos = _settingsContainer.anchoredPosition;
            _settingsContainer.anchoredPosition += new Vector2(80, 0);

            // Animo el contenedor a su posición inicial
            _settingsContainer.DOAnchorPos(initialPos, 0.4f);
        });

        _buttonsContainer.DOAnchorPos(_buttonsContainer.anchoredPosition - new Vector2(80, 0), 0.9f)
                        .OnComplete(() => {
                            _buttonsContainer.anchoredPosition += new Vector2(80, 0);
                        });
    }

    public void GoBack()
    {
        _settingsGroup.DOFade(0, 1.0f).OnComplete(() => 
        {
            _settingsContainer.gameObject.SetActive(false);
            _buttonsContainer.gameObject.SetActive(true);

            _buttonsGroup.alpha = 0;
            _buttonsGroup.DOFade(1, 0.5f);

            Vector2 initialPos = _buttonsContainer.anchoredPosition;
            _buttonsContainer.anchoredPosition -= new Vector2(80, 0);

            _buttonsContainer.DOAnchorPos(initialPos, 0.4f);
        });

        _settingsContainer.DOAnchorPos(_settingsContainer.anchoredPosition + new Vector2(80, 0), 0.9f)
                        .OnComplete(() => {
                            _settingsContainer.anchoredPosition -= new Vector2(80, 0);
                        });
    }

    public void ExitGame()
    {
        _exitUI.gameObject.SetActive(true);
        _exitUI.Open();
    }

    public void NoExitOption()
    {
        _exitUI.Close();
    }
    public void YesExitOption()
    {
        Application.Quit();
    }

    private void InitializeAnimation()
    {
        // Titulo
        _titleGroup.DOFade(1, _titleAppearTime).OnComplete(() => {
            _title.DORotate(new Vector3(0,0,10), 1.0f).SetLoops(-1, LoopType.Yoyo);
        });
        _title.DOAnchorPos(_titleFinalPosition, _titleAppearTime).SetEase(_defaultEase);

        // Grupo de botones
        _buttonsGroup.DOFade(1, _buttonsAppearTime);
        _buttonsContainer.DOAnchorPos(_buttonsFinalPosition, _buttonsAppearTime).SetEase(_defaultEase);
    }
}
