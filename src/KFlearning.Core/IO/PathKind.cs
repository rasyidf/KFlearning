// 
//  PROJECT  :   KFlearning
//  FILENAME :   PathKind.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

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

        ExeHttpd,
        ExeMariadb,
        ExeVscode,
        CmdVscode
    }
}