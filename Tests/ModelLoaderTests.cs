using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;
using Xunit;
using SearchEngine;
namespace Tests

{
    public class ModelLoaderTests
    {
        [Fact]
        public void GetSession_ShouldHaveInputMetadata()
        {
            // Arrange
            var modelLoader = new SearchEngine.ModelLoader();
            // Act
            var session = modelLoader.GetSession();
            // Assert
            Assert.NotNull(session);
            Assert.True(session.InputMetadata.Count > 0, "Model should have input metadata.");
        }
    }
}
