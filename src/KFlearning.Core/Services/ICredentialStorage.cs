﻿namespace KFlearning.Core.Services
{
    public interface ICredentialStorage
    {
        string AccessCode { get; set; }
        string NetworkCode { get; set; }
    }
}