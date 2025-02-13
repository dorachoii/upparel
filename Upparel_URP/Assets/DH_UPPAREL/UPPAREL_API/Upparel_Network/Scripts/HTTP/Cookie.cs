using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Cookie
{

    public string Key;
    public string Value;

    public DateTime? Expires;
    public string Path; //path를 기준으로 storage에서 검색.
    public string Domain;
    public bool IsExpired;//=> Expires.HasValue && DateTime.Now > Expires.Value;

    public bool HttpOnly;
    public bool Secure;

    public static Cookie Parse(string header,UnityWebRequest webrequest)
    {
        if (string.IsNullOrWhiteSpace(header) || string.IsNullOrEmpty(header))
            return null;
        string[] heders = header.Split(";");
        string[] keyvalue = heders[0].Split("=");

        //공백 제거
        string key = keyvalue[0].Trim();
        string value = keyvalue[1].Trim();
        string domain = webrequest.uri.Host; 
        DateTime? expires = null;
        string path = "";
        bool secure = false;
        bool httpOnly = true;

        for(int i  = 1; i< heders.Length; i++)
        {
            
            string[] values = heders[i].Split("=");
            string str = values[0].Trim();
            switch (str)
            {
                case "Domain":
                    domain = values[1].Trim();
                    break;
                case "Path":
                    path = values[1].Trim();
                    break;
                case "Expires":
                    if (values.Length > 1 && DateTime.TryParse(values[1].Trim(), out DateTime parsedDate))
                    {
                        expires = parsedDate;
                    }
                    break;
                case "Secure":
                    secure = true;
                    break;
                case "HttpOnly":
                    httpOnly = true;
                    break;
                
            }
        }

        Cookie cookie = new Cookie();
        cookie.Key = key;
        cookie.Value = value;
        cookie.Path = path;
        cookie.Expires = expires;
        cookie.HttpOnly = httpOnly;
        cookie.Secure = secure;
        cookie.Domain = domain;
        return cookie;
    }
    public string SerializeCookie(Cookie cookie) {
        return cookie.Key + "=" + cookie.Value + "; ";
    }
}
