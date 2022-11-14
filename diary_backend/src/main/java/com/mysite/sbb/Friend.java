package com.mysite.sbb;

import java.time.LocalDateTime;
import java.util.List;

import javax.persistence.*;

import lombok.Getter;
import lombok.Setter;
import javax.persistence.ManyToOne;
import javax.persistence.ManyToMany;
@Getter
@Setter
@Entity
public class Friend {
    @Id
    private String id;


    @ManyToOne
    private Member member;
}