using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTest : MonoBehaviour
{
    // Start is called before the first frame update
    float[,] A;
    float[,] B;
    float[,] C;

    double first;
    double last;
    void Start()
    {
        A  =new float[2560,2560];
        B = new float[2560,2560];
        C = new float[2560,2560];

       first = Time.realtimeSinceStartup;
        for(int j= 0; j<2560;j++){
            for(int k=0; k<2560;k++){
                for(int i=0;i<2560;i++){
                    C[i, j] += A[i, k] * B[k, j];
                }
            }
        }
        last = Time.realtimeSinceStartup;
        Debug.Log(C[5,6]);

        double elapsed = last - first;
        Debug.Log(elapsed + " Secs");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
