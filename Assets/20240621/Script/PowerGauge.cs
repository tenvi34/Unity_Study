using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PowerGauge : MonoBehaviour
{
    public RectTransform gaugeMask;
    
    [NonSerialized]
    public float MaxGaugeWidth;
    
    void Awake()
    {
        MaxGaugeWidth = gaugeMask.sizeDelta.x;
        SetGaugePercent(0);
    }

    public void SetGaugePercent(float normalizeValue)
    {
        float gaugeX = MaxGaugeWidth * normalizeValue;
        
        gaugeMask.sizeDelta = new Vector2(gaugeX, gaugeMask.sizeDelta.y);
    }

    void Update()
    {
        
    }
}
