using UnityEngine;
using Verse;

namespace AmbientEffects
{
    [StaticConstructorOnStartup]
    public class Effect_Particle : Effect
    {
        public Effect_Particle(EffectDef def, Map map, Vector3 position) : base(def, map, position)
        {

        }

        protected override void Initialize(EffectDef def, Map map, Vector3 position)
        {
            base.Initialize(def, map, position);

            if (def.material != null)
            {
                material = def.material;
            }
            else if (def.HasMaterial)
            {
                material = def.Material;
            }
            else
            {
                Log.Warning($"Effect_Particle.Initialize() - No valid material found for EffectDef: {def.defName}");
            }

            Log.Message($"Effect_Particle.Initialize() - material: {material}, mesh: {mesh}, position: {position}, drawSize: {drawSize}");
        }

        protected override void DrawInternal()
        {
            //Log.Message($"DrawInternal() call reached!");
            if (material != null)
            {
                Matrix4x4 matrix = Matrix4x4.TRS(position, Quaternion.identity, drawSize);
                Graphics.DrawMesh(mesh, matrix, material, 0, null, 0, null, false, false, false);
            }
        }
    }
}
