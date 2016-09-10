Shader "CullOffTransparent" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200
		Cull Off

		CGPROGRAM
		#pragma surface surf Lambert alpha
			struct Input {
				float4 color : COLOR;
			};

			float4 _Color;

			void surf (Input IN, inout SurfaceOutput o) {
				o.Albedo = _Color.rgb;
				o.Alpha = _Color.a;
			}
		ENDCG
	}
	FallBack "Diffuse"
}
