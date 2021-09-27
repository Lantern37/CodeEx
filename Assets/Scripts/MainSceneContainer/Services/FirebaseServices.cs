using System.Threading.Tasks;
using Firebase;

namespace Engenious.MainScene.Services
{
    public interface IFirebaseServices
    {
        bool IsInitialized { get; }
        Task Initialization();
    }

    public class FirebaseServices : IFirebaseServices
    {
        public bool IsInitialized { get; private set; }
        
        public Task Initialization()
        {
            if(IsInitialized)
                return null;
                
            return Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    var app = Firebase.FirebaseApp.DefaultInstance;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                } else {
                    UnityEngine.Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
    }
}