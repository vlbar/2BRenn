﻿using OpenTK;
using TwoBRenn.Engine.Render.Utils;

namespace TwoBRenn.ObjectsSetups
{
    public enum EnvironmentType
    {
        AdFlag,
        AdStand,
        AdPlane,
        Spruce,
    }

    class EnvironmentMeshFactory
    {
        public static Mesh CreateMesh(EnvironmentType type)
        {
            switch (type)
            {
                case EnvironmentType.AdFlag:
                    return new Mesh
                    {
                        Vertices = new[]
                        {
                            new Vector3(0.03f, 4f, -0.03f),
                            new Vector3(0.03f, 0f, -0.03f),
                            new Vector3(0.03f, 4f, 0.03f),
                            new Vector3(0.03f, 0f, 0.03f),
                            new Vector3(-0.03f, 4f, -0.03f),
                            new Vector3(-0.03f, 0f, -0.03f),
                            new Vector3(-0.03f, 4f, 0.03f),
                            new Vector3(-0.03f, 0f, 0.03f),
                            new Vector3(0.03f, 1f, 0.03f),
                            new Vector3(0.03f, 4f, 0.03f),
                            new Vector3(0.03f, 1f, -0.03f),
                            new Vector3(0.03f, 4f, -0.03f),
                            new Vector3(1f, 1.5f, 0.03f),
                            new Vector3(1f, 4.5f, 0.03f),
                            new Vector3(1f, 1.5f, -0.03f),
                            new Vector3(1f, 4.5f, -0.03f),
                            new Vector3(0.5f, 1.35f, -0.03f),
                            new Vector3(0.5f, 4.35f, -0.03f),
                            new Vector3(0.5f, 1.35f, 0.03f),
                            new Vector3(0.5f, 4.35f, 0.03f),
                            new Vector3(0.03f, 4f, 0.03f),
                            new Vector3(-0.03f, 4f, 0.03f),
                            new Vector3(-0.03f, 0f, 0.03f),
                            new Vector3(-0.03f, 4f, 0.03f),
                            new Vector3(-0.03f, 4f, -0.03f),
                            new Vector3(0.03f, 4f, -0.03f),
                            new Vector3(0.03f, 4f, 0.03f),
                            new Vector3(0.03f, 0f, 0.03f),
                            new Vector3(-0.03f, 0f, -0.03f),
                            new Vector3(-0.03f, 4f, -0.03f),
                            new Vector3(0.03f, 4f, -0.03f),
                            new Vector3(0.03f, 0f, -0.03f),
                            new Vector3(1f, 1.5f, -0.03f),
                            new Vector3(1f, 4.5f, -0.03f),
                            new Vector3(0.5f, 1.35f, -0.03f),
                            new Vector3(1f, 1.5f, -0.03f),
                            new Vector3(1f, 1.5f, 0.03f),
                            new Vector3(0.5f, 1.35f, 0.03f),
                            new Vector3(0.5f, 4.35f, -0.03f),
                            new Vector3(0.03f, 4f, 0.03f),
                            new Vector3(0.5f, 4.35f, 0.03f),
                            new Vector3(1f, 4.5f, -0.03f),
                            new Vector3(0.5f, 4.35f, -0.03f),
                            new Vector3(0.5f, 4.35f, 0.03f),
                            new Vector3(1f, 4.5f, 0.03f),
                            new Vector3(0.5f, 1.35f, -0.03f),
                            new Vector3(0.5f, 1.35f, 0.03f),
                            new Vector3(0.03f, 1f, 0.03f),
                            new Vector3(1f, 1.5f, 0.03f),
                            new Vector3(1f, 4.5f, 0.03f),
                            new Vector3(0.5f, 4.35f, 0.03f),
                            new Vector3(0.5f, 1.35f, 0.03f),
                            new Vector3(0.03f, 1f, -0.03f),
                            new Vector3(0.03f, 4f, -0.03f),
                            new Vector3(0.5f, 4.35f, -0.03f),
                            new Vector3(0.5f, 1.35f, -0.03f),
                        },
                        UVs = new[]
                        {
                            new Vector2(0.18f, 0.38f),
                            new Vector2(0.94f, 0.38f),
                            new Vector2(0.19f, 0.36f),
                            new Vector2(0.94f, 0.37f),
                            new Vector2(0.17f, 0.36f),
                            new Vector2(0.94f, 0.39f),
                            new Vector2(0.18f, 0.35f),
                            new Vector2(0.94f, 0.35f),
                            new Vector2(-0.06f, 1f),
                            new Vector2(0.9f, 1f),
                            new Vector2(-0.03f, 0.69f),
                            new Vector2(0.84f, 0.69f),
                            new Vector2(0.13f, 1f),
                            new Vector2(0.9f, 0.96f),
                            new Vector2(0.13f, 0.98f),
                            new Vector2(0.89f, 0.95f),
                            new Vector2(0.07f, 0.83f),
                            new Vector2(0.9f, 0.82f),
                            new Vector2(0.05f, 0.84f),
                            new Vector2(1.02f, 0.84f),
                            new Vector2(0.19f, 0.36f),
                            new Vector2(0.18f, 0.35f),
                            new Vector2(0.94f, 0.41f),
                            new Vector2(0.18f, 0.4f),
                            new Vector2(0.18f, 0.39f),
                            new Vector2(0.18f, 0.38f),
                            new Vector2(0.19f, 0.36f),
                            new Vector2(0.94f, 0.37f),
                            new Vector2(0.94f, 0.39f),
                            new Vector2(0.18f, 0.39f),
                            new Vector2(0.18f, 0.38f),
                            new Vector2(0.94f, 0.38f),
                            new Vector2(0.13f, 0.98f),
                            new Vector2(0.89f, 0.95f),
                            new Vector2(0.07f, 0.83f),
                            new Vector2(0.13f, 0.98f),
                            new Vector2(0.11f, 0.98f),
                            new Vector2(0.05f, 0.84f),
                            new Vector2(0.9f, 0.82f),
                            new Vector2(0.85f, 0.69f),
                            new Vector2(0.92f, 0.82f),
                            new Vector2(0.89f, 0.95f),
                            new Vector2(0.9f, 0.82f),
                            new Vector2(0.92f, 0.82f),
                            new Vector2(0.9f, 0.96f),
                            new Vector2(0.07f, 0.83f),
                            new Vector2(0.05f, 0.84f),
                            new Vector2(-0.04f, 0.7f),
                            new Vector2(0.1f, 0.68f),
                            new Vector2(1.06f, 0.68f),
                            new Vector2(1.02f, 0.84f),
                            new Vector2(0.05f, 0.84f),
                            new Vector2(-0.03f, 0.69f),
                            new Vector2(0.84f, 0.69f),
                            new Vector2(0.9f, 0.82f),
                            new Vector2(0.07f, 0.83f),
                        },
                        Triangles = new uint[]
                        {
                            0, 6, 4, 0, 2, 6,
                            3, 21, 20, 3, 7, 21,
                            22, 24, 23, 22, 5, 24,
                            1, 26, 25, 1, 27, 26,
                            28, 30, 29, 28, 31, 30,
                            16, 15, 17, 16, 14, 15,
                            32, 13, 33, 32, 12, 13,
                            18, 9, 19, 18, 8, 9,
                            34, 36, 35, 34, 37, 36,
                            38, 39, 11, 38, 40, 39,
                            41, 43, 42, 41, 44, 43,
                            10, 46, 45, 10, 47, 46,
                            48, 50, 49, 48, 51, 50,
                            52, 54, 53, 52, 55, 54,
                        }
                    };
                case EnvironmentType.AdStand:
                    return new Mesh
                    {
                        Vertices = new[]
                        {
                            new Vector3(0f, 0.5f, -1f),
                            new Vector3(0.11f, 0f, -1f),
                            new Vector3(0f, 0.5f, 1f),
                            new Vector3(0.11f, 0f, 1f),
                            new Vector3(-0.11f, 0f, -1f),
                            new Vector3(-0.11f, 0f, 1f),
                            new Vector3(-0.11f, 0f, -1f),
                            new Vector3(-0.11f, 0f, 1f),
                            new Vector3(0.11f, 0f, -1f),
                            new Vector3(0f, 0.5f, -1f),
                            new Vector3(0f, 0.5f, 1f),
                            new Vector3(0.11f, 0f, 1f),
                        },
                        UVs = new[]
                        {
                            new Vector2(-0.08f, 0.31f),
                            new Vector2(1.08f, 0.01f),
                            new Vector2(1.09f, 0.31f),
                            new Vector2(-0.06f, 0.01f),
                            new Vector2(-0.08f, 0.01f),
                            new Vector2(1.09f, 0.01f),
                            new Vector2(1.08f, -0.11f),
                            new Vector2(-0.06f, -0.11f),
                            new Vector2(1.08f, 0.01f),
                            new Vector2(1.08f, 0.3f),
                            new Vector2(-0.06f, 0.3f),
                            new Vector2(-0.06f, 0.01f),
                        },
                        Triangles = new uint[]
                        {
                            5, 0, 2, 5, 4, 0,
                            6, 3, 1, 6, 7, 3,
                            8, 10, 9, 8, 11, 10,
                        }
                    };
                case EnvironmentType.AdPlane:
                    return new Mesh
                    {
                        Vertices = new[]
                        {
                            new Vector3(0.02f, 0.7f, -1f),
                            new Vector3(0.02f, 0f, -1f),
                            new Vector3(0.02f, 0.7f, 1f),
                            new Vector3(0.02f, 0f, 1f),
                            new Vector3(-0.02f, 0.7f, -1f),
                            new Vector3(-0.02f, 0f, -1f),
                            new Vector3(-0.02f, 0.7f, 1f),
                            new Vector3(-0.02f, 0f, 1f),
                            new Vector3(0.02f, 0.7f, 1f),
                            new Vector3(-0.02f, 0.7f, 1f),
                            new Vector3(-0.02f, 0f, 1f),
                            new Vector3(-0.02f, 0.7f, 1f),
                            new Vector3(-0.02f, 0.7f, -1f),
                            new Vector3(-0.02f, 0f, -1f),
                            new Vector3(0.02f, 0f, 1f),
                            new Vector3(-0.02f, 0f, 1f),
                            new Vector3(0.02f, 0f, -1f),
                            new Vector3(0.02f, 0.7f, -1f),
                            new Vector3(0.02f, 0.7f, 1f),
                            new Vector3(0.02f, 0f, 1f),
                            new Vector3(-0.02f, 0f, -1f),
                            new Vector3(-0.02f, 0.7f, -1f),
                            new Vector3(0.02f, 0.7f, -1f),
                            new Vector3(0.02f, 0f, -1f),
                        },
                        UVs = new[]
                        {
                            new Vector2(0.03f, 0.67f),
                            new Vector2(0.03f, 0.31f),
                            new Vector2(0.94f, 0.67f),
                            new Vector2(0.96f, 0.33f),
                            new Vector2(0.03f, 0.65f),
                            new Vector2(0.03f, 0.33f),
                            new Vector2(0.94f, 0.65f),
                            new Vector2(0.94f, 0.33f),
                            new Vector2(0.96f, 0.65f),
                            new Vector2(0.94f, 0.65f),
                            new Vector2(0.94f, 0.33f),
                            new Vector2(0.94f, 0.65f),
                            new Vector2(0.03f, 0.65f),
                            new Vector2(0.03f, 0.33f),
                            new Vector2(0.94f, 0.31f),
                            new Vector2(0.94f, 0.33f),
                            new Vector2(0.97f, 0.33f),
                            new Vector2(0.97f, 0.67f),
                            new Vector2(0.01f, 0.67f),
                            new Vector2(0.01f, 0.33f),
                            new Vector2(0.03f, 0.33f),
                            new Vector2(0.03f, 0.65f),
                            new Vector2(0.02f, 0.65f),
                            new Vector2(0.02f, 0.33f),
                        },
                        Triangles = new uint[]
                        {
                            0, 6, 4, 0, 2, 6,
                            3, 9, 8, 3, 7, 9,
                            10, 12, 11, 10, 5, 12,
                            13, 14, 1, 13, 15, 14,
                            16, 18, 17, 16, 19, 18,
                            20, 22, 21, 20, 23, 22,
                        }
                    };
                case EnvironmentType.Spruce:
                    return new Mesh
                    {
                        Vertices = new[]
                        {
                            new Vector3(0.04f, 8.13f, -0.05f),
                            new Vector3(0.16f, 0.09f, -0.18f),
                            new Vector3(0.06f, 8.29f, 0.06f),
                            new Vector3(0.2f, 0.11f, 0.19f),
                            new Vector3(-0.07f, 8.13f, -0.05f),
                            new Vector3(-0.19f, 0.09f, -0.18f),
                            new Vector3(-0.07f, 8.22f, 0.05f),
                            new Vector3(-0.19f, 0.1f, 0.18f),
                            new Vector3(0.05f, 2.88f, 0.65f),
                            new Vector3(2.65f, 0.88f, 1.18f),
                            new Vector3(0.05f, 2.88f, -0.65f),
                            new Vector3(2.65f, 0.88f, -1.18f),
                            new Vector3(-0.51f, 3f, 0.4f),
                            new Vector3(0.61f, 1f, 2.77f),
                            new Vector3(0.57f, 3f, -0.31f),
                            new Vector3(2.32f, 1f, 1.64f),
                            new Vector3(-0.36f, 2.88f, -0.54f),
                            new Vector3(-2.81f, 0.88f, 0.39f),
                            new Vector3(0.27f, 2.88f, 0.59f),
                            new Vector3(-1.81f, 0.88f, 2.18f),
                            new Vector3(0.26f, 3f, -0.59f),
                            new Vector3(-1.65f, 1f, -2.5f),
                            new Vector3(-0.36f, 3f, 0.54f),
                            new Vector3(-2.99f, 1f, -0.04f),
                            new Vector3(0.59f, 3f, 0.28f),
                            new Vector3(2.22f, 1f, -1.77f),
                            new Vector3(-0.53f, 3f, -0.37f),
                            new Vector3(0.44f, 1f, -2.8f),
                            new Vector3(-0.43f, 4.08f, 0.74f),
                            new Vector3(1.16f, 1.71f, 2.38f),
                            new Vector3(0.47f, 4.24f, -0.19f),
                            new Vector3(2.58f, 1.96f, 0.92f),
                            new Vector3(-0.64f, 3.62f, -0.08f),
                            new Vector3(-1.55f, 1.62f, 2.38f),
                            new Vector3(0.62f, 3.62f, 0.19f),
                            new Vector3(0.45f, 1.62f, 2.8f),
                            new Vector3(0.13f, 4f, -0.64f),
                            new Vector3(-2.25f, 2f, -1.73f),
                            new Vector3(-0.23f, 4f, 0.61f),
                            new Vector3(-2.83f, 2f, 0.23f),
                            new Vector3(0.45f, 3.79f, -0.03f),
                            new Vector3(0.05f, 2.22f, -2.88f),
                            new Vector3(-0.78f, 3.53f, 0.28f),
                            new Vector3(-1.18f, 1.97f, -2.57f),
                            new Vector3(0.21f, 3.62f, 0.61f),
                            new Vector3(2.9f, 1.62f, 0.69f),
                            new Vector3(-0.11f, 3.62f, -0.64f),
                            new Vector3(2.23f, 1.62f, -1.99f),
                            new Vector3(-0.49f, 4.8f, 0.18f),
                            new Vector3(-0.66f, 3.2f, 2.33f),
                            new Vector3(0.52f, 4.8f, -0.02f),
                            new Vector3(1.5f, 3.2f, 1.9f),
                            new Vector3(-0.24f, 4.66f, -0.32f),
                            new Vector3(-2.41f, 3.43f, 0.67f),
                            new Vector3(0.44f, 4.61f, 0.46f),
                            new Vector3(-0.92f, 3.31f, 2.37f),
                            new Vector3(0.34f, 4.79f, -0.33f),
                            new Vector3(-0.19f, 3.26f, -2.63f),
                            new Vector3(-0.45f, 4.76f, 0.32f),
                            new Vector3(-2.55f, 3.19f, -0.68f),
                            new Vector3(0.52f, 4.8f, 0.1f),
                            new Vector3(1.43f, 3.2f, -1.78f),
                            new Vector3(-0.47f, 4.8f, -0.21f),
                            new Vector3(-0.13f, 3.2f, -2.28f),
                            new Vector3(-0.14f, 4.71f, 0.53f),
                            new Vector3(1.68f, 3.05f, 1.85f),
                            new Vector3(0.21f, 4.71f, -0.48f),
                            new Vector3(2.46f, 3.05f, -0.4f),
                            new Vector3(-0.65f, 6.24f, -0.09f),
                            new Vector3(-1.74f, 4.47f, 1.34f),
                            new Vector3(0.23f, 6.34f, 0.29f),
                            new Vector3(0.24f, 4.69f, 2.21f),
                            new Vector3(-0.17f, 5.75f, -0.44f),
                            new Vector3(-2.11f, 4.17f, -0.97f),
                            new Vector3(-0.06f, 5.73f, 0.52f),
                            new Vector3(-1.83f, 4.13f, 1.43f),
                            new Vector3(0.47f, 5.44f, -0.07f),
                            new Vector3(1.17f, 3.94f, -2.05f),
                            new Vector3(-0.5f, 5.44f, -0.03f),
                            new Vector3(-1.37f, 3.94f, -1.94f),
                            new Vector3(0.26f, 5.77f, 0.37f),
                            new Vector3(2.22f, 4.4f, -0.33f),
                            new Vector3(-0.28f, 5.73f, -0.44f),
                            new Vector3(1.17f, 4.33f, -1.91f),
                            new Vector3(-0.37f, 5.4f, 0.32f),
                            new Vector3(0.23f, 3.89f, 2.38f),
                            new Vector3(0.48f, 5.36f, -0.23f),
                            new Vector3(2.01f, 3.81f, 1.24f),
                            new Vector3(0.26f, 6.78f, -0.42f),
                            new Vector3(-0.89f, 5.25f, -2.12f),
                            new Vector3(-0.4f, 6.78f, 0.33f),
                            new Vector3(-2.24f, 5.25f, -0.58f),
                            new Vector3(0.6f, 6.91f, 0.26f),
                            new Vector3(1.83f, 5.02f, -0.88f),
                            new Vector3(-0.13f, 6.98f, -0.41f),
                            new Vector3(0.67f, 5.14f, -1.94f),
                            new Vector3(0.01f, 6.84f, 0.61f),
                            new Vector3(1.72f, 5.09f, 1.38f),
                            new Vector3(0.2f, 6.9f, -0.36f),
                            new Vector3(2.13f, 5.22f, -0.62f),
                            new Vector3(-0.08f, 7.59f, -0.46f),
                            new Vector3(-1.97f, 6.4f, -0.55f),
                            new Vector3(-0.01f, 7.6f, 0.41f),
                            new Vector3(-1.86f, 6.42f, 0.84f),
                            new Vector3(0.44f, 7.33f, -0.11f),
                            new Vector3(0.56f, 5.88f, -2.01f),
                            new Vector3(-0.49f, 7.33f, -0.03f),
                            new Vector3(-0.92f, 5.88f, -1.88f),
                            new Vector3(-0.15f, 7.46f, 0.18f),
                            new Vector3(2.04f, 6.6f, 1.01f),
                            new Vector3(-0.02f, 7.23f, -0.76f),
                            new Vector3(2.26f, 6.23f, -0.49f),
                            new Vector3(-0.37f, 7.65f, 0.28f),
                            new Vector3(0.36f, 6.29f, 1.9f),
                            new Vector3(0.37f, 7.65f, -0.19f),
                            new Vector3(1.53f, 6.29f, 1.15f),
                            new Vector3(-0.42f, 6.87f, -0.41f),
                            new Vector3(-1.87f, 5.39f, 0.43f),
                            new Vector3(0.06f, 6.97f, 0.32f),
                            new Vector3(-1.11f, 5.53f, 1.58f),
                            new Vector3(0.33f, 8.41f, -0.1f),
                            new Vector3(0.45f, 7.32f, -1.52f),
                            new Vector3(-0.37f, 8.41f, -0.05f),
                            new Vector3(-0.66f, 7.32f, -1.45f),
                            new Vector3(0.23f, 8.07f, 0.22f),
                            new Vector3(1.48f, 6.98f, -0.46f),
                            new Vector3(-0.19f, 8.07f, -0.34f),
                            new Vector3(0.81f, 6.98f, -1.35f),
                            new Vector3(-0.06f, 8.41f, 0.06f),
                            new Vector3(0.15f, 7.32f, 1.59f),
                            new Vector3(0.08f, 8.41f, -0.03f),
                            new Vector3(1.38f, 7.32f, 0.81f),
                            new Vector3(-0.17f, 8.41f, -0.05f),
                            new Vector3(-1.18f, 7.32f, 1.04f),
                            new Vector3(0.1f, 8.41f, 0.08f),
                            new Vector3(-0.1f, 7.32f, 1.55f),
                            new Vector3(-0.01f, 8.41f, -0.15f),
                            new Vector3(-1.05f, 7.32f, -1.22f),
                            new Vector3(-0.11f, 8.41f, 0.05f),
                            new Vector3(-1.59f, 7.32f, -0.15f),
                            new Vector3(-0.19f, 0.1f, 0.18f),
                            new Vector3(-0.07f, 8.22f, 0.05f),
                            new Vector3(0.06f, 8.29f, 0.06f),
                            new Vector3(0.2f, 0.11f, 0.19f),
                            new Vector3(-0.19f, 0.09f, -0.18f),
                            new Vector3(-0.07f, 8.13f, -0.05f),
                            new Vector3(0.04f, 8.13f, -0.05f),
                            new Vector3(0.16f, 0.09f, -0.18f),
                        },
                        UVs = new[]
                        {
                            new Vector2(0.09f, 0.01f),
                            new Vector2(0.07f, 0.53f),
                            new Vector2(0.06f, 0f),
                            new Vector2(0f, 0.52f),
                            new Vector2(0.08f, 0.01f),
                            new Vector2(0.05f, 0.53f),
                            new Vector2(0.07f, 0.01f),
                            new Vector2(0.03f, 0.52f),
                            new Vector2(0.1f, 0.4f),
                            new Vector2(0.31f, 0.37f),
                            new Vector2(0.1f, 0.49f),
                            new Vector2(0.31f, 0.52f),
                            new Vector2(0.31f, 0.5f),
                            new Vector2(0.53f, 0.47f),
                            new Vector2(0.31f, 0.58f),
                            new Vector2(0.53f, 0.61f),
                            new Vector2(0.74f, 0.31f),
                            new Vector2(0.53f, 0.34f),
                            new Vector2(0.74f, 0.23f),
                            new Vector2(0.53f, 0.2f),
                            new Vector2(0.1f, 0.05f),
                            new Vector2(0.31f, 0f),
                            new Vector2(0.1f, 0.13f),
                            new Vector2(0.31f, 0.18f),
                            new Vector2(0.1f, 0.55f),
                            new Vector2(0.31f, 0.52f),
                            new Vector2(0.1f, 0.63f),
                            new Vector2(0.31f, 0.66f),
                            new Vector2(0.48f, 0.03f),
                            new Vector2(0.7f, 0f),
                            new Vector2(0.48f, 0.11f),
                            new Vector2(0.7f, 0.14f),
                            new Vector2(0.53f, 0.45f),
                            new Vector2(0.31f, 0.47f),
                            new Vector2(0.53f, 0.36f),
                            new Vector2(0.31f, 0.34f),
                            new Vector2(0.53f, 0.31f),
                            new Vector2(0.31f, 0.34f),
                            new Vector2(0.53f, 0.23f),
                            new Vector2(0.31f, 0.2f),
                            new Vector2(0.09f, 0.53f),
                            new Vector2(0.09f, 0.74f),
                            new Vector2(0f, 0.53f),
                            new Vector2(0f, 0.74f),
                            new Vector2(0.1f, 0.23f),
                            new Vector2(0.31f, 0.19f),
                            new Vector2(0.1f, 0.32f),
                            new Vector2(0.31f, 0.37f),
                            new Vector2(0.53f, 0.7f),
                            new Vector2(0.7f, 0.66f),
                            new Vector2(0.53f, 0.77f),
                            new Vector2(0.7f, 0.81f),
                            new Vector2(0.31f, 0.65f),
                            new Vector2(0.48f, 0.61f),
                            new Vector2(0.31f, 0.72f),
                            new Vector2(0.48f, 0.76f),
                            new Vector2(0.31f, 0.07f),
                            new Vector2(0.48f, 0f),
                            new Vector2(0.31f, 0.14f),
                            new Vector2(0.48f, 0.2f),
                            new Vector2(0.7f, 0.82f),
                            new Vector2(0.87f, 0.8f),
                            new Vector2(0.7f, 0.89f),
                            new Vector2(0.87f, 0.91f),
                            new Vector2(0.53f, 0.38f),
                            new Vector2(0.71f, 0.34f),
                            new Vector2(0.53f, 0.45f),
                            new Vector2(0.71f, 0.49f),
                            new Vector2(0.7f, 0.04f),
                            new Vector2(0.86f, 0f),
                            new Vector2(0.7f, 0.1f),
                            new Vector2(0.86f, 0.14f),
                            new Vector2(0.26f, 0.77f),
                            new Vector2(0.1f, 0.82f),
                            new Vector2(0.26f, 0.71f),
                            new Vector2(0.1f, 0.66f),
                            new Vector2(0.53f, 0.55f),
                            new Vector2(0.69f, 0.5f),
                            new Vector2(0.53f, 0.61f),
                            new Vector2(0.69f, 0.66f),
                            new Vector2(0.31f, 0.79f),
                            new Vector2(0.47f, 0.76f),
                            new Vector2(0.31f, 0.85f),
                            new Vector2(0.47f, 0.88f),
                            new Vector2(0.69f, 0.53f),
                            new Vector2(0.86f, 0.5f),
                            new Vector2(0.69f, 0.6f),
                            new Vector2(0.86f, 0.63f),
                            new Vector2(0.7f, 0.7f),
                            new Vector2(0.87f, 0.66f),
                            new Vector2(0.7f, 0.76f),
                            new Vector2(0.87f, 0.8f),
                            new Vector2(0.91f, 0.29f),
                            new Vector2(0.74f, 0.31f),
                            new Vector2(0.91f, 0.22f),
                            new Vector2(0.74f, 0.2f),
                            new Vector2(0.71f, 0.37f),
                            new Vector2(0.87f, 0.34f),
                            new Vector2(0.71f, 0.44f),
                            new Vector2(0.87f, 0.47f),
                            new Vector2(0.7f, 0.92f),
                            new Vector2(0.85f, 0.91f),
                            new Vector2(0.7f, 0.98f),
                            new Vector2(0.85f, 1f),
                            new Vector2(0.1f, 0.84f),
                            new Vector2(0.25f, 0.82f),
                            new Vector2(0.1f, 0.9f),
                            new Vector2(0.25f, 0.92f),
                            new Vector2(0.53f, 0.83f),
                            new Vector2(0.69f, 0.81f),
                            new Vector2(0.53f, 0.89f),
                            new Vector2(0.69f, 0.91f),
                            new Vector2(0.31f, 0.9f),
                            new Vector2(0.46f, 0.89f),
                            new Vector2(0.31f, 0.96f),
                            new Vector2(0.46f, 0.98f),
                            new Vector2(0.85f, 0.92f),
                            new Vector2(0.99f, 0.91f),
                            new Vector2(0.85f, 0.98f),
                            new Vector2(0.99f, 1f),
                            new Vector2(0.98f, 0.72f),
                            new Vector2(0.87f, 0.74f),
                            new Vector2(0.98f, 0.68f),
                            new Vector2(0.87f, 0.66f),
                            new Vector2(0.53f, 0.93f),
                            new Vector2(0.64f, 0.91f),
                            new Vector2(0.53f, 0.97f),
                            new Vector2(0.64f, 0.99f),
                            new Vector2(0.86f, 0.54f),
                            new Vector2(0.98f, 0.5f),
                            new Vector2(0.86f, 0.55f),
                            new Vector2(0.98f, 0.59f),
                            new Vector2(0.86f, 0.11f),
                            new Vector2(0.98f, 0.08f),
                            new Vector2(0.86f, 0.13f),
                            new Vector2(0.98f, 0.16f),
                            new Vector2(0.86f, 0.03f),
                            new Vector2(0.98f, 0f),
                            new Vector2(0.86f, 0.05f),
                            new Vector2(0.98f, 0.08f),
                            new Vector2(0.03f, 0.52f),
                            new Vector2(0.07f, 0.01f),
                            new Vector2(0.09f, 0f),
                            new Vector2(0.09f, 0.53f),
                            new Vector2(0.05f, 0.53f),
                            new Vector2(0.08f, 0.01f),
                            new Vector2(0.09f, 0.01f),
                            new Vector2(0.07f, 0.53f),
                        },
                        Triangles = new uint[]
                        {
                            3, 6, 2, 3, 7, 6,
                            140, 4, 141, 140, 5, 4,
                            8, 11, 9, 8, 10, 11,
                            1, 142, 0, 1, 143, 142,
                            12, 15, 13, 12, 14, 15,
                            16, 19, 17, 16, 18, 19,
                            20, 23, 21, 20, 22, 23,
                            24, 27, 25, 24, 26, 27,
                            28, 31, 29, 28, 30, 31,
                            32, 35, 33, 32, 34, 35,
                            36, 39, 37, 36, 38, 39,
                            40, 43, 41, 40, 42, 43,
                            44, 47, 45, 44, 46, 47,
                            48, 51, 49, 48, 50, 51,
                            52, 55, 53, 52, 54, 55,
                            56, 59, 57, 56, 58, 59,
                            60, 63, 61, 60, 62, 63,
                            64, 67, 65, 64, 66, 67,
                            68, 71, 69, 68, 70, 71,
                            72, 75, 73, 72, 74, 75,
                            76, 79, 77, 76, 78, 79,
                            80, 83, 81, 80, 82, 83,
                            84, 87, 85, 84, 86, 87,
                            88, 91, 89, 88, 90, 91,
                            92, 95, 93, 92, 94, 95,
                            96, 99, 97, 96, 98, 99,
                            100, 103, 101, 100, 102, 103,
                            104, 107, 105, 104, 106, 107,
                            108, 111, 109, 108, 110, 111,
                            112, 115, 113, 112, 114, 115,
                            116, 119, 117, 116, 118, 119,
                            120, 123, 121, 120, 122, 123,
                            124, 127, 125, 124, 126, 127,
                            128, 131, 129, 128, 130, 131,
                            132, 135, 133, 132, 134, 135,
                            136, 139, 137, 136, 138, 139,
                            144, 146, 145, 144, 147, 146,
                        }
                    };
                default: return null;
            }
        }
    }
}