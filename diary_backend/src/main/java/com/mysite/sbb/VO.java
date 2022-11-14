package com.mysite.sbb;

import lombok.*;

import javax.persistence.Column;
import java.time.LocalDateTime;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class VO
{
    private String old_id="default";
    private String old_password="default";

    private String id="default";
    private String password="default";
    private String name="default";
    private Double happinessmoney=-1.0;
    private Double disgustmoney=-1.0;
    private Double sadnessmoney=-1.0;
    private Double angrymoney=-1.0;
    private Double surprisemoney=-1.0;
    private Double fearmoney=-1.0;
    private Double neutralmoney=-1.0;

    private String targetdate="default";

    private LocalDateTime datecreate=LocalDateTime.now();
    private String text="default";
    private Double happiness=-1.0;
    private Double disgust=-1.0;
    private Double sadness=-1.0;
    private Double angry=-1.0;
    private Double surprise=-1.0;
    private Double fear=-1.0;
    private Double neutral=-1.0;

    private String friendid="default";

    private String train_text="default";
    private String train_sentiment="default";


    private String treeid="default";
    private String treename="default";
    private Double positionx=-1.0;
    private Double positiony=-1.0;
    private Double positionz=-1.0;

    private String reader="default";
    private String date_start="default";
    private String date_end="default";
    private Integer sum=-1;
    private String message="default";
}