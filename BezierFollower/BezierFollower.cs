using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollower : MonoBehaviour
{
    [SerializeField]
    private List<Route> routes;

    [SerializeField]
    [Tooltip("起始式就移動")]
    bool _autoMove;

    private int routeToGo;

    private float tParam;

    private Vector3 objectPosition;

    //private float speedModifier;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    void Init()
	{
        routeToGo = 0;
        tParam = 0f;
        //speedModifier = 0.5f;
        coroutineAllowed = _autoMove;
    }

    public float _moveDuration;

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;
        var n = routes[routeNum].ControlPoints.Count - 1;

        float time = 0;
        while (tParam < 1)
        {
            objectPosition = Vector3.zero;
            //tParam += Time.deltaTime * speedModifier;
            time += Time.deltaTime;
            tParam = Mathf.Min(time / routes[routeNum].MoveDuration, 1.0f);


            for (int i = 0; i <= n; i++)
            {
                objectPosition += nCr(n, i) * routes[routeNum].ControlPoints[i].position * Mathf.Pow(1 - tParam, n - i) * Mathf.Pow(tParam, i);
            }
            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Count - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;

    }

    static public int nCr(int n, int r)
    {
        return fact(n) / (fact(r) *
                      fact(n - r));
    }

    // Returns factorial of n
    static int fact(int n)
    {
        int res = 1;
        for (int i = 2; i <= n; i++)
            res = res * i;
        return res;
    }
}
