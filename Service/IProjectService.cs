
using FundProjectAPI.Model;
using FundProjectAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundProjectAPI.Service
{
    public interface IProjectService
    {
        public Task<ProjectDto> GetProject(int id);
        public Task<List<ProjectDto>> GetAllProjects();
        public Task<ProjectDto> AddProject(ProjectDto dto);
        public Task<List<ProjectDto>> Search(string title);
        public Task<ProjectDto> Update(int projectId, ProjectDto dto);
        public Task<ProjectDto> Replace(int projectId, ProjectDto dto);
        public Task<bool> Delete(int projectId);
        public Task<ProjectDto> AddProject2Backer(int backerId, decimal fundamount, ProjectDto dto);
        public Task<List<ProjectDto>> SelectCategory(ProjectCategory category);
        public Task<List<ProjectDto>> FundedProjects(int backerId);
        public Task<List<ProjectDto>> CreatorProjects(int creatorId);
    }
}
