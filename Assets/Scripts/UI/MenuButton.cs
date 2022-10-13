using UnityEngine;
using DG.Tweening;

public class MenuButton : MonoBehaviour
{
    private RectTransform _rect;
    private void Awake() 
    {
        _rect = GetComponent<RectTransform>();        
    }

    private void OnMouseEnter() 
    {
        print("Mouse Enter");
        _rect.DOScale(new Vector2(2,2), 0.2f);  
    }

    private void OnMouseExit() 
    {
        print("Mouse exit");
        _rect.DOScale(Vector2.one, 0.2f);
    }
}
