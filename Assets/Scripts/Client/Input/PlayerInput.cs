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
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b287a8a3-970e-43e2-82f8-3bf7ee705efc"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""3a47a665-95fd-45e4-aca3-266cc9db35a5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
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
                },
                {
                    ""name"": ""WeaponBase"",
                    ""type"": ""Button"",
                    ""id"": ""dedcb89d-8c62-45c5-acc0-fd055613348c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponSpecial"",
                    ""type"": ""Button"",
                    ""id"": ""98f12e0a-a0b9-4fab-9b8a-295f74e0d209"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""97708e8d-dbf8-4af6-962c-8af659468fad"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""3e869f11-cc30-4b17-b9bf-dc274857c2b1"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponBase"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15e02dd8-3b26-4d9d-90a8-cb4b46f06623"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponSpecial"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61b5eed5-c6f7-4683-aa52-b3b83501bdaf"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""3d7bd16a-751d-4543-ba33-5ae03bb9c3ca"",
            ""actions"": [
                {
                    ""name"": ""Ability"",
                    ""type"": ""Button"",
                    ""id"": ""6640834a-3fa9-4fb8-a0a6-90f530856aef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""3fedc954-7981-4fbb-aa4e-c7c767da2c7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""a0e247c3-48ab-455c-9960-0a03b9bdc584"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crafting"",
                    ""type"": ""Button"",
                    ""id"": ""e9f8a6d2-674b-4183-8be2-724dcdece29b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GameHud"",
                    ""type"": ""Value"",
                    ""id"": ""97c6b83b-ab4f-4b50-8148-3979c26ddc7a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""db194101-ddf9-4e0d-9a9d-8f94faf0484d"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b50fe582-daf1-447a-9fff-6abbfc196d55"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d6cdd35-ed38-4ed2-8b0c-a816f2642d29"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9622b702-9bc4-462c-80bc-a68c4b067e97"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crafting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac989b77-4a1a-4316-8495-6e7f050572bf"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GameHud"",
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
        m_Controls_Jump = m_Controls.FindAction("Jump", throwIfNotFound: true);
        // Abilities
        m_Abilities = asset.FindActionMap("Abilities", throwIfNotFound: true);
        m_Abilities_Core1 = m_Abilities.FindAction("Core1", throwIfNotFound: true);
        m_Abilities_Core2 = m_Abilities.FindAction("Core2", throwIfNotFound: true);
        m_Abilities_Core3 = m_Abilities.FindAction("Core3", throwIfNotFound: true);
        m_Abilities_WeaponBase = m_Abilities.FindAction("WeaponBase", throwIfNotFound: true);
        m_Abilities_WeaponSpecial = m_Abilities.FindAction("WeaponSpecial", throwIfNotFound: true);
        m_Abilities_Movement = m_Abilities.FindAction("Movement", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Ability = m_UI.FindAction("Ability", throwIfNotFound: true);
        m_UI_Back = m_UI.FindAction("Back", throwIfNotFound: true);
        m_UI_Inventory = m_UI.FindAction("Inventory", throwIfNotFound: true);
        m_UI_Crafting = m_UI.FindAction("Crafting", throwIfNotFound: true);
        m_UI_GameHud = m_UI.FindAction("GameHud", throwIfNotFound: true);
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
    private readonly InputAction m_Controls_Jump;
    public struct ControlsActions
    {
        private @PlayerInput m_Wrapper;
        public ControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Controls_Movement;
        public InputAction @Sprint => m_Wrapper.m_Controls_Sprint;
        public InputAction @Interaction => m_Wrapper.m_Controls_Interaction;
        public InputAction @Look => m_Wrapper.m_Controls_Look;
        public InputAction @Zoom => m_Wrapper.m_Controls_Zoom;
        public InputAction @Jump => m_Wrapper.m_Controls_Jump;
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
                @Jump.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJump;
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
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
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
    private readonly InputAction m_Abilities_WeaponBase;
    private readonly InputAction m_Abilities_WeaponSpecial;
    private readonly InputAction m_Abilities_Movement;
    public struct AbilitiesActions
    {
        private @PlayerInput m_Wrapper;
        public AbilitiesActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Core1 => m_Wrapper.m_Abilities_Core1;
        public InputAction @Core2 => m_Wrapper.m_Abilities_Core2;
        public InputAction @Core3 => m_Wrapper.m_Abilities_Core3;
        public InputAction @WeaponBase => m_Wrapper.m_Abilities_WeaponBase;
        public InputAction @WeaponSpecial => m_Wrapper.m_Abilities_WeaponSpecial;
        public InputAction @Movement => m_Wrapper.m_Abilities_Movement;
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
                @WeaponBase.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponBase;
                @WeaponBase.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponBase;
                @WeaponBase.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponBase;
                @WeaponSpecial.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponSpecial;
                @WeaponSpecial.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponSpecial;
                @WeaponSpecial.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnWeaponSpecial;
                @Movement.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnMovement;
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
                @WeaponBase.started += instance.OnWeaponBase;
                @WeaponBase.performed += instance.OnWeaponBase;
                @WeaponBase.canceled += instance.OnWeaponBase;
                @WeaponSpecial.started += instance.OnWeaponSpecial;
                @WeaponSpecial.performed += instance.OnWeaponSpecial;
                @WeaponSpecial.canceled += instance.OnWeaponSpecial;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public AbilitiesActions @Abilities => new AbilitiesActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Ability;
    private readonly InputAction m_UI_Back;
    private readonly InputAction m_UI_Inventory;
    private readonly InputAction m_UI_Crafting;
    private readonly InputAction m_UI_GameHud;
    public struct UIActions
    {
        private @PlayerInput m_Wrapper;
        public UIActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Ability => m_Wrapper.m_UI_Ability;
        public InputAction @Back => m_Wrapper.m_UI_Back;
        public InputAction @Inventory => m_Wrapper.m_UI_Inventory;
        public InputAction @Crafting => m_Wrapper.m_UI_Crafting;
        public InputAction @GameHud => m_Wrapper.m_UI_GameHud;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Ability.started -= m_Wrapper.m_UIActionsCallbackInterface.OnAbility;
                @Ability.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnAbility;
                @Ability.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnAbility;
                @Back.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Inventory.started -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @Crafting.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCrafting;
                @Crafting.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCrafting;
                @Crafting.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCrafting;
                @GameHud.started -= m_Wrapper.m_UIActionsCallbackInterface.OnGameHud;
                @GameHud.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnGameHud;
                @GameHud.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnGameHud;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Ability.started += instance.OnAbility;
                @Ability.performed += instance.OnAbility;
                @Ability.canceled += instance.OnAbility;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Crafting.started += instance.OnCrafting;
                @Crafting.performed += instance.OnCrafting;
                @Crafting.canceled += instance.OnCrafting;
                @GameHud.started += instance.OnGameHud;
                @GameHud.performed += instance.OnGameHud;
                @GameHud.canceled += instance.OnGameHud;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IAbilitiesActions
    {
        void OnCore1(InputAction.CallbackContext context);
        void OnCore2(InputAction.CallbackContext context);
        void OnCore3(InputAction.CallbackContext context);
        void OnWeaponBase(InputAction.CallbackContext context);
        void OnWeaponSpecial(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnAbility(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnCrafting(InputAction.CallbackContext context);
        void OnGameHud(InputAction.CallbackContext context);
    }
}
