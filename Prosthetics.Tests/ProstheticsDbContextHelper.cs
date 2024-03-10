//using Microsoft.EntityFrameworkCore;
//using Prosthetics.Persistance;

//namespace Prosthetics.Tests;

//public static class ProstheticsDbContextHelper
//{
//    public static ProstheticsDbContext Create()
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<ProstheticsDbContext>();
//        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

//        return new ProstheticsDbContext(optionsBuilder.Options);
//    }
//}