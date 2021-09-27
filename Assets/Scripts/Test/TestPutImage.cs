using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Engenious.Core.Managers;
using Engenious.Core.Managers.Requests;
using Engenious.WindowControllers;
using UnityEngine;
using UnityEngine.Networking;

public class TestPutImage : MonoBehaviour
{
    private INetworkManager _manager;

    [SerializeField] private NetworkConfig _config;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private UploadButton _uploadButton;

    [SerializeField] private string _phone;

    void Start()
    {
        _manager = new NetworkManager();
        _manager.Config = _config;
        Init();
    }

    async void Init()
    {
        await _manager.Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //PuPhone();
            _uploadButton.EndUploadImageState(GetTextureCopy(_sprite.texture));
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            byte[] bytesPass = _sprite.texture.EncodeToPNG();

            byte[] bytesPhys = _sprite.texture.EncodeToPNG();

            PutPassportRequest req = new PutPassportRequest()
            {
                PassportPhoto = bytesPass,
                PhysicianPhoto = bytesPass
            };

            //req.PassportPhotoData.SetData(_sprite.texture, _phone);

            // string srt = "";
            // foreach (var bytes in bytesPass)
            // {
            //     srt += bytes.ToString();
            // }
            //
            Debug.Log("bytesPass = " + bytesPass.Length + "firstbyte " + bytesPass[0]);
            UserReq(req);

            //UserReq(req);
        }
    }

    private Texture2D GetReadTextureCopy (Texture2D texture)
    {
        // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary( 
            texture.width,
            texture.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear);

// Blit the pixels on texture to the RenderTexture
        Graphics.Blit(texture, tmp);
// Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;
// Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;
// Create a new readable Texture2D to copy the pixels to it
        Texture2D myTexture2D = new Texture2D(texture.width, texture.height);
// Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        myTexture2D.Apply();
// Reset the active RenderTexture
        RenderTexture.active = previous;
// Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

// "myTexture2D" now has the same pixels from "texture" and it's readable.
        return myTexture2D;
    }
    
    async void UserReq(PutPassportRequest req)
    {
         await _manager.NetworkUserRequests.PutPassportMultipart(req , 86);
    }

    async void PuPhone()
    {
        var phone = new VerifyPhoneRequest();
        phone.Phone = _phone;
            await _manager.NetworkUserRequests.PutPhoneNumber(phone,61);
    }
    
    Texture2D GetTextureCopy (Texture2D source)
    {
        //Create a RenderTexture
        RenderTexture rt = RenderTexture.GetTemporary (
            source.width,
            source.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear
        );

        //Copy source texture to the new render (RenderTexture) 
        Graphics.Blit (source, rt);

        //Store the active RenderTexture & activate new created one (rt)
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        //Create new Texture2D and fill its pixels from rt and apply changes.
        Texture2D readableTexture = new Texture2D (source.width, source.height);
        readableTexture.ReadPixels (new Rect (0, 0, rt.width, rt.height), 0, 0);
        readableTexture.Apply ();

        //activate the (previous) RenderTexture and release texture created with (GetTemporary( ) ..)
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary (rt);

        return readableTexture;
    }
    
    // public async void PutFormPasport(PutPassportRequest passport, int id)
    // {
    //     var requestString = _manager.Config.BaseURL +_manager.Config.GetUser+"/"+id +_manager.Config.VerifyPassport;
    //
    //     WWWForm form = new WWWForm();
    //     form.AddBinaryData("passportPhoto", passport.PassportPhoto, "passportPhoto.png" ,"image/png");   
    //     form.AddBinaryData("physicianRecPhoto", passport.PhysicianPhoto,"physicianRecPhoto.png" ,"image/png");
    //     var request = UnityWebRequest.Post(requestString, form);
    //     request.method = "PUT";
    //     
    //     //request.SetRequestHeader("Content-Type", "application/json");
    //     request.SetRequestHeader("Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken);
    //
    //     Debug.Log("req = "+ request.url);
    //     
    //     var responce = await request.SendWebRequest();
    //     // {
    //     //     {"Content-Type", "application/json"},
    //     //     {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
    //     //     //{"Content-type", "charset=utf-8"}
    //     // }, false, false, progress);
    //     Debug.Log("PutPasport request done " + responce.downloadHandler.text);
    // }
    
    // public async void PutFormPasportMultipart(PutPassportRequest passport, int id)
    // {
    //     var requestString = _manager.Config.BaseURL +_manager.Config.GetUser+"/"+id +_manager.Config.VerifyPassport;
    //
    //     List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
    //     //Debug.Log("body = "+ body);
    //     formData.Add(new MultipartFormFileSection("passportPhoto",passport.PassportPhoto , "passportExample.png", "image/png"));
    //     formData.Add(new MultipartFormFileSection("physicianRecPhoto",passport.PhysicianPhoto , "physicianPhotoExample.png", "image/png"));
    //
    //     var request = UnityWebRequest.Post(requestString, formData);
    //     request.method = "PUT";
    //     
    //     //request.SetRequestHeader("Content-Type", "application/json");
    //     request.SetRequestHeader("Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken);
    //     Debug.Log("req = "+ request.url);
    //
    //     var responce = await request.SendWebRequest();
    //     // {
    //     //     {"Content-Type", "application/json"},
    //     //     {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
    //     //     //{"Content-type", "charset=utf-8"}
    //     // }, false, false, progress);
    //     Debug.Log("PutFormPasportMultipart request done " + responce.downloadHandler.text);
    // }
}
