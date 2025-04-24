using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField create_Input;
    public TMP_InputField join_Input;
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(create_Input.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(join_Input.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GamePlay");
    }
}
