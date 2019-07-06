// 
//  PROJECT  :   KFlearning
//  FILENAME :   IHtmlTransformer.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

namespace KFlearning.Core.API
{
    public interface IHtmlTransformer
    {
        void TransformHtmlForSave(string filePath);
        void TransformHtmlForStyle(string filePath);
    }
}