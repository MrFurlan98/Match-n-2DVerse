using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
public partial class Backend : MonoBehaviour {

    ///<summary> That's request to server to a new guest player using a hash id of player guest username </summary>
    public void GuestLogin(Action<bool, string> pResponse, string pDisplayName = "")
    {
        new DeviceAuthenticationRequest()
            .SetDisplayName(pDisplayName)
            .Send((response) =>
            { 
                SaveAuthenticationToken(response.AuthToken);

                pResponse(!response.HasErrors, response.DisplayName);
            });
    }

    public bool Logged()
    {
        UserAuthentication tAuthentication = PersistInformation.LoadData<UserAuthentication>(PersistInformation.INFO_TYPE.AUTHENTICATION);

        return !string.IsNullOrEmpty(tAuthentication.m_AuthToken);
    }

    internal void SaveAuthenticationToken(string pToken)
    {
        PersistInformation.PersistData(PersistInformation.INFO_TYPE.AUTHENTICATION, new UserAuthentication { m_AuthToken = pToken });
    }

}
