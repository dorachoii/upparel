using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageContainer : MonoBehaviour
{
    public string address;
    public Dictionary<string, Page> container = new Dictionary<string, Page>();
    public Transform targetParent;
    public Page GetPage(string address)
    {
        Page page = null;
        if(container.TryGetValue(address,out page)){
            return page;
        }
        Debug.Log("ERROR!");
        return page;
    }
    private void Awake()
    {
        Init();
        PageManager.Get.containers.Add(address, this); //�ڱ� �ڽ��� PageManager�� ����ϱ� 
    }
    private void Start()
    {
        
    }
    //�����̳ʸ� ����
    [SerializeField]
    Page[] pages;
    public void Init()
    {
        if (pages != null&& pages.Length > 0)
        {
            Debug.Log("SS");
            for (int i = 0; i < pages.Length; i++)
            {
                Page page = pages[i];
                if (page != null) {
                    container.Add(page.address, page);
                    if (page.address == "/")
                        PageManager.Get.CurrentPage = page;
                }
                
            }
            return;
        }
        for(int i = 0; i < targetParent.childCount; i++)
        {
            //Debug.Log(targetParent.childCount);
            Transform child = targetParent.GetChild(i);
            Page page = child.gameObject.GetComponent<Page>();
            if (page != null)
                container.Add(page.address, page);
            if (page != null && page.address == "/")
                PageManager.Get.CurrentPage = page;
        }
    }
}
