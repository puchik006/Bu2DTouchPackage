using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    private static ImageLoader instance;

    public static ImageLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ImageLoader").AddComponent<ImageLoader>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadImage(string imagePath, Image targetImage)
    {
        StartCoroutine(LoadImageCoroutine(imagePath, targetImage));
    }

    private IEnumerator LoadImageCoroutine(string imagePath, Image targetImage)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imagePath))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                DownloadHandlerTexture handler = (DownloadHandlerTexture)www.downloadHandler;
                Texture2D texture = handler.texture;

                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                targetImage.sprite = sprite;
            }
            else
            {
                Debug.Log("Failed to load image: " + www.error);
            }
        }
    }

    public void ReleaseImage(Image targetImage)
    {
        targetImage.sprite = null;
    }
}