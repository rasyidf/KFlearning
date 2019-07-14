// 
//  PROJECT  :   KFlearning
//  FILENAME :   PathKind.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

namespace KFlearning.Core.IO
{
    public enum PathKind
    {
        PathBase,
        PathReposRoot,
        PathSitesAliasRoot,
        PathVirtualHostRoot,

        PathVscodeRoot,
        PathMingwRoot,
        PathApacheRoot,
        PathMariaDbRoot,
        PathPhpRoot,
        PathComposerRoot,
        PathKflearningRoot,

        TemplateHosts,
        TemplateWeb,
        TemplateCpp,
        TemplatePython,

        ExeHttpd,
        ExeMariadb,
        ExeVscode,
    }
}