using System.Collections.Generic;
using Verse;

namespace AmbientEffects
{
    public class MapComponent_AmbientEffects : MapComponent
    {
        private List<Effect> ambientEffectsList = new List<Effect>();

        public MapComponent_AmbientEffects(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            SpawnEffects();
        }

        public override void MapComponentUpdate()
        {
            base.MapComponentUpdate();

            foreach (Effect effect in ambientEffectsList)
            {
                effect.Draw();
            }
        }

        public void AddEffect(Effect effect)
        {
            ambientEffectsList.Add(effect);
        }

        private void SpawnEffects()
        {
            foreach (EffectDef effectDef in DefDatabase<EffectDef>.AllDefsListForReading)
            {
                int effectsToSpawn = effectDef.count;

                //Log.Message($"Spawning {effectsToSpawn} effects for {effectDef}");

                for (int i = 0; i < effectsToSpawn; i++)
                {
                    IntVec3 randomCell = CellFinder.RandomCell(map);
                    Effect_Particle newEffect = new Effect_Particle(effectDef, map, randomCell.ToVector3());

                    //Log.Message($"Spawned effect at position: {randomCell.x}, {randomCell.z}");

                    ambientEffectsList.Add(newEffect);
                }
            }
        }
    }
}
