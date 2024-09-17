using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPRoduct : MonoBehaviour
{
    public GameObject ProductObj;
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {

        HttpRequest req = new HttpRequestBuilder()
            .Path("market/product/read-products")
            .ReqHandler((webreq)=> {
                string data = webreq.downloadHandler.text;
                ResponseDTO<ProductsWrapperVO> dto = JsonUtility.FromJson<ResponseDTO<ProductsWrapperVO>>(data);
                foreach(ProductVO product in dto.results.products)
                {
                    GameObject go = Instantiate(ProductObj);
                    go.transform.SetParent(content);
                    ProductOBJ obj = go.GetComponent<ProductOBJ>();
                    obj.Set(product);
                }
            
            })
            .build();
        string data = HttpManager.GetQuaery(new List<KeyValue<string, string>>()
        {
            { new KeyValue<string, string>("marketID","fe7e2838-1b8b-4f00-9616-71c2ff343fa3") }
        }) ;
        StartCoroutine(HttpRequestManager.Get(req,data));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
