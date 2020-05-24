// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Utils/Unlit Prototype Ground"
{
	Properties
	{
		_HorizonColor("Horizon Color", Color) = (0,0,0,0)
		_ShadowColor("Shadow Color", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		LOD 200
		Cull Back
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#pragma target 3.0
		#pragma surface surf StandardCustomLighting keepalpha noambient novertexlights nolightmap  nodynlightmap nodirlightmap nometa noforwardadd 
		struct Input
		{
			half filler;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform half4 _HorizonColor;
		uniform half4 _ShadowColor;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float4 temp_cast_0 = (1.0).xxxx;
			float4 lerpResult5 = lerp( _ShadowColor , temp_cast_0 , ase_lightAtten);
			c.rgb = ( _HorizonColor * lerpResult5 ).rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=17601
1927;7;1906;1014;-243.6007;242.1336;1.3;True;False
Node;AmplifyShaderEditor.ColorNode;2;1152,384;Half;False;Property;_ShadowColor;Shadow Color;2;0;Create;True;0;0;False;0;0,0,0,0;0.6096476,0.7360675,0.9433962,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LightAttenuation;6;1152,640;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;1152,560;Half;False;Constant;_Float3;Float 3;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;1046,-67;Half;False;Property;_HorizonColor;Horizon Color;1;0;Create;True;0;0;False;0;0,0,0,0;0.6792453,0.6792453,0.6792453,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;5;1408,384;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;42;1267.235,1143.411;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;1856.568,1057.411;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;35;1285.568,856.4111;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;829.5695,861.4111;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;41;1018.235,1147.411;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;31;1036.568,860.4111;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;36;2023.795,913.3181;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;53;1004.344,121.1982;Half;False;Property;_LineColor;Line Color;0;0;Create;True;0;0;False;0;0,0,0,0;0.1568627,0.1764705,0.1960784,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;56;1246.344,240.1982;Inherit;False;52;myVarName;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;55;1359.344,72.19815;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;2314.426,937.7944;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;52;2539.185,931.5483;Inherit;False;myVarName;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;1600,128;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;47;-30.31233,1754.604;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;28;132.5694,832.4111;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TFHCRemapNode;58;398.921,996.1199;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;57;426.921,800.1199;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;44;-271.3446,1715.143;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;46;-607.8008,1888.118;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceCameraPos;45;-631.345,1695.143;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;48;-321.3137,1926.604;Inherit;False;Property;_Float0;Float 0;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-310.3138,2024.604;Inherit;False;Property;_Float5;Float 5;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;764.2365,1318.411;Inherit;False;Constant;_Float2;Float 2;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;29;669.5696,862.4111;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;782.5695,1031.411;Inherit;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;39;811.2365,1148.411;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;60;479.7275,1431.598;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;173.1179,1457.088;Inherit;False;Property;_LineWidtahFar;Line Widtah Far;4;0;Create;True;0;0;False;0;0.01;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;167.3368,1383.011;Inherit;False;Property;_LineWidth;Line Width;3;0;Create;True;0;0;False;0;0.01;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;38;651.2366,1149.411;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;50;192.6877,1780.604;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2365.9,129.3;Float;False;True;-1;2;;200;0;CustomLighting;Utils/Unlit Prototype Ground;False;False;False;False;True;True;True;True;True;False;True;True;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;200;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;2;0
WireConnection;5;1;26;0
WireConnection;5;2;6;0
WireConnection;42;0;41;0
WireConnection;43;0;35;0
WireConnection;43;1;42;0
WireConnection;35;0;31;0
WireConnection;30;0;29;0
WireConnection;30;1;60;0
WireConnection;41;0;39;0
WireConnection;41;1;40;0
WireConnection;31;0;30;0
WireConnection;31;1;33;0
WireConnection;36;0;43;0
WireConnection;55;0;4;0
WireConnection;55;1;53;0
WireConnection;55;2;56;0
WireConnection;51;0;36;0
WireConnection;51;1;50;0
WireConnection;52;0;51;0
WireConnection;27;0;4;0
WireConnection;27;1;5;0
WireConnection;47;0;44;0
WireConnection;47;1;48;0
WireConnection;47;2;49;0
WireConnection;58;0;28;3
WireConnection;57;0;28;1
WireConnection;44;0;45;0
WireConnection;44;1;46;0
WireConnection;29;0;57;0
WireConnection;39;0;38;0
WireConnection;39;1;60;0
WireConnection;60;0;37;0
WireConnection;60;1;59;0
WireConnection;60;2;50;0
WireConnection;38;0;58;0
WireConnection;50;0;47;0
WireConnection;0;13;27;0
ASEEND*/
//CHKSM=98A0D0073952B0EEC8522531BF08C1C391D22481