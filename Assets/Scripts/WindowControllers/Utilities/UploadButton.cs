using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Engenious.WindowControllers
{
    public class UploadButton : MonoBehaviour//Button
    {
        [SerializeField] private TextMeshProUGUI _uploadText;
        [SerializeField] private TextMeshProUGUI _uploadingText;
        [SerializeField] private TextMeshProUGUI _uploadedText;

        [SerializeField] private Image _photoImage;

        [SerializeField] private LoadingAnimation _loadingAnimation;

        [SerializeField] private Button _uploadButton;

        public bool Interactable
        {
            set
            {
                _uploadButton.interactable = value;
            }
            get
            {
                return _uploadButton.interactable;
            }
        }

        public void UploadImageState()
        {
            _uploadButton.interactable = true;

            _loadingAnimation.StopAnimation();
            _photoImage.sprite = null;
            //_uploadText.text = "Upload photo";
            _uploadText.enabled = true;
            _uploadingText.enabled = false;
            _uploadedText.enabled = false;
            
            _photoImage.enabled = false;
        }
        
        public void ProcessUploadImageState()
        {
            _uploadText.enabled = false;
            _uploadingText.enabled = true;
            
            _loadingAnimation.StartAnimation();
            //_uploadText.text = "Uploading";
            _uploadButton.interactable = false;
        }
        
        public void EndUploadImageState(Texture2D texture)
        {
            _uploadingText.enabled = false;
            _uploadedText.enabled = true;
            
            _loadingAnimation.StopAnimation();
            _uploadButton.interactable = true;
            //_uploadText.text = "Uploaded";
            _photoImage.enabled = true;
            //_photoImage.sprite = Sprite.Create(texture, _photoImage.rectTransform.rect, _photoImage.rectTransform.pivot);
            _photoImage.sprite =Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        }

        public void AddListener(Action onClick)
        {
            if (onClick != null)
                _uploadButton.onClick.AddListener(() => onClick());
        }

        public void RemoveAllListeners()
        {
            _uploadButton.onClick.RemoveAllListeners();
        }
    }
}