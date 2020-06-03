using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerName : MonoBehaviour
{
    public InputField nametf;
    public Button setNameBtn;
    public Button createBtn;
    public Button joinBtn;
    public Button joinDevRoom;
    
    void Start()
    {
        setNameBtn.interactable = false;
        createBtn.interactable = false;
        joinBtn.interactable = false;
        joinDevRoom.interactable = false;
    }
    public void OnTFChange(string value)
    {
        if(nametf.text.Length > 2 && nametf.text.Length < 10)
        {
            setNameBtn.interactable = true;
        } 
        else{
            setNameBtn.interactable = false;
        }
    }

    public void Onclick_SetName()
    {
        PhotonNetwork.NickName = nametf.text;
        if(nametf.text.Length > 2 && nametf.text.Length < 12)
        {
            createBtn.interactable = true;
            joinBtn.interactable = true;
            joinDevRoom.interactable = true;
        }
        else{
            createBtn.interactable = false;
            joinBtn.interactable = false;
            joinDevRoom.interactable = false;
        }
    }
}
