using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightInfoUI : MonoBehaviour
{

    [SerializeField]
    Text temperaturetxt;
    [SerializeField]
    Text brightnestxt;
    [SerializeField]
    Text colortxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightDataChanged(float? brightness, float? colorTemperature, Color? colorCorrection) {
        temperaturetxt.text = colorTemperature.Value.ToString();
        brightnestxt.text = brightness.Value.ToString();
        colortxt.text = "R:" + colorCorrection.Value.r + " G: " + colorCorrection.Value.g + " B: " + colorCorrection.Value.b;
    }
}
