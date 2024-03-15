using HarmonyLib;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using Verse;

namespace AmbientEffects
{
    public class AEMod : Mod
    {
        public static AEMod mod;
        AEModSettings settings;

        public AEMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<AEModSettings>();

            var harmony = new Harmony("com.ambienteffects");

            harmony.Patch(original: AccessTools.PropertyGetter(typeof(ShaderTypeDef), nameof(ShaderTypeDef.Shader)),
                prefix: new HarmonyMethod(typeof(AEMod),
                nameof(ShaderFromAssetBundle)));

            harmony.PatchAll();
        }

        public static void ShaderFromAssetBundle(ShaderTypeDef __instance, ref Shader ___shaderInt)
        {
            if (__instance is AEShaderTypeDef)
            {
                ___shaderInt = AEContentDatabase.AEBundle.LoadAsset<Shader>(__instance.shaderPath);
                if (___shaderInt is null)
                {
                    Log.Message($"[<color=#4494E3FF>Ambient Effects</color>] <color=#e36c45FF>Failed to load Shader from path <text>\"{__instance.shaderPath}\"</text></color>");
                }
            }
        }

        public AssetBundle MainBundle
        {
            get
            {
                string text = "";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    text = "StandaloneOSX";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    text = "StandaloneWindows64";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    text = "StandaloneLinux64";
                }
                string bundlePath = Path.Combine(base.Content.RootDir, "Materials\\Bundles\\" + text + "\\ambienteffectsbundle");
                //Log.Message("[<color=#4494E3FF>Ambient Effects</color>] Bundle Path: " + bundlePath);

                AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);

                if (bundle == null)
                {
                    Log.Message("[<color=#4494E3FF>Ambient Effects</color>] <color=#e36c45FF>Failed to load bundle at path:</color> " + bundlePath);
                }

                foreach (var allAssetName in bundle.GetAllAssetNames())
                {
                    //Log.Message($"[<color=#4494E3FF>Ambient Effects</color>] - [{allAssetName}]");
                }

                return bundle;
            }
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            Listing_Standard list = new Listing_Standard();
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            list.Begin(viewRect);

            // Screen Position Effects Settings
            list.Label("Screen Position Effects Settings");
            list.Gap(3.0f);

            list.CheckboxLabeled("AE_EnableScreenPositionEffects".Translate(), ref settings._EnableScreenPositionEffects, "AE_EnableScreenPositionEffectsDesc".Translate());

            list.End();
        }

        private static void DrawSettingWithTexture(Listing_Standard list, string label, ref bool value, Texture2D texture)
        {
            Rect rect = list.GetRect(24f);
            Widgets.Label(rect.LeftPartPixels(30f), new GUIContent(texture)); // draw our texture
            Widgets.CheckboxLabeled(rect.RightPartPixels(rect.width - 30f), label, ref value); // draw our checkbox with label
        }

        public override string SettingsCategory()
        {
            return "AE_ModName".Translate();
        }
    }

    public class AEModSettings : ModSettings
    {
        private static AEModSettings _instance;
        public bool _EnableScreenPositionEffects = false;
        
        public AEModSettings()
        {
            _instance = this;
        }

        public static bool EnableScreenPositionEffects
        {
            get
            {
                return _instance._EnableScreenPositionEffects;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref _EnableScreenPositionEffects, "_EnableScreenPositionEffects", false);
        }
    }
}
