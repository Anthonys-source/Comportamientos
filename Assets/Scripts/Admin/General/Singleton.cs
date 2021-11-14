using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class
{
    protected static T s_Instance;

    public static T GetInst()
    {
        return s_Instance;
    }
}
