using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//繼承自PostEffectBase
public class ColorAdjustEffect : PostEffect
{

    //通過Range控制可以輸入的參數的範圍
    [Range(0.0f, 3.0f)]
    public float brightness = 1.0f;//亮度
    [Range(0.0f, 3.0f)]
    public float contrast = 1.0f;  //對比度
    [Range(0.0f, 3.0f)]
    public float saturation = 1.0f;//飽和度
    public Color color = Color.white;
    public Color color2 = Color.white;

    //覆寫OnRenderImage函數
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //僅僅當有材質的時候才進行後處理，如果_Material為空，不進行後處理
        if (_Material)
        {
            //通過Material.SetXXX（"name",value）可以設置shader中的參數值
            _Material.SetColor("_Color", color);
            _Material.SetColor("_Color2", color2);

            _Material.SetFloat("_Brightness", brightness);
            _Material.SetFloat("_Saturation", saturation);
            _Material.SetFloat("_Contrast", contrast);
            //使用Material處理Texture，dest不一定是屏幕，後處理效果可以疊加的！
            Graphics.Blit(src, dest, _Material);
        }
        else
        {
            //直接繪制
            Graphics.Blit(src, dest);
        }
    }
}
