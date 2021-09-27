using System;
using System.Collections;
using System.Collections.Generic;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

public class TestRequests : MonoBehaviour
{
    [SerializeField] private Button _send;

    [SerializeField] private Image _image;

    [SerializeField] private Texture2D _texture2D;
    void Start()
    {
        _send.onClick.AddListener(Send);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _image.sprite = Sprite.Create(GetTextureCopy(_texture2D), new Rect(0,0,_texture2D.width, _texture2D.height), Vector2.zero);
        }
    }

    private void Send()
    {
        PickImage(int.MaxValue, Success);
    }

    private void Success(Texture2D texture)
    {
        _image.sprite = Sprite.Create(GetTextureCopy(texture),_image.sprite.rect, Vector2.zero);
        Debug.Log("YES ");
    }
    
    public void PickImage(int maxSize, Action<Texture2D> successCallback = null, Action failCallback = null)
    {
        Texture2D texture = null;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    failCallback?.Invoke();
                    return;
                }
                Debug.Log("!!!!!!!!!texture");
                successCallback?.Invoke(texture);
            }
        }, "Select a PNG image", "image/png");
    }

    private Texture2D GetTextureCopy(Texture2D source)
    {
        //     Debug.Log("COPY " + (source == null));
        //
        //     //Create new Texture2D and fill its pixels from rt and apply changes.
        //     Texture2D readableTexture = new Texture2D(source.width, source.height);
        //     readableTexture.SetPixels(source.GetPixels());
        //     readableTexture.Apply();
        //
        //     Debug.Log("readableTexture " + (readableTexture == null));
        //
        //     return readableTexture;
        //     
        byte[] pix = source.GetRawTextureData();
        Texture2D readableText = new Texture2D(source.width, source.height, source.format, false);
        readableText.LoadRawTextureData(pix);
        readableText.Apply();
        return readableText;
    }
}
