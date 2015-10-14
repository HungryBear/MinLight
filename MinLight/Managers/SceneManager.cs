using System;

namespace MinLight.Managers
{
    using System.IO;

    using MinLight.Entities;

    public class SceneManager
    {
        static string MODEL_FORMAT_ID = "#MiniLight";

        public SceneContext LoadScene(string modelFileName, int width, int height)
        {
            StreamReader modelFile = File.OpenText(modelFileName);

            string formatId = modelFile.ReadLine();
            if (MODEL_FORMAT_ID != formatId)
                throw new ApplicationException("Invalid model file");

            int iterations = (int)modelFile.ReadFloat();
            var Width = (int)modelFile.ReadFloat();
            var Height = (int)modelFile.ReadFloat();
            Image image = new Image(width, height);
            Camera camera = new Camera(modelFile);
            Scene scene = new Scene(modelFile, camera.ViewPosition);

            modelFile.Close();
            return new SceneContext() { Scene = scene, Camera = camera, Image = image };
        }
    }
}
