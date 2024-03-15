using UnityEngine;
using Verse;

namespace AmbientEffects
{
    [StaticConstructorOnStartup]
    public static class Assets
    {
        public static readonly Texture2D FireflyTex = ContentFinder<Texture2D>.Get("AmbientEffects/Effects/Firefly");
        public static readonly Texture2D TESTINGTex = ContentFinder<Texture2D>.Get("AmbientEffects/Effects/TESTING");
    }
}
