package com.purposebuddy.entity;

import jakarta.persistence.*;
import java.util.Date;

@Entity
@Table(name = "attachment")
public class Attachment {

    @Id
    @Column(length = 191, nullable = false)
    private String id;

    @Column(length = 191, nullable = false)
    private String name;

    @Column(columnDefinition = "TEXT", nullable = false)
    private String url;

    @Column(name = "course_id", length = 191, nullable = false)
    private String courseId;

    @Temporal(TemporalType.TIMESTAMP)
    @Column(name = "created_at", nullable = false, columnDefinition = "DATETIME(3) DEFAULT CURRENT_TIMESTAMP(3)")
    private Date createdAt;

    @Temporal(TemporalType.TIMESTAMP)
    @Column(name = "updated_at", nullable = true)
    private Date updatedAt;

    // Constructors, getters, and setters
	public Attachment() {
		super();
	}
    
	public Attachment(String id, String name, String url, String courseId, Date createdAt, Date updatedAt) {
		super();
		this.id = id;
		this.name = name;
		this.url = url;
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

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
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
