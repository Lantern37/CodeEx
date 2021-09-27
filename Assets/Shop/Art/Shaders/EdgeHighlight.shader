// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/EdgeHighlight"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {} 
		_Treshold ("Treshold", Float) = 0.2 
	}
	SubShader
	{
		// No culling or depth
		// Cull Off ZWrite Off ZTest Always
		ZTest Always Cull Off ZWrite Off Fog { Mode off }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragment option ARB_precision_hint_fastest
			
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex; 
			uniform float4 _MainTex_TexelSize; 
			uniform float _Treshold;

			// struct appdata
			// {
			// 	float4 vertex : POSITION;
			// 	float2 uv : TEXCOORD0;
			// };

			struct v2f
			{
				float4 pos : POSITION; 
				float2 uv[3] : TEXCOORD0;
			};

			// v2f vert (appdata v)
			// {
			// 	v2f o;
			// 	o.vertex = UnityObjectToClipPos(v.vertex);
			// 	o.uv = v.uv;
			// 	return o;
			// }

			v2f vert( appdata_img v ) 
			{ 
				v2f o; o.pos = UnityObjectToClipPos (v.vertex); 
				float2 uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord); 
				o.uv[0] = uv; o.uv[1] = uv + float2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y); 
				o.uv[2] = uv + float2(+_MainTex_TexelSize.x, -_MainTex_TexelSize.y); 
				return o; 
			}

			half4 frag (v2f i) : COLOR 
			{ 
				half4 original = tex2D(_MainTex, i.uv[0]);
				half3 p1 = original.rgb;
 				half3 p2 = tex2D(_MainTex, i.uv[1]).rgb;
 				half3 p3 = tex2D(_MainTex, i.uv[2]).rgb;
 
 				half3 diff = p1 * 2 - p2 - p3;
 				half len = dot(diff,diff);

 				if(len == _Treshold){
   				  original.xyzw = half4(1,0,0,0.5);
				}
     
 				return original;
			}

			
			// sampler2D _MainTex;

			// fixed4 frag (v2f i) : SV_Target
			// {
			// 	fixed4 col = tex2D(_MainTex, i.uv);
			// 	// just invert the colors
			// 	col.rgb = 1 - col.rgb;
			// 	return col;
			// }
			ENDCG
		}
	}
}


// Shader "Hidden/Edge Detect X" { Properties { MainTex ("Base (RGB)", 2D) = "white" {} Treshold ("Treshold", Float) = 0.2 }

// SubShader { Pass { ZTest Always Cull Off ZWrite Off Fog { Mode off }

// CGPROGRAM #pragma vertex vert #pragma fragment frag #pragma fragmentoption ARB_precision_hint_fastest #include "UnityCG.cginc"

// uniform sampler2D MainTex; uniform float4 MainTex_TexelSize; uniform float _Treshold;

// struct v2f { float4 pos : POSITION; float2 uv[3] : TEXCOORD0; };

// v2f vert( appdata_img v ) { v2f o; o.pos = mul (UNITY_MATRIX_MVP, v.vertex); float2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord ); o.uv[0] = uv; o.uv[1] = uv + float2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y); o.uv[2] = uv + float2(+_MainTex_TexelSize.x, -_MainTex_TexelSize.y); return o; }

// half4 frag (v2f i) : COLOR { half4 original = tex2D(_MainTex, i.uv[0]);

//  // a very simple cross gradient filter
//  half3 p1 = original.rgb;
//  half3 p2 = tex2D( _MainTex, i.uv[1] ).rgb;
//  half3 p3 = tex2D( _MainTex, i.uv[2] ).rgb;
 
//  half3 diff = p1 * 2 - p2 - p3;
//  half len = dot(diff,diff);
//  if( len &gt;= _Treshold )
//      original.xyzw = half4(1f,0f,0f,0.5f);
     
//  return original;
// } ENDCG } }

// Fallback off

// }