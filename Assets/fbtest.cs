// Include Facebook namespace
/*
using System;
using System.Collections.Generic;
using Facebook.Unity;

using UnityEngine;

public class fbtest : MonoBehaviour
{

// Awake function from Unity's MonoBehavior
void Awake()
{
    if (!FB.IsInitialized)
    {
        // Initialize the Facebook SDK
        FB.Init(InitCallback, OnHideUnity);
    }
    else
    {
        // Already initialized, signal an app activation App Event
        FB.ActivateApp();
    }
}

private void InitCallback()
{
    if (FB.IsInitialized)
    {
        // Signal an app activation App Event
        FB.ActivateApp();
            var perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, AuthCallback);

        }
        else
    {
        Debug.Log("Failed to Initialize the Facebook SDK");
    }
}

private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);

     
            // Print curaTokenrent access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }


           FB.API("/me?fields=email", HttpMethod.GET, (response) => {
               Debug.Log(response);
           });
  
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
    private void OnHideUnity(bool isGameShown)
{
    if (!isGameShown)
    {
        // Pause the game - we will need to hide
        Time.timeScale = 0;
    }
    else
    {
        // Resume the game - we're getting focus again
        Time.timeScale = 1;
    }
}
}
*/