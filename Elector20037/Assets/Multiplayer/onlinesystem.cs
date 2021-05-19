using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class onlinesystem :Photon.MonoBehaviour
{
  public string ServerName,playerNickname;
 public byte maxplayer;
 public GameObject serversList; 
 public GameObject serverButn; 
  public Button[] btuons; 
 public GameObject Cmra;
  public TMP_Dropdown dropDown;
        private void Awake() {
            PhotonNetwork.autoJoinLobby=true;
            
            PhotonNetwork.ConnectUsingSettings("V1");
        }
       public GameObject connectionTXT;
     public UnityEngine.UI.Button servers;
     public UnityEngine.UI.Button createserver;
    // Update is called once per frame
    void Update()
    {
       if (connectionTXT.gameObject.activeSelf==true)
       {
           createserver.interactable=false;
           servers.interactable=false;
       }
    }
    
        public void odakur()
        {
             Debug.Log("a1");
            if (PhotonNetwork.countOfRooms>0)
            {
                Debug.Log("as");
                for (int i = 0; i < PhotonNetwork.countOfRooms; i++)
                    {
                
                        RoomInfo[] rooms=PhotonNetwork.GetRoomList();
                    
                        if (rooms[i].Name!=ServerName)
                        {
                            
                        PhotonNetwork.CreateRoom(ServerName,new RoomOptions(){MaxPlayers=maxplayer},TypedLobby.Default);
                        }
                        else
                        {
                        Debug.Log("exits server name please change");  
                        }
                    }
            }
            else
                PhotonNetwork.CreateRoom(ServerName,new RoomOptions(){MaxPlayers=maxplayer},TypedLobby.Default);
            
        }

        public void ListServers()
        {
             RoomInfo[] rooms=PhotonNetwork.GetRoomList();
            for (int i = 0; i < PhotonNetwork.countOfRooms; i++)
            {
                Debug.Log(PhotonNetwork.countOfRooms);

                if (serversList.transform.Find(rooms[i].Name)==null)
                {
                GameObject tutGm= Instantiate(serverButn,serversList.transform);
                tutGm.name=rooms[i].Name;
                tutGm.GetComponent<Button>().onClick.AddListener(delegate{JoinRoom(tutGm.name);});
                tutGm.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=rooms[i].Name;
                tutGm.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=rooms[i].PlayerCount.ToString()+"/"+rooms[i].MaxPlayers.ToString();
                }   
            }

        }
      
        public void JoinRoom(string name)
        {
      
            PhotonNetwork.JoinRoom(name);
        }
        public void GetName(TMP_InputField text)
        {
            playerNickname=text.text;
        }
         public void GetServerName(TMP_InputField text)
        {
            ServerName=text.text;
            maxplayer=Convert.ToByte(dropDown.options[dropDown.value].text);
            odakur();
        }
 
        void OnJoinedLobby()
        {
            connectionTXT.gameObject.SetActive(false);
            Debug.Log("Lobiye Katıldı");
        }
        public void Onediting(TMP_InputField text)
        {
            text.text="";
        
        }
        public void Deselect(TMP_InputField text)
        {
            if (text.text=="")
            {
                text.text="You need to enter your nickname...";
                   FalseButtons(btuons[0]);
                FalseButtons(btuons[1]);
            }
            else
            {
                OnEndEdit(btuons[0]);
                OnEndEdit(btuons[1]);
            }
        }
         
        public void OnEndEdit(Button buttons)
        {
            buttons.interactable=true;
        }
         public void FalseButtons(Button buttons)
        {
            buttons.interactable=false;
        }
        void OnJoinedRoom()
        {
         
            PhotonNetwork.playerName=playerNickname;
            
            PhotonNetwork.LoadLevel("SampleScene");
           
        }
        
}
