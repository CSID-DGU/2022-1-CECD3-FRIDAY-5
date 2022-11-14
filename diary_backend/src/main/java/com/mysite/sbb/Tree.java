package com.mysite.sbb;

import java.time.LocalDateTime;
import java.util.List;

import javax.persistence.*;

import lombok.Getter;
import lombok.Setter;
import javax.persistence.ManyToOne;
@Getter
@Setter
@Entity
public class Tree {
    @Id
    private String treeid;

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