using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public Material beltMaterial;
    public float offsetValue;
    public float speed;

    public void Update()
    {
        offsetValue += speed * Time.deltaTime;
        beltMaterial.SetTextureOffset("_MainTex", new Vector2(0, offsetValue));
    }
}
