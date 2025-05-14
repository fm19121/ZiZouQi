using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class SingleGridDisplay : MonoBehaviour
{
    [SerializeField] private MeshRenderer singleGridUI;

    private bool isSeleceted;

    void Start()
    {
        isSeleceted = false;
        Default();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    public void Default()
    {
        singleGridUI.material.color = new Color(0.8f, 0.8f, 0.8f, 0.7f);
    }
    private void SetSelectedColor()
    {
        singleGridUI.material.color = new Color(0.4f, 0.3f, 0.8f, 0.8f);
    }

    public void SetIsPlayerPawnOnGrid()
    {
        singleGridUI.material.color = new Color(0.3f, 0.8f, 0.3f, 0.8f);
    }

    public void SetIsAIPawnOnGrid()
    {
        singleGridUI.material.color = new Color(0.8f, 0.2f, 0.3f, 0.8f);
    }
    public void BeSelected()
    {
        isSeleceted = true;
    }

    public void NotSelected()
    {
        isSeleceted = false;
    }

    private void ChangeColor()
    {
        if(isSeleceted)
        {
            SetSelectedColor();
        }
        else
        {
            Default();
        }
    }
}
