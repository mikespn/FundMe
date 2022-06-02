using FundProjectAPI.DTOs;
using FundProjectAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundProjectAPI.Service
{
    public interface IProjectCreatorService
    {
        public Task<ProjectCreatorDto> AddProjectCreator(ProjectCreatorDto dto);
        public Task<ProjectCreatorDto> GetProjectCreator(int id);
        public Task<ProjectCreatorDto> GetProjectCreatorByEmail(string email);
        public Task<List<ProjectCreatorDto>> GetProjectCreators();
        public Task<ProjectCreatorDto> Update(int projectCreatorId, ProjectCreatorDto dto);
        public Task<ProjectCreatorDto> Replace(int projectCreatorId, ProjectCreatorDto dto);
        public Task<bool> Delete(int projectCreatorId);
        public Task<ProjectDto> AddProjectToProjectCreator(int projectCreatorId, ProjectDto dto);
        public Task<List<ProjectDto>> GetProjectCreatorProjects(int creatorId);


    }
}
