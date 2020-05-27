using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : IEnumerator
{
    private int _idx = 0;
    public bool MoveNext()
    {
        Debug.LogError("_idx " + _idx);
        return ++_idx < 10;
    }

    public void Reset()
    {
    }

    public object Current
    {
        get
        {
            return null;
        }
    }

}

public class TestCoroutine : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Debug.LogError("start1");
        // var a = StartCoroutine(Init());

        // Debug.LogError("start2 " + a.ToString());

        // StartCoroutine(coroutineA());

        // Debug.Log("start1");
        // StartCoroutine(Test());
        // Debug.Log("start2");

        var t = new Test();
        StartCoroutine(t);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Init()
    {
        // yield return StartCoroutine(init1());
        yield return new WaitForSeconds(2);
        Debug.LogError("init1 finished");

        yield return StartCoroutine(init2());
        Debug.LogError("init2 finished");

        yield return StartCoroutine(init3());
        Debug.LogError("init3 finished");
    }

    // IEnumerator Test()
    // {
    //     Debug.LogError("test1");
    // 	// yield return new WaitForSeconds(3);
    // 	yield return new WaitForEndOfFrame();
    //     Debug.LogError("test2");

    // }

    IEnumerator init1()
    {
        Debug.LogError("init1");
        yield return new WaitForSeconds(2);
        Debug.LogError("init1 end");
    }

    IEnumerator init2()
    {
        Debug.LogError("init2");
        yield return new WaitForSeconds(2);
        Debug.LogError("init3 end");
    }

    IEnumerator init3()
    {
        Debug.LogError("init3");
        yield return new WaitForSeconds(2);
        Debug.LogError("init3 end");
    }

    IEnumerator coroutineA()
    {
        // wait for 1 second
        Debug.Log("coroutineA created");
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(coroutineB());
        Debug.Log("coroutineA running again");
    }

    IEnumerator coroutineB()
    {
        Debug.Log("coroutineB created");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("coroutineB enables coroutineA to run");
    }

    IEnumerator Test()
    {
        Debug.Log("test1");
        yield return StartCoroutine(DoSomething());
        Debug.Log("test2");
    }

    IEnumerator DoSomething()
    {
        Debug.Log("load 1");
        yield return null;
        Debug.Log("load 2");
    }
}
