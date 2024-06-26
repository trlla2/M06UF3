using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login_Script : MonoBehaviour
{
    [Header("Login Stuff")]
    [SerializeField] private Button loginButton;
    [SerializeField] private Text loginUserText;
    [SerializeField] private Text loginPasswordText;

    [SerializeField] private Button goRegisterButton;
    [SerializeField] private GameObject loginPanel;

    [Header("Register stuff")]
    [SerializeField] private Button registerButton;
    [SerializeField] private Text registerUserText;
    [SerializeField] private Text registerPasswordText;
    [SerializeField] private Text registerRePasswordText;

    [SerializeField] private Button goLoginButton;
    [SerializeField] private GameObject registerPanel;

    private void Awake()
    {
        //Listener
        loginButton.onClick.AddListener(LoginButtonClicked);
        registerButton.onClick.AddListener(RegisterButtonClicked);

        goLoginButton.onClick.AddListener(GoLoginButtonClicked);
        goRegisterButton.onClick.AddListener(GoReggisterButtonClicked);
    }

    private void GoReggisterButtonClicked()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    private void GoLoginButtonClicked()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    private void RegisterButtonClicked()
    {
        //Check if inputs are ok
        if (registerUserText.text.ToString() != "" && registerPasswordText.text.ToString() != "" && registerRePasswordText.text.ToString() != "" )
        {
            string registerUser = registerUserText.text.ToString().Trim();
            string registerPassword = registerPasswordText.text.ToString().Trim();
            string registerRePassword = registerRePasswordText.text.ToString().Trim();
            if(registerPassword == registerRePassword)
            {
                if (!DataBase.DB.GetUser(registerUser))
                {
                    DataBase.DB.RegisterUser(registerUser, registerPassword);
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Debug.Log("Register failed");
                    registerUserText.text = "";
                    registerPasswordText.text = "";
                    registerRePasswordText.text = "";
                }
            }
            else
            {
                Debug.Log("Register failed");
                registerUserText.text = "";
                registerPasswordText.text = "";
                registerRePasswordText.text = "";
            }
        }
        else
        {
            Debug.Log("Register failed");
            registerUserText.text = "";
            registerPasswordText.text = "";
            registerRePasswordText.text = "";
        }
    }

    private void LoginButtonClicked()
    {
        //Check if inputs are ok
        if (loginUserText.text.ToString() != "" && loginPasswordText.text.ToString() != "")
        {
            string loginUser = loginUserText.text.ToString().Trim();
            string loginPassword = loginPasswordText.text.ToString().Trim();
            if (DataBase.DB.LoginUser(loginUser, loginPassword))
            {
                //Load Game
                Debug.Log("Funca");
                SceneManager.LoadScene(1);
            }
            else
            {
                //Delete Text
                SceneManager.LoadScene(0);
            }
        }
    }
}
