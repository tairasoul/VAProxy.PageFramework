using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;
using System.Linq;
using FluffyUnderware.DevTools.Extensions;
using VAP_API;
using Invector.vItemManager;
using Object = UnityEngine.Object;

namespace UIWindowPageFramework
{
    public static class ArrayExtensions
    {
        public static T[] Append<T>(this T[] array, T value) where T : struct
        {
            return array.AddToArray(value);
        }
    }
    public static class GameObjectExtensions
    {
        public static GameObject Find(this GameObject @object, string name)
        {
            return @object.transform.Find(name).gameObject;
        }

        public static void SetParent(this GameObject @object, GameObject parent, bool worldPositionStays)
        {
            @object.transform.SetParent(parent.transform, worldPositionStays);
        }

        public static GameObject AddObject(this GameObject @object, string name)
        {
            GameObject obj = new(name);
            obj.SetParent(@object, false);
            return obj;
        }

        public static GameObject Instantiate(this GameObject @object)
        {
            return GameObject.Instantiate(@object);
        }

        public static GameObject[] GetChildren(this GameObject @object)
        {
            int childCount = @object.transform.childCount;
            GameObject[] children = new GameObject[childCount];
            for (int i = 0; i < childCount; i++)
            {
                children[i] = @object.transform.GetChild(i).gameObject;
            }
            return children;
        }
    }
    public class Framework
    {
        public static bool Ready = false;
        /// <summary>
        /// Get a font from the game.
        /// </summary>
        /// <param name="name">Name of font as shown in UnityExplorer.</param>
        public static Font GetFont(string name)
        {
            Object[] fonts = Object.FindObjectsOfTypeAll(typeof(Font));
            foreach (Font font in fonts.Cast<Font>())
            {
                if (font.name == name) return font;
            }
            return null;
        }
        public static GameObject CreateWindow(string name)
        {
            GameObject window = new(name);
            int UILayer = LayerMask.NameToLayer("UI");
            window.layer = UILayer;
            window.AddComponent<CanvasRenderer>();
            window.AddComponent<RectTransform>();
            //window.AddComponent<UIWindowPage>();
            /*FieldInfo _windowParentField = typeof(UIWindowPage).GetField("_windowParent", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo _rectField = typeof(UIWindowPage).GetField("_rectTransform", BindingFlags.Instance | BindingFlags.NonPublic);
            _rectField?.SetValue(window.GetComponent<UIWindowPage>(), window.GetComponent<RectTransform>());
            _windowParentField?.SetValue(window.GetComponent<UIWindowPage>(), Parent);*/
            GameObject Header = window.AddObject("Header");
            Header.layer = UILayer;
            Header.AddComponent<LayoutElement>();
            Header.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 251.9998f, 99.9995f);
            Header.GetComponent<RectTransform>().sizeDelta = new Vector2(1400, -406);
            Header.transform.localScale = new Vector3(1.12f, 1.12f, 1.12f);
            GameObject Sep = Header.AddObject("Sep");
            Sep.layer = UILayer;
            RectTransform sep = Sep.AddComponent<RectTransform>();
            sep.anchoredPosition = new Vector2(0, 270.5001f);
            sep.sizeDelta = new Vector2(50, 1.5f);
            sep.anchorMax = new Vector2(1, 1);
            sep.anchorMin = new Vector2(0, 1);
            Sep.AddComponent<CanvasRenderer>();
            Sep.AddComponent<Image>().color = new Color(1, 1, 1, 0.0588f);
            GameObject Title = Header.AddObject("Title");
            Title.layer = UILayer;
            RectTransform TitleTransform = Title.AddComponent<RectTransform>();
            TitleTransform.anchoredPosition = new Vector2(161, 304.5f);
            TitleTransform.sizeDelta = new Vector2(322, 42);
            TitleTransform.anchorMax = new Vector2(0, 1);
            TitleTransform.anchorMin = new Vector2(0, 1);
            Title.AddComponent<CanvasRenderer>();
            Text TitleText = Title.AddComponent<Text>();
            TitleText.font = GetFont("Orbitron-Bold");
            TitleText.alignment = TextAnchor.MiddleLeft;
            TitleText.horizontalOverflow = HorizontalWrapMode.Overflow;
            TitleText.fontSize = 36;
            TitleText.text = name;
            GameObject TopRow = Title.AddObject("Top_Row");
            TopRow.layer = UILayer;
            RectTransform TopRowTransform = TopRow.AddComponent<RectTransform>();
            TopRowTransform.anchoredPosition = new Vector2(-250, 183.3f);
            TopRowTransform.anchorMax = new Vector2(0, 1);
            TopRowTransform.anchorMin = new Vector2(0, 1);
            TopRowTransform.pivot = new Vector2(0.5f, 1);
            GameObject JoystickLB = TopRow.AddObject("JoystickLB");
            JoystickLB.layer = UILayer;
            RectTransform LBTransform = JoystickLB.AddComponent<RectTransform>();
            LBTransform.anchoredPosition = new Vector2(268.7993f, -76.9998f);
            LBTransform.localScale = new Vector3(1, 1, 1);
            LBTransform.sizeDelta = new Vector2(40, 30);
            JoystickLB.AddComponent<CanvasRenderer>();
            GameObject JoystickPrev = JoystickLB.AddObject("Text");
            JoystickPrev.layer = UILayer;
            JoystickPrev.AddComponent<CanvasRenderer>();
            RectTransform Prev = JoystickPrev.AddComponent<RectTransform>();
            Prev.anchoredPosition = new Vector2(158.1f, 0);
            Prev.sizeDelta = new Vector2(134.1f, 30);
            Prev.anchorMax = new Vector2(1, 0.5f);
            Prev.anchorMin = new Vector2(1, 0.5f);
            Prev.pivot = new Vector2(1, 0.5f);
            Text PrevText = JoystickPrev.AddComponent<Text>();
            PrevText.alignment = TextAnchor.MiddleLeft;
            PrevText.font = GetFont("Orbitron-Regular");
            PrevText.fontSize = 13;
            PrevText.text = "Prev";
            GameObject LBImgObj = JoystickLB.AddObject("LB");
            LBImgObj.layer = UILayer;
            RectTransform LBImgTransform = LBImgObj.AddComponent<RectTransform>();
            LBImgTransform.anchoredPosition = new Vector2(-8, 0);
            LBImgTransform.sizeDelta = new Vector2(70, 70);
            LBImgTransform.localScale = new Vector3(0.8929f, 0.8929f, 0.8929f);
            LBImgObj.AddComponent<CanvasRenderer>();
            Image LBImg = LBImgObj.AddComponent<Image>();
            LBImg.sprite = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/UITextures/UI_LB_Interact_Xbox.png"), new Rect(66.0268f, 64.0268f, 183.9465f, 127.8971f), new Vector2(128, 128));
            LBImg.color = new Color(1, 1, 1, 0.5882f);
            GameObject JoystickRB = JoystickLB.Instantiate();
            Text JoystickText = JoystickRB.Find("Text").GetComponent<Text>();
            JoystickText.text = "Next";
            JoystickText.fontSize = 13;
            JoystickText.alignment = TextAnchor.MiddleLeft;
            JoystickRB.layer = UILayer;
            JoystickRB.name = "JoystickRB";
            JoystickRB.SetParent(TopRow, true);
            JoystickRB.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            JoystickRB.GetComponent<RectTransform>().anchoredPosition = new Vector2(639.001f, -127f);
            RectTransform JoyRBTransform = JoystickRB.Find("Text").GetComponent<RectTransform>();
            JoyRBTransform.anchoredPosition = new Vector2(-99.7776f, 0);
            JoyRBTransform.anchorMax = new Vector2(1, 0.5f);
            JoyRBTransform.anchorMin = new Vector2(0, 0.5f);
            JoyRBTransform.pivot = new Vector2(1, 0.5f);
            GameObject RB = JoystickRB.Find("LB");
            RB.layer = UILayer;
            RB.name = "RB";
            RB.GetComponent<Image>().sprite = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/UITextures/UI_RB_Interact_Xbox.png"), new Rect(8.0761f, 64.0268f, 191.8477f, 127.8971f), new Vector2(128, 128));
            RB.GetComponent<Image>().color = new Color(1, 1, 1, 0.5882f);
            RectTransform RBTransform = RB.GetComponent<RectTransform>();
            RBTransform.anchoredPosition = new Vector2(29.0127f, 4.8f);
            RBTransform.sizeDelta = new Vector2(70, 70);
            GameObject KeyboardQ = JoystickLB.Instantiate();
            Text KeyboardQText = KeyboardQ.Find("Text").GetComponent<Text>();
            KeyboardQText.text = "Prev";
            KeyboardQText.fontSize = 13;
            KeyboardQText.alignment = TextAnchor.MiddleLeft;
            KeyboardQ.layer = UILayer;
            KeyboardQ.name = "KeyboardQ";
            KeyboardQ.SetParent(TopRow, true);
            KeyboardQ.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(268.8f, -77, 0);
            GameObject KeyboardLB = KeyboardQ.Find("LB");
            KeyboardLB.layer = UILayer;
            KeyboardLB.name = "KeyboardQDisplay";
            KeyboardLB.GetComponent<RectTransform>().localScale = new Vector3(0.6325f, 0.6325f, 0.6325f);
            KeyboardLB.GetComponent<Image>().sprite = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/UITextures/q.png"), new Rect(7.0761f, 7.0761f, 241.8477f, 241.8477f), new Vector2(128, 128));
            GameObject KeyboardE = JoystickLB.Instantiate();
            KeyboardE.Find("Text").GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-69.2811f, 0, 0);
            Text KeyboardText = KeyboardE.Find("Text").GetComponent<Text>();
            KeyboardText.text = "Next";
            KeyboardText.fontSize = 13;
            KeyboardText.alignment = TextAnchor.MiddleLeft;
            KeyboardE.layer = UILayer;
            KeyboardE.name = "KeyboardE";
            KeyboardE.SetParent(TopRow, true);
            KeyboardE.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(639, -77, 0);
            GameObject KeyboardRB = KeyboardE.Find("LB");
            KeyboardRB.layer = UILayer;
            KeyboardRB.name = "KeyboardEDisplay";
            KeyboardRB.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-93.913f, 0, 0);
            KeyboardRB.GetComponent<RectTransform>().localScale = new Vector3(0.6325f, 0.6325f, 0.6325f);
            KeyboardRB.GetComponent<Image>().sprite = Sprite.Create(BundleLoader.GetLoadedAsset<Texture2D>("assets/UITextures/e.png"), new Rect(7.0761f, 7.0761f, 241.8477f, 241.8477f), new Vector2(128, 128));
            JoyRBTransform.anchoredPosition3D = new Vector3(51.1296f, 6.2301f, 0);
            RB.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-52.2477f, 50.001f, 89.2853f);
            GameObject Breadcrumbs = Header.AddObject("Breadcrumbs");
            Breadcrumbs.layer = UILayer;
            RectTransform BreadcrumbTransform = Breadcrumbs.AddComponent<RectTransform>();
            BreadcrumbTransform.anchoredPosition = new Vector2(-539, 137.5f);
            BreadcrumbTransform.sizeDelta = new Vector2(322, 30);
            Breadcrumbs.AddComponent<CanvasRenderer>();
            HorizontalLayoutGroup group = Breadcrumbs.AddComponent<HorizontalLayoutGroup>();
            group.childControlHeight = true;
            group.childControlWidth = true;
            group.spacing = 0;
            group.childAlignment = TextAnchor.MiddleLeft;
            group.childForceExpandHeight = false;
            group.childForceExpandWidth = false;
            GameObject S105Display = Breadcrumbs.AddObject("S105Text");
            S105Display.layer = UILayer;
            RectTransform S105Transform = S105Display.AddComponent<RectTransform>();
            S105Transform.anchoredPosition = new Vector2(29.3125f, -15);
            S105Transform.sizeDelta = new Vector2(58.625f, 11.375f);
            S105Display.AddComponent<CanvasRenderer>();
            Text S105Text = S105Display.AddComponent<Text>();
            S105Text.alignment = TextAnchor.MiddleLeft;
            S105Text.fontSize = 12;
            S105Text.horizontalOverflow = HorizontalWrapMode.Overflow;
            S105Text.text = "S-105 // ";
            S105Text.color = new Color(1, 1, 1, 0.3137f);
            GameObject PageTitle = Breadcrumbs.AddObject("WindowName");
            PageTitle.layer = UILayer;
            PageTitle.AddComponent<CanvasRenderer>();
            RectTransform PageTransform = PageTitle.AddComponent<RectTransform>();
            PageTransform.anchoredPosition = new Vector2(92.3125f, -15);
            PageTransform.sizeDelta = new Vector2(67.375f, 11.375f);
            Text PageText = PageTitle.AddComponent<Text>();
            PageText.alignment = TextAnchor.MiddleLeft;
            PageText.fontSize = 12;
            PageText.horizontalOverflow = HorizontalWrapMode.Overflow;
            PageText.text = name;
            PageText.color = new Color(0.9843f, 0.6902f, 0.2314f, 0.3137f);
            GameObject.DontDestroyOnLoad(window);
            return window;
        }

        public static void RegisterWindow(GameObject window)
        {
            RegisteredWindows.windows = RegisteredWindows.windows.Add(window);
            Plugin.WindowRegistered.Invoke(window);
        }

        public static void UnregisterWindow(GameObject window)
        {
            RegisteredWindows.windows = RegisteredWindows.windows.Remove(window);
        }

        public static bool WindowRegistered(GameObject window)
        {
            return RegisteredWindows.windows.Contains(window);
        }
    }

    internal class RegisteredWindows
    {
        public static GameObject[] windows = [];
    }
}
