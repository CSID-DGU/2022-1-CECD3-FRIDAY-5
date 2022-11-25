package com.mysite.diary_backend;

import org.springframework.data.jpa.repository.JpaRepository;

import java.time.LocalDateTime;

public interface DiaryRepository extends JpaRepository<Diary, String> {
    Diary findByMemberAndDatecreate(Member member, LocalDateTime datecreate);
}