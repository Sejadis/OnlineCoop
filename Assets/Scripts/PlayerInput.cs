// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""27e391f9-63ea-460d-bb2a-acb8971ea2fa"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""e73cb7db-5056-47f4-b945-3f8bd6fa6783"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""33cdfae0-50e5-44ac-b9a5-4d4fd39f0b4d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""f299afd6-d159-4e05-a4a4-03e55bf31f01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""0116de4e-0ef1-4557-a8a9-b1454aac8d9e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""eb79613b-5d72-478d-ad4f-ed519faa9725"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""14105a62-bb61-4cd6-8fb6-d7ff103952bb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""abc173ff-d835-4d44-b8b5-27e4293eeb1d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1f5e7933-8da6-4a08-a2f1-0c4630fa7813"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""71c68db2-17f1-4ed8-9e0b-5f3306ad01ba"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d2a10a8b-0da3-4063-b073-aaa80de59b98"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""1bc6bbcd-151e-4dea-ab1e-fbf41eee9918"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""60d14e4f-a9a4-4986-992d-00f7b049bbd0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""88b9c52e-e014-4964-b9db-339ee874b27a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3881d5d8-b664-4d77-865a-ae9b11797158"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3f7ae084-e02f-4fed-9436-469545faa339"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""78f5eaa3-eb7a-42a3-9ab1-09222f373af3"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a490e5ea-5cc9-4621-9c7d-5394f9aa4acb"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c194c32b-3a37-4bf4-8181-daa03666f305"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f71fcead-8dba-4171-96db-f4f048203d85"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-120,max=120),Scale(factor=-1)"",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Abilities"",
            ""id"": ""f7702eb3-c594-48d6-a12c-92fcd5c91a64"",
            ""actions"": [
                {
                    ""name"": ""Core1"",
                    ""type"": ""Button"",
                    ""id"": ""b3dec1c8-8221-4573-a564-33c46163750b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Core2"",
                    ""type"": ""Button"",
                    ""id"": ""9a454de6-7c56-43fe-85f2-ad74e731f09a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Core3"",
                    ""type"": ""Button"",
                    ""id"": ""5523a313-6022-425d-9938-5d3b7c61b552"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73fd9f32-f102-4220-9e3e-dd4d03063edf"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Core1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c1bc4a8-8e9d-44e8-88c3-ca45a63e5676"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Core2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f53a83f6-d6b8-4194-adf6-743e691b6bd8"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Core3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_Movement = m_Controls.FindAction("Movement", throwIfNotFound: true);
        m_Controls_Sprint = m_Controls.FindAction("Sprint", throwIfNotFound: true);
        m_Controls_Interaction = m_Controls.FindAction("Interaction", throwIfNotFound: true);
        m_Controls_Look = m_Controls.FindAction("Look", throwIfNotFound: true);
        m_Controls_Zoom = m_Controls.FindAction("Zoom", throwIfNotFound: true);
        // Abilities
        m_Abilities = asset.FindActionMap("Abilities", throwIfNotFound: true);
        m_Abilities_Core1 = m_Abilities.FindAction("Core1", throwIfNotFound: true);
        m_Abilities_Core2 = m_Abilities.FindAction("Core2", throwIfNotFound: true);
        m_Abilities_Core3 = m_Abilities.FindAction("Core3", throwIfNotFound: true);
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

    // Controls
    private readonly InputActionMap m_Controls;
    private IControlsActions m_ControlsActionsCallbackInterface;
    private readonly InputAction m_Controls_Movement;
    private readonly InputAction m_Controls_Sprint;
    private readonly InputAction m_Controls_Interaction;
    private readonly InputAction m_Controls_Look;
    private readonly InputAction m_Controls_Zoom;
    public struct ControlsActions
    {
        private @PlayerInput m_Wrapper;
        public ControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Controls_Movement;
        public InputAction @Sprint => m_Wrapper.m_Controls_Sprint;
        public InputAction @Interaction => m_Wrapper.m_Controls_Interaction;
        public InputAction @Look => m_Wrapper.m_Controls_Look;
        public InputAction @Zoom => m_Wrapper.m_Controls_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Sprint.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSprint;
                @Interaction.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnInteraction;
                @Look.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnLook;
                @Zoom.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);

    // Abilities
    private readonly InputActionMap m_Abilities;
    private IAbilitiesActions m_AbilitiesActionsCallbackInterface;
    private readonly InputAction m_Abilities_Core1;
    private readonly InputAction m_Abilities_Core2;
    private readonly InputAction m_Abilities_Core3;
    public struct AbilitiesActions
    {
        private @PlayerInput m_Wrapper;
        public AbilitiesActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Core1 => m_Wrapper.m_Abilities_Core1;
        public InputAction @Core2 => m_Wrapper.m_Abilities_Core2;
        public InputAction @Core3 => m_Wrapper.m_Abilities_Core3;
        public InputActionMap Get() { return m_Wrapper.m_Abilities; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AbilitiesActions set) { return set.Get(); }
        public void SetCallbacks(IAbilitiesActions instance)
        {
            if (m_Wrapper.m_AbilitiesActionsCallbackInterface != null)
            {
                @Core1.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore1;
                @Core1.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore1;
                @Core1.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore1;
                @Core2.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore2;
                @Core2.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore2;
                @Core2.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore2;
                @Core3.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore3;
                @Core3.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore3;
                @Core3.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnCore3;
            }
            m_Wrapper.m_AbilitiesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Core1.started += instance.OnCore1;
                @Core1.performed += instance.OnCore1;
                @Core1.canceled += instance.OnCore1;
                @Core2.started += instance.OnCore2;
                @Core2.performed += instance.OnCore2;
                @Core2.canceled += instance.OnCore2;
                @Core3.started += instance.OnCore3;
                @Core3.performed += instance.OnCore3;
                @Core3.canceled += instance.OnCore3;
            }
        }
    }
    public AbilitiesActions @Abilities => new AbilitiesActions(this);
    public interface IControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IAbilitiesActions
    {
        void OnCore1(InputAction.CallbackContext context);
        void OnCore2(InputAction.CallbackContext context);
        void OnCore3(InputAction.CallbackContext context);
    }
}
