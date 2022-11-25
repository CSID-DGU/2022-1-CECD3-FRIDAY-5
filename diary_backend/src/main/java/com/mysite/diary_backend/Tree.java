package com.mysite.diary_backend;

import javax.persistence.*;

import lombok.Getter;
import lombok.Setter;
import javax.persistence.ManyToOne;
@Getter
@Setter
@Entity
public class Tree {
    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE)
    private Integer treeid;

    @ManyToOne
    private Member member;

    @Column()
    private String treename;

    @Column()
    private Double positionx;

    @Column()
    private Double positiony;

    @Column()
    private Double positionz;


}