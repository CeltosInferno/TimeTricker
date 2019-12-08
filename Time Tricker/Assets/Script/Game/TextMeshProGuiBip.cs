using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class uses a TextMeshProGui and settle its color
 * withe the time in order to give a "bip" effect
 */
public class TextMeshProGuiBip : MonoBehaviour
{
    protected TMPro.TextMeshProUGUI textMeshPro;
    public float bippingFrequency = 2f;
    public Color defaultColor = Color.white;
    private Color bipColor = Color.blue;
    private bool isBipping = false;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TMPro.TextMeshProUGUI>();
        if (textMeshPro == null)
            Debug.LogError("Could not find any TextMeshProUGUI in script TextMeshProGuiBip.cs");
    }

    public void setBippingColor(Color c)
    {
        bipColor = c;
    }

    public void setBip(bool b)
    {
        isBipping = b;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBipping)
        {
            float intensity = Mathf.PingPong(Time.time * bippingFrequency, 1);
            textMeshPro.color = Color.Lerp(defaultColor, bipColor, intensity);
        }
        else
        {
            textMeshPro.color = defaultColor;
        }
    }
}
