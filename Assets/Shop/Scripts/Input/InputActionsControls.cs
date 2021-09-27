// GENERATED AUTOMATICALLY FROM 'Assets/Shop/Scripts/Input/InputActionsControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActionsControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActionsControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionsControls"",
    ""maps"": [
        {
            ""name"": ""User"",
            ""id"": ""b6d2431d-362b-42ba-b801-1b173e90198b"",
            ""actions"": [
                {
                    ""name"": ""TouchInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f4bee1ae-7624-4540-ae73-5f1e5beea8aa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""b89a4c03-ba17-4528-8bf9-8ec499b8579d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""85e759a6-9c76-46a9-a1ca-845f263e0ba8"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1268eeff-25fc-4a7b-868f-2eb71c8707c8"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SelectItem"",
            ""id"": ""e51ef02f-c4f9-4b69-9cac-79c415efefd5"",
            ""actions"": [
                {
                    ""name"": ""SlectItem"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3cf5cdda-bbe7-464d-b080-32bf11796474"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""87f8b43d-a0ea-4a9a-8b32-cea650bd1106"",
                    ""path"": ""<Pointer>/touch"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // User
        m_User = asset.FindActionMap("User", throwIfNotFound: true);
        m_User_TouchInput = m_User.FindAction("TouchInput", throwIfNotFound: true);
        m_User_Tap = m_User.FindAction("Tap", throwIfNotFound: true);
        // SelectItem
        m_SelectItem = asset.FindActionMap("SelectItem", throwIfNotFound: true);
        m_SelectItem_SlectItem = m_SelectItem.FindAction("SlectItem", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // User
    private readonly InputActionMap m_User;
    private IUserActions m_UserActionsCallbackInterface;
    private readonly InputAction m_User_TouchInput;
    private readonly InputAction m_User_Tap;
    public struct UserActions
    {
        private @InputActionsControls m_Wrapper;
        public UserActions(@InputActionsControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchInput => m_Wrapper.m_User_TouchInput;
        public InputAction @Tap => m_Wrapper.m_User_Tap;
        public InputActionMap Get() { return m_Wrapper.m_User; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserActions set) { return set.Get(); }
        public void SetCallbacks(IUserActions instance)
        {
            if (m_Wrapper.m_UserActionsCallbackInterface != null)
            {
                @TouchInput.started -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchInput;
                @TouchInput.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchInput;
                @TouchInput.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnTouchInput;
                @Tap.started -= m_Wrapper.m_UserActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnTap;
            }
            m_Wrapper.m_UserActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchInput.started += instance.OnTouchInput;
                @TouchInput.performed += instance.OnTouchInput;
                @TouchInput.canceled += instance.OnTouchInput;
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
            }
        }
    }
    public UserActions @User => new UserActions(this);

    // SelectItem
    private readonly InputActionMap m_SelectItem;
    private ISelectItemActions m_SelectItemActionsCallbackInterface;
    private readonly InputAction m_SelectItem_SlectItem;
    public struct SelectItemActions
    {
        private @InputActionsControls m_Wrapper;
        public SelectItemActions(@InputActionsControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SlectItem => m_Wrapper.m_SelectItem_SlectItem;
        public InputActionMap Get() { return m_Wrapper.m_SelectItem; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SelectItemActions set) { return set.Get(); }
        public void SetCallbacks(ISelectItemActions instance)
        {
            if (m_Wrapper.m_SelectItemActionsCallbackInterface != null)
            {
                @SlectItem.started -= m_Wrapper.m_SelectItemActionsCallbackInterface.OnSlectItem;
                @SlectItem.performed -= m_Wrapper.m_SelectItemActionsCallbackInterface.OnSlectItem;
                @SlectItem.canceled -= m_Wrapper.m_SelectItemActionsCallbackInterface.OnSlectItem;
            }
            m_Wrapper.m_SelectItemActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SlectItem.started += instance.OnSlectItem;
                @SlectItem.performed += instance.OnSlectItem;
                @SlectItem.canceled += instance.OnSlectItem;
            }
        }
    }
    public SelectItemActions @SelectItem => new SelectItemActions(this);
    public interface IUserActions
    {
        void OnTouchInput(InputAction.CallbackContext context);
        void OnTap(InputAction.CallbackContext context);
    }
    public interface ISelectItemActions
    {
        void OnSlectItem(InputAction.CallbackContext context);
    }
}
