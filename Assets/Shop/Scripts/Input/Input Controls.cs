// GENERATED AUTOMATICALLY FROM 'Assets/Shop/Scripts/Input/Input Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Controls"",
    ""maps"": [
        {
            ""name"": ""Customer"",
            ""id"": ""834834bb-ee90-49da-adf1-b9191e651bc2"",
            ""actions"": [
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""ec7d607d-5493-48a4-b1a6-c89ba13e9242"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Touch"",
                    ""type"": ""Button"",
                    ""id"": ""811b86c0-bdd8-41df-aaed-e817029ed335"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""17e0e6d4-4637-4fd3-aa4d-5560c92b0368"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""626e76c7-a7fe-4d57-a0f0-7651740b088d"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""User"",
            ""id"": ""de20d9d3-2c00-4909-9cbf-8f7ebd81fd13"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9b1d8c7e-d979-4cd9-a67c-aac71d3c8e63"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""3deea328-51c0-4900-80d4-b7edd96e1a2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Pointer"",
                    ""type"": ""Value"",
                    ""id"": ""cd80c3dc-1e80-4e31-bc67-9090b9b4a734"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a3d28b32-580f-4821-9c35-89f5c4e937ed"",
                    ""path"": ""<Touchscreen>/primaryTouch/startPosition"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04a78a5e-1d1c-4928-a0c8-c538855d5165"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MyScheme"",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c25b042-8886-49c3-b569-ce7fea99cc18"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MyScheme"",
            ""bindingGroup"": ""MyScheme"",
            ""devices"": []
        }
    ]
}");
        // Customer
        m_Customer = asset.FindActionMap("Customer", throwIfNotFound: true);
        m_Customer_Tap = m_Customer.FindAction("Tap", throwIfNotFound: true);
        m_Customer_Touch = m_Customer.FindAction("Touch", throwIfNotFound: true);
        // User
        m_User = asset.FindActionMap("User", throwIfNotFound: true);
        m_User_Select = m_User.FindAction("Select", throwIfNotFound: true);
        m_User_Tap = m_User.FindAction("Tap", throwIfNotFound: true);
        m_User_Pointer = m_User.FindAction("Pointer", throwIfNotFound: true);
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

    // Customer
    private readonly InputActionMap m_Customer;
    private ICustomerActions m_CustomerActionsCallbackInterface;
    private readonly InputAction m_Customer_Tap;
    private readonly InputAction m_Customer_Touch;
    public struct CustomerActions
    {
        private @InputControls m_Wrapper;
        public CustomerActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tap => m_Wrapper.m_Customer_Tap;
        public InputAction @Touch => m_Wrapper.m_Customer_Touch;
        public InputActionMap Get() { return m_Wrapper.m_Customer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CustomerActions set) { return set.Get(); }
        public void SetCallbacks(ICustomerActions instance)
        {
            if (m_Wrapper.m_CustomerActionsCallbackInterface != null)
            {
                @Tap.started -= m_Wrapper.m_CustomerActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_CustomerActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_CustomerActionsCallbackInterface.OnTap;
                @Touch.started -= m_Wrapper.m_CustomerActionsCallbackInterface.OnTouch;
                @Touch.performed -= m_Wrapper.m_CustomerActionsCallbackInterface.OnTouch;
                @Touch.canceled -= m_Wrapper.m_CustomerActionsCallbackInterface.OnTouch;
            }
            m_Wrapper.m_CustomerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
                @Touch.started += instance.OnTouch;
                @Touch.performed += instance.OnTouch;
                @Touch.canceled += instance.OnTouch;
            }
        }
    }
    public CustomerActions @Customer => new CustomerActions(this);

    // User
    private readonly InputActionMap m_User;
    private IUserActions m_UserActionsCallbackInterface;
    private readonly InputAction m_User_Select;
    private readonly InputAction m_User_Tap;
    private readonly InputAction m_User_Pointer;
    public struct UserActions
    {
        private @InputControls m_Wrapper;
        public UserActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_User_Select;
        public InputAction @Tap => m_Wrapper.m_User_Tap;
        public InputAction @Pointer => m_Wrapper.m_User_Pointer;
        public InputActionMap Get() { return m_Wrapper.m_User; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserActions set) { return set.Get(); }
        public void SetCallbacks(IUserActions instance)
        {
            if (m_Wrapper.m_UserActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_UserActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnSelect;
                @Tap.started -= m_Wrapper.m_UserActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnTap;
                @Pointer.started -= m_Wrapper.m_UserActionsCallbackInterface.OnPointer;
                @Pointer.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnPointer;
                @Pointer.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnPointer;
            }
            m_Wrapper.m_UserActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
                @Pointer.started += instance.OnPointer;
                @Pointer.performed += instance.OnPointer;
                @Pointer.canceled += instance.OnPointer;
            }
        }
    }
    public UserActions @User => new UserActions(this);
    private int m_MySchemeSchemeIndex = -1;
    public InputControlScheme MySchemeScheme
    {
        get
        {
            if (m_MySchemeSchemeIndex == -1) m_MySchemeSchemeIndex = asset.FindControlSchemeIndex("MyScheme");
            return asset.controlSchemes[m_MySchemeSchemeIndex];
        }
    }
    public interface ICustomerActions
    {
        void OnTap(InputAction.CallbackContext context);
        void OnTouch(InputAction.CallbackContext context);
    }
    public interface IUserActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnTap(InputAction.CallbackContext context);
        void OnPointer(InputAction.CallbackContext context);
    }
}
