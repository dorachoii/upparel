using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ProductOBJ : MonoBehaviour
{
    ProductVO product = new ProductVO();
    //자신이 가지고 있는 UI 객체들 
    public TMP_Text name_text;
    public TMP_Text price_text;
    public RawImage img;
    private void Start()
    {
        name_text.text = product.name;
        price_text.text = product.price.ToString();

        HttpRequest req = new HttpRequestBuilder()
            .Uri("file")
            .ReqHandler((webreq)=> {
                byte[] fileByte = webreq.downloadHandler.data;
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(fileByte);
                img.texture = texture;
            })
            .build();

        string query = HttpManager.GetQuaery(new List<KeyValue<string, string>>() {
            {new KeyValue<string, string>("fileName",product.productImgs[0].img+".png") }
        });
        StartCoroutine(HttpRequestManager.Get(req, query));

    }
    public void Set(ProductVO vo)
    {
        product.marketId = vo.marketId;
        product.name = vo.name;
        product.price = vo.price;
        product.productId = vo.productId;
        product.type = vo.type;
        product.productImgs = vo.productImgs;
    }
}
