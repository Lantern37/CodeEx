// #if APPLE
// using UnityEngine;
// using UnityEditor;
// using UnityEditor.Callbacks;
// using UnityEditor.iOS.Xcode;
//  
// public class EntitlementsPostProcess : ScriptableObject
// {
//     public DefaultAsset m_entitlementsFile;
//  
//     [PostProcessBuild]
//     public static void OnPostProcess(BuildTarget buildTarget, string buildPath)
//     {
//         if (buildTarget != BuildTarget.iOS)
//             return;
//  
//         var dummy = CreateInstance<EntitlementsPostProcess>();
//         var file = dummy.m_entitlementsFile;
//         DestroyImmediate(dummy);
//         if (file == null)
//             return;
//              
//         var proj_path = PBXProject.GetPBXProjectPath(buildPath);
//         var proj = new PBXProject();
//  
//         var unityFrameworkGUID = proj.GetUnityFrameworkTargetGuid();
//  
//         proj.ReadFromFile(proj_path);      
//         var target_name = proj.GetUnityMainTargetGuid();
//  
//         // NEW
//         //Set the entitlements file name to what you want but make sure it has this extension
//         string entitlementsFileName = "my_app.entitlements";
//  
//         var entitlements = new ProjectCapabilityManager(proj_path, entitlementsFileName, "Unity-iPhone", target_name);          
// entitlements.AddPushNotifications(true);
//      
//         //Apply
//         entitlements.WriteToFile();
//     }
// }
// #endif