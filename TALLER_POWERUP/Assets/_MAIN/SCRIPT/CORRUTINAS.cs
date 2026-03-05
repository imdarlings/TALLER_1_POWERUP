using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CORRUTINAS : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Spawn());
        StopAllCoroutines();
    }

// Update is called once per frame
    void InstantineEsfera()
    {
        Instantiate(prefab, new Vector3(2,2,0), Quaternion.identity);
    }

     IEnumerator Spawn()
    {
        InstantineEsfera();
        yield return new WaitForSeconds(5);
        InstantineEsfera();
        yield return new WaitForSeconds(5);
        InstantineEsfera();
        yield return new WaitForSeconds(5);
    }

}