using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScanWindow : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Video player reference")]
    private VideoPlayer m_VideoPlayer;
    
    [SerializeField]
    [Tooltip("Find Clip animation")]
    private VideoClip m_FindImageClip;

    [SerializeField]
    [Tooltip("Tap Clip animation")]
    private VideoClip m_TapClip;
    
    public enum ScanWindowStates
    {
        FindPlane,
        Tap
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        m_VideoPlayer.clip = m_FindImageClip;
        m_VideoPlayer.Play();
    }

    public void Show(ScanWindowStates state)
    {
        gameObject.SetActive(true);
        
        switch (state)
        {
            case ScanWindowStates.FindPlane:
                m_VideoPlayer.clip = m_FindImageClip;
                break;
            
            case ScanWindowStates.Tap:
                m_VideoPlayer.clip = m_TapClip;
                break;
        }
        
        m_VideoPlayer.Play();
    }
    
    public void Hide()
    {
        m_VideoPlayer.Stop();
        gameObject.SetActive(false);
    }
}
