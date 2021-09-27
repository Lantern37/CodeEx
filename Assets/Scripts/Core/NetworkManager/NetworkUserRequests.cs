using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Engenious.Core.Managers.Requests;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Engenious.Core.Managers
{
    public interface INetworkUserRequests
    {
        Task GuestSingIn(Action<float> progress = null);
        Task<LoginUserResponce> LoginUser(LoginUserRequest user, Action<float> progress=null);
        Task<GetUserIdResponse> GetUser(int userId, Action<float> progress = null);
        Task<string> PutUser(PutUserRequest user, Action<float> progress=null);
        Task<string> PutPhoneNumber(VerifyPhoneRequest phone, int userId ,Action<float> progress=null);
        Task<string> PutPassport(PutPassportRequest passport, int id, Action<float> progress = null);
        Task<string> PutPassportMultipart(PutPassportRequest passport, int id, Action<float> progress = null);
        Task<string> PutToken(PutTokenRequest firebaseToken, int id, Action<float> progress = null);

        Task<string> PutPassportMultipartByte(PutPassportRequest passport, int id,
            Action<float> progress = null);
    }

    public class NetworkUserRequests : INetworkUserRequests
    {
        private NetworkManager _manager;

        public NetworkUserRequests(){}
        public NetworkUserRequests(NetworkManager manager)
        {
            _manager = manager;
        }

        public async Task GuestSingIn(Action<float> progress = null)
        {
            //if (singInResult == null)
            {
                // if (_manager.Checker.Type == NetworkType.Disable)
                // {
                //     throw new NetworkRequestException("Internal error", "No internet available ...");
                // }

                var nonce = ((int) ((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds)).ToString();
                
                // Test device
                // var deviceId = "3a743fc0f670270df9a247c22374fb8cb640dc5e";
                
                var deviceId = SystemInfo.deviceUniqueIdentifier;
                var hmac = NetworkHelper.GenerateHMAC(_manager.Config.GuestKey, deviceId, nonce);
                var requestString = _manager.Config.BaseURL + _manager.Config.AuthorizePrefix +
                                    $"guest?device_id={deviceId}&nonce={nonce}&sign={hmac}";
                //singInResult = await Request<SelfResponse>(requestString, RequestTypes.Post, "", null, true,false, progress); 
            }   
        }

        public async Task<GetUserIdResponse> GetUser(int userId, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.GetUser+"/"+userId;
            // var body = JsonConvert.SerializeObject(user);
            var body = "";
            var request = await _manager.Request<GetUserIdResponse>(requestString, 
                NetworkManager.RequestTypes.Get, body, 
                new Dictionary<string, string>
                {
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken},  // Added by Alex
                }, false, false, progress);
            return request;
        }

        public async Task<LoginUserResponce> LoginUser(LoginUserRequest user, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.PostLogin;
            var body = JsonConvert.SerializeObject(user);
            //Debug.Log("Body Login " + body);
            
            //var bodyString = "{\"email\":\"myTest@eng.ua\",\"password\":\"test\"}";
            //Debug.Log("body login= "+ body.ToString());
            
            
            var request = await _manager.Request<LoginUserResponce>(requestString, 
                NetworkManager.RequestTypes.Post, 
                body, 
                new Dictionary<string, string>
                {
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"Accept", "application/json"},
                    {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                }, false, false, progress);

            
            if (request != null)
            {
                _manager.AccessTokenHolder.CurrentToken = request.AccessToken;
            }
            
            return request;
        }

        public async Task<string> PutUser(PutUserRequest user, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.PutUser;
            var body = JsonConvert.SerializeObject(user);
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(body);
            string utf555 = Encoding.UTF8.GetString(utf8Bytes);
            Debug.Log("body = "+ body);
            var request = await _manager.Request<string>(requestString, NetworkManager.RequestTypes.Post, body, new Dictionary<string, string>
            {
                {"Content-Type", "application/json"},
                {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                //{"Content-type", "charset=utf-8"}
            }, false, false, progress);
            
            Debug.Log("PutUser request done ");

            return request;
        }
        
        public async Task<string> PutPhoneNumber(VerifyPhoneRequest phone, int id, Action<float> progress = null)
        {
            Debug.Log("PutPhoneNumber = " + phone.Phone + " id = " + id);
            var requestString = _manager.Config.BaseURL +_manager.Config.GetUser+"/"+id +_manager.Config.VerifyPhone;
            var body = JsonConvert.SerializeObject(phone);
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(body);
            string utf555 = Encoding.UTF8.GetString(utf8Bytes);
            //Debug.Log("body = "+ body);
            var request = await _manager.Request<string>(requestString, NetworkManager.RequestTypes.Put, body, new Dictionary<string, string>
            {
                {"Content-Type", "application/json"},
                {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                //{"Content-type", "charset=utf-8"}
            }, false, false, progress);
            
            Debug.Log("PutPhone request done ");

            return request;
        }
        
        public async Task<string> PutPassport(PutPassportRequest passport, int id, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL +_manager.Config.GetUser+"/"+id +_manager.Config.VerifyPassport;
            var body = JsonConvert.SerializeObject(passport);
            // byte[] utf8Bytes = Encoding.UTF8.GetBytes(body);
            // string utf555 = Encoding.UTF8.GetString(utf8Bytes);
            Debug.Log("body = "+ body);
            var request = await _manager.Request<string>(requestString, NetworkManager.RequestTypes.Put, body, new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken}
                //{"Content-type", "charset=utf-8"}
            }, false, false, progress);
            
            Debug.Log("PutPasport request done ");

            return request;
        }

        public async Task<string> PutPassportMultipartByte(PutPassportRequest passport, int id,
            Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.GetUser + "/" + id +
                                _manager.Config.VerifyPassport;

            WWWForm form = new WWWForm();

            // if (passport.PassportPhoto != null)
            // {
                //string extension = passport.PassportPhotoData.Extension;
                form.AddBinaryData("passportPhoto", passport.PassportPhoto);
                //}

            // if (passport.PhysicianPhotoData.ImageData != null)
            // {
            //     string extension = passport.PhysicianPhotoData.Extension;
            //
            //     form.AddBinaryData("physicianRecPhoto", passport.PhysicianPhotoData.ImageData,
            //         "physicianRecPhoto.png", "image/png");
            // }


            UnityWebRequest req = UnityWebRequest.Post(requestString, form);

            req.SetRequestHeader("Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken);
            //req.SetRequestHeader("Content-Type", "multipart/form-data");

            req.method = "PUT";

            //Debug.Log("Form " + form.data);

            var response = await _manager.GetResponse(req);
            
            Debug.Log("Form " + response.downloadHandler.text);

            return response.downloadHandler.text;
        }

        public async Task<string> PutPassportMultipart(PutPassportRequest passport, int id,
            Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL + _manager.Config.GetUser + "/" + id +
                                _manager.Config.VerifyPassport;

            WWWForm form = new WWWForm();

            if (passport.PassportPhoto != null)
            {
                form.AddBinaryData("passportPhoto", passport.PassportPhoto);
            }
            
            if (passport.PhysicianPhoto != null)
            {
                form.AddBinaryData("physicianRecPhoto", passport.PhysicianPhoto);
            }
           
            UnityWebRequest req = UnityWebRequest.Post(requestString, form);

            req.SetRequestHeader("Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken);
            //req.SetRequestHeader("Content-Type", "multipart/form-data");

            req.method = "PUT";

            //Debug.Log("Form " + form.data);

            var response = await _manager.GetResponse(req);

            Debug.LogError("PASSPORT PutPassportMultipart = " + passport.PassportPhoto.Length);

            
            //Debug.Log("PutPasport request done " + response.downloadHandler.text);


            // List<IMultipartFormSection> formMP= new List<IMultipartFormSection>
            // {
            //     new MultipartFormFileSection("passportPhoto", passport.PassportPhoto, "passportPhoto" + ".jpeg", "image/*")
            // };

            // byte[] boundary = UnityWebRequest.GenerateBoundary();
            // byte[] formSections = UnityWebRequest.SerializeFormSections(formMP, boundary);
            // // my termination string consisting of CRLF--{boundary}--
            // byte[] terminate = Encoding.UTF8.GetBytes(String.Concat("\r\n--", Encoding.UTF8.GetString(boundary), "--"));
            // // Make my complete body from the two byte arrays
            // byte[] body = new byte[formSections.Length + terminate.Length];
            // Buffer.BlockCopy(formSections, 0, body, 0, formSections.Length);
            // Buffer.BlockCopy(terminate, 0, body, formSections.Length, terminate.Length);
            // // Set the content type - NO QUOTES around the boundary
            // string contentType = String.Concat("multipart/form-data; boundary=", Encoding.UTF8.GetString(boundary));
            // // Make my request object and add the raw body. Set anything else you need here
            // UnityWebRequest wr = new UnityWebRequest(requestString);
            //
            // wr.SetRequestHeader("Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken);
            // //wr.SetRequestHeader("Content-Type", "multipart/form-data");
            //
            // wr.method = "PUT";
            //     
            // UploadHandler uploader = new UploadHandlerRaw(body);
            // uploader.contentType = contentType;
            // wr.uploadHandler = uploader;
            //
            // var response = await _manager.GetResponse(wr);


            // byte[] boundary = UnityWebRequest.GenerateBoundary();
            // //serialize form fields into byte[] => requires a bounday to put in between fields
            // byte[] formSections = UnityWebRequest.SerializeFormSections(formMP, boundary);
            //
            // UnityWebRequest request = UnityWebRequest.Post(requestString, formMP);
            // request.uploadHandler = new UploadHandlerRaw(formSections);
            //
            // /*note: adding the boundary to the uploadHandler.contentType is essential! It won't have the boundary otherwise and won't know how to split up the fields; also it must be encoded to a string otherwise it just says prints the type, 'byte[]', which is not what you want*/
            //
            // request.uploadHandler.contentType = "multipart/form-data; boundary=\"" + System.Text.Encoding.UTF8.GetString(boundary) + "\"";
            // request.downloadHandler = new DownloadHandlerBuffer();
            // request.method = "PUT";
            // request.SetRequestHeader("Content-Type", "multipart/form-data");
            // request.SetRequestHeader("Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken);
            //
            // var response = await _manager.GetResponse(request);
            //Debug.Log("PutPasport request done " + response.downloadHandler.text);

            return response.downloadHandler.text;
        }

        public async Task<string> PutToken(PutTokenRequest firebaseToken, int id, Action<float> progress = null)
        {
            var requestString = _manager.Config.BaseURL +_manager.Config.GetUser+"/"+id +_manager.Config.PutToken;
            var body = JsonConvert.SerializeObject(firebaseToken);
            // byte[] utf8Bytes = Encoding.UTF8.GetBytes(body);
            // string utf555 = Encoding.UTF8.GetString(utf8Bytes);
            Debug.Log("body = "+ body);
            var request = await _manager.Request<string>(requestString, NetworkManager.RequestTypes.Put, body, new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + _manager.AccessTokenHolder.CurrentToken},
                {"Content-Type", "application/json"}
            }, false, false, progress);
            
            UnityEngine.Debug.LogError("SendLoginData: " + firebaseToken.FirebaseToken);

            Debug.Log("PutToken request done ");

            return request;
        }
    }
}