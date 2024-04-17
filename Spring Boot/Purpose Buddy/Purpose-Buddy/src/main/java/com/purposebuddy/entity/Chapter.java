package com.purposebuddy.entity;

import jakarta.persistence.*;
import java.util.Date;


@Entity
@Table(name = "chapter")
public class Chapter {

    @Id
    @Column(length = 191, nullable = false)
    private String id;

    @Column(length = 191, nullable = false)
    private String title;

    @Column(columnDefinition = "TEXT")
    private String description;

    @Column(columnDefinition = "TEXT")
    private String videoUrl;

    @Column(nullable = false)
    private int position;

    @Column(name = "isPublished", nullable = false)
    private boolean isPublished;

    @Column(name = "isFree", nullable = false)
    private boolean isFree;

    @Column(name = "courseId", length = 191, nullable = false)
    private String courseId;

    @Temporal(TemporalType.TIMESTAMP)
    @Column(name = "createdAt", nullable = false, columnDefinition = "DATETIME(3) DEFAULT CURRENT_TIMESTAMP(3)")
    private Date createdAt;

    @Temporal(TemporalType.TIMESTAMP)
    @Column(name = "updatedAt", nullable = true)
    private Date updatedAt;
    
 // Constructors, getters, and setters
	public Chapter() {
		super();
	}

public Chapter(String id, String title, String description, String videoUrl, int position, boolean isPublished,
		boolean isFree, String courseId, Date createdAt, Date updatedAt) {
	super();
	this.id = id;
	this.title = title;
	this.description = description;
	this.videoUrl = videoUrl;
	this.position = position;
	this.isPublished = isPublished;
	this.isFree = isFree;
	this.courseId = courseId;
	this.createdAt = createdAt;
	this.updatedAt = updatedAt;
}

public String getId() {
	return id;
}

public void setId(String id) {
	this.id = id;
}

public String getTitle() {
	return title;
}

public void setTitle(String title) {
	this.title = title;
}

public String getDescription() {
	return description;
}

public void setDescription(String description) {
	this.description = description;
}

public String getVideoUrl() {
	return videoUrl;
}

public void setVideoUrl(String videoUrl) {
	this.videoUrl = videoUrl;
}

public int getPosition() {
	return position;
}

public void setPosition(int position) {
	this.position = position;
}

public boolean isPublished() {
	return isPublished;
}

public void setPublished(boolean isPublished) {
	this.isPublished = isPublished;
}

public boolean isFree() {
	return isFree;
}

public void setFree(boolean isFree) {
	this.isFree = isFree;
}

public String getCourseId() {
	return courseId;
}

public void setCourseId(String courseId) {
	this.courseId = courseId;
}

public Date getCreatedAt() {
	return createdAt;
}

public void setCreatedAt(Date createdAt) {
	this.createdAt = createdAt;
}

public Date getUpdatedAt() {
	return updatedAt;
}

public void setUpdatedAt(Date updatedAt) {
	this.updatedAt = updatedAt;
}
	
	

    
    
}

