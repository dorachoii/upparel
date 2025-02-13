using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class RegisterView : UINode
{
    [SerializeField]
    TMP_InputField field_email_front;
    [SerializeField]
    TMP_InputField field_email_back;
    [SerializeField]
    TMP_InputField field_password;
    [SerializeField]
    TMP_InputField field_password_check;
    [SerializeField]
    TMP_InputField field_nickname;
    [SerializeField]
    GameObject alert_password;


    [SerializeField]
    Button btn_register;

    void Start()
    {

        field_email_front.onValueChanged.AddListener((s) => {
            btn_register.interactable = FieldCheck();
        });
        field_email_back.onValueChanged.AddListener((s) => {
            btn_register.interactable = FieldCheck();
        });
        field_password.onValueChanged.AddListener((s) => {
            btn_register.interactable = FieldCheck();
        });
        field_password_check.onValueChanged.AddListener((s) => {
            btn_register.interactable = FieldCheck();
            PasswordFieldCheck();
        });
        field_nickname.onValueChanged.AddListener((s) => {
            btn_register.interactable = FieldCheck();
        });

    }

    public override void Init()
    {
        base.Init();
        //UI ����

        UILink.LinkSetComponent<TMP_InputField>(out field_email_front, field_email_front, "");
        UILink.LinkSetComponent<TMP_InputField>(out field_email_back, field_email_back, "");
        UILink.LinkSetComponent<TMP_InputField>(out field_password, field_password, "");
        UILink.LinkSetComponent<TMP_InputField>(out field_password_check, field_password_check, "");
        UILink.LinkSetComponent<TMP_InputField>(out field_nickname, field_nickname, "");
    }

    //패스워드 체크하기
    public void PasswordFieldCheck()
    {
        string password = field_password.text;
        string password_check = field_password_check.text;

        if (password != password_check)
        {
            alert_password.SetActive(true);
        }
        else
        {
            alert_password.SetActive(false);
        }
    }

    public bool FieldCheck()
    {
        string email_front = field_email_front.text;
        string email_back = field_email_back.text;
        string password = field_password.text;
        string password_check = field_password_check.text;
        string nickname = field_nickname.text;

        return !IsNullToCheck(email_front) && !IsNullToCheck(email_back) && !IsNullToCheck(password) && !IsNullToCheck(password_check) && !IsNullToCheck(nickname);
    }


    public bool IsNullToCheck(string str)
    {
        return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
    }
}

