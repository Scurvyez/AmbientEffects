﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AmbientEffects
{
    public class EffectDef : Def
    {
        public Material material = null;
        public string materialPath = null;
        public List<Material> materials = null;
        public List<string> materialPaths = null;
        public AltitudeLayer altitude = AltitudeLayer.VisEffects;
        public int altitudeAdjustment = 0;
        public ShaderTypeDef shaderType = null;
        public List<ShaderParameter> shaderParameters;
        public int count = 1;
        public Vector3 drawSize = Vector3.one;

        public bool HasMaterial => material != null || !materials.NullOrEmpty();

        public Material Material
        {
            get
            {
                if (!materials.NullOrEmpty())
                {
                    return materials.RandomElement();
                }
                return material;
            }
        }

        public EffectDef()
        {
            LongEventHandler.ExecuteWhenFinished(delegate
            {
                Shader shader = shaderType?.Shader ?? ShaderDatabase.TransparentPostLight;
                if (!materialPath.NullOrEmpty())
                {
                    material = MaterialPool.MatFrom(new MaterialRequest(ContentFinder<Texture2D>.Get(materialPath), shader)
                    {
                        shaderParameters = shaderParameters
                    });
                }
                if (!materialPaths.NullOrEmpty())
                {
                    materials = new List<Material>(materialPaths.Count);
                    foreach (string materialPath in materialPaths)
                    {
                        Material material2 = MaterialPool.MatFrom(new MaterialRequest(ContentFinder<Texture2D>.Get(materialPath), shader)
                        {
                            shaderParameters = shaderParameters
                        });
                        materials.Add(material2);
                    }
                }
            });
        }
    }
}
