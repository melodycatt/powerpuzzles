using UnityEngine;

namespace Assets.Pixelation.Scripts
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/Color Adjustments/Pixelation")]
    public class Pixelation : ImageEffectBase
    {
        [Range(64.0f, 1024f)] public float BlockCount = 256;
        public int operation = 0;

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            float k = Camera.main.aspect;
            Vector2 count = new Vector2(BlockCount, BlockCount/k);
            // (256, 160)
            Vector2 size = new Vector2(1.0f/count.x, 1.0f/count.y);
            // (0.00390625, 0.00625)
            material.SetVector("BlockCount", count);
            material.SetVector("BlockSize", size);
            material.SetInt("operation", operation);
            Graphics.Blit(source, destination, material);
        }
    }
}