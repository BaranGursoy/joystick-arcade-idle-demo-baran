using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    public Material eraseMaterial;
    public Transform broomTransform;
    public float eraseRadius = 0.1f;
    private Texture2D paintTexture;

    void Start()
    {
        // Initialize the paint texture
        paintTexture = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);
        eraseMaterial.SetTexture("_PaintMap", paintTexture);

        // Fill the paint texture with white (fully opaque)
        Color[] colors = new Color[paintTexture.width * paintTexture.height];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.white;
        }
        paintTexture.SetPixels(colors);
        paintTexture.Apply();
    }

    void Update()
    {
        if (broomTransform != null)
        {
            Vector3 localPos = transform.InverseTransformPoint(broomTransform.position);
            Vector2 uvPos = new Vector2(localPos.x + 0.5f, localPos.y + 0.5f);
            
            PaintTexture(uvPos);
        }
    }

    void PaintTexture(Vector2 uvPos)
    {
        int x = (int)(uvPos.x * paintTexture.width);
        int y = (int)(uvPos.y * paintTexture.height);

        int radius = (int)(eraseRadius * paintTexture.width);
        
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                int px = x + i;
                int py = y + j;

                if (px >= 0 && px < paintTexture.width && py >= 0 && py < paintTexture.height)
                {
                    float distance = Vector2.Distance(new Vector2(px, py), new Vector2(x, y));
                    if (distance <= radius)
                    {
                        Color existingColor = paintTexture.GetPixel(px, py);
                        paintTexture.SetPixel(px, py, new Color(existingColor.r, existingColor.g, existingColor.b, 0)); // Erase by setting alpha to 0
                    }
                }
            }
        }

        paintTexture.Apply();
    }
}