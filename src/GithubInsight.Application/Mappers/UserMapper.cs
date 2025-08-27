using GithubInsight.Application.DTO;
using GithubInsight.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Mappers
{
    internal class UserMapper
    {
        public static User ToEntity(GitHubUserResponse dto)
        {
            return new User
            {
                Login = dto.Login,
                Name = dto.Name,
                Location = dto.Location,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
            };
        }

        public static UserDTO ToDto(User entity, Stat stat)
        {
            if (entity == null) return null;

            return new UserDTO
            {
                Login = entity.Login,
                Name = entity.Name,
                Location = entity.Location,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Stat = StatsMapper.ToDto(stat)
            };
        }
    }
}
