using UnityEngine;
using Verse;

namespace AmbientEffects.PostProcessing
{
    public class MapComponent_ScreenPositionEffects : MapComponent
    {
        FullScreenEffects fullScreenEffects = FullScreenEffects.instance;

        private Pawn trackedPawn;
        private Vector3 trackedPosition;
        
        public MapComponent_ScreenPositionEffects(Map map) : base(map) { }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            trackedPawn = map.mapPawns.AllPawnsSpawned.RandomElement();
            fullScreenEffects.screenPositionEffectsMat.SetTexture("_EffectTexture", Assets.TESTINGTex);
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();

            if (AEModSettings.EnableScreenPositionEffects)
            {
                trackedPosition = trackedPawn.DrawPos;
                fullScreenEffects.screenPositionEffectsMat.SetVector("_TrackedPosition", trackedPosition);
            }
        }
    }
}
