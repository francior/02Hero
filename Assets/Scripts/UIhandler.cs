using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class UIhandler : MonoBehaviourPunCallbacks
{
    public InputField createRoomTF;
    public InputField joinRoomTF;
   public void OnClick_JoinRoom()
   {
       PhotonNetwork.JoinRoom(joinRoomTF.text, null);
   }
   public void OnClick_DevRoom()
   {
       PhotonNetwork.JoinOrCreateRoom("DevRoom", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default, null);
   }
   public void OnClick_CreateRoom()
   {
       PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions {MaxPlayers = 4},null);
   }

   public override void OnJoinedRoom()
   {
       print("Room Joined Bien");
       PhotonNetwork.LoadLevel(1);
   }

   public override void OnJoinRandomFailed(short returnCode, string message)
   {
       print("RoomError"+returnCode+" Message: "+message);
   }
}
