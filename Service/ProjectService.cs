using FundProjectAPI.Data;
using FundProjectAPI.DTOs;
using FundProjectAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundProjectAPI.Service
{
    public class ProjectService : IProjectService
    {
        private readonly FundContext _fundContext;
        public ProjectService(FundContext context)
        {
            _fundContext = context;         
        }
        public async Task<ProjectDto> AddProject(ProjectDto dto) 
        {
            ProjectCreator projectCreator = await _fundContext.ProjectCreators.SingleOrDefaultAsync(c => c.Id == dto.ProjectCreatorId);
            if (projectCreator == null) return null;

            Project project = new Project()
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Goal = dto.Goal
            };

            _fundContext.Projects.Add(project);
            _fundContext.SaveChanges();

            return new ProjectDto()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                Goal = project.Goal
            };
        }

        public async Task<bool> Delete(int projectId)
        {
            Project project = await _fundContext.Projects.SingleOrDefaultAsync(p => p.Id == projectId);
            if (project is null) return false;

            _fundContext.Remove(project);
            await _fundContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProjectDto>> GetAllProjects()
        {

            return await _fundContext.Projects
                .Select(p => p.Convert())
                .ToListAsync();
        }


        public async Task<ProjectDto> GetProject(int id)
        {
            var project = await _fundContext.Projects
                //.Include(p => p.ProjectCreatorId)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return null;
            }

            return new ProjectDto()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                Goal = project.Goal

            };
        }

        public async Task<ProjectDto> Replace(int projectId, ProjectDto dto)
        {
            Project project = await _fundContext.Projects
                .SingleOrDefaultAsync(p => p.Id == projectId);

            if (project is null) return null;
                //throw new NotFoundException("The book id is invalid or has been removed.");


            //If a property does not exist in the request body, it will be updated to null
            project.Title = dto.Title;
            project.Description = dto.Description;
            project.Category = dto.Category;
            project.Goal = dto.Goal;

            await _fundContext.SaveChangesAsync();

            return new ProjectDto()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                Goal = project.Goal

            };
        }

            public async Task<List<ProjectDto>> Search([FromQuery] string title)
            {
            var results = _fundContext.Projects.Select(p => p);

            if (title != null)
            {
                results = results.Where(b => b.Title.ToLower().Contains(title.ToLower()));
            } 
            
            var resultsList = await results.ToListAsync();

            //if (resultsList == null) return null;

            List<ProjectDto> projectDtos = new();
            foreach (var p in resultsList)
            {
                projectDtos.Add(new ProjectDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Category = p.Category,
                    Goal= p.Goal,
                    GoalGained = p.GoalGained
                });
            }

            return projectDtos;
            }

        public async Task<ProjectDto> Update(int projectId, ProjectDto dto)
        {
            Project project = await _fundContext.Projects
                .SingleOrDefaultAsync(p => p.Id == projectId);

            if (project is null) return null;
                //throw new NotFoundException("The project id is invalid or the project has been removed."); //ask how to fix this

            if (dto.Title is not null) project.Title = dto.Title;
            if (dto.Description is not null) project.Description = dto.Description;
            
            
            await _fundContext.SaveChangesAsync();

            return new ProjectDto()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                Goal = project.Goal,
                GoalGained = project.GoalGained
            };
        }
        public async Task<ProjectDto> AddProject2Backer(int backerId, decimal fundamount, ProjectDto dto)
        {
            if (dto is null)
                throw new ArgumentException("Data format problem");
            Backer backer = await _fundContext.Backers
                .SingleOrDefaultAsync(b => b.Id == backerId);
            if (backer is null)
                throw new NotFoundException("The backer id is invalid, or backer has been removed");
            if (dto.Title is null || dto.Description is null)
                throw new ArgumentException("Project must have Title and description");

            Project project = dto.Convert();
            project.GoalGained += fundamount;  
            backer.Projects.Add(project);


            await _fundContext.SaveChangesAsync();
            return project.Convert();


        }

        public async Task<List<ProjectDto>> SelectCategory([FromQuery] ProjectCategory category)
        {
            var results = _fundContext.Projects.Select(p => p);
                results = results.Where(b => b.Category.Equals(category));
            
            var resultsList = await results.ToListAsync();


            List<ProjectDto> projectDtos = new();
            foreach (var p in resultsList)
            {
                projectDtos.Add(new ProjectDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Category = p.Category,
                    Goal = p.Goal,
                    GoalGained = p.GoalGained
                });
            }

            return projectDtos;
        }
        public async Task<List<ProjectDto>> FundedProjects(int backerId) 
        {            
            Backer backer = await _fundContext.Backers.SingleOrDefaultAsync(b => b.Id == backerId);
      
            return backer.Projects.Select(p => p.Convert()).ToList();            
        }

        public async Task<List<ProjectDto>> CreatorProjects(int creatorId)
        {
            ProjectCreator projectCreator = await _fundContext.ProjectCreators.SingleOrDefaultAsync(b => b.Id == creatorId);

            return projectCreator.Projects.Select(p => p.Convert()).ToList();
        }
    }
}
