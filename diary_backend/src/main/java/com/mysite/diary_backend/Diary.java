package com.mysite.diary_backend;

import java.time.LocalDateTime;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;

import lombok.Getter;
import lombok.Setter;
import javax.persistence.ManyToOne;

@Getter
@Setter
@Entity
public class Diary {

    @Id
    private String memberDatecreate;

    @ManyToOne
    private Member member;

    @Column()
    private LocalDateTime datecreate;

    @Column(columnDefinition = "TEXT")
    private String text;

    @Column()
    private Double happiness;

    @Column()
    private Double disgust;

    @Column()
    private Double sadness;

    @Column()
    private Double angry;

    @Column()
    private Double surprise;

    @Column()
    private Double fear;

    @Column()
    private Double neutral;


}