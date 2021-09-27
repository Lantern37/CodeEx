Shader "Custom/DissolveDirectionStandart" {
	 Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "white" {}

        // _Glossiness ("Smoothness", Range(0,1)) = 0.5
        // _Metallic ("Metallic", Range(0,1)) = 0.0

        // _SliceGuide ("Slice Guide", 2D) = "white" {}
        // _SliceAmount ("Slice Amount", Range(0, 1)) = 0.0
        // _BurnRamp ("Burn Ramp", 2D) = "white" {}
        // _BurnSize ("Burn Size", Range(0, 1)) = 0.15
        // _EmissionAmount ("Emission Amount", float) = 2.0

//directional
 		_DissolveAmount("Dissolve amount", Range(0,1)) = 0
        _Direction("Direction", vector) = (0,1,0,0)
        [HDR]_Emission("Emission", Color) = (1,1,1,1)
		_EmissionThreshold("Emission threshold", float) = 0.1
      	_NoiseSize("Noise size", float ) = 1

    }
    SubShader {
        Tags { "RenderType"="Opaque" "DisableBatching" = "True"}
        LOD 200
		Cull Off

        CGPROGRAM
        // #pragma surface surf Standard addshadow
		#pragma surface surf Lambert addshadow vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        // sampler2D _SliceGuide;
        // sampler2D _BurnRamp;

        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpMap;

			 float3 worldPosAdj;
        };

        // half _Glossiness;
        // half _Metallic;
        fixed4 _Color;

        // half _SliceAmount;
        // half _BurnSize;
        // half _EmissionAmount;


//direction
		 float _DissolveAmount;
        half4 _Direction;
        fixed4 _Emission;
        float _EmissionThreshold;
        float _NoiseSize;

 void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.worldPosAdj =  mul (unity_ObjectToWorld, v.vertex.xyz);
        }
 
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
 
        float random (float2 input) { 
            return frac(sin(dot(input, float2(12.9898,78.233)))* 43758.5453123);
        }
//direction






        void surf (Input IN, inout SurfaceOutput o) {
            // Albedo comes from a texture tinted by color


//from normal dissolve
            // fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            // half test = tex2D(_SliceGuide, IN.uv_MainTex).rgb - _SliceAmount;
            // clip(test);

            // // I skipped the _BurnColor here 'cause I was getting enough 
            // // colour from the BurnRamp texture already.
            // if (test < _BurnSize && _SliceAmount > 0) {
            //     o.Emission = tex2D(_BurnRamp, float2(test  *(1 / _BurnSize), 0)) * _EmissionAmount;
            // }

//from directional
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			 //Clipping
            half test = (dot(IN.worldPosAdj, normalize(_Direction)) + 1) / 2;
            clip (test - _DissolveAmount);

            //Emission noise
            float squares = step(0.5, random(floor(IN.uv_MainTex  * _NoiseSize) * _DissolveAmount));
            half emissionRing = step(test - _EmissionThreshold, _DissolveAmount) * squares;
 ////////////////


            o.Albedo = c.rgb * _Color.rgb;
			// o.Albedo = c.rgb;
			 o.Emission = _Emission * emissionRing;
            // Apply normal mapping.
            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));


            // o.Metallic = _Metallic;

            // My Albedo map has smoothness in its Alpha channel.
            // o.Smoothness = _Glossiness * c.a;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}