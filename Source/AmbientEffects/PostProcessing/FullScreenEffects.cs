using UnityEngine;

namespace AmbientEffects
{
    public class FullScreenEffects : MonoBehaviour
    {
        public Material screenPositionEffectsMat;
        public static FullScreenEffects instance;

        public void Start()
        {
            instance = this;
            screenPositionEffectsMat = new Material(AEContentDatabase.ScreenPositionEffects);
        }

        public void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (AEModSettings.EnableScreenPositionEffects)
            {
                Graphics.Blit(source, destination, screenPositionEffectsMat);
            }
            else
            {
                Graphics.Blit(source, destination);
            }
        }
    }
}
