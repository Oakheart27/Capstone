using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] fallingObjectsPrefabs;



    [Range(0.5f, 2f)]
    [SerializeField]
    float minTimeInterval = 1;

    [Range(2f, 4f)]
    [SerializeField]
    float maxTimeInterval = 2;
    float halfWidth;

    float[] positions = { -450, -150, 150, 450 };


    public void StartSpawn(float widthImage)
    {
        halfWidth = widthImage / 2;
        StartCoroutine(SpawnObject(10f,0));
        StartCoroutine(SpawnObject(10f, 1));
        StartCoroutine(SpawnObject(10f, 2));
        StartCoroutine(SpawnObject(10f, 3));

    }

    IEnumerator SpawnObject(float waitingTime,int pos)
    {
        yield return new WaitForSeconds(waitingTime);
        float randX = positions[pos];
        Vector3 localSpawnPosition = new Vector3(randX, 0, 0);

        GameObject currentObject = Instantiate(fallingObjectsPrefabs[pos]);
        currentObject.transform.SetParent(gameObject.transform, true);
        currentObject.transform.localPosition = localSpawnPosition;
        StartCoroutine(SpawnObject(waitingTime,pos));
    }
}
