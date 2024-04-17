using Microsoft.EntityFrameworkCore;
using PurposeBuddy.Controllers;
using PurposeBuddy.Models;
using PurposeBuddy.Models.Entities;

namespace PurposeBuddy.Service
{
    public class DbService : IDbService
    {
        private readonly ProjectDbContext _dbContext;

        public DbService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Course> GetCourse(string courseId, string userId)
        {
            var course = await _dbContext.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId && c.UserId == userId);
            return course;
        }

        public async Task<Course> GetCourseWithChapters(string courseId, string userId)
        {
            var course = await _dbContext.Courses
                .Include(c => c.Chapters)
                    .ThenInclude(ch => ch.Muxdatum)
                .FirstOrDefaultAsync(c => c.Id == courseId && c.UserId == userId);

            return course;
        }

        public async Task DeleteCourse(string courseId)
        {
            var course = await _dbContext.Courses.FindAsync(courseId);

            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Course?> UpdateCourse(string courseId, string userId, CourseUpdateModel updateModel)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.UserId == userId);
            if (course == null) return null;

            _dbContext.Courses.Attach(course);
            var entry = _dbContext.Entry(course);

            // Reflectively set properties that are not null on the update model
            foreach (var property in typeof(CourseUpdateModel).GetProperties())
            {
                var value = property.GetValue(updateModel);
                if (value != null)
                {
                    var courseProperty = typeof(Course).GetProperty(property.Name);
                    if (courseProperty != null)
                    {
                        courseProperty.SetValue(course, value);
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            _dbContext.Courses.Update(course);
            await _dbContext.SaveChangesAsync();
            return course;
        }
        public String ValidateUserId()
        {
           String userId = "user_2cQ0890BvlgGUmteLZKAgujOaP4"; // Get the user ID from authentication Logic
            return userId;
        }

        public bool IsTeacher(string userId)
        {

            return userId == "user_2cQ0890BvlgGUmteLZKAgujOaP4";  //Logic To Check User
        }


        public async Task<bool> IsCourseOwnedByUser(string courseId, string userId)
        {
            return await _dbContext.Courses.AnyAsync(c => c.Id == courseId && c.UserId == userId);
        }

        public async Task<int> GetLastChapterPosition(string courseId)
        {
            var lastChapter = await _dbContext.Chapters
                .Where(c => c.CourseId == courseId)
                .OrderByDescending(c => c.Position)
                .FirstOrDefaultAsync();

            return lastChapter?.Position ?? 0;
        }

        public async Task<Chapter> CreateChapter(string courseId, string title, int position)
        {
            var chapter = new Chapter
            {
                Id = Guid.NewGuid().ToString(), // Generate a new UUID and convert it to string
                Title = title,
                CourseId = courseId,
                Position = position
            };

            _dbContext.Chapters.Add(chapter);
            await _dbContext.SaveChangesAsync();

            return chapter;
        }

        public async Task<Chapter> GetChapter(string courseId, string chapterId)
        {
            return await _dbContext.Chapters.FirstOrDefaultAsync(c => c.Id == chapterId && c.CourseId == courseId);
        }

        public async Task<Muxdatum> GetMuxDataByChapterId(string chapterId)
        {
            return await _dbContext.Muxdata.FirstOrDefaultAsync(m => m.ChapterId == chapterId);
        }

        public async Task DeleteMuxData(string muxDataId)
        {
            var muxDataToDelete = await _dbContext.Muxdata.FindAsync(muxDataId);
            if (muxDataToDelete != null)
            {
                _dbContext.Muxdata.Remove(muxDataToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteChapter(string chapterId)
        {
            var chapterToDelete = await _dbContext.Chapters.FindAsync(chapterId);
            if (chapterToDelete != null)
            {
                _dbContext.Chapters.Remove(chapterToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Chapter>> GetPublishedChaptersInCourse(string courseId)
        {
            return await _dbContext.Chapters.Where(c => c.CourseId == courseId && c.IsPublished).ToListAsync();
        }

        public async Task UpdateCourseIsPublished(string courseId, bool isPublished)
        {
            var courseToUpdate = await _dbContext.Courses.FindAsync(courseId);
            if (courseToUpdate != null)
            {
                courseToUpdate.IsPublished = isPublished;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateChapter(string chapterId, ChapterUpdateModel model)
        {
            var chapter = await _dbContext.Chapters.FirstOrDefaultAsync(c => c.Id == chapterId);
            if (chapter != null)
            {
                if (!string.IsNullOrEmpty(model.Title))
                {
                    chapter.Title = model.Title;
                }

                if (!string.IsNullOrEmpty(model.Description))
                {
                    chapter.Description = model.Description;
                }

                if (!string.IsNullOrEmpty(model.VideoUrl))
                {
                    chapter.VideoUrl = model.VideoUrl;
                }

                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
