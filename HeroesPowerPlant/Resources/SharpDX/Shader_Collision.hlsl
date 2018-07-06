
cbuffer data :register(b0)
{
	float4x4 viewProjection;
	float4 ambientColor;
	float4 lightDirection;
	float4 lightDirection2;
};

struct VertexShaderInput
{
	float4 position : POSITION;
	float3 normal : NORMAL;
	float4 color : COLOR;
};

struct VertexShaderOutput
{
	float4 position : SV_POSITION;
	float3 normal : NORMAL;
	float4 color : COLOR0;
	float4 color2 : COLOR1;
	float4 lightDirection : POSITION1;
	float4 lightDirection2 : POSITION2;
};

VertexShaderOutput VS(VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	output.position = mul(viewProjection, input.position);
	output.normal = input.normal;
	output.color = input.color;
	output.color2 = ambientColor;
	output.lightDirection = lightDirection;
	output.lightDirection2 = lightDirection2;

	return output;
}

float4 PS(VertexShaderOutput input) : SV_Target
{
	return saturate(dot(input.normal, input.lightDirection) + dot(input.normal, input.lightDirection2)) * input.color + input.color2;
	// return saturate(0.75F + dot(input.normal, lightDirection.xyz)) * input.color;
}