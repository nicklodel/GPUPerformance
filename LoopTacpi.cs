using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTacpi : MonoBehaviour
{
    private float[,,] A;

    private float[,,] B;

    private float[,,,] C;

    private float[,,,] D;
    // Start is called before the first frame update
    void Start()
    {
        A = new float[120, 120, 120];
        B = new float [120, 120, 120];
        C = new float[120, 120, 120, 120];
        D = new float[120, 120, 120, 120];


        double first;
        double last;

        first = Time.realtimeSinceStartup;
        for (int k = 0; k < 120; k++)
        {
            for (int l = 0; l < 120; l++)
            {
                for (int j = 0; j < 120; j++)
                {
                    for (int i = 0; i < 120; i++)
                    {
                        B[k, l, i] += A[i, k, j] + C[l, j, k, i] * D[k, j, l, i];
                        
                    }
                }
            }
        }
        last = Time.realtimeSinceStartup;
        double elapsed = last - first;

        Debug.Log(elapsed + " secs");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
