package com.mysite.diary_backend;

import javax.persistence.*;

import lombok.Getter;
import lombok.Setter;
import javax.persistence.ManyToOne;

@Getter
@Setter
@Entity
public class Friend {
    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE)
    private Integer nothing;

    @Column
    private String id;


    @ManyToOne
    private Member member;
}