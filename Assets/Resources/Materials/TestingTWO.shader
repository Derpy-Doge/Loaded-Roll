Shader "Unlit/TestingStuff2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR] _ColorA ("Color A", Color) = (0.1890176, 0.6430788, 2.357161, 0) 
        [HDR] _ColorB ("Color B", Color) = (0.1890176, 0.6430788, 2.357161, 0)
        [Toggle] _Outline ("Outline", Float) = 1
        [HideInInspector]_AnimationSpeed ("Animation Speed", Float) = 1 
        _GlowIntensity ("Glow Intensity", Float) = 2
        _OutlineSize ("Outline Size", Float) = 0.01
    }

    SubShader
    {
        Tags { "RenderType"="Transparent"
                "Queue"="Transparent" }
        
        Pass{
            
            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.28318530718

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float4 _ColorA;
            float4 _ColorB;
            float _AnimationSpeed;
            float _GlowIntensity;
            float _OutlineSize;
            float _Outline;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float getOutline(float2 uv)
            {
                float offset = _OutlineSize;

                float a = tex2D(_MainTex, uv).a;
                float up = tex2D(_MainTex, uv + float2(0, offset)).a;
                float down = tex2D(_MainTex, uv - float2(0, offset)).a;
                float left = tex2D(_MainTex, uv - float2(offset, 0)).a;
                float right = tex2D(_MainTex, uv + float2(offset, 0)).a;

                float maxNeighbor = max(max(up, down), max(left, right));

                return saturate(maxNeighbor - a);
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                float4 baseTexture = tex2D(_MainTex, i.uv);
                baseTexture.rgb *= baseTexture.a;
                //float4 combined = baseTexture + (getOutline(i.uv) * lerp(_ColorA, _ColorB, i.uv.y) * (cos((i.uv.y + (cos(i.uv.x * TAU * 8) * 0.01) - _Time.y * 0.2) * TAU * 5) * 0.5 + 0.5) * _GlowIntensity);
                if (_Outline < 0.5){
                    float4 combined = (baseTexture + ( baseTexture  * (cos((i.uv.y + (cos(i.uv.x * TAU * 8) * 0.01) - _AnimationSpeed) * TAU * 5) * 0.5 + 0.5) * _GlowIntensity)) * lerp(_ColorA, _ColorB, i.uv.y);
                    return combined;

                    }
                float4 combined = baseTexture + (getOutline(i.uv) * lerp(_ColorA, _ColorB, i.uv.y) * (cos((i.uv.y + (cos(i.uv.x * TAU * 8) * 0.01) - _AnimationSpeed) * TAU * 5) * 0.5 + 0.5) * _GlowIntensity);
                return combined;
                
              
            }

            ENDCG
        }
    }
}