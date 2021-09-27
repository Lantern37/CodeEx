// GENERATED AUTOMATICALLY FROM 'Assets/Shop/Scripts/Input/TestInput/TestIput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TestIput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TestIput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TestIput"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""ad52239c-c99f-4f9d-8e49-a66c6aaf9413"",
            ""actions"": [
                {
                    ""name"": ""PrimaryContact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1d34a4d8-5c71-4a8c-be43-3b6908d18539"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PrimaryPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""474a4b3e-ee5b-4aac-be7d-5e6c3169df51"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b80c02cf-4041-40fd-a83f-4f752f1591ac"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""097869cb-b04c-4392-8290-6f83e3ba1129"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Rotate"",
            ""id"": ""ee0d6dbd-9e1f-4705-af33-64c6a998927b"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""b877f611-989f-4523-b1eb-60f278f9cb1c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""InvertVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0ddb0f93-cb9d-4c12-be01-44b284203efb"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_PrimaryContact = m_Touch.FindAction("PrimaryContact", throwIfNotFound: true);
        m_Touch_PrimaryPosition = m_Touch.FindAction("PrimaryPosition", throwIfNotFound: true);
        // Rotate
        m_Rotate = asset.FindActionMap("Rotate", throwIfNotFound: true);
        m_Rotate_Rotate = m_Rotate.FindAction("Rotate", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_PrimaryContact;
    private readonly InputAction m_Touch_PrimaryPosition;
    public struct TouchActions
    {
        private @TestIput m_Wrapper;
        public TouchActions(@TestIput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryContact => m_Wrapper.m_Touch_PrimaryContact;
        public InputAction @PrimaryPosition => m_Wrapper.m_Touch_PrimaryPosition;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @PrimaryContact.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContact;
                @PrimaryContact.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContact;
                @PrimaryContact.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContact;
                @PrimaryPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryContact.started += instance.OnPrimaryContact;
                @PrimaryContact.performed += instance.OnPrimaryContact;
                @PrimaryContact.canceled += instance.OnPrimaryContact;
                @PrimaryPosition.started += instance.OnPrimaryPosition;
                @PrimaryPosition.performed += instance.OnPrimaryPosition;
                @PrimaryPosition.canceled += instance.OnPrimaryPosition;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);

    // Rotate
    private readonly InputActionMap m_Rotate;
    private IRotateActions m_RotateActionsCallbackInterface;
    private readonly InputAction m_Rotate_Rotate;
    public struct RotateActions
    {
        private @TestIput m_Wrapper;
        public RotateActions(@TestIput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Rotate_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_Rotate; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RotateActions set) { return set.Get(); }
        public void SetCallbacks(IRotateActions instance)
        {
            if (m_Wrapper.m_RotateActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_RotateActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_RotateActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_RotateActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_RotateActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public RotateActions @Rotate => new RotateActions(this);
    public interface ITouchActions
    {
        void OnPrimaryContact(InputAction.CallbackContext context);
        void OnPrimaryPosition(InputAction.CallbackContext context);
    }
    public interface IRotateActions
    {
        void OnRotate(InputAction.CallbackContext context);
    }
}
