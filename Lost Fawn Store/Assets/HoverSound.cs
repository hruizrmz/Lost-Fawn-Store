using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private AudioSource hover;
    private AudioSource click;
    // Start is called before the first frame update
    void Start()
    {
        hover = GameObject.Find("UI_Hover").GetComponent<AudioSource>();
        click = GameObject.Find("UI_Click").GetComponent<AudioSource>();
    }
    public void OnPointerEnter(PointerEventData eventdata)
    {
        hover.Play();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontStyle = (FontStyles)64;
    }
    public void OnPointerExit(PointerEventData eventdata)
    {
        hover.Stop();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontStyle = (FontStyles)8;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        click.Play();
    }
}
