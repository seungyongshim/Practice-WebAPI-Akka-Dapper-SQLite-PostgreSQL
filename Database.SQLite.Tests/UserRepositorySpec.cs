using Database.Core;
using Domain;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Database.SQLite.Tests
{
    public class UserRepositorySpec
    {
        public UserRepositorySpec(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        [Fact]
        public void Insert()
        {
            // Arrange
            // 메모리DB를 테스트 종료 전까지 유지한다.
            using var _ = ServiceProvider.GetService<IUnitOfWork>();

            
            using (var scope = ServiceProvider.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetService<UserRepository>();
                
                // 테이블을 생성한다.
                userRepo.Initialize();
            }

            using (var scope = ServiceProvider.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetService<UserRepository>();

                userRepo.Insert(new User
                {
                    BLOB = new byte[] { 0x00, 0x01, 0x02 },
                    USER_GROUP = "Heros",
                    USER_NAME = "Clark",
                    PASSWORD = "Superman",
                });
            }

            using (var scope = ServiceProvider.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetService<UserRepository>();

                var users = userRepo.FindAll().ToArray();
                users.Length.Should().Be(1);
                users.First().Should().BeEquivalentTo(new User
                {
                    BLOB = new byte[] { 0x00, 0x01, 0x02 },
                    USER_ID = 1,
                    USER_GROUP = "Heros",
                    USER_NAME = "Clark",
                    PASSWORD = "Superman",
                }) ;
            }
        }
    }
}