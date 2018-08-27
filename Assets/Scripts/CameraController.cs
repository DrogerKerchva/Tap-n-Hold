using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform tr;

    [SerializeField] float targetHeigth;

    //reset data
    Quaternion defaultR;
    Vector3 defaultP;

    void Start()
    {
        tr = transform;
        defaultR = tr.rotation;
        defaultP = tr.position;
    }

    public void LookDown(float h)
    {
        Vector3 begPoint = Camera.main.WorldToViewportPoint(Vector3.zero);
        Vector3 endPoint = Camera.main.WorldToViewportPoint(Vector3.up * h);
        float targetDis, dis;
        dis = Vector3.Distance(begPoint, endPoint);
        targetDis = (dis > 0.9f) ? dis * 0.3f : 0;

        tr.LookAt(Vector3.up * h * 0.3f);
        tr.position += tr.forward * -targetDis;
    }

    public void Reset()
    {
        tr.rotation = defaultR;
        tr.position = defaultP;
    }

    public void UpdateHeight(float _th)
    {
        targetHeigth = _th;
        if (!runing)
            StartCoroutine(Upd());
    }

    bool runing = false;
    IEnumerator Upd()
    {
        runing = true;
        while (tr.position.y < targetHeigth)
        {
            tr.position += Vector3.up * Time.fixedDeltaTime;
            if (tr.position.y > targetHeigth)
                tr.position = new Vector3(tr.position.x, targetHeigth, tr.position.z);
            yield return new WaitForFixedUpdate();
        }
        runing = false;
    }


}
