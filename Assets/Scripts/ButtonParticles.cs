using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonParticles : MonoBehaviour
{
    public ParticleSystem coinEffect;
    public int coinEffect_particleNumber;

    bool spawnParticles = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveEffectOverTime(0.3f));
    }

    // Update is called once per frame
    void Update()
    {
        coinEffect.maxParticles = coinEffect_particleNumber;

        if(coinEffect_particleNumber > 15 ) { coinEffect_particleNumber = 15; }
    }


    public void ButtonClick()
    {
        coinEffect_particleNumber++;
    }

    private void RemoveEffect()
    {
        if(coinEffect_particleNumber >= 1)
        {
            coinEffect_particleNumber--;
        }
    }

    IEnumerator RemoveEffectOverTime(float secondsBetween)
    {
        while(spawnParticles == true)
        {
            yield return new WaitForSeconds(secondsBetween);

            RemoveEffect();
        }
    }
}
