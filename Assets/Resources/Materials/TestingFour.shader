Shader "Unlit/Replace"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR] _ColorA ("Color A", Color) = (0.1890176, 0.6430788, 2.357161, 0) 
        [HDR] _ColorB ("Color B", Color) = (0.1890176, 0.6430788, 2.357161, 0)
        _ReplaceColor ("Replace Color", Color) = (1, 1, 1, 1)
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
            
            Cull Back 
            ZWrite Off
            Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.28318530718

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float4 _ColorA;
            float4 _ColorB;
            float4 _ReplaceColor;
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

            // void ReplaceColor(float3 In, float3 From, float3 To, float Range, float Fuzziness, out float3 Out)
            // {
            //     float Distance = distance(From, In);
            //     Out = lerp(To, In, saturate((Distance - Range));
            // }

            fixed4 frag (Interpolators i) : SV_Target
            {
                float4 baseTexture = tex2D(_MainTex, i.uv);
                // float3 test = float3(1, 1, 1)
                // ReplaceColor(baseTexture.rgb, _ReplaceColor.rgb, float3(1, 0, 0), .1, .1, test);


                //return float4(test, 1);
                //baseTexture.rgb *= baseTexture.a;
                if (distance(baseTexture.rgb, _ReplaceColor.rgb) < .5){
                    
                    if (_Outline < 0.5){

                    float4 combined = (baseTexture + ( baseTexture  * (cos((i.uv.y + (cos(i.uv.x * TAU * 8) * 0.01) - _Time.y) * TAU * 5) * 0.5 + 0.5) * _GlowIntensity)) * lerp(_ColorA, _ColorB, i.uv.y);
                    return combined;

                    }
                    float4 combined = baseTexture + (getOutline(i.uv) * lerp(_ColorA, _ColorB, i.uv.y) * (cos((i.uv.y + (cos(i.uv.x * TAU * 8) * 0.01) - _AnimationSpeed) * TAU * 5) * 0.5 + 0.5) * _GlowIntensity);
                    return combined;
                }
                else{
                    return baseTexture;
                    }
                
              
            }

            ENDCG
        }
    }
}