// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : TemplateService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.Collections.Generic;
using System.IO;
using KFlearning.Core.IO;
using KFlearning.Core.Resources;

namespace KFlearning.Core.Services
{
    public interface ITemplateService
    {
        IEnumerable<Template> GetTemplates();
        void Extract(Template template, string outputPath);
    }

    public class TemplateService : ITemplateService
    {
        private readonly List<Template> _templates;

        public TemplateService(IPathManager path)
        {
            var path1 = path;
            _templates = new List<Template>
            {
                new Template("Konsol (C++)", new List<Transformable>
                {
                    new Transformable("program.cpp", () => TR.CPP_program),
                    new Transformable(".vscode/c_cpp_properties.json", () => TR.CPP_c_cpp_properties,
                        x => x.Replace("{GXX}", path1.GetPath(PathKind.MingwGXXExecutable, true))
                            .Replace("{ENV1}", path1.GetPath(PathKind.MingwInclude1Directory, true))
                            .Replace("{ENV2}", path1.GetPath(PathKind.MingwInclude2Directory, true))),
                    new Transformable(".vscode/launch.json", () => TR.CPP_launch,
                        x => x.Replace("{GDB}", path1.GetPath(PathKind.MingwGDBExecutable, true))),
                    new Transformable(".vscode/settings.json", () => TR.CPP_settings),
                    new Transformable(".vscode/tasks.json", () => TR.CPP_Console_tasks)
                }),
                new Template("GUI Freeglut (C++)", new List<Transformable>
                {
                    new Transformable("program.cpp", () => TR.CPP_program),
                    new Transformable(".vscode/c_cpp_properties.json", () => TR.CPP_c_cpp_properties,
                        x => x.Replace("{GXX}", path1.GetPath(PathKind.MingwGXXExecutable, true))
                            .Replace("{ENV1}", path1.GetPath(PathKind.MingwInclude1Directory, true))
                            .Replace("{ENV2}", path1.GetPath(PathKind.MingwInclude2Directory, true))),
                    new Transformable(".vscode/launch.json", () => TR.CPP_launch,
                        x => x.Replace("{GDB}", path1.GetPath(PathKind.MingwGDBExecutable, true))),
                    new Transformable(".vscode/settings.json", () => TR.CPP_settings),
                    new Transformable(".vscode/tasks.json", () => TR.CPP_GUI_tasks)
                }),
                new Template("Web (PHP/HTML/CSS/JS)", new List<Transformable>
                {
                    new Transformable("index.html", () => TR.WEB_index)
                }),
                new Template("Python/Jupyter Notebook", new List<Transformable>
                {
                    new Transformable("program.py", () => TR.PY_program)
                }),
                new Template("Kosong", null)
            };
        }

        public IEnumerable<Template> GetTemplates()
        {
            return _templates;
        }

        public void Extract(Template template, string outputPath)
        {
            if (template.FileMapping == null) return;
            foreach (var file in template.FileMapping)
            {
                file.Transform(outputPath);
            }
        }
    }
}