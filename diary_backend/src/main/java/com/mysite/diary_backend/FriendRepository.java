package com.mysite.diary_backend;

import org.springframework.data.jpa.repository.JpaRepository;

import java.time.LocalDateTime;

public interface FriendRepository extends JpaRepository<Friend, String> {
    Friend findByIdAndMember(String id, Member member);
}