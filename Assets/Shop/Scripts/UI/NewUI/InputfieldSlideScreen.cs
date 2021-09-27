using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputfieldSlideScreen : MonoBehaviour
{
    // Assign canvas here in editor
public Canvas canvas;


// Used internally - set by InputfieldFocused.cs
public bool InputFieldActive = false;
public RectTransform childRectTransform;

#if UNITY_IOS

 void LateUpdate () {
        if ((InputFieldActive)&&((TouchScreenKeyboard.visible)))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
 
            Vector3[] corners = {Vector3.zero,Vector3.zero,Vector3.zero,Vector3.zero};
            Rect rect = RectTransformExtension.GetScreenRect(childRectTransform, canvas);
            float keyboardHeight = TouchScreenKeyboard.area.height;
 
            float heightPercentOfKeyboard = keyboardHeight/Screen.height*100f;
            float heightPercentOfInput = (Screen.height-(rect.y+rect.height))/Screen.height*100f;
 
 
            if (heightPercentOfKeyboard>heightPercentOfInput)
            {
                // keyboard covers input field so move screen up to show keyboard
                float differenceHeightPercent = heightPercentOfKeyboard - heightPercentOfInput;
                float newYPos = transform.GetComponent<RectTransform>().rect.height /100f*differenceHeightPercent;
 
                Vector2 newAnchorPosition = Vector2.zero;
                newAnchorPosition.y = newYPos;
                transform.GetComponent<RectTransform>().anchoredPosition = newAnchorPosition;
            } else {
                // Keyboard top is below the position of the input field, so leave screen anchored at zero
                transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        } else {
            // No focus or touchscreen invisible, set screen anchor to zero
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        InputFieldActive = false;
    }
#endif

}
