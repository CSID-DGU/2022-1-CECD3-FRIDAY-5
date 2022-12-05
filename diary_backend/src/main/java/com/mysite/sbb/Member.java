package com.mysite.sbb;

import java.time.LocalDateTime;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Entity
public class Member {
    //Email
    @Id
    private String id;

    @Column()
    private String password;

    //NickName
    @Column()
    private String name;

    @Column()
    private Double happinessmoney;

    @Column()
    private Double disgustmoney;

    @Column()
    private Double sadnessmoney;

    @Column()
    private Double angrymoney;

    @Column()
    private Double surprisemoney;

    @Column()
    private Double fearmoney;

    @Column()
    private Double neutralmoney;

    @OneToMany(mappedBy = "member", cascade = CascadeType.REMOVE)
    private List<Diary> diaryList;

    @OneToMany(mappedBy = "member", cascade = CascadeType.REMOVE)
    private List<Friend> friendList;

    @OneToMany(mappedBy = "member", cascade = CascadeType.REMOVE)
    private List<Tree> treeList;

}