using System.Collections;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    bool trueOrFalse;

    void Start()
    {
        StartCoroutine(Coroutine1());

        StartCoroutine(Coroutine3());
    }


    private IEnumerator Coroutine1()
    {
        if (!trueOrFalse) yield break;

        yield return Coroutine2();
    }


    private IEnumerator Coroutine2()
    {
        yield return null;
    }


    private IEnumerator Coroutine3()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("Une seconde est passée.");

        yield return new WaitForSeconds(1);

        Debug.Log("Une autre seconde est passée.");
    }


    private IEnumerator LerpCoroutine(GameObject objectToLerp, Vector3 start, Vector3 end)
    {
        float u = 0f;
        while (u <= 1f)
        {            
            objectToLerp.transform.position = Vector3.Lerp(start, end, u);            
            u += Time.deltaTime * 0.5f;            
            yield return null;
        }
        Debug.Log("Le mouvement a duré deux secondes.");
    }
}

