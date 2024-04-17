package com.purposebuddy.repository;


import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.purposebuddy.entity.Course;

@Repository
public interface CourseRepository extends JpaRepository<Course, String> {

    List<Course> findByTitleContainingAndCategoryIdAndPurchasesUserIdAndIsPublishedTrue(
            String title, Long categoryId, String userId);

    List<Course> findByTitleContainingAndCategoryIdAndIsPublishedTrue(String title, Long categoryId);

    List<Course> findByCategoryIdAndPurchasesUserIdAndIsPublishedTrue(Long categoryId, String userId);

    List<Course> findByTitleContainingAndPurchasesUserIdAndIsPublishedTrue(String title, String userId);

    List<Course> findByTitleContainingAndIsPublishedTrue(String title);

    List<Course> findByCategoryIdAndIsPublishedTrue(Long categoryId);

    List<Course> findByPurchasesUserIdAndIsPublishedTrue(String userId);

    List<Course> findByIsPublishedTrue();
}

