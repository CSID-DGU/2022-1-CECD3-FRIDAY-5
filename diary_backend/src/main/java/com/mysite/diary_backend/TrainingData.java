package com.mysite.diary_backend;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Entity
public class TrainingData {
    @Id
    private String train_text;

    @Column()
    private String train_sentiment;
}