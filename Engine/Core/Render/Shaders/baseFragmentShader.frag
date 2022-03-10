#version 330 core

in vec2 uv;

out vec4 outColor;

uniform sampler2D texture0;
uniform vec4 baseColor;
uniform vec2 offset;
uniform vec2 tiling;

void main()
{
	outColor = texture(texture0, uv * tiling + offset) * baseColor;
}
