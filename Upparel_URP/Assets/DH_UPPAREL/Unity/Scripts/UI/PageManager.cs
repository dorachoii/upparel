using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    static PageManager instance;
    public Stack<Page> next_pageStack = new Stack<Page>();
    public Stack<Page> prev_pageStack = new Stack<Page>();
    public Dictionary<string, object> data = new Dictionary<string, object>();
    public Dictionary<string, PageContainer> containers = new Dictionary<string, PageContainer>();

    Page currentPage = null;
    public Page CurrentPage { get { return currentPage; } set {
            if(CurrentPage)
                CurrentPage.Close();
            currentPage = value;
            CurrentPage.Open();
        } }

    public static PageManager Get
    {
        get {
            if (instance == null)
            {
                GameObject go = new GameObject("PageManager");
                PageManager manager = go.AddComponent<PageManager>();
                instance = manager;
            }
                
            return instance;
        }
        
    }

    private void Awake()
    {

    }



    public void ChangePage(string container,string address)
    {   //���� �������� ���� ���ؿ� �ְ�
        //���� üũ : ���ÿ� ���� �������� ������ ����
/*        if (prev_pageStack.Contains(currentPage))
        {
            return;
        }*/
        //���� �������� ��û�Ѵ�.
        PageContainer cont = null;
        containers.TryGetValue(container, out cont);
        if (cont)
        {
            Page page = cont.GetPage(address);
            /*if (prev_pageStack.Contains(page))
                return;*/
            if (CurrentPage == page)
                return;
            prev_pageStack.Push(CurrentPage);
            CurrentPage = page;
        }
        else
        {
            Debug.Log("ERROR!!");
        }
 
    }


    public void PressNext()
    {
        if(next_pageStack.Count > 0)
        {
            //���� �������� ���� �����ϰ�
            prev_pageStack.Push(CurrentPage);
            //���� �������� ���� �������� ���ư���.
            CurrentPage = next_pageStack.Pop();
        }
        
    }
    public void PressPrev()
    {
        if(prev_pageStack.Count > 0)
        {
            //���� ���������� ���� �����ϰ�
            next_pageStack.Push(CurrentPage);
            //���� �������� ���� �������� ���ư���.
            CurrentPage = prev_pageStack.Pop();
        }
       
    }


    

}
