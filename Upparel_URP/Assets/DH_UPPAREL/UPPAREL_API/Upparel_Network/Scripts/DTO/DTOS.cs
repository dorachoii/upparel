using System.Collections.Generic;

[System.Serializable]
public class MemberVO
{
	public string id;
	public string email;
	public string password;
	public string nickname;
	public string rule;
	public long reward;
	public string character;
}
[System.Serializable]
public class MarketVO
{
	public string marketId;
	public string memberId;
	public string name;
	public string introPage;
	public string address;
}
[System.Serializable]
public class ResponseDTO<T>
{
	public int httpstatus;
	public string message;
	public T results;
}
[System.Serializable]
public class ProductVO
{
	public string productId;
	public string marketId;
	public string name;
	public long price;
	public string type;
	public ProductImgVO[] productImgs;
}
[System.Serializable]
public class ProductImgVO
{
	public string imageID;
	public string productID;
	public string img;
}
[System.Serializable]
public class ProductsWrapperVO
{
	public List<ProductVO> products = new List<ProductVO>();
}
