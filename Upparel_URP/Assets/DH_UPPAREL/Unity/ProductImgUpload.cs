using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProductImgUpload : MonoBehaviour
{

    public Button imgButton;
    public RawImage image;
    private ProductImage selectedProductImage;
    public List<ProductImage> lst = new List<ProductImage>();
    // Start is called before the first frame update
    void Start()
    {

        imgButton.onClick.AddListener(CreateUI);

        yesButton.onClick.AddListener(() => {
            lst.Remove(selectedProductImage);
            Destroy(selectedProductImage.gameObject);
            ImageAlert.SetActive(false);
        });
        noButton.onClick.AddListener(() => {
            ImageAlert.SetActive(false);
        });
        ProductImage.onclick += (T) => {
            OnProductImageClick(T);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //req
    [SerializeField]
    RectTransform content;
    [SerializeField]
    GameObject imgObj;


    [SerializeField]
    GameObject ImageAlert;
    [SerializeField]
    Button yesButton;
    [SerializeField]
    Button noButton;
    public void CreateUI()
    {
        Texture2D texture = UnityNativeGallery.GetImage();
        if (!texture)
            return;
        GameObject go = Instantiate(imgObj,content); //이미지 생성하기
        RawImage img =  go.GetComponent<RawImage>();
        ProductImage proimg = go.GetComponent<ProductImage>();
        img.texture = texture;
        proimg.texture = texture;
        //이미지 텍스쳐 바꾸기
        lst.Add(proimg);

    }
    private void OnProductImageClick(ProductImage productImage)
    {
        selectedProductImage = productImage;
        ImageAlert.SetActive(true);
    }
    public List<IMultipartFormSection> Send(List<IMultipartFormSection> result)
    {
        foreach(ProductImage img in lst)
        {
            byte[] bytes = img.texture.EncodeToPNG();
            Debug.Log(bytes.Length);
            result.Add(new MultipartFormFileSection("files", img.texture.EncodeToPNG(),"dd.png", "multipart/form-data"));

        }
        return result;
    }
}
