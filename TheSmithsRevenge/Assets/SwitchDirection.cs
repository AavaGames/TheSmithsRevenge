using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDirection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerPlatformerController>().SwitchDirection();

            GameObject tempObject = GameObject.Find("GameManager");

            tempObject.GetComponent<ChunkBuilder>().BuildChunks();
            float yOffset = tempObject.GetComponent<ChunkBuilder>().levelEndYOffset;
            
            GameObject.Find("PlayerGround").GetComponent<followX>().MoveY(-yOffset);
            GameObject.Find("MainCamera").GetComponent<followPlayer>().MoveY(yOffset, true);

            GameManagment.totalScore += 500;
        }
    }
}
