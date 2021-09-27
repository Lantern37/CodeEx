using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformExtension : MonoBehaviour
{
    public static Rect GetScreenRect(RectTransform rectTransform, Canvas canvas) {
 
        Vector3[] corners = new Vector3[4];
        Vector3[] screenCorners = new Vector3[2];
 
        rectTransform.GetWorldCorners(corners);
 
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
        {
            screenCorners[0] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[1]);
            screenCorners[1] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[3]);
        }
        else
        {
            screenCorners[0] = RectTransformUtility.WorldToScreenPoint(null, corners[1]);
            screenCorners[1] = RectTransformUtility.WorldToScreenPoint(null, corners[3]);
        }
 
        screenCorners[0].y = Screen.height - screenCorners[0].y;
        screenCorners[1].y = Screen.height - screenCorners[1].y;
 
        return new Rect(screenCorners[0], screenCorners[1] - screenCorners[0]);
    }
    
//     public static int GetKeyboardHeight(bool includeInput)
//     {
// #if UNITY_ANDROID
//         using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
//         {
//             AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");
//  
//             using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
//             {
//                 View.Call("getWindowVisibleDisplayFrame", Rct);
//  
//                 int height = Rct.Call<int>("height");
//                 int width = Rct.Call<int>("width");
//  
//                 int systemHeight = Display.main.systemHeight;
//                 int systemWidth = Display.main.systemWidth;
//  
//                 return Screen.height - Rct.Call<int>("height");
//             }
//         }
// #elif UNITY_IOS
//         return (int)TouchScreenKeyboard.area.height;
// #else
//         return 0;
// #endif
//     }
    
    public static int GetRelativeKeyboardHeight(RectTransform rectTransform, bool includeInput)
    {
        int keyboardHeight = GetKeyboardHeight(includeInput);
        float screenToRectRatio = Screen.height / rectTransform.rect.height;
        float keyboardHeightRelativeToRect = keyboardHeight / screenToRectRatio;
   
        return (int) keyboardHeightRelativeToRect;
    }
   
    private static int GetKeyboardHeight(bool includeInput)
    {
#if UNITY_EDITOR
        return 0;
#elif UNITY_ANDROID
            using (AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject unityPlayer = unityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
                AndroidJavaObject view = unityPlayer.Call<AndroidJavaObject>("getView");
                AndroidJavaObject dialog = unityPlayer.Get<AndroidJavaObject>("mSoftInputDialog");
                if (view == null || dialog == null)
                    return 0;
                var decorHeight = 0;
                if (includeInput)
                {
                    AndroidJavaObject decorView = dialog.Call<AndroidJavaObject>("getWindow").Call<AndroidJavaObject>("getDecorView");
                    if (decorView != null)
                        decorHeight = decorView.Call<int>("getHeight");
                }
                using (AndroidJavaObject rect = new AndroidJavaObject("android.graphics.Rect"))
                {
                    view.Call("getWindowVisibleDisplayFrame", rect);
                    return Screen.height - rect.Call<int>("height") + decorHeight;
                }
            }
#elif UNITY_IOS
            return (int)TouchScreenKeyboard.area.height;
#endif
    }

}
