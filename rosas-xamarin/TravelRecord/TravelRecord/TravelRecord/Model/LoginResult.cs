using System;
namespace TravelRecord.Model
{
    public enum LoginResult
    {
        Success,
        Failure,
        MissingField,
        WrongPassword,
    }
}
