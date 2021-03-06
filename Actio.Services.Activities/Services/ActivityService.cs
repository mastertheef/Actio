﻿using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activiryCategory = await _categoryRepository.GetAsync(category);
            if (activiryCategory == null)
            {
                throw new ActioException("category_not_found", $"Category for a given name '{category}' was not found.");
            }

            var activity = new Activity(id, name, activiryCategory, description, userId, createdAt);
            await _activityRepository.AddAsync(activity);
        }
    }
}
