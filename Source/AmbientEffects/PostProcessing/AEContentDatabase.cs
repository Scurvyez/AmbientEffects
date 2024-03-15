using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Verse;

namespace AmbientEffects
{
    [StaticConstructorOnStartup]
    public static class AEContentDatabase
    {
        private static AssetBundle bundleInt;
        private static Dictionary<string, Shader> lookupShaders;
        private static Dictionary<string, Material> lookupMaterials;
        public static readonly Shader ScreenPositionEffects = LoadShader(Path.Combine("Assets", "ScreenPositionEffects.shader"));

        public static AssetBundle AEBundle
        {
            get
            {
                if (bundleInt == null)
                {
                    bundleInt = AEMod.mod.MainBundle;
                    //Log.Message("[<color=#4494E3FF>Ambient Effects</color>] <color=#e36c45FF>bundleInt:</color> " + bundleInt.name);
                }
                return bundleInt;
            }
        }

        public static Shader LoadShader(string shaderName)
        {
            if (lookupShaders == null)
            {
                lookupShaders = new Dictionary<string, Shader>();
            }
            if (!lookupShaders.ContainsKey(shaderName))
            {
                //Log.Message("[<color=#4494E3FF>Ambient Effects</color>] lookupShaders: " + lookupShaders.ToList().Count);
                lookupShaders[shaderName] = AEBundle.LoadAsset<Shader>(shaderName);
            }
            Shader shader = lookupShaders[shaderName];
            if (shader == null)
            {
                Log.Warning("[<color=#4494E3FF>Ambient Effects</color>] <color=#e36c45FF>Could not load shader:</color> " + shaderName);
                return ShaderDatabase.DefaultShader;
            }
            if (shader != null)
            {
                //Log.Message("[<color=#4494E3FF>Ambient Effects</color>] Loaded shaders: " + lookupShaders.Count );
            }
            return shader;
        }

        public static Material LoadMaterial(string materialName)
        {
            if (lookupMaterials == null)
            {
                lookupMaterials = new Dictionary<string, Material>();
            }
            if (!lookupMaterials.ContainsKey(materialName))
            {
                lookupMaterials[materialName] = AEBundle.LoadAsset<Material>(materialName);
            }
            Material material = lookupMaterials[materialName];
            if (material == null)
            {
                Log.Warning("[<color=#4494E3FF>Ambient Effects</color>] <color=#e36c45FF>Could not load material:</color> " + materialName);
                return BaseContent.BadMat;
            }
            return material;
        }
    }
}
