using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CriticalClick : MonoBehaviour
{
    public GameObject popUp;

    public Animator animator;

    public TextMeshProUGUI theText;

    public void Play()
    {
        animator.Play("CriticalClick");
        Destroy(gameObject, 2.2f);
    }

    public void ChangeText(float multiplier)
    {
        theText.text = $"{multiplier}x";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
