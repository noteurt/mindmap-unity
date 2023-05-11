using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeText : MonoBehaviour
{
    public string text;
    
    public TMP_Text inputField1;
    public TMP_Text  inputField2;
    public TMP_Text  inputField3;
    public TMP_Text inputField4;
    // Start is called before the first frame update
    void Start()
    {
        inputField1.text = text;
        inputField2.text = text;
        inputField3.text = text;
        inputField4.text = text;
    }
}
