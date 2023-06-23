using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePixelCount : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D inputTexture;

    private Color[] vector;
    public ComputeShader computeShader;
    private int count;
    private int redPixelCount;

    private double first;
    private double last;
    private bool once;
    void Start()
    {
        once = true;
        
        int kernel = computeShader.FindKernel("CountRedPixels");
        
      
            ComputeBuffer resultBuffer = new ComputeBuffer(1, sizeof(int));
            resultBuffer.SetData(new int[] { 0 });

            computeShader.SetBuffer(kernel, "result", resultBuffer);
            computeShader.SetTexture(kernel, "inputTexture", inputTexture);
            first = Time.realtimeSinceStartup;
            computeShader.Dispatch(kernel, inputTexture.width / 8, inputTexture.height / 8, 1);
            last = Time.realtimeSinceStartup;
            int[] resultArray = new int[1];
            resultBuffer.GetData(resultArray);
            redPixelCount = resultArray[0];

            Debug.Log(redPixelCount + " Puntos (Computed) " + inputTexture.name);

            resultBuffer.Release();
            
            
            
            double elapsed = last - first;
        
            Debug.Log(" Computed elapsed: " + elapsed + "Secs " + inputTexture.name );
    }

    // Update is called once per frame
    void Update()
    {
        if (once)
        {
            count = 0;
            vector = inputTexture.GetPixels();
            first = Time.realtimeSinceStartup;
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i].r == 1 && vector[i].a != 0)
                    count++;
            }
            last = Time.realtimeSinceStartup;
            Debug.Log(count + " Puntos (Secuencial) " + inputTexture.name);
            once = false;
            
            double elapsed = last - first;
            Debug.Log("Lineal elapsed: " + elapsed + "Secs " + inputTexture.name );
        }

    }
    }
