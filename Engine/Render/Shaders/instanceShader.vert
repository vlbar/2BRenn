#version 330 core

layout (location = 0) in vec3 aVertexPos;
layout (location = 1) in vec2 aTexCoords;
layout (location = 2) in vec3 aNormal;

 layout(location = 3) in vec4 aModelMatrix0;
 layout(location = 4) in vec4 aModelMatrix1;
 layout(location = 5) in vec4 aModelMatrix2;
 layout(location = 6) in vec4 aModelMatrix3;

out vec2 uv;
out vec3 normal;
out vec3 fragPos;

uniform mat4 view;
uniform mat4 projection;

void main()
{
	uv = aTexCoords;
	mat4 model = mat4(aModelMatrix0, aModelMatrix1, aModelMatrix2, aModelMatrix3);
	normal = mat3(transpose(inverse(model))) * aNormal;
    fragPos = vec3(model * vec4(aVertexPos, 1.0f));
    gl_Position = projection * view * vec4(fragPos, 1.0f);
}