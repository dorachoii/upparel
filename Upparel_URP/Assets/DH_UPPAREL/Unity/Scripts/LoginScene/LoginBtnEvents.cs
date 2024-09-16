using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoginBtnEvents : MonoBehaviour
{
    //LoginPage
    [SerializeField]
    Button p1_LoginBtn;
    [SerializeField]
    Button p1_registerBtn;

    [SerializeField]
    Button p2_LoginBtn;
    [SerializeField]
    Button p2_registerBtn;

    [SerializeField]
    Button regi_completeBtn;

    [SerializeField]
    Button startBtn;


    CharButton[] charButtons;
    private void Awake()
    {
        p1_LoginBtn.onClick.AddListener(Login);
        p1_registerBtn.onClick.AddListener(Register);
        startBtn.onClick.AddListener(GetStart);
        business_reg.onClick.AddListener(() => {
            member.rule = "maker";
            PageManager.Get.ChangePage("/", "BusinessLogin");
        });
        business_log.onClick.AddListener(Login);



        addFieldsEvent(p1_LoginBtn,login_password_field, login_email_field);
        addFieldsEvent(p1_registerBtn, emailField,emailField2,passwordField,passwordField2,nicknameField);
        addFieldsEvent(business_log,business_login_email_field, business_login_password_field);
        
        //��..
        /*for(int i = 0; i < charButtons.Length; i++)
        {
            charButtons[i].OnEnable = () => { startBtn.interactable = true; };
        }*/

    }

    [SerializeField]
    TMP_InputField login_email_field;
    [SerializeField]
    TMP_InputField login_password_field;

    [SerializeField]
    TMP_InputField business_login_password_field;
    [SerializeField]
    TMP_InputField business_login_email_field;

    public void Login()
    {
        string email = "";
        string password = "";
        //�� �������׼� ȣ���Ѱž�?
        if (PageManager.Get.CurrentPage.address == "LoginPage")
        {
            email = login_email_field.text;
            password = login_password_field.text;
        }
        else
        {
            email = business_login_email_field.text;
            password = business_login_password_field.text;
        }
        //���� ����
        
        Debug.Log(email);
        Debug.Log(password);
        HttpRequest req = new HttpRequestBuilder().Uri("member/login")
            .Headers(new List<KeyValue<string, string>> {
                //new KeyValue<string, string>("Content-Type", "application/json; charset=utf-8")
            })
            .ReqHandler((webreq) => {
                //��û�� �������̸� ������ ü����.
                ResponseDTO<MemberVO> responseDTO = JsonUtility.FromJson<ResponseDTO<MemberVO>>(webreq.downloadHandler.text);
                print(responseDTO.results.nickname);

            })
            .build();
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);


        StartCoroutine(HttpRequestManager.Post(req, form));

        

        //���� ������ ���� �� ��ȯ 


    }


    //fields
    [SerializeField]
    TMP_InputField emailField;
    [SerializeField]
    TMP_InputField emailField2;
    [SerializeField]
    TMP_InputField passwordField;
    [SerializeField]
    TMP_InputField passwordField2;
    [SerializeField]
    TMP_InputField nicknameField;



    //message
    [SerializeField]
    GameObject email_message;
    [SerializeField]
    GameObject password_message;
    [SerializeField]
    GameObject nickname_message;

    MemberVO member = new MemberVO();
    public void Register()
    {
        Debug.Log("ȸ������ ������!");
        object objrule = null;
        PageManager.Get.data.TryGetValue("rule",out objrule);
        string rule = (string)objrule;

        string email = emailField.text +"@"+ emailField2.text;
        string password = passwordField.text;
        string nickname = nicknameField.text;
        
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nickname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(nickname))
            return;
        if (string.IsNullOrWhiteSpace(rule) || string.IsNullOrEmpty(rule))
            rule = "customer";

        //reset
        email_message.SetActive(false);
        password_message.SetActive(false);
        nickname_message.SetActive(false);

        if (!(passwordField.text==passwordField2.text))
        {
            password_message.SetActive(true);
            return; 
        }
        Debug.Log("������!");

        //���� ����� 
        MemberVO vo = member;
        vo.email = email;
        vo.password = password;
        vo.nickname = nickname;
        vo.rule = rule;
        vo.character = "char1";
        string json = JsonUtility.ToJson(vo);
        Debug.Log(json);
        //web request
        HttpRequest req = new HttpRequestBuilder().Uri("member/checkAccount")
            .Headers(new List<KeyValue<string, string>> {
                new KeyValue<string, string>("Content-Type", "application/json; charset=utf-8")
            })
            .ReqHandler((webreq) => {
                //��û�� �������̸� ������ ü����.
                ResponseDTO<string> responseDTO = JsonUtility.FromJson<ResponseDTO<string>>(webreq.downloadHandler.text);
                ResponseMessage msg = ShowMessage(responseDTO.results);
                switch (msg)
                {
                    case ResponseMessage.OK:
                        PageManager.Get.data.Add("nickname", nickname);
                        PageManager.Get.ChangePage("/", "Character");
                        break;
                    case ResponseMessage.EXISTS_NICKNAME:
                        nickname_message.SetActive(true);
                        break;
                    case ResponseMessage.EXISTS_EMAIL:
                        email_message.SetActive(true);
                        break;
                    default:
                        break;
                }


            })
            .build();
        
        //StartCoroutine(HttpRequestManager.Post(req, json));

        PageManager.Get.data.Add("nickname", nickname);
                        PageManager.Get.ChangePage("/", "Character");
    }


    public UnityEngine.Events.UnityEvent onSucceed_Register;
    public void GetStart()
    {
        string uri = "member/customer/register";
        if (member.rule == "maker")
            uri = "member/maker/register";
        member.character = CharButton.currentBtn.type.ToString();
        string json = JsonUtility.ToJson(member);
        HttpRequest req = new HttpRequestBuilder().Uri(uri)
            .Headers(new List<KeyValue<string, string>> {
                new KeyValue<string, string>("Content-Type", "application/json; charset=utf-8")
            })
            .ReqHandler((Action<UnityEngine.Networking.UnityWebRequest>)((webreq) => {
                //��û�� �������̸� ������ ü����.
                /*ResponseDTO<string> responseDTO = JsonUtility.FromJson<ResponseDTO<string>>(webreq.downloadHandler.text);
                ShowMessage(responseDTO.results);*/

                onSucceed_Register?.Invoke();
            }))
            .build();

        StartCoroutine(HttpRequestManager.Post(req, json));
    }
    ResponseMessage ShowMessage(string results)
    {
        ResponseMessage rs = (ResponseMessage)Enum.Parse(typeof(ResponseMessage), results);
        switch (rs)
        {
            case ResponseMessage.OK:

                //PageManager.Get.ChangePage("/", "Character");
                break;
            case ResponseMessage.EXISTS_NICKNAME:
                //nickname_message.SetActive(true);
                break;
            case ResponseMessage.EXISTS_EMAIL:
                //email_message.SetActive(true);
                break;
            default:
                break;
        }
        return rs;
    }


    [SerializeField]
    Button business_reg;
    [SerializeField]
    Button business_log;
    public void SetRule()
    {

    }
    public bool CheckEmptyFields(params TMP_InputField[] fields)
    {
        for(int i = 0; i < fields.Length; i++)
        {
            string text = fields[i].text;
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                return false;
        }
        return true;
    }
    public void addFieldsEvent(Button button,params TMP_InputField[] fields)
    {
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].onValueChanged.AddListener((s) => { button.interactable = CheckEmptyFields(fields); });
        }
    }
    
}


public enum ResponseMessage
{
    OK,
    EXISTS_NICKNAME,
    EXISTS_EMAIL,
    
}
public enum CharacterType
{
    ONE,TWO,THREE,FOUR,FIVE,SIX,SEVEN,EIGHT,NINE,TEN
}