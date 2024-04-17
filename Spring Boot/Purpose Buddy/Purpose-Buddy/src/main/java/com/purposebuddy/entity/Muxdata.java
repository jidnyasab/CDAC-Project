package com.purposebuddy.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "muxdata")
public class Muxdata {

    @Id
    @Column(length = 191, nullable = false)
    private String id;

    @Column(name = "asset_id", length = 191, nullable = false)
    private String assetId;

    @Column(name = "playback_id", length = 191)
    private String playbackId;

    @Column(name = "chapter_id", length = 191, nullable = false, unique = true)
    private String chapterId;
    
 // Constructors, getters, and setters
	public Muxdata() {
		super();
	}


	public Muxdata(String id, String assetId, String playbackId, String chapterId) {
		super();
		this.id = id;
		this.assetId = assetId;
		this.playbackId = playbackId;
		this.chapterId = chapterId;
	}


	public String getId() {
		return id;
	}


	public void setId(String id) {
		this.id = id;
	}


	public String getAssetId() {
		return assetId;
	}


	public void setAssetId(String assetId) {
		this.assetId = assetId;
	}


	public String getPlaybackId() {
		return playbackId;
	}


	public void setPlaybackId(String playbackId) {
		this.playbackId = playbackId;
	}


	public String getChapterId() {
		return chapterId;
	}


	public void setChapterId(String chapterId) {
		this.chapterId = chapterId;
	}
	
	
     
    
    
}

