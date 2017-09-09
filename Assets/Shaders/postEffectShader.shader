// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//shader的目錄
Shader "Custom/ColorAdjustEffect"
{
	//屬性塊，shader用到的屬性，可以直接在Inspector面板調整
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Brightness("Brightness", Float) = 1
		_Saturation("Saturation", Float) = 1
		_Contrast("Contrast", Float) = 1
		_Color("Color",Color) = (0,0,0,1)
		_Color("Color2",Color) = (0,0,0,1)

	}

	//每個shader都有Subshaer，各個subshaer之間是平行關系，只可能運行一個subshader，主要針對不同硬件
	SubShader
	{
		//真正幹活的就是Pass了，一個shader中可能有不同的pass，可以執行多個pass
		Pass
			{
				//設置一些渲染狀態，此處先不詳細解釋
				ZTest Always Cull Off ZWrite Off
				
				CGPROGRAM
				//在Properties中的內容只是給Inspector面板使用，真正聲明在此處，註意與上面一致性
				sampler2D _MainTex;
				half _Brightness;
				half _Saturation;
				half _Contrast;
				float4 _Color;
				float4 _Color2;

				//vert和frag函數
				#pragma vertex vert
				#pragma fragment frag
				#include "Lighting.cginc"

				 fixed ColorLerp(fixed3 tmp_nowcolor,fixed3 tmp_FilterfColor) 
				{ 
					fixed3 dis = fixed3(abs(tmp_nowcolor.x - tmp_FilterfColor.x),abs(tmp_nowcolor.y - tmp_FilterfColor.y),abs(tmp_nowcolor.z - tmp_FilterfColor.z)); 
					fixed dis0 =sqrt(pow(dis.x,2)+pow(dis.y,2)+pow(dis.z,2)); 
					fixed maxdis = sqrt(3); 
					fixed dis1 = lerp(0,maxdis,dis0); 
					return dis1; 
				}
				//從vertex shader傳入pixel shader的參數
				struct v2f
				{
					float4 pos : SV_POSITION; //頂點位置
					half2  uv : TEXCOORD0;	  //UV坐標
				};

				//vertex shader
				//appdata_img：帶有位置和一個紋理坐標的頂點著色器輸入
				v2f vert(appdata_img v)
				{
					v2f o;
					//從自身空間轉向投影空間
					o.pos = UnityObjectToClipPos(v.vertex);
					//uv坐標賦值給output
					o.uv = v.texcoord;
					return o;
				}

				//fragment shader
				fixed4 frag(v2f i) : SV_Target
				{
					//從_MainTex中根據uv坐標進行采樣
					fixed4 renderTex = tex2D(_MainTex, i.uv);
					//brigtness亮度直接乘以一個系數，也就是RGB整體縮放，調整亮度
					fixed3 finalColor = renderTex * _Brightness;
					//saturation飽和度：首先根據公式計算同等亮度情況下飽和度最低的值：
					fixed gray = 0.2125 * renderTex.r + 0.7154 * renderTex.g + 0.0721 * renderTex.b;
					fixed3 grayColor = fixed3(gray, gray, gray);
					//根據Saturation在飽和度最低的圖像和原圖之間差值
					finalColor = lerp(grayColor, finalColor, _Saturation);
					//contrast對比度：首先計算對比度最低的值
					fixed3 avgColor = fixed3(0.5, 0.5, 0.5);
					//根據Contrast在對比度最低的圖像和原圖之間差值
					finalColor = lerp(avgColor, finalColor, _Contrast);
					//返回結果，alpha通道不變
					//renderTex.a *= ColorLerp(finalColor , _Color);
					finalColor-= _Color.rgb;
					finalColor-= _Color2.rgb;

					return fixed4(finalColor, renderTex.a);
				}
					ENDCG
		}
	}
	//防止shader失效的保障措施
	FallBack Off
}