using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectionScr : MonoBehaviour
{
    [SerializeField] GameObject Character, Description, BG;
    void Awake()
    {
        Character.SetActive(false);
        Description.SetActive(false);
        BG.SetActive(false);
    }

    public void OnMouseOver()
    {
        Character.SetActive(true);
        Description.SetActive(true);
        BG.SetActive(true);
        
    }

    public void OnMouseExit()
    {
        Character.SetActive(false);
        Description.SetActive(false);
        BG.SetActive(false);
    }

}
