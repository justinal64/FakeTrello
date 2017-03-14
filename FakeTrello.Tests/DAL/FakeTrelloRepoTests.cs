using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeTrello.DAL;
using Moq;
using FakeTrello.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace FakeTrello.Tests.DAL
{
    [TestClass]
    public class FakeTrelloRepoTests
    {
        public Mock<FakeTrelloContext> fakeContext { get; set; }
        public FakeTrelloRepository repo { get; set; }
        public Mock<DbSet<Board>> mockBoardSet { get; set; }
        public IQueryable<Board> queryBoards { get; set; }
        public List<Board> fakeBoardTable { get; set; }

        [TestInitialize]
        public void Setup()
        {
            fakeContext = new Mock<FakeTrelloContext>();
            repo = new FakeTrelloRepository(fakeContext.Object);
            mockBoardSet = new Mock<DbSet<Board>>();
        }

        public void CreateFakeDatabase()
        {
            // IQueryable<Board>
            queryBoards = fakeBoardTable.AsQueryable(); // Re-express this list as something that understands queries
            // Hey LINQ, use the provider from our fake board
            mockBoardSet.As<IQueryable<Board>>().Setup(b => b.Provider).Returns(queryBoards.Provider);
            mockBoardSet.As<IQueryable<Board>>().Setup(b => b.Expression).Returns(queryBoards.Expression);
            mockBoardSet.As<IQueryable<Board>>().Setup(b => b.ElementType).Returns(queryBoards.ElementType);
            mockBoardSet.As<IQueryable<Board>>().Setup(b => b.GetEnumerator()).Returns(() => queryBoards.GetEnumerator());

            mockBoardSet.Setup(b => b.Add(It.IsAny<Board>())).Callback((Board board) => fakeBoardTable.Add(board));
            fakeContext.Setup(c => c.Boards).Returns(mockBoardSet.Object); // Context.Boards returns fakeBoardTable

        }

        [TestMethod]
        public void EnsureICanCreateAnInstanceOfRepo()
        {
            FakeTrelloRepository repo = new FakeTrelloRepository();

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureICanInjectContextInstance()
        {
            FakeTrelloContext context = new FakeTrelloContext();

            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void EnsureIHaveNotNullContext()
        {
            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void EnsureICanAddBoard()
        {
            CreateFakeDatabase();


            ApplicationUser aUser = new ApplicationUser {
                Id = "my_User_id",
                UserName = "Justin",
                Email = "test@test.com"
            };

            repo.AddBoard("My Board", aUser);

            Assert.AreEqual(1, repo.Context.Boards.Count());
        }

        [TestMethod]
        public void EnsureICanReturnBoards()
        {
            // Arrange
            fakeBoardTable.Add(new Board { Name = "My Board"});

            CreateFakeDatabase();

            int expectedBoardCount = 1;
            int actualBoardCount = repo.Context.Boards.Count();


            Assert.AreEqual(expectedBoardCount, actualBoardCount);
        }
    }
}
