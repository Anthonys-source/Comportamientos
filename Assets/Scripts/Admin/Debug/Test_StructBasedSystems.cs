using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_StructBasedSystems : MonoBehaviour
{
    private void Start()
    {
        InputStruct input = new InputStruct();
        input.m_A = 3;
        Debug.Log($"Pre: {input.m_A}");

        OutputStruct output = new OutputStruct();
        SystemUpdate(in input, ref output);

        Debug.Log($"Post: {output.m_OutputA}");
    }

    public void SystemUpdate(in InputStruct i, ref OutputStruct o)
    {
        o.m_OutputA = i.m_A;
    }
}


public struct InputStruct
{
    public int m_A;
    public int m_B;
}

public struct OutputStruct
{
    public int m_OutputA;
}