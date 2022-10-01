using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nucle.Cloud;
using UnityEngine;
using UnityEngine.UI;

public class SignUpUI : MonoBehaviour
{
    public Text userName;
    public Text email;
    public Text password;
    public Text confirmPassword;

    public async void Create()
    {
        if (!ValidateForm()) return;

        LoginResult loginResult = null;
        if (NucleSettings.Instance.IsUserLoggedIn()) 
        {
            loginResult = await UpgradeToRealUser(loginResult);
        }
        else 
        {
            loginResult = await CreateNewUser(loginResult);
        }

        if (loginResult != null)
        {
            NucleSettings.Instance.user = loginResult.user;
            NucleSettings.Instance.SetUserToken(loginResult.userToken);
        }

        UIManager.Instance.UIType = UIType.HomeUI;
    }

    private async Task<LoginResult> CreateNewUser(LoginResult loginResult)
    {
        try
        {
            //Create new account and perform real account login
            var emailValue = email.text;
            var passwordValue = password.text;
            var userNameValue = userName.text;

            await User.Create(NucleSettings.Instance.projectId,emailValue,passwordValue,userNameValue);
            loginResult = await User.LoginWithEmail( NucleSettings.Instance.projectId,emailValue, passwordValue);
        }
        catch (Exception)
        {
            UIManager.Instance.DisconnectedUI();
        }

        return loginResult;
    }

    private async Task<LoginResult> UpgradeToRealUser(LoginResult loginResult)
    {
        try
        {
            //Upgrate to real user and perform account login
            var emailValue = email.text;
            var passwordValue = password.text;
            var userNameValue = userName.text;

            await User.Upgrade( NucleSettings.Instance.GetUserToken(),emailValue,passwordValue,userNameValue);
            loginResult = await User.LoginWithEmail(NucleSettings.Instance.projectId,emailValue,passwordValue);

        }
        catch (Exception)
        {
            UIManager.Instance.DisconnectedUI();
        }

        return loginResult;
    }

    private bool ValidateForm()
    {
        bool valideForm = true;
        if (string.IsNullOrWhiteSpace(email.text))
        {
            valideForm = false;
            email.transform.parent.Find("Title").GetComponent<Text>().color = Color.red;
        }
        else
        {
            email.transform.parent.Find("Title").GetComponent<Text>().color = Color.white;
        }

        if (string.IsNullOrWhiteSpace(userName.text))
        {
            valideForm = false;
            userName.transform.parent.Find("Title").GetComponent<Text>().color = Color.red;
        }
        else
        {
            userName.transform.parent.Find("Title").GetComponent<Text>().color = Color.white;
        }

        if (string.IsNullOrWhiteSpace(password.text))
        {
            valideForm = false;
            password.transform.parent.Find("Title").GetComponent<Text>().color = Color.red;
        }
        else
        {
            password.transform.parent.Find("Title").GetComponent<Text>().color = Color.white;
        }

        if (string.IsNullOrWhiteSpace(confirmPassword.text))
        {
            valideForm = false;
            confirmPassword.transform.parent.Find("Title").GetComponent<Text>().color = Color.red;
        }
        else
        {
            confirmPassword.transform.parent.Find("Title").GetComponent<Text>().color = Color.white;
        }

        if (password.text != confirmPassword.text)
        {
            valideForm = false;
            confirmPassword.transform.parent.Find("Title").GetComponent<Text>().color = Color.red;
        }
        else
        {
            confirmPassword.transform.parent.Find("Title").GetComponent<Text>().color = Color.white;
        }

        return valideForm;
    }
}
