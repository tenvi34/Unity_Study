using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeHpBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI hpText;

    [SerializeField] private RectTransform maskTrasform;
    [SerializeField] private RectTransform backgroundTransform;

    private float maxWidth;
    private float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        maxWidth = backgroundTransform.sizeDelta.x;
        maxHeight = backgroundTransform.sizeDelta.y;
    }

    public void UpdateHpStatus(float currentHp, float maxHp)
    {
        hpText.text = $"{currentHp}/{maxHp}";

        // 조건문
        float factor = 1.0f;
        if (maxHp != 0.0f)
        {
            factor = currentHp / maxHp;
        }
        //
        // //삼항 연산자
        // factor = maxHp != 0.0f ? currentHp / maxHp : 1.0f;
        //
        maskTrasform.sizeDelta = new Vector2(factor * maxWidth, maxHeight);
    }
}
