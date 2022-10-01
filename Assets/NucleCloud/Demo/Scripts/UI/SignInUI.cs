using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nucle.Cloud;
using UnityEngine;
using UnityEngine.UI;

public class SignInUI : MonoBehaviour
{
    public Text email;
    public Text password;
    public Text error;

    public void OnEnable()
    {
        error.gameObject.SetActive(false);
    }

    public async void Login()
    {
        if (!ValidateForm()) return;
        LoginResult loginResult = null;

        try
        {
            var emailValue = email.text;
            var passwordValue = password.text;
            loginResult = await User.LoginWithEmail( NucleSettings.Instance.projectId,emailValue,passwordValue);
        }
        catch (Exception ex)
        {
            error.gameObject.SetActive(true);
            error.text = ex.Message;
        }


        if (loginResult != null)
        {

            NucleSettings.Instance.user = loginResult.user;
            NucleSettings.Instance.SetUserToken(loginResult.userToken);
            UIManager.Instance.HomeUI();
        }
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

        if (string.IsNullOrWhiteSpace(password.text))
        {
            valideForm = false;
            password.transform.parent.Find("Title").GetComponent<Text>().color = Color.red;
        }
        else
        {
            password.transform.parent.Find("Title").GetComponent<Text>().color = Color.white;
        }

        return valideForm;
    }
}
