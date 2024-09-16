using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class LoginView : UINode
{
    
    [SerializeField]
    Toggle toggle_selectCustomer;
    [SerializeField]
    Toggle toggle_selectBusiness;


    //c is customer and b is business
    [SerializeField]
    GameObject informationBoard_c;
    [SerializeField]
    GameObject informationBoard_b;

    [SerializeField]
    TMP_InputField field_c_email;
    [SerializeField]
    TMP_InputField field_c_password;

    [SerializeField]
    TMP_InputField field_b_email;
    [SerializeField]
    TMP_InputField field_b_password;

    [SerializeField]
    Button btn_submit;
    [SerializeField]
    Button btn_c_register;
    [SerializeField]
    Button btn_b_register;

    //�α��� �� ����� �׼�
    public UnityEvent onSuccess_C_Login;
    public UnityEvent onSuccess_B_Login;

    public override void Init()
    {
        base.Init();
        //UI ����
        UILink.LinkSetComponent<Toggle>(out toggle_selectCustomer, toggle_selectCustomer, "");
        UILink.LinkSetComponent<Toggle>(out toggle_selectBusiness, toggle_selectBusiness, "");

        UILink.LinkSetComponent<TMP_InputField>(out field_c_password, field_c_password, "");
        UILink.LinkSetComponent<TMP_InputField>(out field_b_password, field_b_password, "");

        UILink.LinkSetComponent<TMP_InputField>(out field_c_email, field_c_email, "");
        UILink.LinkSetComponent<TMP_InputField>(out field_b_email, field_b_email, "");

        UILink.LinkSetComponent<Button>(out btn_submit, btn_submit, "");
    }
    // Start is called before the first frame update

    void Start()
    {
        toggle_selectCustomer.onValueChanged.AddListener((isSelected) => {
            if (isSelected) SelectCustomer();
        });

        toggle_selectBusiness.onValueChanged.AddListener((isSelected) => {
            if (isSelected) SelectBusiness();
        });
        btn_submit.onClick.AddListener(Submit);

        field_c_email.onValueChanged.AddListener((s)=> {
            btn_submit.interactable = isSelectedCustomer && CustomerFieldCheck();
        });
        field_c_password.onValueChanged.AddListener((s) => {
            btn_submit.interactable = isSelectedCustomer && CustomerFieldCheck();
        });
        field_b_email.onValueChanged.AddListener((s) => {
            btn_submit.interactable = !isSelectedCustomer && BusinessFieldCheck();
        });
        field_b_password.onValueChanged.AddListener((s) => {
            btn_submit.interactable = !isSelectedCustomer && BusinessFieldCheck();
        });

        btn_b_register.onClick.AddListener(()=> {
            PageManager.Get.data.Add("rule","maker");
        });
        btn_c_register.onClick.AddListener(()=> {
            PageManager.Get.data.Add("rule", "customer");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isSelectedCustomer = true;

    public void SelectCustomer()
    {
        informationBoard_b.SetActive(false);
        informationBoard_c.SetActive(true);
        isSelectedCustomer = true;
        btn_submit.interactable = isSelectedCustomer && CustomerFieldCheck();

    }
    public void SelectBusiness()
    {
        informationBoard_b.SetActive(true);
        informationBoard_c.SetActive(false);
        isSelectedCustomer = false;

        btn_submit.interactable = !isSelectedCustomer && BusinessFieldCheck();
    }


    public void Submit()
    {
        //Ŀ���͸� ����Ͻ� �б� �ϱ�
        //�б� ���� : ���� ������ ��������
        if (isSelectedCustomer)
        {
            string email = field_c_email.text;
            string password = field_c_password.text;

            HttpRequest req = new HttpRequestBuilder().Uri("member/login")
            .ReqHandler((webreq) => {
                ResponseDTO<MemberVO> responseDTO = JsonUtility.FromJson<ResponseDTO<MemberVO>>(webreq.downloadHandler.text);
                print(responseDTO.results.nickname);
                onSuccess_C_Login?.Invoke();
                }).build();
            WWWForm form = new WWWForm();
            form.AddField("email", email);
            form.AddField("password", password);

            StartCoroutine(HttpRequestManager.Post(req, form));
        }
        else
        {
            string email = field_b_email.text;
            string password = field_b_password.text;
            HttpRequest req = new HttpRequestBuilder().Uri("member/login")
                .ReqHandler((webreq) =>
                {
                    onSuccess_B_Login?.Invoke();
                }).build();
            WWWForm form = new WWWForm();
            form.AddField("email", email);
            form.AddField("password", password);

            StartCoroutine(HttpRequestManager.Post(req, form));
        }

    }

    public bool CustomerFieldCheck()
    {
        string email = field_c_email.text;
        string password = field_c_password.text;
        return !IsNullToCheck(email) && !IsNullToCheck(password); 
    }

    public bool BusinessFieldCheck()
    {
        string email = field_b_email.text;
        string password = field_b_password.text;
        return !IsNullToCheck(email) && !IsNullToCheck(password);
    }

    public bool IsNullToCheck(string str)
    {
        return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
    }
}
