using FundProjectAPI.Data;
using FundProjectAPI.DTOs;
using FundProjectAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundProjectAPI.Service
{
    public class ProjectCreatorService : IProjectCreatorService
    {
        private readonly FundContext _fundContext;

        public string Reward { get; private set; }

        public ProjectCreatorService(FundContext context)
        {
            _fundContext = context;
        }

        public async Task<ProjectCreatorDto> AddProjectCreator(ProjectCreatorDto dto)
        {
            var projectCreator = new ProjectCreator()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
            var creator = await GetProjectCreatorByEmail(dto.Email);
            if (creator is not null){
                throw new Exception("There is already a user with this email.");
            }

            _fundContext.ProjectCreators.Add(projectCreator);

            await _fundContext.SaveChangesAsync();
            return projectCreator.Convert();
        }

        public async Task<ProjectCreatorDto> GetProjectCreator(int id)
        {
            var projectCreator = await _fundContext.ProjectCreators.SingleOrDefaultAsync(a => a.Id == id);
            return projectCreator.Convert();

        }

        public async Task<List<ProjectCreatorDto>> GetProjectCreators()
        {
            return await _fundContext.ProjectCreators
               .Select(p => p.Convert())
               .ToListAsync();
        }

        public async Task<ProjectCreatorDto> Update(int projectCreatorId, ProjectCreatorDto dto)
        {
            if (dto is null)
                throw new ArgumentException("Data format problem");
            ProjectCreator projectCreator = await _fundContext.ProjectCreators
              .SingleOrDefaultAsync(pc => pc.Id == projectCreatorId);

            if (projectCreator is null) throw new NotFoundException("The project Creator id is invalid or has been removed.");

            if (dto.FirstName is null || dto.LastName is null || dto.Email is null)
            {
                throw new ArgumentException("Project creator must have first name,last name and email.");
            }

            projectCreator.FirstName = dto.FirstName;
            projectCreator.LastName = dto.LastName;
            projectCreator.Email = dto.Email;
            await _fundContext.SaveChangesAsync();
            return projectCreator.Convert();
        }

        public async Task<ProjectCreatorDto> Replace(int projectCreatorId, ProjectCreatorDto dto)
        {
            ProjectCreator projectCreator = await _fundContext.ProjectCreators
                .SingleOrDefaultAsync(pc => pc.Id == projectCreatorId);

            if (projectCreator is null) throw new NotFoundException("The project creator id is invalid or has been removed.");

            projectCreator.FirstName = dto.FirstName;
            projectCreator.LastName = dto.LastName;
            projectCreator.Email = dto.Email;

            await _fundContext.SaveChangesAsync();

            return projectCreator.Convert();
        }

        public async Task<bool> Delete(int projectCreatorId)
        {
            ProjectCreator projectCreator = await _fundContext.ProjectCreators.SingleOrDefaultAsync(pc => pc.Id == projectCreatorId);
            if (projectCreator is null) return false;
            _fundContext.Remove(projectCreator);

            await _fundContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProjectDto> AddProjectToProjectCreator(int projectCreatorId, ProjectDto dto)
        {
            if (dto is null)
                throw new ArgumentException("Data format problem");
            ProjectCreator projectCreator = await _fundContext.ProjectCreators
                .SingleOrDefaultAsync(pc => pc.Id == projectCreatorId);

            if (projectCreator is null)
                throw new NotFoundException("The project creator id is invalid or the project creator has been removed.");
            if (dto.Title is null || dto.Description is null)
                throw new ArgumentException("Project creator must have first name,last name and email.");

            Project project = dto.Convert();
            if (dto.RewardPackageDtos is not null)
            {
                dto.RewardPackageDtos.ForEach(dto => project.RewardPackages.Add(dto.Convert()));
                //foreach (var rewardPackageDto in dto.RewardPackageDtos)
                //{
                //    project.RewardPackages.Add(rewardPackageDto.Convert());
                //}
            }
            projectCreator.Projects.Add(project);

            await _fundContext.SaveChangesAsync();

            return dto;
        }


        public async Task<ProjectCreatorDto> GetProjectCreatorByEmail(string email) {
            var projectCreator = await _fundContext.ProjectCreators.SingleOrDefaultAsync(a => a.Email == email);
            return projectCreator.Convert();
        }

        public async Task<List<ProjectDto>> GetProjectCreatorProjects(int creatorId)
        {
            var projectCreator = await _fundContext.ProjectCreators.SingleOrDefaultAsync(c => c.Id == creatorId);
            return projectCreator.Projects.Select(c=>c.Convert()).ToList();
        }

        }
}
