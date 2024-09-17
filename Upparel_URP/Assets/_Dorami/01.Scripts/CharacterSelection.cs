using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;
    
    public GameObject selectedCharacter;
    public GameObject[] charactersPrefab = new GameObject[6];

    private int selectedCharacterIndex = -1;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 선택된 캐릭터 인덱스를 설정하는 인스턴스 메서드
    public void SetSelectedCharacterIndex(int index)
    {
        selectedCharacterIndex = index;

        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < charactersPrefab.Length)
        {
            selectedCharacter = charactersPrefab[selectedCharacterIndex];
        }
    }

    // Main Scene에서 캐릭터를 Instantiate하는 함수
    public void InstantiateSelectedCharacterInMainScene()
    {
        if (selectedCharacter != null)
        {
            Instantiate(selectedCharacter, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
        }
    }
}
