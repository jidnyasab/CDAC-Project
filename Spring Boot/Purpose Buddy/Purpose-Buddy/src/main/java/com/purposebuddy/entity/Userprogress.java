package com.purposebuddy.entity;

import jakarta.persistence.*;
import java.util.Date;

@Entity
@Table(name = "userprogress")
public class Userprogress {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "user_id", nullable = false)
    private String userId;

    @Column(name = "chapter_id", nullable = false)
    private String chapterId;

    @Column(name = "is_completed", nullable = false)
    private boolean isCompleted;

    @Temporal(TemporalType.TIMESTAMP)
    @Column(name = "created_at", nullable = false)
    private Date createdAt;

    @Temporal(TemporalType.TIMESTAMP)
    @Column(name = "updated_at", nullable = false)
    private Date updatedAt;
   
 // Constructors, getters, and setters
    
	public Userprogress() {
		super();
	}



	public Userprogress(Long id, String userId, String chapterId, boolean isCompleted, Date createdAt, Date updatedAt) {
		super();
		this.id = id;
		this.userId = userId;
		this.chapterId = chapterId;
		this.isCompleted = isCompleted;
		this.createdAt = createdAt;
		this.updatedAt = updatedAt;
	}



	public Long getId() {
		return id;
	}



	public void setId(Long id) {
		this.id = id;
	}



	public String getUserId() {
		return userId;
	}



	public void setUserId(String userId) {
		this.userId = userId;
	}



	public String getChapterId() {
		return chapterId;
	}



	public void setChapterId(String chapterId) {
		this.chapterId = chapterId;
	}



	public boolean isCompleted() {
		return isCompleted;
	}



	public void setCompleted(boolean isCompleted) {
		this.isCompleted = isCompleted;
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
