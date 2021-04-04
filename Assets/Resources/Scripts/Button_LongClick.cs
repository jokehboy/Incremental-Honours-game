using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Button_LongClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    public float reqHoldTime;

    public UnityEvent onLongClick;

    [SerializeField]
    Image fillImage;

    [SerializeField]
    Color buttonPress, buttonRelease;





    private void Start()
    {
        this.GetComponent<Image>().color = buttonRelease;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        this.GetComponent<Image>().color = buttonPress;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = buttonRelease;
        Reset();
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = pointerDownTimer / reqHoldTime;
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= reqHoldTime)
            {
                if(onLongClick!=null)
                {
                    onLongClick.Invoke();

                    Reset();
                }

            }

            fillImage.fillAmount = pointerDownTimer / reqHoldTime;
        }
    }

}