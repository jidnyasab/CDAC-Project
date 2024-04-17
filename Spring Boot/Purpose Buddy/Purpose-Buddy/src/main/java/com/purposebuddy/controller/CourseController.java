package com.purposebuddy.controller;

import com.purposebuddy.entity.Course;
import com.purposebuddy.repository.CourseRepository;
import com.purposebuddy.service.CourseService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import java.util.List;

@RestController
@RequestMapping("/api")
public class CourseController {

    @Autowired
    private CourseService courseService;
    @Autowired
    private CourseRepository courseRepository;
    
    @GetMapping("/try") 
    public String getTry() {
        return "hi";
    }

    @GetMapping("/api/courses")
    public ResponseEntity<List<Course>> getCourses(
            @RequestParam(required = false) String title,
            @RequestParam(required = false) Long categoryId,
            @RequestParam(required = false) String userId) {

        List<Course> courses;

        // Check which combination of parameters are provided and retrieve courses accordingly
        if (title != null && categoryId != null && userId != null) {
            courses = courseRepository.findByTitleContainingAndCategoryIdAndPurchasesUserIdAndIsPublishedTrue(
                    title, categoryId, userId);
        } else if (title != null && categoryId != null) {
            courses = courseRepository.findByTitleContainingAndCategoryIdAndIsPublishedTrue(title, categoryId);
        } else if (categoryId != null && userId != null) {
            courses = courseRepository.findByCategoryIdAndPurchasesUserIdAndIsPublishedTrue(categoryId, userId);
        } else if (title != null && userId != null) {
            courses = courseRepository.findByTitleContainingAndPurchasesUserIdAndIsPublishedTrue(title, userId);
        } else if (title != null) {
            courses = courseRepository.findByTitleContainingAndIsPublishedTrue(title);
        } else if (categoryId != null) {
            courses = courseRepository.findByCategoryIdAndIsPublishedTrue(categoryId);
        } else if (userId != null) {
            courses = courseRepository.findByPurchasesUserIdAndIsPublishedTrue(userId);
        } else {
            courses = courseRepository.findByIsPublishedTrue();
        }

        // Return the courses as ResponseEntity
        return ResponseEntity.ok(courses);
    }
}

