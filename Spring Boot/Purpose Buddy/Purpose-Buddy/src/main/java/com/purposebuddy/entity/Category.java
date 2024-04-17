package com.purposebuddy.entity;


import jakarta.persistence.*;

@Entity
@Table(name = "category")
public class Category {

    @Id
    @Column(length = 191, nullable = false)
    private String id;

    @Column(length = 191, nullable = false, unique = true)
    private String name;
  
    // Constructors, getters, and setters
	public Category() {
		super();
	}
	
	

	public Category(String id, String name) {
		super();
		this.id = id;
		this.name = name;
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
	
	

   
    
}

