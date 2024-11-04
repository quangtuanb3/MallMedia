﻿using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories;

public interface IMediaRepository
{
    public Task<int> Create(Media entity);
}