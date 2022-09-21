using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nucle.Cloud;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    public Text PointsText;
    public Text InfoText;
    public Text InfoBubbleText;
    public GameObject LoginBtn;
    public GameObject LogoutBtn;
    public Button LeaderboardBtn;

    private async void OnEnable()
    {
        this.GetComponent<GraphicRaycaster>().enabled = false;
        LogoutBtn.gameObject.SetActive(false);
        PointsText.gameObject.SetActive(false);
        InfoBubbleText.gameObject.SetActive(false);
        LoginBtn.SetActive(false);
        LogoutBtn.SetActive(false);

        InfoText.gameObject.SetActive(true);
        InfoText.text = "Connecting....";

        if (!NucleSettings.Instance.IsUserLoggedIn())
        {
            await AnonymouUserLogin();
        }
        else
        {
            await RevokeUserToken();
        }

        await SyncUI();
    }

    private async Task RevokeUserToken()
    {
        var userToken = NucleSettings.Instance.GetUserToken();
        try
        {
            var loginResult = await User.RevokeToken(userToken);

            if (loginResult != null)
            {
                NucleSettings.Instance.user = loginResult.user;
                NucleSettings.Instance.SetUserToken(loginResult.userToken);

                LogoutBtn.gameObject.SetActive(true);
            }
        }
        catch (Exception)
        {
            NucleSettings.Instance.Logout();
            UIManager.Instance.DisconnectedUI();
        }
    }

    private async Task AnonymouUserLogin()
    {
        try
        {
            var deviceId = SystemInfo.deviceUniqueIdentifier;
            var loginResult = await Anonymous.Login( NucleSettings.Instance.projectId, deviceId);
            if (loginResult == null)//create new anoymous user and perform a login at the same time
            {
                await Anonymous.Create(NucleSettings.Instance.projectId, deviceId);
                loginResult = await Anonymous.Login(NucleSettings.Instance.projectId, deviceId);
            }
 
            if (loginResult != null)
            {
                NucleSettings.Instance.user = loginResult.user;
                NucleSettings.Instance.SetUserToken(loginResult.userToken);
                LogoutBtn.gameObject.SetActive(true);
            }
        }
        catch (Exception) { }
    }

    private async Task  SyncUI()
    {
  
        if (NucleSettings.Instance.IsUserLoggedIn())
        {
            var user = NucleSettings.Instance.user;
            var userName = user.userName;

            var userType = await User.GetType(NucleSettings.Instance.GetUserToken());
            if (userType == "REAL")
            {
                InfoText.text = "Welcome " + userName;
                LoginBtn.SetActive(false);
            }
            else
            {
                InfoText.gameObject.SetActive(false);
                LoginBtn.SetActive(true);
            }
            PointsText.text = await GameManager.Instance.GetPoints();
            PointsText.gameObject.SetActive(true);
            LeaderboardBtn.interactable = true;

            await OpenAppEvent();
            await InfoBubble_RemoteControl();
        }
        else
        {
            InfoText.gameObject.SetActive(false);
            PointsText.gameObject.SetActive(false);
            LoginBtn.SetActive(true);
            LogoutBtn.SetActive(false);
            LeaderboardBtn.interactable = false;

        }
        this.GetComponent<GraphicRaycaster>().enabled = true;
    }

    private async Task InfoBubble_RemoteControl()
    {
 
           var infoBubble_PresetId = NucleSettings.Instance.GetPresetId("InfoBubble");
  
        var infoBubble = await Preset.GetById( NucleSettings.Instance.GetUserToken(), infoBubble_PresetId);

        var showInfoBubble = bool.Parse(infoBubble.globalValue);
        if (showInfoBubble)
        {
            //Get the Info bubble text value from the server
            var InfoBubbleText_PresetId = NucleSettings.Instance.GetPresetId("InfoBubbleText");
            var infoBubbleText = await Preset.GetById( NucleSettings.Instance.GetUserToken(), InfoBubbleText_PresetId);

            InfoBubbleText.text = infoBubbleText.globalValue;
            InfoBubbleText.gameObject.SetActive(true);
        }
    }

    private static async Task OpenAppEvent()
    {
        //OpenApp Event registration
        var presetId = NucleSettings.Instance.GetPresetId("openApp");
        var value = "test value";
        
        await Variable.Update(NucleSettings.Instance.GetUserToken(),presetId,value);
    }

    public async void Logout()
    {
        NucleSettings.Instance.Logout();
        await SyncUI();
    }
}
