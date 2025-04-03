using System;

namespace eItems.Shared.Data.Seed;

public interface IDataSeeder
{
    Task SeedAllAsync();
}
