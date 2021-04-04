using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductGain : MonoBehaviour
{
    public GameObject popUp;

    public Animator animator;

    public TextMeshProUGUI theText;

    public void Play()
    {
        animator.Play("ProductGain");
        Destroy(transform.parent.gameObject, 2.2f);
    }

    public void ChangeText(float productAmount)
    {
        theText.text = $"+{productAmount}P";
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
