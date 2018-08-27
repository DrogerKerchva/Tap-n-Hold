using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFloor : MonoBehaviour
{
    public TowerFloor prevFloor;
    Transform tr;
    float mainSize = -1f;
    void Start()
    {
        tr = transform;
    }

    void Update()
    {

    }

    public void StartChain()
    {
        StartCoroutine(AnimFirst());
        StartCoroutine(CallNext());
    }

    void AnimChain()
    {
        StartCoroutine(Anim());
        StartCoroutine(CallNext());
    }

    IEnumerator CallNext()
    {
        yield return new WaitForSeconds(0.2f);
        if (prevFloor != null)
            prevFloor.AnimChain();
    }

    IEnumerator AnimFirst()
    {
        if (mainSize <= 0) mainSize = tr.localScale.x;
        for (float i = 0; i <= 0.4f; i += 0.05f)
        {
            tr.localScale = new Vector3(mainSize + i, 0.25f, mainSize + i);
            yield return new WaitForFixedUpdate();
        }
        for (float i = 0.4f; i >= 0.2f; i -= 0.05f)
        {
            tr.localScale = new Vector3(mainSize + i, 0.25f, mainSize + i);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator Anim()
    {
        if (mainSize <= 0) mainSize = tr.localScale.x;
        for (float i = 0; i <= 0.3f; i += 0.05f)
        {
            tr.localScale = new Vector3(mainSize + i, 0.25f, mainSize + i);
            yield return new WaitForFixedUpdate();
        }
        float ms = mainSize * 0.8f;
        for (float i = tr.localScale.x; i >= ms; i -= 0.05f)
        {
            tr.localScale = new Vector3(i, 0.25f, i);
            yield return new WaitForFixedUpdate();
        }
    }

    public void Mark()
    {
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
}