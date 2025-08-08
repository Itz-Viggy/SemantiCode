using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;

namespace SearchEngine
{
    public class ModelLoader
    {
        public InferenceSession GetSession(){

            var modelPath = Path.Combine(AppContext.BaseDirectory, Constants.ModelPath);
            if (!File.Exists(modelPath))
                throw new FileNotFoundException($"ONNX model not found at: {modelPath}");


            return new InferenceSession(Constants.ModelPath);


        }
    }
}
