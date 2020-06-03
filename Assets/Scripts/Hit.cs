
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class Hit : MonoBehaviourPun
{
    public float speed = 10;
    public float destroyTime = 2f;
    public bool hitLeft = false;

    IEnumerator destroyHit()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("destroy",RpcTarget.AllBuffered);
    }
    
    void Update()
    {
        if(!hitLeft)
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    } 

    [PunRPC]
    public void destroy()
    {
        Destroy(this.gameObject);
    }

    [PunRPC]
    public void changeDirection()
    {
        hitLeft = true;
    }
}
