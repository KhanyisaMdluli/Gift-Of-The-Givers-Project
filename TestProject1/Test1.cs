
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace TestProject1
{
    using Microsoft.AspNetCore.Mvc;
    using WebApplication2KS;
    using WebApplication2KS.Controllers;
    using WebApplication2KS.Data;
    using WebApplication2KS.Models;
    [TestClass]
    public class ReliefEffortsControllerTests
    {
        private WebAppDbContext _context;
        private ReliefEffortsController _controller;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WebAppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new WebAppDbContext(options);
            _controller = new ReliefEffortsController(_context);
        }

        [TestMethod]
        public async Task Details_IdIsNull_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_IdNotFound_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_ValidId_ReturnsViewWithModel()
        {
            // Arrange
            var entity = new ReliefEffort { EffortId = 1, Title = "Food Distribution",Description="Description" };
            _context.ReliefEfforts.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(ReliefEffort));
            var model = result.Model as ReliefEffort;
            Assert.AreEqual(entity.EffortId, model.EffortId);
        }
    }
}
