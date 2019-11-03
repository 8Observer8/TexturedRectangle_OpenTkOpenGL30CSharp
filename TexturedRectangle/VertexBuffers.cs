
using OpenTK.Graphics.OpenGL;

namespace TexturedRectangle
{
    static class VertexBuffers
    {
        public static int Init(int program, float[] vertices, float[] texCoords)
        {
            int verticesVBO;
            GL.GenBuffers(1, out verticesVBO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, verticesVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);
            GL.BindAttribLocation(program, 0, "aPosition");
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            int texCoordsVBO;
            GL.GenBuffers(1, out texCoordsVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordsVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * texCoords.Length, texCoords, BufferUsageHint.StaticDraw);
            GL.BindAttribLocation(program, 1, "aTexCoord");
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);

            return vertices.Length / 3;
        }
    }
}
