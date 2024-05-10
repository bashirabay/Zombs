using UnityEngine;
using UnityEngine.UI;

public class MiniMapFogOfWar : MonoBehaviour
{
    public Texture2D mapTexture;
    public RawImage minimapImage;
    public Transform player;
    public float revealRadius = 50f; // Adjust this to control the reveal radius

    private Color32[] originalColors;
    private Color32[] maskedColors;
    private float textureWidth;
    private float textureHeight;

    private void Start()
    {
        textureWidth = mapTexture.width;
        textureHeight = mapTexture.height;

        // Store original colors of the map texture
        originalColors = mapTexture.GetPixels32();

        // Initialize masked colors with black (unrevealed)
        maskedColors = new Color32[originalColors.Length];
        for (int i = 0; i < maskedColors.Length; i++)
        {
            maskedColors[i] = Color.black;
        }

        // Apply the initial texture to the minimap
        UpdateMinimapTexture();
    }

    private void Update()
    {
        // Update the masked areas based on player's position
        UpdateMaskedAreas();
        // Update the minimap texture
        UpdateMinimapTexture();
    }

    private void UpdateMaskedAreas()
    {
        // Calculate the coordinates of the player on the texture
        int playerX = Mathf.RoundToInt((player.position.x - transform.position.x + textureWidth / 2f) * (textureWidth / transform.localScale.x / mapTexture.width));
        int playerY = Mathf.RoundToInt((player.position.y - transform.position.y + textureHeight / 2f) * (textureHeight / transform.localScale.y / mapTexture.height));

        // Iterate through the pixels within the reveal radius
        for (int x = Mathf.Max(0, playerX - Mathf.RoundToInt(revealRadius)); x < Mathf.Min(textureWidth, playerX + Mathf.RoundToInt(revealRadius)); x++)
        {
            for (int y = Mathf.Max(0, playerY - Mathf.RoundToInt(revealRadius)); y < Mathf.Min(textureHeight, playerY + Mathf.RoundToInt(revealRadius)); y++)
            {
                // If the pixel is within the reveal radius, reveal it
                if (Vector2.Distance(new Vector2(x, y), new Vector2(playerX, playerY)) <= revealRadius)
                {
                    int index = y * Mathf.RoundToInt(textureWidth) + x;
                    maskedColors[index] = originalColors[index];
                }
            }
        }
    }

    private void UpdateMinimapTexture()
    {
        // Update the map texture with the masked areas
        mapTexture.SetPixels32(maskedColors);
        mapTexture.Apply();

        // Apply the updated texture to the minimap image
        minimapImage.texture = mapTexture;
    }
}
