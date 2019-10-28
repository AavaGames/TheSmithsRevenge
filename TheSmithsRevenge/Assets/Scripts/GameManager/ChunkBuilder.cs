using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBuilder : MonoBehaviour
{
    public GameObject[] forestNormal;
    public GameObject[] forestSlowDown;
    public GameObject[] forestSpeedUp;
    public GameObject forestResetSpeed;
    public GameObject forestStart;
    public GameObject spaceWall;
    public GameObject[] spaceNormal;
    public GameObject[] spaceSlowDown;
    public GameObject[] spaceSpeedUp;
    public GameObject spaceResetSpeed;
    public GameObject spaceStart;
    public GameObject forestWall;
    public int chunkSize = 58;
    private int buildRightChunkSize;
    private int buildLeftChunkSize;
    private bool buildForest = true;
    private GameObject tempObject;
    public Transform chunkParent;
    private Vector3 chunkPlacement = Vector3.zero;
    private enum ChunkType {NORMAL, SLOWDOWN, SPEEDUP}
    private ChunkType currentType = ChunkType.NORMAL;
    public int maxSpecialChunks = 3;
    public int maxLevelLength = 20;
    public int levelEndYOffset = -35;

    /*
    
    On BuildChunks

    Place 20 blocks 

    Start in the normal cycle if you fall on slow or speed stay there for 1-3 blocks then reset speed and continue in normal

    */

    private void Start() {
        buildRightChunkSize = chunkSize;
        buildLeftChunkSize = chunkSize * -1;
    }
    public void BuildChunks()
    {
        GameObject chunk = null;
        int temp = 0;
        int otherTemp = 0;
        int previousNum = -1;
        int incrementer = 1;
        int chunkSize = 0;
        bool reset = true;

        if (buildForest)
        {
            chunkSize = buildRightChunkSize;
        }
        else if (!buildForest)
        {
            chunkSize = buildLeftChunkSize;
        }

        if (buildForest)
        {
            for(int i = 0; i < maxLevelLength; i++)
            {
                chunkPlacement.x += chunkSize;
                
                //randomize chunk inside normal block
                if (currentType == ChunkType.NORMAL)
                {
                    do
                    {
                        temp = Random.Range(1, forestNormal.Length + 1);
                    } while (temp == previousNum);

                    
                    if (temp == forestNormal.Length)
                    {
                        previousNum = -1;
                        currentType = ChunkType.SPEEDUP;
                    }
                    else if (temp == forestNormal.Length - 1)
                    {
                        previousNum = -1;
                        currentType = ChunkType.SLOWDOWN;
                    }
                    previousNum = temp;
                    temp--;
                    
                    chunk = forestNormal[temp];
                }
                else if (currentType == ChunkType.SLOWDOWN)
                {
                    if (reset == true)
                    {
                        incrementer = 1;
                        reset = false;

                        otherTemp = Random.Range(1, maxSpecialChunks + 1);
                    }

                    if (incrementer <= otherTemp && i != maxLevelLength - 1)
                    {
                        do
                        {
                            temp = Random.Range(1, forestSlowDown.Length + 1);
                        } while (temp == previousNum);

                        previousNum = temp;
                        temp--;

                        chunk = forestSlowDown[temp];

                        incrementer++;
                    }
                    else
                    {
                        chunk = forestResetSpeed;
                        currentType = ChunkType.NORMAL;
                        reset = true;
                        previousNum = -1;
                    }
                }
                else if (currentType == ChunkType.SPEEDUP)
                {
                    if (reset == true)
                    {
                        incrementer = 1;
                        reset = false;

                        otherTemp = Random.Range(1, maxSpecialChunks + 1);
                    }

                    if (incrementer <= otherTemp && i != maxLevelLength - 1)
                    {
                        do
                        {
                            temp = Random.Range(1, forestSpeedUp.Length + 1);
                        } while (temp == previousNum);

                        previousNum = temp;
                        temp--;

                        chunk = forestSpeedUp[temp];

                        incrementer++;
                    }
                    else
                    {
                        chunk = forestResetSpeed;
                        currentType = ChunkType.NORMAL;
                        reset = true;
                        previousNum = -1;
                    }
                }

                tempObject = Instantiate(chunk, chunkPlacement, Quaternion.identity);
                tempObject.transform.SetParent(chunkParent);
            }

            chunkPlacement.y += levelEndYOffset;

            chunkPlacement.x += chunkSize;

            chunk = spaceStart;

            tempObject = Instantiate(chunk, chunkPlacement, Quaternion.identity);
            tempObject.transform.SetParent(chunkParent);

            chunkPlacement.x += chunkSize;

            chunk = spaceWall;

            tempObject = Instantiate(chunk, chunkPlacement, Quaternion.identity);
            tempObject.transform.SetParent(chunkParent);

            chunkPlacement.x -= chunkSize;
        }
        else if (!buildForest)
        {
            for(int i = 0; i < maxLevelLength; i++)
            {
                chunkPlacement.x += chunkSize;
                //randomize chunk inside normal block
                if (currentType == ChunkType.NORMAL)
                {
                    do
                    {
                        temp = Random.Range(1, spaceNormal.Length + 1);
                    } while (temp == previousNum);

                    
                    if (temp == spaceNormal.Length)
                    {
                        previousNum = -1;
                        currentType = ChunkType.SPEEDUP;
                    }
                    else if (temp == spaceNormal.Length - 1)
                    {
                        previousNum = -1;
                        currentType = ChunkType.SLOWDOWN;
                    }
                    previousNum = temp;
                    temp--;
                    
                    chunk = spaceNormal[temp];
                }
                else if (currentType == ChunkType.SLOWDOWN)
                {
                    if (reset == true)
                    {
                        incrementer = 1;
                        reset = false;

                        otherTemp = Random.Range(1, maxSpecialChunks + 1);
                    }

                    if (incrementer <= otherTemp && i != maxLevelLength - 1)
                    {
                        do
                        {
                            temp = Random.Range(1, spaceSlowDown.Length + 1);
                        } while (temp == previousNum);

                        previousNum = temp;
                        temp--;

                        chunk = spaceSlowDown[temp];

                        incrementer++;
                    }
                    else
                    {
                        chunk = spaceResetSpeed;
                        currentType = ChunkType.NORMAL;
                        reset = true;
                        previousNum = -1;
                    }
                }
                else if (currentType == ChunkType.SPEEDUP)
                {
                    if (reset == true)
                    {
                        incrementer = 1;
                        reset = false;

                        otherTemp = Random.Range(1, maxSpecialChunks + 1);
                    }

                    if (incrementer <= otherTemp && i != maxLevelLength - 1)
                    {
                        do
                        {
                            temp = Random.Range(1, spaceSpeedUp.Length + 1);
                        } while (temp == previousNum);

                        previousNum = temp;
                        temp--;

                        chunk = spaceSpeedUp[temp];

                        incrementer++;
                    }
                    else
                    {
                        chunk = spaceResetSpeed;
                        currentType = ChunkType.NORMAL;
                        reset = true;
                        previousNum = -1;
                    }
                }

                tempObject = Instantiate(chunk, chunkPlacement, Quaternion.identity);
                tempObject.transform.SetParent(chunkParent);
            }

            chunkPlacement.y += levelEndYOffset;

            chunkPlacement.x += chunkSize;

            chunk = forestStart;

            tempObject = Instantiate(chunk, chunkPlacement, Quaternion.identity);
            tempObject.transform.SetParent(chunkParent);

            chunkPlacement.x += chunkSize;

            chunk = forestWall;

            tempObject = Instantiate(chunk, chunkPlacement, Quaternion.identity);
            tempObject.transform.SetParent(chunkParent);

            chunkPlacement.x -= chunkSize;
        }
        buildForest = !buildForest;
    }
}
