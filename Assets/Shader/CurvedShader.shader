Shader "GT01/RoundWorld"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _CurveValueZ("Curve Z",Range(0.00001,0.1)) = 0.01
        _CurveValueX("Curve X",Range(0.00001,0.1)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _CurveValueZ;
        float _CurveValueX;

        void vert (inout appdata_full vertex)
        {
            float4 distance = mul(unity_ObjectToWorld, vertex.vertex);

            distance.xyz -= _WorldSpaceCameraPos.xyz;

            // Z ekseni boyunca kıvrılma
            float curveZ = (distance.z * distance.z + distance.x * distance.x) * -1 * _CurveValueZ;
            // X ekseni boyunca kıvrılma (pozitif X yönüne doğru)
            float curveX = (distance.x * distance.x + distance.z * distance.z) * _CurveValueX;

            // İki kıvrımı birleştir
            distance = float4(curveX, curveZ, 0.0f, 0.0f);

            vertex.vertex += mul(unity_WorldToObject, distance);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
