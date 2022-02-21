#version 330 core

layout (location = 0) in vec3 vertexPos;
out vec4 vertexColor;

uniform vec4 baseColor;

void main()
{
	vertexColor = baseColor;
	gl_Position = vec4(vertexPos, 1.0);
}