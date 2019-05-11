using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class Backend : MonoBehaviour {

    public void Login(string pLogin, string pPassword, Action<LoginResult> pResponse, bool pRetry = false)
    {

    }
    ///<summary> That's request to server to a new guest player using a hash id of player guest username </summary>
    public void GuestLogin(Action<bool, string> pResponse)
    {
        string tGuestID = PlayerPrefs.GetString(GUEST_KEY);
        string tUserID = PlayerPrefs.GetString(GUEST_USERNAME);

        if (string.IsNullOrEmpty(tGuestID)) {
            tUserID = "Guest_" + UnityEngine.Random.Range(0, 500);
            tGuestID = tUserID.GetHashCode().ToString();
        }

        LoginWithCustomIDRequest pRequest = new LoginWithCustomIDRequest() { CustomId = tGuestID, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(pRequest, delegate (LoginResult pResult) 
        {
            PlayerPrefs.SetString(GUEST_KEY, tGuestID);
            PlayerPrefs.SetString(GUEST_USERNAME, tUserID);
            pResponse(true, tUserID);
        } , 
        delegate(PlayFabError pError) 
        {
            pResponse(false, tUserID);
            Debug.Log(pError.Error);
        });
    }
}
