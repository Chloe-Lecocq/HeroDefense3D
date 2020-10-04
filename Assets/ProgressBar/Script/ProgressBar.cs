using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{

    [Header("Title Setting")]
    public string Title;
    public Color TitleColor;
    public Font TitleFont;
    public int TitleFontSize = 10;

    [Header("Bar Setting")]
    public Color BarColor;   

    [Range(1f, 100f)]
    public int Alert = 20;
    public Color BarAlertColor;

    private Image bar;
    private float nextPlay;
    private Text txtTitle;
    protected float barValue;
    public float start;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, start);

            barValue = value;
            UpdateValue(barValue, start);

        }
    }

    public void setBarValue(float value)
    {
        barValue = value;
    }

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        txtTitle = transform.Find("Text").GetComponent<Text>();

    }

    private void Start()
    {
        txtTitle.text = Title;
        txtTitle.color = TitleColor;
        txtTitle.font = TitleFont;
        txtTitle.fontSize = TitleFontSize;

        bar.color = BarColor;

        UpdateValue(barValue, start);


    }

    void UpdateValue(float val, float max)
    {
        bar.fillAmount = val / max;

        if (Alert >= val)
        {
            bar.color = BarAlertColor;
        }
        else
        {
            bar.color = BarColor;
        }

    }


    private void Update()
    {
        UpdateValue(barValue, start);

        if (!Application.isPlaying)
        {           
            txtTitle.color = TitleColor;
            txtTitle.font = TitleFont;
            txtTitle.fontSize = TitleFontSize;

            bar.color = BarColor;      
        }
       
    }

}
