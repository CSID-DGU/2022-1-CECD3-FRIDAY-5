package com.mysite.sbb;



import lombok.*;
import org.json.JSONArray;
import org.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;
import java.time.LocalDateTime;
import org.springframework.data.jpa.repository.JpaRepository;

import java.time.format.DateTimeFormatter;
import java.util.Map;
import java.util.Optional;

@RestController
@RequestMapping("")
public class DiaryController {

    class HttpUtils {

        public HttpURLConnection getHttpURLConnection(String strUrl, String method) {
            URL url;
            HttpURLConnection conn = null;
            try {
                url = new URL(strUrl);

                conn = (HttpURLConnection) url.openConnection();
                conn.setRequestMethod(method);
                conn.setConnectTimeout(5000);
                conn.setRequestProperty("Content-Type", "application/json; utf-8");
                conn.setRequestProperty("Accept", "application/json");

            } catch (MalformedURLException e)
            {
                e.printStackTrace();
            } catch(IOException e)
            {
                e.printStackTrace();
            }

            return conn;

        }

        public String getHttpResponse_(HttpURLConnection conn) {
            StringBuilder sb = null;

            try {
                if(conn.getResponseCode() == 200) {
                    sb = readResopnseData(conn.getInputStream());
                }else{
                    sb = readResopnseData(conn.getErrorStream());
                    return null;
                }
            } catch (IOException e) {
                e.printStackTrace();
            }finally {
                conn.disconnect();
            };
            if(sb == null) return null;

            return sb.toString();
        }

        public StringBuilder readResopnseData(InputStream in) {
            if(in == null ) return null;

            StringBuilder sb = new StringBuilder();
            String line = "";

            try (InputStreamReader ir = new InputStreamReader(in,"UTF-8");
                 BufferedReader br = new BufferedReader(ir)){
                while( (line = br.readLine()) != null) {
                    sb.append(line);
                }
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
            return sb;
        }
    }



    @Autowired
    private DiaryRepository diaryRepository;
    @Autowired
    private FriendRepository FriendRepository;
    @Autowired
    private MemberRepository MemberRepository;
    @Autowired
    private TreeRepository TreeRepository;
    @Autowired
    private TrainingDataRepository TrainingDataRepository;

    @GetMapping("/index")
    public String to_index(){
        return "index";
        }

    //회원 create
    @PostMapping("/member_create")
    public String member_create(@RequestBody VO vo) throws IOException
    {
        String id= vo.getId();
        String name=vo.getName();
        String password=vo.getPassword();
        Optional<Member> testMember=this.MemberRepository.findById(id);
        String returnJSON="";

        if(testMember.isPresent())
        {
            returnJSON= "{\"message\" : \""+"member_create_fail"
                    +"\"}";
        }
        else
        {
            Member member=new Member();
            member.setId(id);
            member.setName(name);
            member.setPassword(password);
            member.setHappinessmoney(0.0);
            member.setDisgustmoney(0.0);
            member.setSadnessmoney(0.0);
            member.setAngrymoney(0.0);
            member.setSurprisemoney(0.0);
            member.setFearmoney(0.0);
            member.setNeutralmoney(0.0);
            this.MemberRepository.save(member);
            returnJSON= "{\"message\" : \""+"member_create_success"
                    +"\"}";
        }
        return returnJSON;
    }

