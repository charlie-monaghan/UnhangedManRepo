using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitGame : MonoBehaviour
{    
    public void Quit()
    {      
        
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #else      
    Application.Quit();
    #endif
    }
}
