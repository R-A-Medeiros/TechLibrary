﻿using System.Net;

namespace TechLibrary.Exception;
public class ConflitsException : TechLibraryException
{
    public ConflitsException(string message) : base(message)
    {
        
    }
    public override List<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;
}
