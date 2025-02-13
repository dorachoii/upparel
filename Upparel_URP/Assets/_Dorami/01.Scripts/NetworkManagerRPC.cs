using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.Mathematics;

public class NetworkManagerRPC : MonoBehaviourPunCallbacks
{
   
   void Awake()
   {
        Screen.SetResolution(960, 540, false);
        // 1. 바로 서버 연결
        PhotonNetwork.ConnectUsingSettings();
   }

    // 2. 서버 연결되면 callback으로 방 만들어서 들어간다.
   public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 6}, null);

   public override void OnJoinedRoom() 
   {
        // 3. 방에 들어가면 동기화할 물체Prefab을 생성한다.
        // 여기에 선택된 캐릭터 정보를 넘겨줘야 함!
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1,0), quaternion.identity);
        //PhotonNetwork.Instantiate("Duck Variant", new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));

   }
}
