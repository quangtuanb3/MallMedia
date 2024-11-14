﻿namespace MallMedia.Domain.Exceptions
{
    public class BadRequestException(string resourceType)
    : Exception($"{resourceType} already existed!")
    {
    }
}
