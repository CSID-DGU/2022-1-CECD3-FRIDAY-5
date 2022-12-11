package com.mysite.diary_backend;

import org.springframework.data.jpa.repository.JpaRepository;

import java.time.LocalDateTime;

public interface TreeRepository extends JpaRepository<Tree, String> {
    Tree findByTreeid(Integer treeid);
}