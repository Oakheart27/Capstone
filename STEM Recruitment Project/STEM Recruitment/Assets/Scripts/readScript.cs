using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class NewBehaviourScript : MonoBehaviour
{

    void readTextFile(string file_path)
    {
    StreamReader inp_stm = new StreamReader(file_path);

    while (!inp_stm.EndOfStream)
    {
        string inp_ln = inp_stm.ReadLine();
        // Do Something with the input. 
    }

    inp_stm.Close();
    }
}
