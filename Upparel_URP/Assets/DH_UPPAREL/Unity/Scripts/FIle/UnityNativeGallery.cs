using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UnityNativeGallery { 
    // Start is called before the first frame update


    
    public static Texture2D GetImage(int maxByte = 5000000)
    {
        Texture2D texture = null;
        NativeGallery.GetImageFromGallery((file) =>
        {
            FileInfo selected = new FileInfo(file);
            //용량제한 바이트
            if (selected.Length > maxByte)
                return;
            if (!string.IsNullOrEmpty(file))
            {
                texture = Load(file);
            }
        });
        return texture;
    }

    public static Texture2D Load(string path)
    {
        byte[] f_byte = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(f_byte);
        return tex;

    }
}
