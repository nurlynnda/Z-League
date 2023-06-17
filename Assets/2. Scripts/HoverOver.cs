using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class HoverOver : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverPanel;
    public AudioSource audio;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverSound()
    {
        audio.PlayOneShot(hoverSound);
    }

    public void ClickSound()
    {
        audio.PlayOneShot(clickSound);
    }

    public void OnPointerEnter(PointerEventData enterData)
    {
        HoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData enterData)
    {
        HoverPanel.SetActive(false);
    }
}
