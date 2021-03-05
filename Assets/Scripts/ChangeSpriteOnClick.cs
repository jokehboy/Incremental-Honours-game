using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteOnClick : MonoBehaviour
{
    public Sprite OffSprite;
    public Sprite OnSprite;
    public Button but;
    public void ChangeImagePress()
    {
        but.image.sprite = OffSprite;

    }
    public void ChangeImageRelease()
    {
        but.image.sprite = OnSprite;

    }
}
