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
            return new InferenceSession(Constants.ModelPath);


        }
    }
}
