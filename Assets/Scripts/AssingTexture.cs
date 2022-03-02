using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssingTexture : MonoBehaviour
{
    public ComputeShader shader;
    public int textResolution = 256;

    Renderer rend;
    RenderTexture outTex; //outputTexture
    int kernelHandle;

    
    // Start is called before the first frame update
    void Start()
    {
        outTex = new RenderTexture(textResolution, textResolution, 0);
        outTex.enableRandomWrite = true;
        outTex.Create();       

        rend = GetComponent<Renderer>();
        rend.enabled = true; 

        InitShader();
    }

    private void InitShader()
    {
        kernelHandle = shader.FindKernel("CSMain");
        shader.SetTexture(kernelHandle, "Result", outTex);
        rend.material.SetTexture("_MainTex", outTex);
        DispatchShader(textResolution/16, textResolution/16);
    }

    private void DispatchShader(int x, int y){
        shader.Dispatch(kernelHandle, x, y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.U)){
            DispatchShader( textResolution / 8, textResolution / 8);
        }
    }
}
