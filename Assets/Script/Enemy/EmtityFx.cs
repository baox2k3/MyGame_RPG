using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmtityFx : MonoBehaviour
{
  private SpriteRenderer sr;

  [Header("Flash FX")] 
  [SerializeField] private float flashDuration;
  [SerializeField] private Material hitMat;
   private Material originaMat;

   [Header("Ailment colors")] 
   [SerializeField] private Color[] igniteColor;
   [SerializeField] private Color[] chillColor;
   [SerializeField] private Color[] shockColor;
  private void Start()
  {
    sr = GetComponentInChildren<SpriteRenderer>();
    originaMat = sr.material;
  }

  private IEnumerator FlashFx()
  {
    sr.material = hitMat;
    Color currentColor = sr.color;
    sr.color = Color.white;

    yield return new WaitForSeconds(flashDuration);

    sr.color = currentColor;
    sr.material = originaMat;
    
    
  }

  private void RedColorBlink()
  {
    if(sr.color != Color.white)
      sr.color = Color.white;
    else
      sr.color = Color.red;
    

  }

  private void CancelColorChange()
  {
    CancelInvoke();
    sr.color = Color.white;
  }

  public void IgniteFxFor(float _seconds)
  {
    InvokeRepeating("IgniteColorFx",0,.3f);
    Invoke("CancelColorChange",_seconds);
  }

  private void IgniteColorFx()
  {
    if (sr.color != igniteColor[0])
      sr.color = igniteColor[0];
    else
    {
      sr.color = igniteColor[1];
    }
  }

  
  
  public void ChillFxFor(float _seconds)
  {
    InvokeRepeating("ChillColorFx",0,.3f);
    Invoke("CancelColorChange",_seconds);
  }

  private void ChillColorFx()
  {
    if (sr.color != chillColor[0])
      sr.color = chillColor[0];
    else
    {
      sr.color = chillColor[1];
    }
  }

  public void ShockFxFor(float _seconds)
  {
    InvokeRepeating("ShockColorfx",0,.3f);
    Invoke("CancelColorChange",_seconds);
  }

  private void ShockColorfx()
  {
    if (sr.color != shockColor[0])
      sr.color = shockColor[0];
    else
    {
      sr.color = shockColor[1];
    }
  }

 
}
