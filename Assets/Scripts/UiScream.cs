using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    public Toggle toggle;
    public Toggle hueToggle;
    public Slider hueSlider;
    public Slider sizeSlider;
    public Slider colorSlider;
    public TMP_Text myText;
    public TMP_Dropdown myDropdownOpcion;
    public TMP_InputField InputField;
    public GameObject image;
    public float SizeValue = 30;

   
    private void Start()
    {
        myDropdownOpcion.onValueChanged.AddListener(HandleDropdownChange);
        AnimatedImage();
    }
    
    
    private void Update()
    {
        SizeLetter();
        toggleSizeEnable();
        ChangeColor();
        toggleHueEnable(); 
    }
    public void AnimatedImage()
    {
        LeanTween.moveX(image.GetComponent<RectTransform>(),0,1f).setDelay(1.5f);
    }
    public void ChangeColor()
    {
        myText.color = Color.HSVToRGB(colorSlider.value, 1f, 1f);
    }
    private void HandleDropdownChange(int index)
    {
        switch (index)
        {
            case 0:

                InputField.text = myText.text.ToUpper(); 
                break;
            case 1:

                InputField.text = myText.text.ToLower();
                break;
        }
    }
    public void SizeLetter()
    {
        if (sizeSlider)
        {
            myText.fontSize = sizeSlider.value * SizeValue;
        }   
    }
    public void toggleSizeEnable()
    {
        if (toggle.isOn)
        {
            sizeSlider.enabled = true;
        }
        else if (!toggle.isOn)
        {
            sizeSlider.enabled = false;
        }
    }
    public void toggleHueEnable()
    {
        if (hueToggle.isOn)
        {
            hueSlider.enabled = true;
        }
        else if (!hueToggle.isOn)
        {
            hueSlider.enabled = false;
        }
    }



}
