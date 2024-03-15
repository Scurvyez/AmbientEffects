using UnityEngine;
using Verse;

namespace AmbientEffects
{
    public abstract class Effect
    {
        protected Material material;
        protected Mesh mesh = MeshPool.plane10;
        protected Vector3 drawSize;
        protected Vector3 position;

        public Effect(EffectDef def, Map map, Vector3 position)
        {
            this.Initialize(def, map, position);
        }

        protected virtual void Initialize(EffectDef def, Map map, Vector3 position)
        {
            this.material = def.material;
            this.drawSize = def.drawSize;
            this.position = position;
        }

        public virtual void Draw()
        {
            DrawInternal();
        }

        protected abstract void DrawInternal();
    }
}
