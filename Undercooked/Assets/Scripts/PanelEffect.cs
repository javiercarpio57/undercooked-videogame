using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Shadow shadow;
    Color hover = new Color(0, 0, 0, 255);
    Color notHover = new Color(0, 0, 0, 0);
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        shadow.effectColor = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shadow.effectColor = notHover;
    }

    // Start is called before the first frame update
    void Start()
    {
        shadow = GetComponent<Shadow>();
        shadow.effectColor = notHover;
    }
}
