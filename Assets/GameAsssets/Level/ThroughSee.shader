Shader "Castom/ThroughSee"
{
    Properties
    {
        _Position("PlayerPosition", Vector) = (0.5, 0.5, 0, 0)
        _Size("Size", Float) = 1
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
        _Opacity("Opacity", Range(0, 1)) = 0.5
        _NoiseIntensity("NoiseIntensity", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags
        {
            // RenderPipeline: <None>
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        Pass
        {
            // Name: <None>
            Tags
            {
                // LightMode: <None>
            }

            // Render State
            // RenderState: <None>

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>

            // Defines
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_PREVIEW
        #define SHADERGRAPH_PREVIEW
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Packing.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"
        #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariables.hlsl"
        #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
        #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 positionWS;
            float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpacePosition;
            float4 ScreenPosition;
            float4 uv0;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float4 interp1 : TEXCOORD1;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float2 _Position;
        float _Size;
        float _Smoothness;
        float _Opacity;
        float _NoiseIntensity;
        CBUFFER_END

        // Object and Global properties

            // Graph Functions
            
        void Unity_Remap_float2(float2 In, float2 InMinMax, float2 OutMinMax, out float2 Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }

        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }

        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }

        void Unity_Multiply_float(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }

        void Unity_OneMinus_float2(float2 In, out float2 Out)
        {
            Out = 1 - In;
        }

        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }

        void Unity_Multiply_float(float A, float B, out float Out)
        {
            Out = A * B;
        }

        void Unity_Divide_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A / B;
        }

        void Unity_Length_float2(float2 In, out float Out)
        {
            Out = length(In);
        }

        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }

        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }

        void NoiseSineWave_float(float In, float2 MinMax, out float Out)
        {
            float sinIn = sin(In);
            float sinInOffset = sin(In + 1.0);
            float randomno =  frac(sin((sinIn - sinInOffset) * (12.9898 + 78.233))*43758.5453);
            float noise = lerp(MinMax.x, MinMax.y, randomno);
            Out = sinIn + noise;
        }


        float2 Unity_GradientNoise_Dir_float(float2 p)
        {
            // Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
            p = p % 289;
            // need full precision, otherwise half overflows when p > 1
            float x = float(34 * p.x + 1) * p.x % 289 + p.y;
            x = (34 * x + 1) * x % 289;
            x = frac(x / 41) * 2 - 1;
            return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
        }

        void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
        { 
            float2 p = UV * Scale;
            float2 ip = floor(p);
            float2 fp = frac(p);
            float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
            float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
            float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
            float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
            fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
            Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
        }

        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }

            // Graph Vertex
            // GraphVertex: <None>

            // Graph Pixel
            struct SurfaceDescription
        {
            float4 Out;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_773f410fcb654cb1bef12e4f90c3aca2_Out_0 = _Smoothness;
            float4 _ScreenPosition_46cd67ffb06844b49aa7e7b1ea19a375_Out_0 = float4(IN.ScreenPosition.xy / IN.ScreenPosition.w, 0, 0);
            float2 _Property_76ac43e5278445ac944e949cc9809dbe_Out_0 = _Position;
            float2 _Remap_268748404bd34adab948c0a212617e3d_Out_3;
            Unity_Remap_float2(_Property_76ac43e5278445ac944e949cc9809dbe_Out_0, float2 (0, 1), float2 (0.5, -1.5), _Remap_268748404bd34adab948c0a212617e3d_Out_3);
            float2 _Add_7f6c888447ce4e65a622c46f341a3c52_Out_2;
            Unity_Add_float2((_ScreenPosition_46cd67ffb06844b49aa7e7b1ea19a375_Out_0.xy), _Remap_268748404bd34adab948c0a212617e3d_Out_3, _Add_7f6c888447ce4e65a622c46f341a3c52_Out_2);
            float2 _TilingAndOffset_5d122e1989124c17a33adb9ef74fc67c_Out_3;
            Unity_TilingAndOffset_float((_ScreenPosition_46cd67ffb06844b49aa7e7b1ea19a375_Out_0.xy), float2 (1, 1), _Add_7f6c888447ce4e65a622c46f341a3c52_Out_2, _TilingAndOffset_5d122e1989124c17a33adb9ef74fc67c_Out_3);
            float2 _Multiply_317d137692b2499eb05688498a40fd4e_Out_2;
            Unity_Multiply_float(_TilingAndOffset_5d122e1989124c17a33adb9ef74fc67c_Out_3, float2(2, 2), _Multiply_317d137692b2499eb05688498a40fd4e_Out_2);
            float2 _OneMinus_61dac3ae58fd48f89cfaec702fa7484a_Out_1;
            Unity_OneMinus_float2(_Multiply_317d137692b2499eb05688498a40fd4e_Out_2, _OneMinus_61dac3ae58fd48f89cfaec702fa7484a_Out_1);
            float _Divide_a164cb2e91684bcf8486f65e92602c75_Out_2;
            Unity_Divide_float(unity_OrthoParams.y, unity_OrthoParams.x, _Divide_a164cb2e91684bcf8486f65e92602c75_Out_2);
            float _Property_4ab960079fe44ce8a2cac7b0f602d2ef_Out_0 = _Size;
            float _Multiply_54a54badcedd4ca7ac7071372db2aa84_Out_2;
            Unity_Multiply_float(_Divide_a164cb2e91684bcf8486f65e92602c75_Out_2, _Property_4ab960079fe44ce8a2cac7b0f602d2ef_Out_0, _Multiply_54a54badcedd4ca7ac7071372db2aa84_Out_2);
            float2 _Vector2_af5f3677e9464590a1525bb247e813ba_Out_0 = float2(_Multiply_54a54badcedd4ca7ac7071372db2aa84_Out_2, _Property_4ab960079fe44ce8a2cac7b0f602d2ef_Out_0);
            float2 _Divide_36956db0aa2f4b10929bb2fb900ecf33_Out_2;
            Unity_Divide_float2(_OneMinus_61dac3ae58fd48f89cfaec702fa7484a_Out_1, _Vector2_af5f3677e9464590a1525bb247e813ba_Out_0, _Divide_36956db0aa2f4b10929bb2fb900ecf33_Out_2);
            float _Length_32b6f024332942c8982c6fc01f0e926d_Out_1;
            Unity_Length_float2(_Divide_36956db0aa2f4b10929bb2fb900ecf33_Out_2, _Length_32b6f024332942c8982c6fc01f0e926d_Out_1);
            float _OneMinus_b983a9a2496743a88792c3b44bf7ba14_Out_1;
            Unity_OneMinus_float(_Length_32b6f024332942c8982c6fc01f0e926d_Out_1, _OneMinus_b983a9a2496743a88792c3b44bf7ba14_Out_1);
            float _Smoothstep_cb345d0fb4324615abe2c59bdfaaeb89_Out_3;
            Unity_Smoothstep_float(0, _Property_773f410fcb654cb1bef12e4f90c3aca2_Out_0, _OneMinus_b983a9a2496743a88792c3b44bf7ba14_Out_1, _Smoothstep_cb345d0fb4324615abe2c59bdfaaeb89_Out_3);
            float _Property_6e5732ffcc414b9e98fadc86f13352ad_Out_0 = _NoiseIntensity;
            float _NoiseSineWave_61652ec796174c4891091c556c855b4c_Out_2;
            NoiseSineWave_float(1, float2 (3, 0.5), _NoiseSineWave_61652ec796174c4891091c556c855b4c_Out_2);
            float _Multiply_7028d6df4cfe4d6ca4360a550a337a04_Out_2;
            Unity_Multiply_float(IN.TimeParameters.x, _NoiseSineWave_61652ec796174c4891091c556c855b4c_Out_2, _Multiply_7028d6df4cfe4d6ca4360a550a337a04_Out_2);
            float _Multiply_b9665547f42040599b70f2a5707697cd_Out_2;
            Unity_Multiply_float(_Property_6e5732ffcc414b9e98fadc86f13352ad_Out_0, _Multiply_7028d6df4cfe4d6ca4360a550a337a04_Out_2, _Multiply_b9665547f42040599b70f2a5707697cd_Out_2);
            float _GradientNoise_bc2c36d9f39e47eebf1d9f7a2af709f0_Out_2;
            Unity_GradientNoise_float(IN.uv0.xy, _Multiply_b9665547f42040599b70f2a5707697cd_Out_2, _GradientNoise_bc2c36d9f39e47eebf1d9f7a2af709f0_Out_2);
            float _Multiply_953abb08790e4112bd51d8fa8255a153_Out_2;
            Unity_Multiply_float(_Smoothstep_cb345d0fb4324615abe2c59bdfaaeb89_Out_3, _GradientNoise_bc2c36d9f39e47eebf1d9f7a2af709f0_Out_2, _Multiply_953abb08790e4112bd51d8fa8255a153_Out_2);
            float _Add_82ad71bbbdaf4c6a9346ae7c07622763_Out_2;
            Unity_Add_float(_Smoothstep_cb345d0fb4324615abe2c59bdfaaeb89_Out_3, _Multiply_953abb08790e4112bd51d8fa8255a153_Out_2, _Add_82ad71bbbdaf4c6a9346ae7c07622763_Out_2);
            float _Property_958f30130c8a4dfca631b27b19948ec6_Out_0 = _Opacity;
            float _Multiply_94ae338c763f4b1b8fb7d2c628b1f472_Out_2;
            Unity_Multiply_float(_Add_82ad71bbbdaf4c6a9346ae7c07622763_Out_2, _Property_958f30130c8a4dfca631b27b19948ec6_Out_0, _Multiply_94ae338c763f4b1b8fb7d2c628b1f472_Out_2);
            surface.Out = all(isfinite(_Multiply_94ae338c763f4b1b8fb7d2c628b1f472_Out_2)) ? half4(_Multiply_94ae338c763f4b1b8fb7d2c628b1f472_Out_2, _Multiply_94ae338c763f4b1b8fb7d2c628b1f472_Out_2, _Multiply_94ae338c763f4b1b8fb7d2c628b1f472_Out_2, 1.0) : float4(1.0f, 0.0f, 1.0f, 1.0f);
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





            output.WorldSpacePosition =          input.positionWS;
            output.ScreenPosition =              ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
            output.uv0 =                         input.texCoord0;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/PreviewVaryings.hlsl"
        #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/PreviewPass.hlsl"

            ENDHLSL
        }
    }
    FallBack "Hidden/Shader Graph/FallbackError"
}