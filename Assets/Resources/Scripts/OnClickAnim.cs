using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class OnClickAnim : MonoBehaviour
{
    public GameObject popUp;

    public GameController game;

    public Animator animator;

    public TextMeshProUGUI theText;

    public void Play()
    {
        animator.Play("ClickGain");
        Destroy(transform.parent.gameObject, 1.0f);
    }

    public void ChangeText(BigDouble multiplier)
    {
        theText.text = $"+{game.UpdateNotation(multiplier, "F3")}";
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
