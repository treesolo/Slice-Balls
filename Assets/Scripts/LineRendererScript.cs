using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LineRendererScript : MonoBehaviour
{
    public GameObject trailPrefab;
    private GameObject currentTrail;
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartTrail();
        }
        if (Input.GetMouseButton(0)) 
        {
            UpdateTrail();
        }
    }

    void StartTrail()
    {
        currentTrail = Instantiate(trailPrefab);
        lineRenderer = currentTrail.GetComponent<LineRenderer>();
        points.Clear();

        StartCoroutine(FadeTrail(lineRenderer, 1.5f));
    }

    void UpdateTrail()
    {
        if (lineRenderer == null) return;

        Vector3 mousePosition = GetMouseWorldPosition();
        if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], mousePosition) > 0.1f)
        {
            points.Add(mousePosition);
            lineRenderer.positionCount = points.Count;  
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    IEnumerator FadeTrail(LineRenderer trail, float fadeTime)
    {
        float elapsedTime = 0f;

        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(Color.red, 0.0f),
            new GradientColorKey(Color.red, 1.0f)
        };
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 1.0f)
        };

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeTime);

            if (trail == null) yield break;

            alphaKeys[0].alpha = alpha;
            alphaKeys[1].alpha = alpha;

            gradient.SetKeys(colorKeys, alphaKeys);
            trail.colorGradient = gradient;

            yield return null;
        }

        if (trail != null && trail.gameObject != null)
        {
            Destroy(trail.gameObject);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;     
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}