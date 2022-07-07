using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _controlPoints;

    public List<Transform> ControlPoints { get { return _controlPoints; } }

    [SerializeField]
    float _moveDuration;
    public float MoveDuration { get {return _moveDuration; } }


    private Vector3 _gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            _gizmosPosition = Vector3.zero;
            int n = _controlPoints.Count - 1;
            for (int i = 0; i <= n; i++)
			{
                _gizmosPosition += BezierFollower.nCr(n, i) * _controlPoints[i].position * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
            }


            Gizmos.DrawSphere(_gizmosPosition, 0.25f);
        }

		for (int i = 0; i < _controlPoints.Count - 1; i++)
		{
            Gizmos.DrawLine(new Vector2(_controlPoints[i].position.x, _controlPoints[i].position.y), new Vector2(_controlPoints[i + 1].position.x, _controlPoints[i + 1].position.y));
        }
        //Gizmos.DrawLine(new Vector2(_controlPoints[0].position.x, _controlPoints[0].position.y), new Vector2(_controlPoints[1].position.x, _controlPoints[1].position.y));
        //Gizmos.DrawLine(new Vector2(_controlPoints[2].position.x, _controlPoints[2].position.y), new Vector2(_controlPoints[3].position.x, _controlPoints[3].position.y));

    }
}
