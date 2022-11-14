package com.mysite.sbb;

import java.time.LocalDateTime;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import lombok.Getter;
import lombok.Setter;
import javax.persistence.ManyToOne;

@Getter
@Setter
@Entity
public class Diary {

    @ManyToOne
    private Member member;

    @Id
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