using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_manager : MonoBehaviour
{

    public static level_manager main;

    public Transform StartNode;

    public Transform[] path;

    private void Awake(){
        main = this;
    }

}
