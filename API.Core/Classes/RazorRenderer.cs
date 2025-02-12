using RazorLight;

namespace API.Core.Classes;

public class RazorRenderer
{
    private readonly RazorLightEngine _engine;

    public RazorRenderer()
    {
        _engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(typeof(RazorRenderer).Assembly)
            .UseMemoryCachingProvider()
            .Build();
    }

    public async Task<string> RenderRazorViewToStringAsync(string viewName, object model)
    {
        return await _engine.CompileRenderAsync(viewName, model);
    }
}
