#version 330 core

in vec2 uv;
in vec4 color;

out vec4 outColor;

uniform sampler2D texture0;

void main()
{
	outColor = texture(texture0, uv) * color;
}