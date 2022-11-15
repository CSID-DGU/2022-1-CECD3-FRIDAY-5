package com.mysite.sbb;

import org.springframework.data.jpa.repository.JpaRepository;

public interface MemberRepository extends JpaRepository<Member, String> {
    Member findByIdAndPassword(String id, String password);
}