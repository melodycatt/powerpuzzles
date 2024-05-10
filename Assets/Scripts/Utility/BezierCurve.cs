using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class BezierCurve
{
    public Vector3[] Points;

    public BezierCurve(int i)
    {
        Points = new Vector3[i];
    }

    public BezierCurve(Vector3[] Points)
    {
        this.Points = Points;
    }

    public BezierCurve(Vector3 s, Vector3 e)
    {
        Points = InterpolateEnds(s, e);
    }

    private Vector3[] InterpolateEnds(Vector3 s, Vector3 e)
    {
        Vector3[] output = new Vector3[10];
        float dx = e.x - s.x;
        output[0] = s;
        output[1] = s;
        output[2] = new Vector3(s.x + dx / 3, s.y);
        output[3] = new Vector3(s.x + dx / 3, s.y);
        output[4] = new Vector3(s.x + dx / 3 + (dx / 3 * 0.8f), s.y);
        output[5] = new Vector3(s.x + dx / 3 + (dx / 3 * 0.2f), e.y);
        output[6] = new Vector3(e.x - dx / 3, e.y);
        output[7] = new Vector3(e.x - dx / 3, e.y);
        output[8] = e;
        output[9] = e;
        return output;
    }

    public void Render(LineRenderer lr)
    {
        lr.SetPositions(GetSegments(50));
    }

    public void UpdateEnd(Vector3 e, LineRenderer lr)
    {
        Points = InterpolateEnds(StartPosition, e);
        Render(lr);
    }

    public void UpdateStart(Vector3 s, LineRenderer lr)
    {
        Points = InterpolateEnds(s, EndPosition);
        Render(lr);
    }

    public void Update(Vector3 s, Vector3 e, LineRenderer lr)
    {
        Points = InterpolateEnds(s, e);
        Render(lr);
    }

    public Vector3 StartPosition
    {
        get
        {
            return Points[0];
        }
    }

    public Vector3 EndPosition
    {
        get
        {
            return Points[^1];
        }
    }

    // Equations from: https://en.wikipedia.org/wiki/B%C3%A9zier_curve
    public Vector3 GetSegment(float Time)
    {
        Time = Mathf.Clamp01(Time);
        float time = 1 - Time;
        Vector3 output = Vector3.zero;
        for (int i = 0; i < Points.Length; i++)
        {
            output += GetBinCoeff(Points.Length - 1, i) * Mathf.Pow(time, Points.Length - 1 - i) * Mathf.Pow(Time, i) * Points[i];
        }
        return output;
    }

    public static long GetBinCoeff(long N, long K)
    {
        // This function gets the total number of unique combinations based upon N and K.
        // N is the total number of items.
        // K is the size of the group.
        // Total number of unique combinations = N! / ( K! (N - K)! ).
        // This function is less efficient, but is more likely to not overflow when N and K are large.
        // Taken from:  http://blog.plover.com/math/choose.html
        //
        long r = 1;
        long d;
        if (K > N) return 0;
        for (d = 1; d <= K; d++)
        {
            r *= N--;
            r /= d;
        }
        return r;
    }

    public Vector3[] GetSegments(int Subdivisions)
    {
        Vector3[] segments = new Vector3[Subdivisions];

        float time;
        for (int i = 0; i < Subdivisions; i++)
        {
            time = (float)i / Subdivisions;
            segments[i] = GetSegment(time);
        }

        return segments;
    }
}