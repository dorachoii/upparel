using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Image targetImage; // Existing image to display the captured photo

    [SerializeField]
    private Image targetImage2; // Second image to update with the captured photo

    [SerializeField]
    private Image targetImage3; // Third image to update with the captured photo

    public bool IsSessionComplete { get; private set; } = false; // Flag to indicate completion

    // Method to open camera session
    public void OpenCameraSession()
    {
        IsSessionComplete = false; // Reset the flag

        // Check permission
        NativeCamera.Permission permission = NativeCamera.CheckPermission(true);

        if (permission == NativeCamera.Permission.ShouldAsk)
        {
            permission = NativeCamera.RequestPermission(true);
        }

        if (permission == NativeCamera.Permission.Granted)
        {
            NativeCamera.TakePicture((path) =>
            {
                if (path != null)
                {
                    // Load the image from the specified path
                    Texture2D texture = NativeCamera.LoadImageAtPath(path, 417, false);
                    if (texture == null)
                    {
                        Debug.LogError("Couldn't load texture from " + path);
                        IsSessionComplete = true;
                        return;
                    }

                    // Process the texture to 417x417 pixels
                    Texture2D processedTexture = ProcessTexture(texture, 417, 417);

                    // Create a new sprite from the processed texture
                    Sprite newSprite = Sprite.Create(processedTexture, new Rect(0, 0, processedTexture.width, processedTexture.height), new Vector2(0.5f, 0.5f));

                    // Assign the new sprite to the UI Image components
                    targetImage.sprite = newSprite;
                    targetImage2.sprite = newSprite;
                    targetImage3.sprite = newSprite;

                    // Set the UI Image components' size to match the desired size
                    Vector2 desiredSize = new Vector2(417, 417);

                    targetImage.rectTransform.sizeDelta = desiredSize;
                    targetImage2.rectTransform.sizeDelta = desiredSize;
                    targetImage3.rectTransform.sizeDelta = desiredSize;

                    // Disable preserve aspect to fill the UI Images completely
                    targetImage.preserveAspect = false;
                    targetImage2.preserveAspect = false;
                    targetImage3.preserveAspect = false;

                    // Set the flag to indicate completion
                    IsSessionComplete = true;
                }
                else
                {
                    // User canceled the camera
                    IsSessionComplete = true;
                }
            }, maxSize: 417);
        }
        else
        {
            Debug.Log("Camera permission not granted");
            IsSessionComplete = true;
        }
    }

    // Method to crop and resize a texture to the specified width and height
    private Texture2D ProcessTexture(Texture2D sourceTexture, int targetWidth, int targetHeight)
    {
        // First, crop the texture to a square
        int size = Mathf.Min(sourceTexture.width, sourceTexture.height);
        int xOffset = (sourceTexture.width - size) / 2;
        int yOffset = (sourceTexture.height - size) / 2;

        Texture2D croppedTexture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Color[] pixels = sourceTexture.GetPixels(xOffset, yOffset, size, size);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        // Resize the cropped texture to the target size using RenderTexture
        RenderTexture rt = RenderTexture.GetTemporary(targetWidth, targetHeight);
        Graphics.Blit(croppedTexture, rt);

        Texture2D resultTexture = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false);
        RenderTexture.active = rt;
        resultTexture.ReadPixels(new Rect(0, 0, targetWidth, targetHeight), 0, 0);
        resultTexture.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        return resultTexture;
    }
}
