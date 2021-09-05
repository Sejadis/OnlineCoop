using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.Input
{
    public static class InputManager
    {
        //TODO expose keybinds directly
        public static PlayerInput PlayerInput { get; private set; } //TODO private?

        public static event Action<InputAction.CallbackContext> OnCore1;
        public static event Action<InputAction.CallbackContext> OnCore2;

        public static event Action<InputAction.CallbackContext> OnCore3;
        // public Action<InputAction.CallbackContext> OnWeaponBase;
        // public Action<InputAction.CallbackContext> OnWeaponSpecial;

        public static event Action<InputAction.CallbackContext> OnMovement;
        public static event Action<InputAction.CallbackContext> OnLook;
        public static event Action<InputAction.CallbackContext> OnZoom;
        public static event Action<InputAction.CallbackContext> OnJump;

        public static event Action<InputAction.CallbackContext> OnSprint;

        // public static event Action<InputAction.CallbackContext> OnInteraction;

        public static event Action<InputAction.CallbackContext> OnBackUI;
        public static event Action<InputAction.CallbackContext> OnInventoryUI;
        public static event Action<InputAction.CallbackContext> OnCraftingUI;
        public static event Action<InputAction.CallbackContext> OnAbilityUI;
        public static event Action<InputAction.CallbackContext> OnGameHudUI;

        // Start is called before the first frame update
        static InputManager()
        {
            Application.targetFrameRate = 120;
            PlayerInput = new PlayerInput();

            HookUpControls();
            PlayerInput.Controls.Enable();

            HookUpAbilities();
            PlayerInput.Abilities.Enable();

            HookUpUI();
            PlayerInput.UI.Enable();
        }

        private static void HookUpControls()
        {
            PlayerInput.Controls.Movement.started += ctx => OnMovement?.Invoke(ctx);
            PlayerInput.Controls.Movement.performed += ctx => OnMovement?.Invoke(ctx);
            PlayerInput.Controls.Movement.canceled += ctx => OnMovement?.Invoke(ctx);

            // PlayerInput.Controls.Interaction.performed += ctx => OnInteraction?.Invoke(ctx);


            PlayerInput.Controls.Look.started += ctx => OnLook?.Invoke(ctx);
            PlayerInput.Controls.Look.performed += ctx => OnLook?.Invoke(ctx);
            PlayerInput.Controls.Look.canceled += ctx => OnLook?.Invoke(ctx);

            // PlayerInput.Controls.Zoom.started += ctx => OnZoom?.Invoke(ctx);
            PlayerInput.Controls.Zoom.performed += ctx => OnZoom?.Invoke(ctx);
            // PlayerInput.Controls.Zoom.canceled += ctx => OnZoom?.Invoke(ctx);
            //
            // PlayerInput.Controls.Jump.started += ctx => OnJump?.Invoke(ctx);
            PlayerInput.Controls.Jump.performed += ctx => OnJump?.Invoke(ctx);
            // PlayerInput.Controls.Jump.canceled += ctx => OnJump?.Invoke(ctx);

            // PlayerInput.Controls.Jump.started += ctx => OnJump?.Invoke(ctx);
            PlayerInput.Controls.Sprint.performed += ctx => OnSprint?.Invoke(ctx);
            PlayerInput.Controls.Sprint.canceled += ctx => OnSprint?.Invoke(ctx);
        }

        private static void HookUpAbilities()
        {
            // PlayerInput.Abilities.Core1.started += ctx => OnCore1?.Invoke(ctx);
            PlayerInput.Abilities.Core1.performed += ctx => OnCore1?.Invoke(ctx);
            // PlayerInput.Abilities.Core1.canceled += ctx => OnCore1?.Invoke(ctx);
            //
            // PlayerInput.Abilities.Core2.started += ctx => OnCore2?.Invoke(ctx);
            PlayerInput.Abilities.Core2.performed += ctx => OnCore2?.Invoke(ctx);
            // PlayerInput.Abilities.Core2.canceled += ctx => OnCore2?.Invoke(ctx);
            //
            // PlayerInput.Abilities.Core3.started += ctx => OnCore3?.Invoke(ctx);
            PlayerInput.Abilities.Core3.performed += ctx => OnCore3?.Invoke(ctx);
            // PlayerInput.Abilities.Core3.canceled += ctx => OnCore3?.Invoke(ctx);
            //
            // // PlayerInput.Abilities.WeaponBase.started += ctx => OnWeaponBase?.Invoke(ctx);
            // PlayerInput.Abilities.WeaponBase.performed += ctx => OnWeaponBase?.Invoke(ctx);
            // // PlayerInput.Abilities.WeaponBase.canceled += ctx => OnWeaponBase?.Invoke(ctx);
            //
            // // PlayerInput.Abilities.WeaponSpecial.started += ctx => OnWeaponSpecial?.Invoke(ctx);
            // PlayerInput.Abilities.WeaponSpecial.performed += ctx => OnWeaponSpecial?.Invoke(ctx);
            // // PlayerInput.Abilities.WeaponSpecial.canceled += ctx => OnWeaponSpecial?.Invoke(ctx);
        }

        private static void HookUpUI()
        {
            // PlayerInput.UI.Upgrade.started += ctx => OnUpgradeUI?.Invoke(ctx);
            PlayerInput.UI.Ability.performed += ctx => OnAbilityUI?.Invoke(ctx);
            // PlayerInput.UI.Upgrade.canceled += ctx => OnUpgradeUI?.Invoke(ctx);

            // PlayerInput.UI.Upgrade.started += ctx => OnUpgradeUI?.Invoke(ctx);
            PlayerInput.UI.Back.performed += ctx => OnBackUI?.Invoke(ctx);
            // PlayerInput.UI.Upgrade.canceled += ctx => OnUpgradeUI?.Invoke(ctx);

            PlayerInput.UI.Inventory.performed += ctx => OnInventoryUI?.Invoke(ctx);

            PlayerInput.UI.Crafting.performed += ctx => OnCraftingUI?.Invoke(ctx);

            PlayerInput.UI.GameHud.performed += ctx => OnGameHudUI?.Invoke(ctx);
        }
    }
}