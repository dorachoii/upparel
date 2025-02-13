using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager
{
    public static bool CreateDirorFile(string path,string fileName = null)
    {
        bool isDirExis = Directory.Exists(path);

        if (!isDirExis)
        {
            Directory.CreateDirectory(path);
            return true;
        }
        if (!string.IsNullOrEmpty(fileName))
        {
            string filepath = Path.Combine(path, fileName);
            if (!File.Exists(filepath))
            {
                File.WriteAllText(filepath, "");
            }
            return true;
        }
        return false;
    }
}
