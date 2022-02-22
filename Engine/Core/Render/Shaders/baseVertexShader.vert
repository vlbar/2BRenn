#version 330 core

layout (location = 0) in vec3 aVertexPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 uv;

void main()
{
	uv = aTexCoord;
	gl_Position = vec4(aVertexPos, 1.0);
}