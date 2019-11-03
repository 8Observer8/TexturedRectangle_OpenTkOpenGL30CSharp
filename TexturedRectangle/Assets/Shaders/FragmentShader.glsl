#version 130

in vec2 vTexCoord;

out vec4 fragColor;

uniform sampler2D uSampler;

void main()
{
	fragColor = texture(uSampler, vTexCoord);
}
