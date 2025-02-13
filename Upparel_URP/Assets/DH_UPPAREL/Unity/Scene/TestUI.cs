/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    enum Texts
    {
        Text1,Text2
    }
    //내가 사용할 게임 오브젝트>?
    public void bind<T>(Type type) //T는 내가 가져올 컴포넌트.
    {
        string[] names = Enum.GetNames(type);
        
        for(int j = 0; j < names.Length; j++)
        {
            FindChild(transform, names[j], false);
        }
    }


    public T FindChild<T>(Transform go ,string name,bool recursive = false)
    {
        if (!recursive)
        {
            for (int i = 0; i < go.childCount; i++)
            {
                Transform child = go.GetChild(i);
                if (name == child.gameObject.name)
                {
                    return child;
                }
            }
        }
        else
        {

        }
        return null;
    }
    
}
*/