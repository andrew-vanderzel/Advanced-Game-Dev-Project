using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBarVisual : MonoBehaviour
{
    public Vector2 range;
    private PlayerJetpack jetpackScript;
    public Color disabledColor;
    private Color defaultColor;
    private Image chargeImage;
    private Image backImage;
    void Start()
    {
        jetpackScript = FindObjectOfType<PlayerJetpack>();
        chargeImage = transform.GetChild(0).GetComponent<Image>();
        backImage = GetComponent<Image>();
        defaultColor = backImage.color;
    }

    void Update()
    {
        float fill = Mathf.InverseLerp(range.x + 2, range.y, jetpackScript.ChargeAmount);
        chargeImage.fillAmount = fill;

        if (jetpackScript.maxCharge < range.x + 2)
        {
            backImage.color = disabledColor;
        }
        else
        {
            backImage.color = defaultColor;
        }
    }
}
