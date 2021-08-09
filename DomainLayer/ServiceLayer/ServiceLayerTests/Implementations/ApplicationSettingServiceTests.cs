using System.Collections.Generic;
using System.Linq;
using DataMapper;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ServiceLayer.Implementations.Tests
{
    public class ApplicationSettingServiceTests
    {
        private readonly Mock<AuctionEnterpriseAppContext> _context = new Mock<AuctionEnterpriseAppContext>();

        [Fact()]
        public void GetByNameTest()
        {

            // var data = new List<ApplicationSetting>
            // {
            //     new ApplicationSetting { Name = "BBB" },
            // }.AsQueryable();
            //
            //
            // var mockContext = new Mock<AuctionEnterpriseAppContext>();
            // mockContext.Setup(c => c.Blogs).Returns(mockSet.Object);
            //
            // var service = new BlogService(mockContext.Object);
            // var blogs = service.GetAllBlogs();
        }
    }
}