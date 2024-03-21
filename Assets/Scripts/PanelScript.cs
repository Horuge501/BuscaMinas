using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        GetComponent<Animator>().SetTrigger("Close");

    }

    public void SetTimePlay()
    {
        Time.timeScale = 1;
    }
}
