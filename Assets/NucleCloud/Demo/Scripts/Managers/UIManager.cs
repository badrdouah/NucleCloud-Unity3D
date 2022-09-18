using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public enum UIType {HomeUI, InGameUI,
   SignUpUI, SignInUI,ForgotPassword, LeaderboardUI,
DisconnectedUI }
public class UIManager : MonoBehaviour
{
 
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

     UIType uIType = UIType.HomeUI;
    public UIType UIType
    {
        set
        {
            uIType = value;
            StateChanged(uIType);
        }
         get
         {
             return uIType;
         }
    }

    public List<UIElement> UIElements = new List<UIElement>();

    void Start()
    {
        UIType = UIType.HomeUI;
    }

    void StateChanged(UIType uIType)
    {
        for (int i = 0; i < UIElements.Count; i++)
        {
            var uIElement = UIElements[i];
            if (uIElement.UIType == uIType)
            {
                uIElement.UIGameObject.SetActive(true);
            }
            else
            {
                uIElement.UIGameObject.SetActive(false);
            }
        }
    }

    public UIElement GetUIElement(UIType uIType)
    {
        for (int i = 0; i < UIElements.Count; i++)
        {
            var uIElement = UIElements[i];
            if (uIElement.UIType == uIType) return uIElement;
        }
        return null;
    }
 
 

    public void HomeUI()
    {
        UIManager.Instance.UIType = UIType.HomeUI;
    }

    public void InGameUI()
    {
        UIManager.Instance.UIType = UIType.InGameUI;
    }


    public void SignInUI()
    {
        UIManager.Instance.UIType = UIType.SignInUI;
    }

    public void SignUpUI()
    {
        UIManager.Instance.UIType = UIType.SignUpUI;
    }


    public void ForgotPasswordUI()
    {
        UIManager.Instance.UIType = UIType.ForgotPassword;
    }

    public void LeaderboardUI()
    {
        UIManager.Instance.UIType = UIType.LeaderboardUI;
    }

    public void DisconnectedUI()
    {
        UIManager.Instance.UIType = UIType.DisconnectedUI;
    }

    public void Quit()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class UIElement
{
    public GameObject UIGameObject;
    public UIType UIType;
}