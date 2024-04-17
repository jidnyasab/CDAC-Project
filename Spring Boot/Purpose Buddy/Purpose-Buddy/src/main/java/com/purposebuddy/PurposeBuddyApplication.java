package com.purposebuddy;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;



@SpringBootApplication(scanBasePackages = "com.purposebuddy")
@EnableJpaRepositories(basePackages = "com.purposebuddy.repository")
@EntityScan(basePackages = "com.purposebuddy.entity")
public class PurposeBuddyApplication {

	public static void main(String[] args) {
		SpringApplication.run(PurposeBuddyApplication.class, args);
	}

}
