using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ARWrapper;

public class ARWrapperTest : MonoBehaviour
{
   // [SerializeField]
   // GameObject m_PlacedPrefab;
   // GameObject spawnedObject;

   // [SerializeField]
   // LightInfoUI lightinfoPanel;

   //[SerializeField]
   // Button startTrackingbtn;
   // [SerializeField]
   // Button stopTrackingbtn;
   // [SerializeField]
   // Button resetTrackingbtn;
   // [SerializeField]
   // Button enablePointCloudbtn;
   // [SerializeField]
   // Button enableFacetrackingbtn;
   // [SerializeField]
   // Button enableDefaultSessionbtn;
   // [SerializeField]
   // Button enableLightEstimation;


   // IARController _arController;


   // bool isPointcloudactive;
   // bool isLightEstimationEnabled;

   // private void OnEnable() {
   //     _arController.OnPlaneRaycasted += PLaneRayCast;
   // }
   // private void OnDisable() {
   //     _arController.OnPlaneRaycasted -= PLaneRayCast;
   // }
   // private void Awake() {
   //     ARFactory<IARController> arFactory = new ARFoundationFactory();

   //     _arController = arFactory.GetArController();
   //     _arController.SetLightEstimationState(true);
   //     SubscribeButtons();
   // }
   // // Start is called before the first frame update
   // void Start()
   // {
       
   // }

   // void SubscribeButtons() {
   //     startTrackingbtn.onClick.AddListener(_arController.StartTracking);
   //     stopTrackingbtn.onClick.AddListener(_arController.StopTracking);
   //     resetTrackingbtn.onClick.AddListener(_arController.ResetTracking);


   //     enablePointCloudbtn.onClick.AddListener(() => {
   //         if(isPointcloudactive) {
   //             _arController.StartPointCloudVisualization();
   //         } else{
   //             _arController.StopPointCloudVisualization();
   //         }
   //         isPointcloudactive = !isPointcloudactive;
   //     });

   //     enableLightEstimation.onClick.AddListener(() => {
   //         _arController.SetLightEstimationState(isLightEstimationEnabled);
   //         if(isLightEstimationEnabled) {
   //             _arController.OnLightDataChanged += RecieveLightData;
   //         } else {
   //             _arController.OnLightDataChanged -= RecieveLightData;
   //         }
   //         isLightEstimationEnabled = !isLightEstimationEnabled;
   //     });


   //     enableFacetrackingbtn.onClick.AddListener(_arController.StartFaceTrackingSession);
   //     enableDefaultSessionbtn.onClick.AddListener(_arController.StartDefaultSession);
   // }
 
   // // Update is called once per frame
   // void Update(){

   // }

   // void RecieveLightData(float? brightness,float? colorTemperature,Color? colorCorrection) {
   //     lightinfoPanel.LightDataChanged(brightness, colorTemperature, colorCorrection);
   // }

   // void PLaneRayCast(Pose hitPose) {
   //     if(spawnedObject == null) {
   //         spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
   //     } else {
   //         spawnedObject.transform.position = hitPose.position;
   //     }
   // }
}
