using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nucle.Cloud;
using UnityEngine;
using UnityEngine.UI;

public class ForgetPasswordUI : MonoBehaviour
{
    public Text email;
    public Text info;

    public void OnEnable()
    {
        info.gameObject.SetActive(false);
    }

    public async void sendResetPassword()
    {
        //form validation
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

        if (!valideForm) return;
        try
        {
             var emailValue = email.text;
            //Create new account and perform real account login
             await User.SendResetPassword( NucleSettings.Instance.projectId,emailValue);

            info.gameObject.SetActive(true);
            info.text = "Incorrect email or password ";
        }
        catch (Exception)
        {
            UIManager.Instance.DisconnectedUI();
        }
    }
}
