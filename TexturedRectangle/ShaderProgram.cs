
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TexturedRectangle
{
    class ShaderProgram
    {
        public int Id { get; set; }

        private int _modelMatrixLocation;
        private int _viewMatrixLocation;
        private int _projMatrixLocation;

        public ShaderProgram(string vertexShaderPath, string fragmentShaderPath)
        {
            Id = -1;

            // Check a vertex shader file
            if (!File.Exists(vertexShaderPath))
            {
                Logger.Print("Failed to open the vertex shader: " + vertexShaderPath);
                return;
            }

            // Check a fragment shader file
            if (!File.Exists(fragmentShaderPath))
            {
                Logger.Print("Failed to open the fragment shader: " + fragmentShaderPath);
                return;
            }

            // Get shader sources
            string vShaderSource = File.ReadAllText(vertexShaderPath);
            string fShaderSource = File.ReadAllText(fragmentShaderPath);

            // Create shader IDs
            int vShader = CreateShaderId(vShaderSource, ShaderType.VertexShader);
            int fShader = CreateShaderId(fShaderSource, ShaderType.FragmentShader);

            // Create a shader program ID
            Id = GL.CreateProgram();
            GL.AttachShader(Id, vShader);
            GL.AttachShader(Id, fShader);
            GL.LinkProgram(Id);
            GL.UseProgram(Id);

            // Check program linking
            int ok;
            GL.GetProgram(Id, GetProgramParameterName.LinkStatus, out ok);
            if (ok == 0)
            {
                string errorMessage = GL.GetProgramInfoLog(Id);
                Logger.Print("Failed to link a program: " + errorMessage);
                return;
            }

            _modelMatrixLocation = GetUniform("uModelMatrix");
            _viewMatrixLocation = GetUniform("uViewMatrix");
            _projMatrixLocation = GetUniform("uProjMatrix");
        }

        public void SetModelMatrix(Matrix4 matrix)
        {
            GL.UniformMatrix4(_modelMatrixLocation, false, ref matrix);
        }

        public void SetViewMatrix(Matrix4 matrix)
        {
            GL.UniformMatrix4(_viewMatrixLocation, false, ref matrix);
        }

        public void SetProjMatrix(Matrix4 matrix)
        {
            GL.UniformMatrix4(_projMatrixLocation, false, ref matrix);
        }

        private int GetUniform(string locationName)
        {
            int location = GL.GetUniformLocation(Id, locationName);
            if (location == -1)
            {
                Logger.Print("Failed to get the location: " + locationName);
                return -1;
            }
            return location;
        }

        private int CreateShaderId(string shaderSource, ShaderType type)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, shaderSource);
            GL.CompileShader(shader);

            // Check shader compilation
            int ok;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out ok);
            if (ok == 0)
            {
                string errorMessage = GL.GetShaderInfoLog(shader);
                Logger.Print(string.Format("Failed to compile the {0}. Message: {1}", type.ToString(), errorMessage));
                return -1;
            }
            return shader;
        }
    }
}
