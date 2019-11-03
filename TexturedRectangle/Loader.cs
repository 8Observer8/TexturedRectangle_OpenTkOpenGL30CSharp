
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace TexturedRectangle
{
    static class Loader
    {
        public static int LoadTexture(string path)
        {
            if (!File.Exists(path))
            {
                Logger.Print("This file is not exist: " + path);
                return -1;
            }

            // Create a texture object
            int textureID = GL.GenTexture();

            // Bind the texture object to the target
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            // Set the texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);

            using (var image = new Bitmap(path))
            {
                // Lock Bits
                BitmapData data = image.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                // Set the texture image
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                image.UnlockBits(data);
            }

            return textureID;
        }
    }
}
