using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
[RequireComponent(typeof(Image), typeof(RectTransform))]
[ExecuteInEditMode]
public partial class PixelArtGUIScaler : MonoBehaviour
{
    Image image;
    RectTransform rectTransform;

     const float pixelSize = 3.704f;

    [SerializeField]
    bool unlockSize;


    [ContextMenu("Test Source Size")]
    public void TextSize()
    {
        float width = image.sprite.rect.width;
        float height = image.sprite.rect.height;

        Debug.Log("Width: " + width);
        Debug.Log("Height: " + height);
    }

    void Update()
    {
       
        if (Application.isPlaying) return;

        if (unlockSize)
            return;

        if (image == null)
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
        }

        if (image.sprite == null) return;

        //Get source size values
        float width = image.sprite.rect.width;
        float height = image.sprite.rect.height;

        float worldWidth = width * pixelSize;
        float worldHeight = height * pixelSize;

        rectTransform.sizeDelta = new Vector2(worldWidth ,worldHeight);
    }
}
#endif

public partial class PixelArtGUIScaler : MonoBehaviour
{
    private void Start()
    {
       

        if (unlockSize)
            return;

        if (image == null)
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
        }

        if (image.sprite == null) return;

        //Get source size values
        float width = image.sprite.rect.width;
        float height = image.sprite.rect.height;

        float worldWidth = width * pixelSize;
        float worldHeight = height * pixelSize;

        rectTransform.sizeDelta = new Vector2(worldWidth, worldHeight);
    }
}