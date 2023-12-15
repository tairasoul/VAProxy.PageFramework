using BepInEx;
using BepInEx.Logging;
using Devdog.General.UI;
using FluffyUnderware.DevTools.Extensions;
using HarmonyLib;
using Invector.vItemManager;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VAP_API;

namespace UIWindowPageFramework
{
    [BepInPlugin("va.proxy.uiwindowpage.framework", "UIWindowPages", "1.1.0")]
    public class Plugin: BaseUnityPlugin
    {
        public static ManualLogSource Log;

        public static Texture2D Spritemap;

        internal static Action<GameObject> WindowRegistered;

        internal static string CurrentScene = "Intro";

        internal void Awake()
        {
            Log = Logger;
            Log.LogInfo("UIWindowPages awake.");
            BundleLoader.LoadComplete += LoadDotSprite;
            GameObject BehaviourHolder = new("UIFramework.Behaviour");
            DontDestroyOnLoad(BehaviourHolder);
            Behaviour behaviour = BehaviourHolder.AddComponent<Behaviour>();
            SceneManager.activeSceneChanged += (Scene oldS, Scene New) =>
            {
                CurrentScene = New.name;
                if (CurrentScene != "Intro" && CurrentScene != "Menu" && GameObject.Find("MAINMENU/Canvas/Pages"))
                {
                    foreach (GameObject RegisteredWindow in RegisteredWindows.Windows.Keys)
                    {
                        MenuArray Pages = GameObject.Find("MAINMENU/Canvas/Pages").GetComponent<MenuArray>();
                        GameObject Instantiated = RegisteredWindow.Instantiate();
                        Instantiated.name = RegisteredWindow.name;
                        Instantiated.SetParent(Pages.gameObject, true);
                        RectTransform WindowTransform = Instantiated.GetComponent<RectTransform>();
                        Text TitleText = Instantiated.Find("Header/Title").GetComponent<Text>();
                        TitleText.font = Framework.GetFont("Orbitron-Bold");
                        GameObject TopRow = Instantiated.Find("Header/Title/Top_Row");
                        Text JoystickPrevText = TopRow.Find("JoystickLB/Text").GetComponent<Text>();
                        JoystickPrevText.font = Framework.GetFont("Orbitron-Regular");
                        Text JoystickNextText = TopRow.Find("JoystickRB/Text").GetComponent<Text>();
                        JoystickNextText.font = Framework.GetFont("Orbitron-Regular");
                        Text KeyboardQText = TopRow.Find("KeyboardQ/Text").GetComponent<Text>();
                        KeyboardQText.font = Framework.GetFont("Orbitron-Regular");
                        Text KeyboardEText = TopRow.Find("KeyboardE/Text").GetComponent<Text>();
                        KeyboardEText.font = Framework.GetFont("Orbitron-Regular");
                        Text S105Text = Instantiated.Find("Header/Breadcrumbs/S105Text").GetComponent<Text>();
                        S105Text.font = Framework.GetFont("Orbitron-Bold");
                        Text PageTitle = Instantiated.Find("Header/Breadcrumbs/WindowName").GetComponent<Text>();
                        PageTitle.font = Framework.GetFont("Orbitron-Bold");
                        GameObject Dots = GameObject.Find("MAINMENU/Canvas/Pages/Setting/Header/Dots").Instantiate();
                        Dots.SetParent(Instantiated.Find("Header"), true);
                        RectTransform DotTransform = Dots.GetComponent<RectTransform>();
                        DotTransform.anchoredPosition3D = new Vector3(74.4008f, 307.4999f, 0);
                        DotTransform.offsetMax = new Vector2(-132.6492f, 308.9999f);
                        DotTransform.offsetMin = new Vector2(281.4508f, 305.9999f);
                        Dots.name = "Dots";
                        vChangeInputTypeTrigger trigger = GameObject.Find("MAINMENU/Canvas/Pages/Setting/Header/Title/Top_Row (1)").GetComponent<vChangeInputTypeTrigger>();
                        trigger.OnChangeToJoystick.AddListener(() =>
                        {
                            TopRow.Find("JoystickLB")?.SetActive(true);
                            TopRow.Find("JoystickRB")?.SetActive(true);
                            TopRow.Find("KeyboardQ")?.SetActive(false);
                            TopRow.Find("KeyboardE")?.SetActive(false);
                        });
                        trigger.OnChangeToKeyboard.AddListener(() =>
                        {
                            TopRow.Find("JoystickLB")?.SetActive(false);
                            TopRow.Find("JoystickRB")?.SetActive(false);
                            TopRow.Find("KeyboardQ")?.SetActive(true);
                            TopRow.Find("KeyboardE")?.SetActive(true);
                        });
                        WindowTransform.localScale = new Vector3(1, 1, 1);
                        WindowTransform.anchorMax = new Vector2(1, 1);
                        WindowTransform.anchorMin = new Vector2(0, 0);
                        WindowTransform.sizeDelta = new Vector2(-280, -80);
                        WindowTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                        Pages.pages = Pages.pages.Add(Instantiated.AddComponent<UIWindowPage>());
                        RegisteredWindows.Windows.GetValueSafe(RegisteredWindow).Invoke(Instantiated);
                    }
                }
            };
            WindowRegistered = (GameObject RegisteredWindow) =>
            {
                if (CurrentScene != "Intro" && CurrentScene != "Menu" && GameObject.Find("MAINMENU/Canvas/Pages"))
                {
                    MenuArray Pages = GameObject.Find("MAINMENU/Canvas/Pages").GetComponent<MenuArray>();
                    GameObject Instantiated = RegisteredWindow.Instantiate();
                    Instantiated.name = RegisteredWindow.name;
                    Instantiated.SetParent(Pages.gameObject, true);
                    RectTransform WindowTransform = Instantiated.GetComponent<RectTransform>();
                    Text TitleText = Instantiated.Find("Header/Title").GetComponent<Text>();
                    TitleText.font = Framework.GetFont("Orbitron-Bold");
                    GameObject TopRow = Instantiated.Find("Header/Title/Top_Row");
                    Text JoystickPrevText = TopRow.Find("JoystickLB/Text").GetComponent<Text>();
                    JoystickPrevText.font = Framework.GetFont("Orbitron-Regular");
                    Text JoystickNextText = TopRow.Find("JoystickRB/Text").GetComponent<Text>();
                    JoystickNextText.font = Framework.GetFont("Orbitron-Regular");
                    Text KeyboardQText = TopRow.Find("KeyboardQ/Text").GetComponent<Text>();
                    KeyboardQText.font = Framework.GetFont("Orbitron-Regular");
                    Text KeyboardEText = TopRow.Find("KeyboardE/Text").GetComponent<Text>();
                    KeyboardEText.font = Framework.GetFont("Orbitron-Regular");
                    Text S105Text = Instantiated.Find("Header/Breadcrumbs/S105Text").GetComponent<Text>();
                    S105Text.font = Framework.GetFont("Orbitron-Bold");
                    Text PageTitle = Instantiated.Find("Header/Breadcrumbs/WindowName").GetComponent<Text>();
                    PageTitle.font = Framework.GetFont("Orbitron-Bold");
                    GameObject Dots = GameObject.Find("MAINMENU/Canvas/Pages/Setting/Header/Dots").Instantiate();
                    Dots.SetParent(Instantiated.Find("Header"), true);
                    Dots.GetComponent<RectTransform>().anchoredPosition = new Vector2(-616.671f, 320.0399f);
                    Dots.name = "Dots";
                    vChangeInputTypeTrigger trigger = GameObject.Find("MAINMENU/Canvas/Pages/Setting/Header/Title/Top_Row (1)").GetComponent<vChangeInputTypeTrigger>();
                    trigger.OnChangeToJoystick.AddListener(() =>
                    {
                        TopRow.Find("JoystickLB")?.SetActive(true);
                        TopRow.Find("JoystickRB")?.SetActive(true);
                        TopRow.Find("KeyboardQ")?.SetActive(false);
                        TopRow.Find("KeyboardE")?.SetActive(false);
                    });
                    trigger.OnChangeToKeyboard.AddListener(() =>
                    {
                        TopRow.Find("JoystickLB")?.SetActive(false);
                        TopRow.Find("JoystickRB")?.SetActive(false);
                        TopRow.Find("KeyboardQ")?.SetActive(true);
                        TopRow.Find("KeyboardE")?.SetActive(true);
                    });
                    WindowTransform.localScale = new Vector3(1, 1, 1);
                    WindowTransform.anchorMax = new Vector2(1, 1);
                    WindowTransform.anchorMax = new Vector2(0, 0);
                    WindowTransform.sizeDelta = new Vector2(-280, 80);
                    WindowTransform.anchoredPosition = new Vector2(420, 236.25f);
                    Pages.pages = Pages.pages.Add(Instantiated.AddComponent<UIWindowPage>());
                    RegisteredWindows.Windows.GetValueSafe(RegisteredWindow).Invoke(Instantiated);
                }
            };
        }
        internal void LoadDotSprite()
        {
            Framework.Ready = true;
            Log.LogInfo("Loading spritemap.");
            Spritemap = BundleLoader.GetLoadedAsset<Texture2D>("assets/UITextures/Spritemap.png");
            Log.LogInfo("Loaded spritemap.");
            BundleLoader.LoadComplete -= LoadDotSprite;
        }
    }
}
