using PurposeBuddy.Controllers;
using PurposeBuddy.Models;
using PurposeBuddy.Models.Entities;

namespace PurposeBuddy.Service
{
    public interface IDbService
    {
        String ValidateUserId();

        //Services For Course
        Task<Course> GetCourse(string courseId, string userId);
        Task<Course> GetCourseWithChapters(string courseId, string userId);
        Task DeleteCourse(string courseId);

        Task<Course?> UpdateCourse(string courseId, string userId, CourseUpdateModel updateModel);

        Task<Course> UpdateCourse(Course course);
        

        bool IsTeacher(string userId);

        //Services For Chapter
        Task<bool> IsCourseOwnedByUser(string courseId, string userId);
        Task<int> GetLastChapterPosition(string courseId);
        Task<Chapter> CreateChapter(string courseId, string title, int position);

        Task<Chapter> GetChapter(string courseId, string chapterId);

        Task<Muxdatum> GetMuxDataByChapterId(string chapterId);
        Task DeleteMuxData(string muxDataId);
        Task DeleteChapter(string chapterId);
        Task<List<Chapter>> GetPublishedChaptersInCourse(string courseId);
        Task UpdateCourseIsPublished(string courseId, bool isPublished);

        Task UpdateChapter(string chapterId, ChapterUpdateModel model);

    }
}
