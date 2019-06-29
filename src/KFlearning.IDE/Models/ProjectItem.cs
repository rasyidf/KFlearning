using System;
using KFlearning.DAL;
using MahApps.Metro.IconPacks;

namespace KFlearning.IDE.Models
{
    public class ProjectItem
    {
        public PackIconMaterialKind Icon => ProjectTypeToMaterialIcon(Item.Type);

        public string Title => Item.Title;

        public string Path => Item.Path;

        public Project Item { get; }

        public ProjectItem(Project project)
        {
            Item = project;
        }

        private PackIconMaterialKind ProjectTypeToMaterialIcon(ProjectType type)
        {
            switch (type)
            {
                case ProjectType.Web:
                    return PackIconMaterialKind.Web;
                case ProjectType.Cpp:
                    return PackIconMaterialKind.LanguageCpp;
                case ProjectType.Python:
                    return PackIconMaterialKind.LanguagePython;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private ProjectType MaterialIconToProjectType(PackIconMaterialKind icon)
        {
            switch (icon)
            {
                case PackIconMaterialKind.Web:
                    return ProjectType.Web;
                case PackIconMaterialKind.LanguageCpp:
                    return ProjectType.Cpp;
                case PackIconMaterialKind.LanguagePython:
                    return ProjectType.Python;
                default:
                    throw new ArgumentOutOfRangeException(nameof(icon), icon, null);
            }
        }
    }
}
