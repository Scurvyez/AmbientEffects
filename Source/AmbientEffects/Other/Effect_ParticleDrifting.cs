using UnityEngine;
using Verse;
using Verse.Noise;

namespace AmbientEffects
{
    [StaticConstructorOnStartup]
    public class Effect_ParticleDrifting : Effect_Particle
    {
        private Map thisMap;
        private IntVec3 targetCell;
        private float movementSpeed;
        private int framesPerMovementChange;
        private int currentFrame;
        private float alphaChangeRate;
        private float currentAlpha;

        public Effect_ParticleDrifting(EffectDef def, Map map, Vector3 position) : base(def, map, position)
        {
            thisMap = map;
            movementSpeed = 0.01f; // Adjust the speed as needed (lower value for slower movement)
            framesPerMovementChange = 60;

            // Initialize currentFrame with a random value to add variability among particles
            currentFrame = Rand.Range(0, framesPerMovementChange);

            alphaChangeRate = 0.01f;
            currentAlpha = 1f;

            // Set initial target cell
            targetCell = CellFinder.RandomCell(map);
        }

        public override void Draw()
        {
            base.Draw();
            UpdateMovement();
            UpdateAlpha();
        }

        private void UpdateMovement()
        {
            // Slow down the movement change frequency
            if (currentFrame >= framesPerMovementChange)
            {
                // Select a new random target cell
                targetCell = CellFinder.RandomCell(thisMap);

                // Reset frame counter
                currentFrame = 0;
            }

            // Move toward the target cell
            position = Vector3.Lerp(position, targetCell.ToVector3(), movementSpeed);

            // Increment the frame counter
            currentFrame++;
        }

        private void UpdateAlpha()
        {
            currentAlpha += alphaChangeRate;
            if (currentAlpha > 1f)
            {
                currentAlpha = 1f;
                alphaChangeRate *= -1f;
            }
            else if (currentAlpha < 0f)
            {
                currentAlpha = 0f;
                alphaChangeRate *= -1f;
            }

            if (material != null)
            {
                Color color = material.color;
                color.a = currentAlpha;
                material.color = color;
            }
        }
    }
}
