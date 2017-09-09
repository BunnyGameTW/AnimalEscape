using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //非運行時也觸發效果
    [ExecuteInEditMode]
    //屏幕後處理特效一般都需要綁定在攝像機上
    [RequireComponent(typeof(Camera))]
    //提供一個後處理的基類，主要功能在於直接通過Inspector面板拖入shader，生成shader對應的材質
    public class PostEffect : MonoBehaviour
    {

        //Inspector面板上直接拖入
        public Shader shader = null;
        private Material _material = null;
        public Material _Material
        {
            get
            {
                if (_material == null)
                    _material = GenerateMaterial(shader);
                return _material;
            }
        }

        //根據shader創建用於屏幕特效的材質
        protected Material GenerateMaterial(Shader shader)
        {
            if (shader == null)
                return null;
            //需要判斷shader是否支持
            if (shader.isSupported == false)
                return null;
            Material material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            if (material)
                return material;
            return null;
        }

    }