    //회원 read (id)
    @PostMapping("/member_read")
    public String member_read(@RequestBody VO vo) throws IOException
    {
        String id= vo.getId();
        String password=vo.getPassword();
        Member testMember=this.MemberRepository.findByIdAndPassword(id,password);
        String returnJSON;


        if(testMember==null)
        {
            returnJSON= "{\"message\" : \""+"member_read_fail"
                    +"\"}";
        }
        else
        {
            returnJSON= "{\"id\" : \""+testMember.getId()+"\",\"name\" : \""
                    +testMember.getName()+"\",\"password\" : \""
                    +testMember.getPassword()+"\",\"happinessmoney\" : \""
                    +testMember.getHappinessmoney()+"\",\"disgustmoney\" : \""
                    +testMember.getDisgustmoney()+"\",\"sadnessmoney\" : \""
                    +testMember.getSadnessmoney()+"\",\"angrymoney\" : \""
                    +testMember.getAngrymoney()+"\",\"surprisemoney\" : \""
                    +testMember.getSurprisemoney()+"\",\"fearmoney\" : \""
                    +testMember.getFearmoney()+"\",\"neutralmoney\" : \""
                    +testMember.getNeutralmoney()+"\",\"message\" : \""
                    +"member_read_success"
                    +"\"}";
        }
        return returnJSON;
    }
    //회원 update
    @PostMapping("/member_update")
    public String member_update(@RequestBody VO vo) throws IOException
    {
        String old_id=vo.getOld_id();
        String old_password=vo.getOld_password();
        String id= vo.getId();
        String password=vo.getPassword();
        String name=vo.getName();
        Double happinessmoney=vo.getHappinessmoney();
        Double disgustmoney=vo.getDisgustmoney();
        Double sadnessmoney=vo.getSadnessmoney();
        Double angrymoney=vo.getAngrymoney();
        Double surprisemoney=vo.getSurprisemoney();
        Double fearmoney=vo.getFearmoney();
        Double neutralmoney=vo.getNeutralmoney();

        Member testMember=this.MemberRepository.findByIdAndPassword(old_id,old_password);
        String returnJSON;

        if(testMember!=null)
        {
            if(id!="default") testMember.setId(id);
            if(name!="default") testMember.setName(name);
            if(password!="default") testMember.setPassword(password);
            if(happinessmoney!=-1.0) testMember.setHappinessmoney(happinessmoney);
            if(disgustmoney!=-1.0) testMember.setId(id);
            if(sadnessmoney!=-1.0) testMember.setSadnessmoney(sadnessmoney);
            if(angrymoney!=-1.0) testMember.setAngrymoney(angrymoney);
            if(surprisemoney!=-1.0) testMember.setSurprisemoney(surprisemoney);
            if(fearmoney!=-1.0) testMember.setFearmoney(fearmoney);
            if(neutralmoney!=-1.0) testMember.setNeutralmoney(neutralmoney);
            this.MemberRepository.save(testMember);
            returnJSON= "{\"message\" : \""+"member_update_success"
                    +"\"}";
        }
        else
        {
            returnJSON= "{\"message\" : \""+"member_update_fail"
                    +"\"}";
        }
        return returnJSON;
    }
    //회원 delete
    @PostMapping("/member_delete")
    public String member_delete(@RequestBody VO vo) throws IOException
    {

        String id= vo.getId();
        String password=vo.getPassword();
        Member testMember=this.MemberRepository.findByIdAndPassword(id,password);
        String returnJSON;

        if(testMember!=null)
        {
            this.MemberRepository.delete(testMember);
            returnJSON= "{\"message\" : \""+"member_delete_success"
                    +"\"}";
        }
        else
        {
            returnJSON= "{\"message\" : \""+"member_delete_fail"
                    +"\"}";
        }
        return returnJSON;
    }

