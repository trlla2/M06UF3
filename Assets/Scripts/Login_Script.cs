using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login_Script : MonoBehaviour
{
    [Header("Login Stuff")]
    [SerializeField] private Button loginButton;
    [SerializeField] private Text loginUserText;
    [SerializeField] private Text loginPasswordText;

    [SerializeField] private Button goRegisterButton;
    [SerializeField] private GameObject loginPanel;

    //[Header("Register stuff")]
    //[SerializeField] private Button registerButton;
    //[SerializeField] private Text registerUserText;
    //[SerializeField] private Text registerPasswordText;
    //[SerializeField] private Text registerRePasswordText;

    //[SerializeField] private Button goLoginButton;
    //[SerializeField] private GameObject registerPanel;

    private void Awake()
    {
        //Listener
        loginButton.onClick.AddListener(LoginButtonClicked);
        //registerButton.onClick.AddListener(RegisterButtonClicked);

        //goLoginButton.onClick.AddListener(GoLoginButtonClicked);
        //goRegisterButton.onClick.AddListener(GoReggisterButtonClicked);
    }

    private void LoginButtonClicked()
    {
        //Check if inputs are ok
        if (loginUserText.text.ToString() != "" && loginPasswordText.text.ToString() != "")
        {
            if (DataBase.DB.GetUser(loginUserText.text.ToString(), loginPasswordText.text.ToString()))
            {
                Debug.Log("Funca");
            }
            else
            {
                Debug.Log("aaaa");
            }
        }
    }
}
