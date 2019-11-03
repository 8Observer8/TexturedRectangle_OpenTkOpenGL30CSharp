
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace TexturedRectangle
{
    class Window : GameWindow
    {
        private int _programID;
        private int _amountOfVertices;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "OpenTK, OpenGL 3.0, C#";
            Width = 256;
            Height = 256;

            Logger.Init();

            int texture = Loader.LoadTexture("Assets/Textures/tank_yellow_small_up_01.png");
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            ShaderProgram shaderProgram = new ShaderProgram(
                "Assets/Shaders/VertexShader.glsl",
                "Assets/Shaders/FragmentShader.glsl");
            _programID = shaderProgram.Id;

            int uSamplerLocation = GL.GetUniformLocation(_programID, "uSampler");
            GL.Uniform1(uSamplerLocation, 0);

            float[] vertices = new float[]
            {
                -0.5f, 0.5f, 0f,
                -0.5f, -0.5f, 0f,
                0.5f, 0.5f, 0f,
                0.5f, -0.5f, 0f
            };
            float[] texCoords = new float[]
            {
                0f, 0f,
                0f, 1f,
                1f, 0f,
                1f, 1f
            };
            _amountOfVertices = VertexBuffers.Init(_programID, vertices, texCoords);

            Matrix4 modelMatrix = Matrix4.CreateScale(10f);
            shaderProgram.SetModelMatrix(modelMatrix);

            Matrix4 viewMatrix = Matrix4.LookAt(
                eye: new Vector3(0f, 0f, 1f),
                target: new Vector3(0f, 0f, 0f),
                up: new Vector3(0f, 1f, 0f));
            shaderProgram.SetViewMatrix(viewMatrix);

            Matrix4 projMatrix = Matrix4.CreateOrthographic(20f, 20f, 10f, -10f);
            shaderProgram.SetProjMatrix(projMatrix);

            GL.ClearColor(0.1f, 0.3f, 0.2f, 1f);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, _amountOfVertices);

            SwapBuffers();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Logger.Close();
        }
    }
}
