using System.Collections.Generic;
using Client.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIScreen abilityScreen;
        [SerializeField] private UIScreen menuScreen;
        [SerializeField] private UIScreen inventoryScreen;
        [SerializeField] private UIScreen craftingScreen;
        [SerializeField] private UIScreen gameHudScreen;
        private readonly List<UIScreen> activeScreens = new List<UIScreen>();

        public static UIManager Instance { get; private set; }
        public GameHudUI GameHUD => (GameHudUI) gameHudScreen;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("UIManager instance already set, there cant be 2");
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            // ChangeScreenState(abilityScreen, false);
            // ChangeScreenState(menuScreen, false);
            // ChangeScreenState(inventoryScreen, false);
            // ChangeScreenState(craftingScreen, false);
            // ChangeScreenState(gameHudScreen, false);

            InputManager.OnAbilityUI += AbilityUi;
            InputManager.OnBackUI += Back;
            InputManager.OnInventoryUI += InventoryUI;
            InputManager.OnCraftingUI += CraftingUI;
            InputManager.OnGameHudUI += GameHudUI;
        }

        private void GameHudUI(InputAction.CallbackContext obj)
        {
            ChangeScreenState(gameHudScreen, !gameHudScreen.IsActive);
        }

        private void CraftingUI(InputAction.CallbackContext obj)
        {
            ChangeScreenState(craftingScreen, !craftingScreen.IsActive);
        }

        private void InventoryUI(InputAction.CallbackContext obj)
        {
            ChangeScreenState(inventoryScreen, !inventoryScreen.IsActive);
        }

        private void AbilityUi(InputAction.CallbackContext obj)
        {
            ChangeScreenState(abilityScreen, !abilityScreen.IsActive);
        }

        private void Back(InputAction.CallbackContext obj)
        {
            if (activeScreens.Count > 0)
            {
                var screen = activeScreens[activeScreens.Count - 1];
                ChangeScreenState(screen, !screen.IsActive);
            }
            else
            {
                ChangeScreenState(menuScreen, true);
            }
        }

        private void ChangeScreenState(UIScreen screen, bool newState)
        {
            if (screen == null)
            {
                Debug.LogWarning($"Trying to change state of unreferenced UIScreen");
                return;
            }

            if (newState)
            {
                activeScreens.Add(screen);
                screen.Show();
            }
            else
            {
                activeScreens.Remove(screen);
                screen.Hide();
            }

            CheckStuff();
        }

//TODO rename
        private void CheckStuff()
        {
            // var uiClosed = activeScreens.Count == 0;
            // if (uiClosed)
            // {
            //     GameManager.Instance.EnablePlayer();
            // }
            // else
            // {
            //     GameManager.Instance.DisablePlayer();
            // }
        }
    }
}