using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Moq;
using AutoMapper;
using Services;
using Services.Dto;
using Model;
using Contracts.DataAccess.SpecificRepos;
using EntityMap;
using NonDomain;

namespace OnlineStore.Tests
{
    public class CategoriesServiceTests
    {
        private CategoriesService GetSystemUnderTest(ICategoriesRepository mockObject)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDtoMappingProfile());
                cfg.AddProfile(new DtoToEntityMappingProfile());
            });
            var mapper = config.CreateMapper();
            return new CategoriesService(mockObject, mapper);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnCategoryWithCorrectId()
        {
            //Arrange
            var category = new Category { Id = 1, ParentCategoryId = null, Name = "Laptops", Description = "Description", ImagePath = "laptop.jpg" };
            var expected = category.Id;    
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.GetByIdAsync(expected)).Returns(Task.FromResult(category));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            //Act
            var actual = await sut.GetCategoryById(category.Id);

            //Assert
            Assert.Equal(expected, actual.Id);
        }

        [Fact]
        public async Task AddCategory_ShouldIncreaseCategoriesCount()
        {
            var categories = new List<Category>();
            var categoryDtoToAdd = new CategoryDto { Id = 7, ParentCategoryId = 3, Name = "RAM", Description = "Description", ImagePath = "ram.jpg" };
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.InsertAsync(It.IsAny<Category>(), true))
                                    .Callback((Category c, bool b) => categories.Add(c))
                                    .Returns<Category, bool>((c, b) => Task.FromResult(new OperationResult() { IsSuccess = c.Id == categoryDtoToAdd.Id }));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            var operationResult = await sut.AddCategory(categoryDtoToAdd);

            Assert.True(operationResult.IsSuccess);
            Assert.Single(categories);
        }

        [Fact]
        public async Task EditCategory_ShouldEditCategoryNameWithSameId()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, ParentCategoryId = null, Name = "Laptops", Description = "Description", ImagePath = "laptop.jpg"}
            };
            var expected = "Displays";
            var editedCategory = new CategoryDto { Id = 1, ParentCategoryId = null, Name = expected, Description = "Description", ImagePath = "display.jpg" };
            var categoryId = categories[0].Id;
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Category>(), true))
                                    .Callback((Category c, bool b) => categories[0] = c)
                                    .Returns<Category, bool>((c, b) => Task.FromResult(new OperationResult() { IsSuccess = c.Id == categoryId }));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            var operationResult = await sut.EditCategory(editedCategory);

            Assert.True(operationResult.IsSuccess);
            Assert.Equal(expected, categories[0].Name);
        }

        [Fact]
        public async Task DeleteCategory_ShouldDeleteCategoryWithCorrectId()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, ParentCategoryId = null, IsDeleted = false, Name = "Laptops", Description = "Description", ImagePath = "laptop.jpg"}
            };
            var categoryId = categories[0].Id;
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.DeleteCategoryById(categoryId, false)).Callback((int id, bool requireSave) => categories.FirstOrDefault(c => c.Id == id).IsDeleted = true);
            categoriesRepositoryMock.Setup(m => m.GetNotDeletedCategoriesByParentId(categoryId)).Returns(Task.FromResult(new List<Category>()));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            await sut.DeleteCategoryById(categoryId);

            Assert.True(categories.FirstOrDefault(c => c.Id == categoryId).IsDeleted);
        }

        [Fact]
        public async Task DeleteCategory_ShouldDeleteCategoryWithChildren()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, ParentCategoryId = null, IsDeleted = false, Name = "Gaming", Description = "Description", ImagePath = "gaming.jpg"},
                new Category { Id = 2, ParentCategoryId = 1, IsDeleted = false, Name = "Computer Components", Description = "Description", ImagePath = "computerComponents.jpg"},
                new Category { Id = 3, ParentCategoryId = 2, IsDeleted = false, Name = "Videocards", Description = "Description", ImagePath = "videocard.jpg"}
            };
            var categoryId = categories[0].Id;
            var childCategoryId = categories[1].Id;
            var childChildCategoryId = categories[2].Id;
            var expected = categories.Count;
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.DeleteCategoryById(categoryId, false)).Callback((int id, bool requireSave) => categories[id - 1].IsDeleted = true);
            categoriesRepositoryMock.Setup(m => m.GetNotDeletedCategoriesByParentId(categoryId)).Returns(Task.FromResult(new List<Category>() { categories[childCategoryId - 1] }));
            categoriesRepositoryMock.Setup(m => m.DeleteCategoryById(childCategoryId, false)).Callback((int id, bool requireSave) => categories[id - 1].IsDeleted = true);
            categoriesRepositoryMock.Setup(m => m.GetNotDeletedCategoriesByParentId(childCategoryId)).Returns(Task.FromResult(new List<Category>() { categories[childChildCategoryId - 1] }));
            categoriesRepositoryMock.Setup(m => m.DeleteCategoryById(childChildCategoryId, false)).Callback((int id, bool requireSave) => categories[id - 1].IsDeleted = true);
            categoriesRepositoryMock.Setup(m => m.GetNotDeletedCategoriesByParentId(childChildCategoryId)).Returns(Task.FromResult(new List<Category>()));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            await sut.DeleteCategoryById(categoryId);

            Assert.Equal(expected, categories.Where(c => c.IsDeleted).Count());
        }

        [Fact]
        public async Task DeleteCategory_ShouldSaveChanges()
        {
            var categoryId = 1;
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.GetNotDeletedCategoriesByParentId(categoryId)).Returns(Task.FromResult(new List<Category>()));
            categoriesRepositoryMock.Setup(m => m.SaveAsync()).Returns(Task.FromResult(new OperationResult() { IsSuccess = true }));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            var result = await sut.DeleteCategoryById(categoryId);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task RestoreCategory_ShouldRestoreCategoryWithCorrectId()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, ParentCategoryId = null, IsDeleted = true, Name = "Laptops", Description = "Description", ImagePath = "laptop.jpg"}
            };
            var categoryId = categories[0].Id;
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.RestoreCategoryById(categoryId, false)).Callback((int id, bool requireSave) => categories.FirstOrDefault(c => c.Id == id).IsDeleted = false);
            categoriesRepositoryMock.Setup(m => m.GetDeletedCategoriesByParentId(categoryId)).Returns(Task.FromResult(new List<Category>()));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            await sut.RestoreCategoryById(categoryId);

            Assert.True(categories.FirstOrDefault(c => c.Id == categoryId).IsDeleted == false);
        }

        [Fact]
        public async Task RestoreCategory_ShouldRestoreCategoryWithChildren()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, ParentCategoryId = null, IsDeleted = true, Name = "Gaming", Description = "Description", ImagePath = "gaming.jpg"},
                new Category { Id = 2, ParentCategoryId = 1, IsDeleted = true, Name = "Computer Components", Description = "Description", ImagePath = "computerComponents.jpg"},
                new Category { Id = 3, ParentCategoryId = 2, IsDeleted = true, Name = "Videocards", Description = "Description", ImagePath = "videocard.jpg"}
            };
            var categoryId = categories[0].Id;
            var childCategoryId = categories[1].Id;
            var childChildCategoryId = categories[2].Id;
            var expected = categories.Count;
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.RestoreCategoryById(categoryId, false)).Callback((int id, bool requireSave) => categories[id - 1].IsDeleted = false);
            categoriesRepositoryMock.Setup(m => m.GetDeletedCategoriesByParentId(categoryId)).Returns(Task.FromResult(new List<Category>() { categories[childCategoryId - 1] }));
            categoriesRepositoryMock.Setup(m => m.RestoreCategoryById(childCategoryId, false)).Callback((int id, bool requireSave) => categories[id - 1].IsDeleted = false);
            categoriesRepositoryMock.Setup(m => m.GetDeletedCategoriesByParentId(childCategoryId)).Returns(Task.FromResult(new List<Category>() { categories[childChildCategoryId - 1] }));
            categoriesRepositoryMock.Setup(m => m.RestoreCategoryById(childChildCategoryId, false)).Callback((int id, bool requireSave) => categories[id - 1].IsDeleted = false);
            categoriesRepositoryMock.Setup(m => m.GetDeletedCategoriesByParentId(childChildCategoryId)).Returns(Task.FromResult(new List<Category>()));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            await sut.RestoreCategoryById(categoryId);

            Assert.Equal(expected, categories.Where(c => !c.IsDeleted).Count());
        }

        [Fact]
        public async Task RestoreCategory_ShouldSaveChanges()
        {
            var categoryId = 1;
            var categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesRepositoryMock.Setup(m => m.GetDeletedCategoriesByParentId(categoryId)).Returns(Task.FromResult(new List<Category>()));
            categoriesRepositoryMock.Setup(m => m.SaveAsync()).Returns(Task.FromResult(new OperationResult() { IsSuccess = true }));
            var sut = GetSystemUnderTest(categoriesRepositoryMock.Object);

            var result = await sut.RestoreCategoryById(categoryId);

            Assert.True(result.IsSuccess);
        }
    }
}