    //일기 create
    @PostMapping("/diary_create")
    public String diary_create(@RequestBody VO vo) throws IOException
    {
        String targetdate = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyyMMdd"));
        LocalDateTime targetdate_ = LocalDateTime.of(Integer.parseInt(targetdate.substring(0,4)),Integer.parseInt(targetdate.substring(4,6)),Integer.parseInt(targetdate.substring(6,8)),0,0);
        System.out.println(targetdate_);
        System.out.println(targetdate);


        String id=vo.getId();
        String text=vo.getText();
        HttpUtils httpUtils = new HttpUtils();
        System.out.println(text);
        String url = "http://172.30.1.9:5000/Diary";

        JSONObject result=new JSONObject();
        HttpURLConnection conn = null;
        conn = httpUtils.getHttpURLConnection(url, "POST");
        conn.setDoOutput(true);
        System.out.println("test0");
        try (DataOutputStream dataOutputStream = new DataOutputStream(conn.getOutputStream());)
        {

            String str = "{\"Text\" : \""+text+"\"}";


            OutputStream wr1 = conn.getOutputStream();
            DataOutputStream wr = new DataOutputStream(conn.getOutputStream());

            wr1.write(str.getBytes("UTF-8"));
            wr1.flush();
            wr1.close();

            result = new JSONObject(httpUtils.getHttpResponse_(conn));


        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println("test01");
        System.out.println(result);
        String strToFront = "{\"sentence\" : \""+(result.get("sentence")).toString()+"\",\"neutral\" : \""
                +(result.get("neutral")).toString()+"\",\"surprise\" : \""+(result.get("surprise")).toString()
                +"\",\"angry\" : \""+(result.get("angry")).toString()+"\",\"happiness\" : \""+(result.get("happiness")).toString()
                +"\",\"fear\" : \""+(result.get("fear")).toString()+"\",\"sadness\" : \""+(result.get("sadness")).toString()+"\",\"disgust\" : \""
                +(result.get("disgust")).toString()+"\"}";



        System.out.println("test1");
      //일기 데이터 저장
        Diary diary=new Diary();
        Optional<Member> testMember = this.MemberRepository.findById(id);
        diary.setMember(testMember.get());
        diary.setDatecreate(targetdate_);
        diary.setText(text);
        diary.setHappiness(Double.parseDouble((result.get("happiness")).toString()));
        diary.setDisgust(Double.parseDouble((result.get("disgust")).toString()));
        diary.setSadness(Double.parseDouble((result.get("sadness")).toString()));
        diary.setAngry(Double.parseDouble((result.get("angry")).toString()));
        diary.setSurprise(Double.parseDouble((result.get("surprise")).toString()));
        diary.setFear(Double.parseDouble((result.get("fear")).toString()));
        diary.setNeutral(Double.parseDouble((result.get("neutral")).toString()));
        this.diaryRepository.save(diary);

        System.out.println("test2");
        //학습 데이터 저장
        JSONArray jarray=(JSONArray) (result.get("sentence"));
        for(int i=0;i< jarray.length();i++)
        {
            JSONArray jarrayarray=(JSONArray)(jarray.get(i));
            TrainingData trainingData=new TrainingData();
            trainingData.setTrain_text(jarrayarray.get(0).toString());
            trainingData.setTrain_sentiment(jarrayarray.get(1).toString());
            this.TrainingDataRepository.save(trainingData);
        }

        System.out.println("test3");
        return strToFront;
    }

    //일기 read
    @PostMapping("/diary_read")
    public String diary_read(@RequestBody VO vo) throws IOException
    {
        String targetdate = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyyMMdd"));
        LocalDateTime targetdate_ = LocalDateTime.of(Integer.parseInt(targetdate.substring(0,4)),Integer.parseInt(targetdate.substring(4,6)),Integer.parseInt(targetdate.substring(6,8)),0,0);

        String id=vo.getId();
        Optional<Member> targetmember = this.MemberRepository.findById(id);


        Diary targetdiary=this.diaryRepository.findByMemberAndDatecreate(targetmember.get(),targetdate_);
        String returnJSON;
        if(targetdiary==null)
        {
            returnJSON= "{\"message\" : \""+"diary_read_fail"
                    +"\"}";
        }
        else
        {
            returnJSON= "{\"datecreate\" : \""+targetdiary.getDatecreate()+"\",\"text\" : \""
                    +targetdiary.getText()+"\",\"happiness\" : \""
                    +targetdiary.getHappiness()+"\",\"disgust\" : \""
                    +targetdiary.getDisgust()+"\",\"sadness\" : \""
                    +targetdiary.getSadness()+"\",\"angry\" : \""
                    +targetdiary.getAngry()+"\",\"surprise\" : \""
                    +targetdiary.getSurprise()+"\",\"fear\" : \""
                    +targetdiary.getFear()+"\",\"neutral\" : \""
                    +targetdiary.getNeutral()+"\",\"message\" : \""
                    +"diary_read_success"
                    +"\"}";
        }
        return returnJSON;
    }
    //일기 update
    @PostMapping("/diary_update")
    public String diary_update(@RequestBody VO vo) throws IOException
    {
        String targetdate = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyyMMdd"));
        LocalDateTime targetdate_ = LocalDateTime.of(Integer.parseInt(targetdate.substring(0,4)),Integer.parseInt(targetdate.substring(4,6)),Integer.parseInt(targetdate.substring(6,8)),0,0);

        String id=vo.getId();
        String text=vo.getText();

        HttpUtils httpUtils = new HttpUtils();
        System.out.println(text);
        String url = "http://172.30.1.9:5000/Diary";

        JSONObject result=new JSONObject();
        HttpURLConnection conn = null;
        conn = httpUtils.getHttpURLConnection(url, "POST");
        conn.setDoOutput(true);
        try (DataOutputStream dataOutputStream = new DataOutputStream(conn.getOutputStream());){

            String str = "{\"Text\" : \""+text+"\"}";


            OutputStream wr1 = conn.getOutputStream();
            DataOutputStream wr = new DataOutputStream(conn.getOutputStream());

            wr1.write(str.getBytes("UTF-8"));
            wr1.flush();
            wr1.close();

            result = new JSONObject(httpUtils.getHttpResponse_(conn));


        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println(result);
        String strToFront = "{\"sentence\" : \""+(result.get("sentence")).toString()+"\",\"neutral\" : \""
                +(result.get("neutral")).toString()+"\",\"surprise\" : \""+(result.get("surprise")).toString()
                +"\",\"angry\" : \""+(result.get("angry")).toString()+"\",\"happiness\" : \""+(result.get("happiness")).toString()
                +"\",\"fear\" : \""+(result.get("fear")).toString()+"\",\"sadness\" : \""+(result.get("sadness")).toString()+"\",\"disgust\" : \""
                +(result.get("disgust")).toString()+"\"}";




        //기존 일기 데이터 삭제
        Optional<Member> targetmember = this.MemberRepository.findById(id);


        Diary targetdiary=this.diaryRepository.findByMemberAndDatecreate(targetmember.get(),targetdate_);
        targetdate_=targetdiary.getDatecreate();
        this.diaryRepository.delete(targetdiary);


        //일기 데이터 저장
        Diary diary=new Diary();
        Optional<Member> testMember = this.MemberRepository.findById(id);
        diary.setMember(testMember.get());
        //diary.setDatecreate(targetdate_);
        diary.setDatecreate(LocalDateTime.now());
        diary.setText(text);
        diary.setHappiness(Double.parseDouble((result.get("happiness")).toString()));
        diary.setDisgust(Double.parseDouble((result.get("disgust")).toString()));
        diary.setSadness(Double.parseDouble((result.get("sadness")).toString()));
        diary.setAngry(Double.parseDouble((result.get("angry")).toString()));
        diary.setSurprise(Double.parseDouble((result.get("surprise")).toString()));
        diary.setFear(Double.parseDouble((result.get("fear")).toString()));
        diary.setNeutral(Double.parseDouble((result.get("neutral")).toString()));
        this.diaryRepository.save(diary);


        //학습 데이터 저장
        JSONArray jarray=(JSONArray) (result.get("sentence"));
        for(int i=0;i< jarray.length();i++)
        {
            JSONArray jarrayarray=(JSONArray)(jarray.get(i));
            TrainingData trainingData=new TrainingData();
            trainingData.setTrain_text(jarrayarray.get(0).toString());
            trainingData.setTrain_sentiment(jarrayarray.get(1).toString());
            this.TrainingDataRepository.save(trainingData);
        }


        return strToFront;
    }
    //일기 delete
    @PostMapping("/diary_delete")
    public String diary_delete(@RequestBody VO vo) throws IOException
    {
        String targetdate = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyyMMdd"));
        LocalDateTime targetdate_ = LocalDateTime.of(Integer.parseInt(targetdate.substring(0,4)),Integer.parseInt(targetdate.substring(4,6)),Integer.parseInt(targetdate.substring(6,8)),0,0);

        String id=vo.getId();
        Optional<Member> targetmember = this.MemberRepository.findById(id);

        Diary targetdiary=this.diaryRepository.findByMemberAndDatecreate(targetmember.get(),targetdate_);

        String returnJSON;
        if(targetdiary==null)
        {
            returnJSON= "{\"message\" : \""+"diary_delete_fail"
                    +"\"}";
        }
        else
        {
            this.diaryRepository.delete(targetdiary);
            returnJSON= "{\"message\" : \""+"diary_delete_success"
                    +"\"}";
        }
        return returnJSON;
    }

    //나무 create
    @PostMapping("/tree_create")
    public String tree_create(@RequestBody VO vo) throws IOException
    {
        String id= vo.getId();
        String name=vo.getName();
        String password=vo.getPassword();
        Optional<Member> testMember=this.MemberRepository.findById(id);
        String returnJSON="";

        if(testMember.isPresent())
        {
            returnJSON= "{\"message\" : \""+"member_create_fail"
                    +"\"}";
        }
        else
        {
            Member member=new Member();
            member.setId(id);
            member.setName(name);
            member.setPassword(password);
            member.setHappinessmoney(0.0);
            member.setDisgustmoney(0.0);
            member.setSadnessmoney(0.0);
            member.setAngrymoney(0.0);
            member.setSurprisemoney(0.0);
            member.setFearmoney(0.0);
            member.setNeutralmoney(0.0);
            this.MemberRepository.save(member);
            returnJSON= "{\"message\" : \""+"member_create_success"
                    +"\"}";
        }
        return returnJSON;
    }

    //나무 read
    @PostMapping("/tree_read")
    public String tree_read(@RequestBody VO vo) throws IOException
    {
        String returnString="";

        return returnString;
    }
    //나무 update
    @PostMapping("/tree_update")
    public String tree_update(@RequestBody VO vo) throws IOException
    {
        String returnString="";

        return returnString;
    }
    //나무 delete
    @PostMapping("/tree_delete")
    public String tree_delete(@RequestBody VO vo) throws IOException
    {
        String returnString="";

        return returnString;
    }

    //친구 create
    @PostMapping("/friend_create")
    public String friend_create(@RequestBody VO vo) throws IOException
    {
        String returnString="";

        return returnString;
    }

    //친구 read
    @PostMapping("/friend_read")
    public String friend_read(@RequestBody VO vo) throws IOException
    {
        String returnString="";
        String str = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyyMMdd"));
        LocalDateTime dateTime = LocalDateTime.of(Integer.parseInt(str.substring(0,4)),Integer.parseInt(str.substring(4,6)),Integer.parseInt(str.substring(6,8)),0,0);
        System.out.println(dateTime);
        System.out.println(str);
        return returnString;
    }

    //친구 update
    @PostMapping("/friend_update")
    public String friend_update(@RequestBody VO vo) throws IOException
    {
        String returnString="";

        return returnString;
    }
    //친구 delete
    @PostMapping("/friend_delete")
    public String friend_delete(@RequestBody VO vo) throws IOException
    {
        String returnString="";

        return returnString;
    }

    //통계 read
    @PostMapping("/statistic_read")
    public String statistic_read(@RequestBody VO vo) throws IOException
    {
        String id=vo.getId();
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyyMMdd");
        String startdate=vo.getDate_start();
        String enddate=vo.getDate_end();
        LocalDateTime startdate_ = LocalDateTime.of(Integer.parseInt(startdate.substring(0,4)),Integer.parseInt(startdate.substring(4,6)),Integer.parseInt(startdate.substring(6,8)),0,0);
        LocalDateTime enddate_ = LocalDateTime.of(Integer.parseInt(enddate.substring(0,4)),Integer.parseInt(enddate.substring(4,6)),Integer.parseInt(enddate.substring(6,8)),0,0);

        Optional<Member> targetmember = this.MemberRepository.findById(id);

        Integer sum=0;
        Double happiness=0.0;
        Double disgust=0.0;
        Double sadness=0.0;
        Double angry=0.0;
        Double surprise=0.0;
        Double fear=0.0;
        Double neutral=0.0;

        while(true)
        {
            Boolean flag=false;
            if(startdate_.getDayOfMonth()==enddate_.getDayOfMonth())
                flag=true;

            Diary targetdiary=this.diaryRepository.findByMemberAndDatecreate(targetmember.get(),startdate_);
            if(targetdiary!=null)
            {
                happiness+= targetdiary.getHappiness();
                disgust+= targetdiary.getDisgust();
                sadness+= targetdiary.getSadness();
                angry+= targetdiary.getAngry();
                surprise+= targetdiary.getSurprise();
                fear+= targetdiary.getFear();
                neutral+= targetdiary.getNeutral();
                sum++;
            }
            startdate_=startdate_.plusDays(1);

            if(flag)
                break;
        }
        happiness/=Double.valueOf(sum);
        disgust/=Double.valueOf(sum);
        sadness/=Double.valueOf(sum);
        angry/=Double.valueOf(sum);
        surprise/=Double.valueOf(sum);
        fear/=Double.valueOf(sum);
        neutral/=Double.valueOf(sum);
        String returnJSON;
        returnJSON= "{\"sum\" : \""+sum+"\",\"happiness\" : \""
                +happiness+"\",\"disgust\" : \""
                +disgust+"\",\"sadness\" : \""
                +sadness+"\",\"angry\" : \""
                +angry+"\",\"surprise\" : \""
                +surprise+"\",\"fear\" : \""
                +fear+"\",\"neutral\" : \""
                +neutral+"\",\"message\" : \""
                +"statistic_read_success"
                +"\"}";


        return returnJSON;
    }




}
