Shader "Custom/tattoo" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Layer1 ("Layer1 (RGB)", 2D) = "white" {}
		_Layer2 ("Layer2 (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _Layer1;
		sampler2D _Layer2;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 basecol = tex2D (_MainTex, IN.uv_MainTex);
			half4 layer1col = tex2D (_Layer1, IN.uv_MainTex);
			half4 layer2col = tex2D (_Layer2, IN.uv_MainTex);
			o.Albedo = basecol.rgb * layer1col * layer2col;
			o.Alpha = basecol.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}


