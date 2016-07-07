using System;
using System.Reflection;

namespace Unipluss.Eiendom.RebusHeartBeat.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}