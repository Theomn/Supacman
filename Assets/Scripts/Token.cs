using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            GameAccessor.Instance().level.TakeToken();
            gameObject.SetActive(false);
        }
    }
}
