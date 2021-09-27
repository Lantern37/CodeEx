using System.Collections;
using System.Collections.Generic;
using NotificationSamples;
using UnityEngine;

public class TestNotification : MonoBehaviour
{

    [SerializeField] private GameNotificationsManager _manager;
    
    [SerializeField] private 
    
    void Start()
    {
        
    }

    private void Init()
    {
        GameNotificationChannel chanel =
            new GameNotificationChannel("Test", "Mobile notification", "Test notification");
        _manager.Initialize(chanel);
    }
    
    // private void 
    
    void Update()
    {
        
    }
}
