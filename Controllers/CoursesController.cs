using Microsoft.AspNetCore.Mvc;
using PurposeBuddy.Models;
using PurposeBuddy.Models.Entities;
using PurposeBuddy.Service;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace PurposeBuddy.Controllers
{

    [ApiController]
    [Route("/api/courses")]
    public class CoursesController : Controller
    {

        private readonly ProjectDbContext _dbContext;
        private readonly IDbService _dbService;

        public CoursesController(ProjectDbContext Context, IDbService dbService)
        {

            _dbContext = Context;
            _dbService = dbService;
        }


        [HttpPost] //Create Course Starts
        public async Task<IActionResult> CreateCourse([FromBody] CourseRequestModel model)

        {
            try
            {
                string userId = _dbService.ValidateUserId();

                if (string.IsNullOrEmpty(userId) || !_dbService.IsTeacher(userId))
                {
                    return Unauthorized();
                }

                var course = new Course
                {
                    Id = Guid.NewGuid().ToString(), // Generate a new UUID
                    UserId = userId,
                    Title = model.Title
                };

                _dbContext.Courses.Add(course);
                await _dbContext.SaveChangesAsync();

                return Ok(course);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[COURSES]" + ex.Message);
                return StatusCode(500, "Internal Error");
            }
        }


        [HttpDelete("{courseId}")] //Delete Course Starts
        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            try
            {
                string userId = _dbService.ValidateUserId();

                if (string.IsNullOrEmpty(userId) || !_dbService.IsTeacher(userId))
                {
                    return Unauthorized();
                }

                var course = await _dbService.GetCourseWithChapters(courseId, userId);

                if (course == null)
                {
                    return NotFound();
                }

                await _dbService.DeleteCourse(courseId);

                return Ok(course);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[COURSE_DELETE]", ex);
                return StatusCode(500, "Internal Error");
            }
        }


        [HttpPatch("{courseId}")] //Update Aay Open Field Of the Model Like Title,Price etc
        public async Task<IActionResult> UpdateCourse(string courseId, [FromBody] CourseUpdateModel model)
        {
            var userId = _dbService.ValidateUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var updatedCourse = await _dbService.UpdateCourse(courseId, userId, model);

            if (updatedCourse == null)
            {
                return NotFound($"No course found with ID {courseId} for the current user.");
            }

            return Ok(updatedCourse);
        }


        [HttpPatch("{courseId}/publish")] //Publish The Course
        public async Task<IActionResult> PublishCourse(string courseId)
        {
            try
            {
                var userId = _dbService.ValidateUserId();

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Unauthorized");
                }

                var course = await _dbService.GetCourseWithChapters(courseId, userId);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                var hasPublishedChapter = course.Chapters.Any(chapter => chapter.IsPublished);

                if (string.IsNullOrEmpty(course.Title) ||
                    string.IsNullOrEmpty(course.Description) ||
                    string.IsNullOrEmpty(course.ImageUrl) ||
                    string.IsNullOrEmpty(course.CategoryId) ||
                    !hasPublishedChapter)
                {
                    return BadRequest("Missing required fields");
                }

                course.IsPublished = true;
                await _dbService.UpdateCourse(course);

                // Serialize the course object with reference handling
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    // Add any other options you may need
                };

                return Ok(JsonSerializer.Serialize(course, options));
            }
            catch (Exception ex)
            {
                Console.WriteLine("[COURSE_ID_PUBLISH] Exception: " + ex.ToString());
                return StatusCode(500, "Internal Error");
            }
        }

        [HttpPatch("{courseId}/unpublish")] //UnPublish The Course
        public async Task<IActionResult> UnpublishCourse(string courseId)
        {
            try
            {
                var userId = _dbService.ValidateUserId();

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Unauthorized");
                }

                var course = await _dbService.GetCourse(courseId, userId);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                course.IsPublished = false;
                await _dbService.UpdateCourse(course);

                return Ok(course);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[COURSE_ID_UNPUBLISH]" + ex);
                return StatusCode(500, "Internal Error");
            }
        }


        //API's For Chapter

        [HttpPost("{courseId}/chapters")]  //Creating Chapter Api  
        public async Task<IActionResult> CreateChapter(string courseId, [FromBody] ChapterCreateModel model)
        {
            try
            {
                var userId = _dbService.ValidateUserId();

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Unauthorized");
                }

                var courseOwner = await _dbService.IsCourseOwnedByUser(courseId, userId);

                if (!courseOwner)
                {
                    return Unauthorized("Unauthorized");
                }

                var lastChapterPosition = await _dbService.GetLastChapterPosition(courseId);

                var newPosition = lastChapterPosition + 1;

                var chapter = await _dbService.CreateChapter(courseId, model.Title, newPosition);

                return Ok(chapter);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CHAPTERS]" + ex);
                return StatusCode(500, "Internal Error");
            }
        }

        [HttpDelete("{courseId}/chapters/{chapterId}")]
        public async Task<IActionResult> DeleteChapter(string courseId, string chapterId)
        {
            try
            {
                var userId = _dbService.ValidateUserId();

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Unauthorized");
                }

                // Check if the user owns the course
                var ownCourse = await _dbService.IsCourseOwnedByUser(courseId, userId);
                if (!ownCourse)
                {
                    return Unauthorized("Unauthorized");
                }

                // Find the chapter to delete
                var chapter = await _dbService.GetChapter(courseId, chapterId);
                if (chapter == null)
                {
                    return NotFound("Not Found");
                }

                // If the chapter has a video, delete associated MuxData
                if (!string.IsNullOrEmpty(chapter.VideoUrl))
                {
                    var existingMuxData = await _dbService.GetMuxDataByChapterId(chapterId);
                    if (existingMuxData != null)
                    {

                        await _dbService.DeleteMuxData(existingMuxData.Id);
                    }
                }

                // Delete the chapter
                await _dbService.DeleteChapter(chapterId);

                // Check if there are any published chapters left in the course
                var publishedChaptersInCourse = await _dbService.GetPublishedChaptersInCourse(courseId);
                if (!publishedChaptersInCourse.Any())
                {
                    // Update the course to set isPublished to false
                    await _dbService.UpdateCourseIsPublished(courseId, false);
                }

                return Ok("Chapter deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CHAPTER_ID_DELETE]", ex);
                return StatusCode(500, "Internal Error");
            }

        }

        [HttpPatch("{courseId}/chapters/{chapterId}")]
        public async Task<IActionResult> UpdateChapter(string courseId, string chapterId, [FromBody] ChapterUpdateModel model)
        {
            try
            {
                var userId =  _dbService.ValidateUserId();

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Unauthorized");
                }
                
                var ownCourse = await _dbService.IsCourseOwnedByUser(courseId, userId);
                if (!ownCourse)
                {
                    return Unauthorized("Unauthorized");
                }

                await _dbService.UpdateChapter(chapterId, model);

                return Ok("Chapter updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[COURSES_CHAPTER_ID]", ex);
                return StatusCode(500, "Internal Error");
            }
        }
    }
}

