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
        //���� �ʰ� Ȱ��ȭ �Ǿ��ִ��� ��Ȱ��ȭ �Ǿ��ִ��� ����.
        //�ʸ� �����ؼ� ��� �۾��� �����ؾ� �ϴµ�
        //�ʰ� ��Ȱ��ȭ �Ǿ� ���� ���, ���� ������ ����.
        //� ������� �� ������ �ذ�����?
        //1. �� ��ü�� Start���� ���������� �Լ��� ȣ���Ѵ�.(Start�� �� �ٰ�.)
        //2. ���ʿ� �̰� ���� �ȵȴ�. ���������� �ٲ�� �Ѵ�.
        createProductButton.onClick.AddListener(CreateProduct);
        //2���� UI��ũ��Ʈ�� ������ �������� �ʰ� ���� ������ ��ü�� �̰��� �����ϴ°�.
        //���û� 2���� ���� �ʳ�..
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
            .Path("market/register")
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
            .Path("market/product/create-product")
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
