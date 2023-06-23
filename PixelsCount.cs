using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelsCount : MonoBehaviour
{
    public Texture2D[] inputTexture;

    private Color[] vector;
    public ComputeShader computeShader;
    private int count;
    private int redPixelCount;

    private double first;
    private double last;
    private bool once;
    
    // Start is called before the first frame update
    void Start()
    {
        once = true;
        
        int kernel = computeShader.FindKernel("CountRedPixels");
        first = Time.realtimeSinceStartup;
        for (int i = 0; i < inputTexture.Length; i++)
        {
            ComputeBuffer resultBuffer = new ComputeBuffer(1, sizeof(int));
            resultBuffer.SetData(new int[] { 0 });

            computeShader.SetBuffer(kernel, "result", resultBuffer);
            computeShader.SetTexture(kernel, "inputTexture", inputTexture[i]);
            computeShader.Dispatch(kernel, inputTexture[i].width / 8, inputTexture[i].height / 8, 1);
            int[] resultArray = new int[1];
            resultBuffer.GetData(resultArray);
            redPixelCount = resultArray[0];

            Debug.Log(redPixelCount + " Puntos (Computed)");

            resultBuffer.Release();
        }
        last = Time.realtimeSinceStartup;

        double elapsed = last - first;
        
        Debug.Log(" Computed elapsed: " + elapsed + "Secs" );
        /*int kernel = compute.FindKernel("CSMain");
        result = new RenderTexture(512, 512,24);
        result.enableRandomWrite = true;
        result.Create();
        
        compute.SetTexture(kernel,"Result",result);
        compute.Dispatch(kernel, 512/8,512/8,1);
        once = true;*/
    }

   

    // Update is called once per frame
    void Update()
    {
        if (once)
        {
            first= Time.realtimeSinceStartup;
            count = 0;
            for (int j = 0; j < inputTexture.Length; j++)
            {
                count = 0;
                vector = inputTexture[j].GetPixels();
                for (int i = 0; i < vector.Length; i++)
                {
                    if (vector[i].r == 1.0f && vector[i].a != 0)
                        count++;
                }
                Debug.Log(count + " Puntos (Secuencial)");
            }
            
            last = Time.realtimeSinceStartup;
            double elapsed = last - first;
         
            Debug.Log("Lineal elapsed: " + elapsed + "Secs" );
            once = false;
        }
    }
}
