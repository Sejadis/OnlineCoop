using System.Collections;
using UnityEngine;

public class Wait : MonoBehaviour
{
    WaitForSecondsRealtime _wait;

    private void Start()
    {
        _wait = new WaitForSecondsRealtime(3f);
        StartCoroutine(CoroutineWait());
        StartCoroutine(Delay());
    }

    private IEnumerator CoroutineWait()
    {
        while (true)
        {
            Debug.Log("TICK " + Time.time);
            yield return _wait;
        }
    }

    private IEnumerator CoroutineWait2()
    {
        while (true)
        {
            Debug.Log("tick " + Time.time);
            yield return _wait;
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(CoroutineWait2());
    }
}