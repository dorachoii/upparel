using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MarketUI : UINode
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        createMarketButton.onClick.AddListener(Register); 
        //나는 너가 활성화 되어있는지 비활성화 되어있는지 몰라.
        //너를 참조해서 어떠한 작업을 진행해야 하는데
        //너가 비활성화 되어 있을 경우, 나는 문제가 생겨.
        //어떤 방법으로 이 문제를 해결할지?
        //1. 이 객체의 Start에서 간접적으로 함수를 호출한다.(Start는 봐 줄게.)
        //2. 애초에 이게 말이 안된다. 설계적으로 바꿔야 한다.
        createProductButton.onClick.AddListener(CreateProduct);
        //2번은 UI스크립트가 모든것은 관리하지 않고 껏다 켜지는 주체가 이것을 관리하는것.
        //선택상 2번이 낫지 않나..
    }

    [Header("Create_Market_Fields")]
    [SerializeField]
    TMP_InputField marketName;
    [SerializeField]
    TMP_InputField marketIntro;
    [SerializeField]
    TMP_InputField marketAddress;

    [Header("Create_Market_Buttons")]
    [SerializeField]
    Button createMarketButton;

    [Header("Create_Product_Fields")]
    [SerializeField] TMP_InputField productName;
    [SerializeField] TMP_InputField productPrice;
    [SerializeField] TMP_InputField productType;

    [Header("Create_Product_Buttons")]
    [SerializeField]
    Button createProductButton;

    [Header("Etc")]
    [SerializeField]
    ProductImgUpload productImgUpload;
    public override void Init()
    {
        base.Init();
        Debug.Log("INIT");
        UILink.LinkSetComponent<TMP_InputField>(out marketName,marketName, Fields.marketName);
        UILink.LinkSetComponent<TMP_InputField>(out marketIntro, marketIntro, Fields.marketIntro);
        UILink.LinkSetComponent<TMP_InputField>(out marketAddress,marketAddress, Fields.marketAddress);
        UILink.LinkSetComponent<Button>(out createMarketButton, createMarketButton, Buttons.createMarketButton);

        UILink.LinkSetComponent<TMP_InputField>(out productName, productName, Fields.productName);
        UILink.LinkSetComponent<TMP_InputField>(out productPrice, productPrice, Fields.productPrice);
        UILink.LinkSetComponent<TMP_InputField>(out productType, productType, Fields.productType);
        UILink.LinkSetComponent<Button>(out createProductButton, createProductButton, Buttons.createProductButton);

        UILink.LinkSetComponent<ProductImgUpload>(out productImgUpload, productImgUpload, Etc.ProductImgUpload);
    }

    public void Register()
    {
        MarketVO market = new MarketVO();
        market.name = marketName.text;
        market.introPage = marketIntro.text;
        market.address = marketAddress.text;
        string  json = JsonUtility.ToJson(market);


        HttpRequest req = new HttpRequestBuilder()
            .Uri("market/register")
            .Headers(new List<KeyValue<string, string>> {
                new KeyValue<string, string>("Content-Type", "application/json; charset=utf-8")
            })
            .build();

        StartCoroutine(HttpRequestManager.Post(req, json));

    }

    public void CreateProduct()
    {
        ProductVO product = new ProductVO();
        product.name = productName.text;
        product.price = long.Parse(productPrice.text);
        product.type = productType.text;
        string json = JsonUtility.ToJson(product);

        List<IMultipartFormSection> partData = new List<IMultipartFormSection>();
        partData.Add(new MultipartFormDataSection("product", json,"application/json"));
        productImgUpload.Send(partData);


        HttpRequest req = new HttpRequestBuilder()
            .Uri("market/product/create-product")
            .build();

        StartCoroutine(HttpRequestManager.Post(req, partData));
    }
    enum Fields
    {
        marketName,
        marketIntro,
        marketAddress,
        productName,
        productPrice,
        productType

    }
    enum Buttons
    {
        createMarketButton,
        createProductButton

    }

    enum Etc
    {
        ProductImgUpload
    }

}
