package com.mysite.diary_backend;

import org.json.JSONArray;
import org.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import java.io.*;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
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
        System.out.println("server error test1234");
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
        System.out.println("server error test1234");
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
            returnJSON= "{"+"\"data\":{"+"\"id\" : \""+testMember.getId()+"\",\"name\" : \""
                    +testMember.getName()+"\",\"password\" : \""
                    +testMember.getPassword()+"\",\"happinessmoney\" : \""
                    +testMember.getHappinessmoney()+"\",\"disgustmoney\" : \""
                    +testMember.getDisgustmoney()+"\",\"sadnessmoney\" : \""
                    +testMember.getSadnessmoney()+"\",\"angrymoney\" : \""
                    +testMember.getAngrymoney()+"\",\"surprisemoney\" : \""
                    +testMember.getSurprisemoney()+"\",\"fearmoney\" : \""
                    +testMember.getFearmoney()+"\",\"neutralmoney\" : \""
                    +testMember.getNeutralmoney()+"\"},"+"\"message\" : \""
                    +"member_read_success"
                    +"\"}";
        }
        return returnJSON;
    }
    //회원 update
    @PostMapping("/member_update")
    public String member_update(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
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
        System.out.println("server error test1234");
        String id= vo.getId();
        String password=vo.getPassword();
        Member testMember=this.MemberRepository.findByIdAndPassword(id,password);
        String returnJSON;

        if(testMember!=null)
        {
            this.MemberRepository.delete(testMember);

            List<Friend> allFriend=this.FriendRepository.findAll();
            for(Friend friends : allFriend){
                if(friends.getId().equals(id))
                    this.FriendRepository.delete(friends);
            }
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
        System.out.println("server error test1234");
        String targetdate = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyyMMdd"));
        LocalDateTime targetdate_ = LocalDateTime.of(Integer.parseInt(targetdate.substring(0,4)),Integer.parseInt(targetdate.substring(4,6)),Integer.parseInt(targetdate.substring(6,8)),0,0);


        String id=vo.getId();
        Optional<Member> targetmember = this.MemberRepository.findById(id);
        String memberDatecreate=targetmember.get().getId()+targetdate;
        System.out.println("memberdatecreate"+memberDatecreate);
        String text=vo.getText();
        text= text.replaceAll("\n", "\\n");
        HttpUtils httpUtils = new HttpUtils();
        String url = "http://118.37.53.178:50512/Diary";

        JSONObject result=new JSONObject();
        HttpURLConnection conn = null;
        conn = httpUtils.getHttpURLConnection(url, "POST");
        conn.setDoOutput(true);
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

        Diary targetdiary=this.diaryRepository.findByMemberAndDatecreate(targetmember.get(),targetdate_);

        String strToFront="";

        if(targetdiary!=null) {
            strToFront= "{\"message\" : \""+"diary_create_fail"
                    +"\"}";
        }
        else {
            strToFront = "{"+"\"data\":{"+"\"neutral\" : \""
                    +(result.get("neutral")).toString()+"\",\"surprise\" : \""+(result.get("surprise")).toString()
                    +"\",\"angry\" : \""+(result.get("angry")).toString()+"\",\"happiness\" : \""+(result.get("happiness")).toString()
                    +"\",\"fear\" : \""+(result.get("fear")).toString()+"\",\"sadness\" : \""+(result.get("sadness")).toString()+"\",\"disgust\" : \""
                    +(result.get("disgust")).toString()+"\"}, "+"\"message\" : \""
                    +"diary_create_success"
                    +"\"}";

            //일기 데이터 저장
            Diary diary=new Diary();
            Optional<Member> testMember = this.MemberRepository.findById(id);
            diary.setMember(testMember.get());
            diary.setMemberDatecreate(memberDatecreate);
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

            Member moneychange=testMember.get();
            moneychange.setHappinessmoney(moneychange.getHappinessmoney()+Double.parseDouble((result.get("happiness")).toString()));
            moneychange.setDisgustmoney(moneychange.getDisgustmoney()+Double.parseDouble((result.get("disgust")).toString()));
            moneychange.setSadnessmoney(moneychange.getSadnessmoney()+Double.parseDouble((result.get("sadness")).toString()));
            moneychange.setAngrymoney(moneychange.getAngrymoney()+Double.parseDouble((result.get("angry")).toString()));
            moneychange.setSurprisemoney(moneychange.getSurprisemoney()+Double.parseDouble((result.get("surprise")).toString()));
            moneychange.setFearmoney(moneychange.getFearmoney()+Double.parseDouble((result.get("fear")).toString()));
            moneychange.setNeutralmoney(moneychange.getNeutralmoney()+Double.parseDouble((result.get("neutral")).toString()));
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
        }
        return strToFront;
    }

    //일기 read
    @PostMapping("/diary_read")
    public String diary_read(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
        String targetdate = vo.getTargetdate();
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
            returnJSON= "{"+"\"data\":{"+"\"datecreate\" : \""+targetdiary.getDatecreate()+"\",\"text\" : \""
                    +targetdiary.getText()+"\",\"happiness\" : \""
                    +targetdiary.getHappiness()+"\",\"disgust\" : \""
                    +targetdiary.getDisgust()+"\",\"sadness\" : \""
                    +targetdiary.getSadness()+"\",\"angry\" : \""
                    +targetdiary.getAngry()+"\",\"surprise\" : \""
                    +targetdiary.getSurprise()+"\",\"fear\" : \""
                    +targetdiary.getFear()+"\",\"neutral\" : \""
                    +targetdiary.getNeutral()+"\"},\"message\" : \""
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
        String url = "http://118.37.53.178:50512/Diary";


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
        diary.setDatecreate(targetdate_);
        diary.setText(text);
        diary.setHappiness(Double.parseDouble((result.get("happiness")).toString()));
        diary.setDisgust(Double.parseDouble((result.get("disgust")).toString()));
        diary.setSadness(Double.parseDouble((result.get("sadness")).toString()));
        diary.setAngry(Double.parseDouble((result.get("angry")).toString()));
        diary.setSurprise(Double.parseDouble((result.get("surprise")).toString()));
        diary.setFear(Double.parseDouble((result.get("fear")).toString()));
        diary.setNeutral(Double.parseDouble((result.get("neutral")).toString()));
        String memberDatecreate=targetmember.get().getId()+targetdate;
        diary.setMemberDatecreate(memberDatecreate);
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
        System.out.println("server error test1234");
        String targetdate = vo.getTargetdate();
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
        System.out.println("server error test1234");
        String id= vo.getId();
        String cost_sentiment=vo.getCost_sentiment();
        Double cost_quantity=vo.getCost_quantity();
        String treename=vo.getTreename();
        Double positionx=vo.getPositionx();
        Double positiony=vo.getPositiony();
        Double positionz=vo.getPositionz();
        Optional<Member> testMember=this.MemberRepository.findById(id);
        List<Tree> allTree=this.TreeRepository.findAll();
        Boolean duplicatedTree=false;
        for(Tree trees : allTree)
        {
            if((trees.getMember().getId().equals(id))&&(Math.abs(trees.getPositionx()-positionx)<0.01)&&(Math.abs(trees.getPositiony()-positiony)<0.01)&&(Math.abs(trees.getPositionz()-positionz)<0.01))
                duplicatedTree=true;
        }

        String returnJSON="";

        if((testMember.isPresent())&&(!duplicatedTree))
        {
            Member target_member=testMember.get();
            returnJSON= "{\"message\" : \""+"tree_create_success"
                    +"\"}";
            if(cost_sentiment.equals("happiness")){
                if(cost_quantity<=testMember.get().getHappinessmoney()){
                    target_member.setHappinessmoney(target_member.getHappinessmoney()-cost_quantity);
                }
                else{
                    returnJSON= "{\"message\" : \""+"tree_create_fail_insufficient_money"
                            +"\"}";
                }
            }
            else if(cost_sentiment.equals("disgust")){
                if(cost_quantity<=testMember.get().getDisgustmoney()){
                    target_member.setDisgustmoney(target_member.getDisgustmoney()-cost_quantity);
                }
                else{
                    returnJSON= "{\"message\" : \""+"tree_create_fail_insufficient_money"
                            +"\"}";
                }
            }
            else if(cost_sentiment.equals("sadness")){
                if(cost_quantity<=testMember.get().getSadnessmoney()){
                    target_member.setSadnessmoney(target_member.getSadnessmoney()-cost_quantity);
                }
                else{
                    returnJSON= "{\"message\" : \""+"tree_create_fail_insufficient_money"
                            +"\"}";
                }
            }
            else if(cost_sentiment.equals("angry")){
                if(cost_quantity<=testMember.get().getAngrymoney()){
                    target_member.setAngrymoney(target_member.getAngrymoney()-cost_quantity);
                }
                else{
                    returnJSON= "{\"message\" : \""+"tree_create_fail_insufficient_money"
                            +"\"}";
                }
            }
            else if(cost_sentiment.equals("surprise")){
                if(cost_quantity<=testMember.get().getSurprisemoney()){
                    target_member.setSurprisemoney(target_member.getSurprisemoney()-cost_quantity);
                }
                else{
                    returnJSON= "{\"message\" : \""+"tree_create_fail_insufficient_money"
                            +"\"}";
                }
            }
            else if(cost_sentiment.equals("fear")){
                if(cost_quantity<=testMember.get().getFearmoney()){
                    target_member.setFearmoney(target_member.getFearmoney()-cost_quantity);
                }
                else{
                    returnJSON= "{\"message\" : \""+"tree_create_fail_insufficient_money"
                            +"\"}";
                }
            }
            else if(cost_sentiment.equals("neutral")){
                if(cost_quantity<=testMember.get().getNeutralmoney()){
                    target_member.setNeutralmoney(target_member.getNeutralmoney()-cost_quantity);
                }
                else{
                    returnJSON= "{\"message\" : \""+"tree_create_fail_insufficient_money"
                            +"\"}";
                }
            }

            this.MemberRepository.save(target_member);
            Tree targetTree=new Tree();
            targetTree.setMember(testMember.get());
            targetTree.setTreename(treename);
            targetTree.setPositionx(positionx);
            targetTree.setPositiony(positiony);
            targetTree.setPositionz(positionz);
            this.TreeRepository.save(targetTree);



        }
        else if(duplicatedTree)
        {
            returnJSON= "{\"message\" : \""+"tree_create_fail_overlapped_tree"
                    +"\"}";
        }
        else if(!testMember.isPresent())
        {
            returnJSON= "{\"message\" : \""+"tree_create_fail"
                    +"\"}";
        }
        return returnJSON;
    }

    //나무 read
    //사용하지 않음
    @PostMapping("/tree_read")
    public String tree_read(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
        Integer treeid= vo.getTreeid();
        Tree target_tree=this.TreeRepository.findByTreeid(treeid);
        String returnJSON;


        if(target_tree==null)
        {
            returnJSON= "{\"message\" : \""+"tree_read_fail"
                    +"\"}";
        }
        else
        {
            returnJSON= "{"+"\"data\":{"+"\"id\" : \""+target_tree.getMember().getId()+"\",\"treeid\" : \""
                    +target_tree.getTreeid()+"\",\"treename\" : \""
                    +target_tree.getTreename()+"\",\"positionx\" : \""
                    +target_tree.getPositionx()+"\",\"positiony\" : \""
                    +target_tree.getPositiony()+"\",\"positionz\" : \""
                    +target_tree.getPositionz()+"\"}, \"message\" : \""
                    +"tree_read_success"
                    +"\"}";
        }
        return returnJSON;
    }
    //나무 update
    @PostMapping("/tree_update")
    public String tree_update(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
        String id=vo.getId();
        Double old_positionx=vo.getOld_positionx();
        Double old_positiony=vo.getOld_positiony();
        Double old_positionz=vo.getOld_positionz();
        Double positionx=vo.getPositionx();
        Double positiony=vo.getPositiony();
        Double positionz=vo.getPositionz();

        Optional<Member> testMember=this.MemberRepository.findById(id);
        List<Tree> allTree=this.TreeRepository.findAll();
        Tree target_tree=null;

        //새로운 좌표가 다른 나무와 겹치는지 확인
        Boolean duplicatedTree=false;
        for(Tree trees : allTree)
            if((trees.getMember().getId().equals(id))&&(Math.abs(trees.getPositionx()-positionx)<0.01)&&(Math.abs(trees.getPositiony()-positiony)<0.01)&&(Math.abs(trees.getPositionz()-positionz)<0.01))
                duplicatedTree=true;


        for(Tree trees : allTree)
        {
            if((trees.getMember().getId().equals(id))&&(Math.abs(trees.getPositionx()-old_positionx)<0.01)&&(Math.abs(trees.getPositiony()-old_positiony)<0.01)&&(Math.abs(trees.getPositionz()-old_positionz)<0.01))
                target_tree=trees;
        }
        String returnJSON="";

        if(target_tree==null)
        {
            returnJSON= "{\"message\" : \""+"tree_update_fail"
                    +"\"}";
        }
        else if((!duplicatedTree)&&(target_tree!=null))
        {
            target_tree.setPositionx(positionx);
            target_tree.setPositiony(positiony);
            target_tree.setPositionz(positionz);
            this.TreeRepository.save(target_tree);
            returnJSON= "{\"message\" : \""+"tree_update_success"
                    +"\"}";
        }
        else if((duplicatedTree)&&(target_tree!=null))
        {
            returnJSON= "{\"message\" : \""+"tree_update_fail_overlapped_tree"
                    +"\"}";
        }
        return returnJSON;
    }
    //나무 delete
    @PostMapping("/tree_delete")
    public String tree_delete(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
        String id=vo.getId();
        Double positionx=vo.getPositionx();
        Double positiony=vo.getPositiony();
        Double positionz=vo.getPositionz();
        Optional<Member> testMember=this.MemberRepository.findById(id);
        List<Tree> allTree=this.TreeRepository.findAll();
        Tree target_tree=null;

        for(Tree trees : allTree)
        {
            if((trees.getMember().getId().equals(id))&&(Math.abs(trees.getPositionx()-positionx)<0.01)&&(Math.abs(trees.getPositiony()-positiony)<0.01)&&(Math.abs(trees.getPositionz()-positionz)<0.01))
                target_tree=trees;
        }
        String returnJSON="";
        if(target_tree!=null)
        {
            this.TreeRepository.delete(target_tree);
            returnJSON= "{\"message\" : \""+"tree_delete_success"
                    +"\"}";
        }
        else
        {
            returnJSON= "{\"message\" : \""+"tree_delete_fail"
                    +"\"}";
        }
        return returnJSON;
    }

    //친구 create
    @PostMapping("/friend_create")
    public String friend_create(@RequestBody VO vo) throws IOException {
        System.out.println("server error test1234");
        String id=vo.getId();
        String friendid=vo.getFriendid();

        Optional<Member> testMember1=this.MemberRepository.findById(id);
        Optional<Member> testMember2=this.MemberRepository.findById(friendid);


        String returnJSON="";

        if((testMember1.isPresent())&&(testMember2.isPresent())){
            Friend already_friend=this.FriendRepository.findByIdAndMember(id, testMember2.get());
            if(already_friend==null) {
                Friend friend1=new Friend();
                Friend friend2=new Friend();

                friend1.setId(id);
                friend1.setMember(testMember2.get());

                friend2.setId(friendid);
                friend2.setMember(testMember1.get());

                this.FriendRepository.save(friend1);
                this.FriendRepository.save(friend2);

                returnJSON= "{\"message\" : \""+"friend_create_success"
                        +"\"}";
            }
        }
        else {

            returnJSON= "{\"message\" : \""+"friend_create_fail"
                    +"\"}";
        }

        return returnJSON;
    }

    //친구 update
    //사용하지 않음
    @PostMapping("/friend_update")
    public String friend_update(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
        String returnString="";

        return returnString;
    }
    //친구 delete
    @PostMapping("/friend_delete")
    public String friend_delete(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
        String id=vo.getId();
        String friendid=vo.getFriendid();


        Optional<Member> testMember1=this.MemberRepository.findById(id);
        Optional<Member> testMember2=this.MemberRepository.findById(friendid);

        String returnJSON="";

        if((testMember1.get()!=null)&&(testMember2.get()!=null)){
            Friend already_friend=this.FriendRepository.findByIdAndMember(id, testMember2.get());
            if(already_friend!=null){
                Friend friend1=this.FriendRepository.findByIdAndMember(id, testMember2.get());
                Friend friend2=this.FriendRepository.findByIdAndMember(friendid, testMember1.get());

                this.FriendRepository.delete(friend1);
                this.FriendRepository.delete(friend2);

                returnJSON= "{\"message\" : \""+"friend_delete_success"
                        +"\"}";

            }

        }
        else {
            returnJSON= "{\"message\" : \""+"friend_delete_fail"
                    +"\"}";
        }

        return returnJSON;
    }

    //통계 read
    @PostMapping("/statistic_read")
    public String statistic_read(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
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
        returnJSON= "{"+"\"data\":{"+"\"sum\" : \""+sum+"\",\"happiness\" : \""
                +happiness+"\",\"disgust\" : \""
                +disgust+"\",\"sadness\" : \""
                +sadness+"\",\"angry\" : \""
                +angry+"\",\"surprise\" : \""
                +surprise+"\",\"fear\" : \""
                +fear+"\",\"neutral\" : \""
                +neutral+"\"},\"message\" : \""
                +"statistic_read_success"
                +"\"}";

        if(sum==0){
            returnJSON="{"+"\"data\":{"+"\"sum\" : \""+sum+"\",\"happiness\" : \""
                    +0+"\",\"disgust\" : \""
                    +0+"\",\"sadness\" : \""
                    +0+"\",\"angry\" : \""
                    +0+"\",\"surprise\" : \""
                    +0+"\",\"fear\" : \""
                    +0+"\",\"neutral\" : \""
                    +0+"\"},\"message\" : \""
                    +"statistic_read_success"
                    +"\"}";
        }

        return returnJSON;
    }

    //나무 목록 read
    @PostMapping("/tree_list_read")
    public String tree_list_read(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
        String returnString="{\"tree_list\" : [";
        String id=vo.getId();
        List<Tree> allTree=this.TreeRepository.findAll();

        for(Tree trees : allTree){
            if(trees.getMember().getId().equals(id)){
                returnString+="{\"treeid\" : \""+trees.getTreeid()+"\",";
                returnString+="\"treename\" : \""+trees.getTreename()+"\",";
                returnString+="\"id\" : \""+trees.getMember().getId()+"\",";
                returnString+="\"positionx\" : \""+trees.getPositionx()+"\",";
                returnString+="\"positiony\" : \""+trees.getPositiony()+"\",";
                returnString+="\"positionz\" : \""+trees.getPositionz()+"\"},";
            }

        }
        returnString=returnString.substring(0,returnString.length()-1);
        returnString+="]}";

        return returnString;
    }


    //친구 목록 read
    @PostMapping("/friend_list_read")
    public String friend_list_read(@RequestBody VO vo) throws IOException
    {
        System.out.println("server error test1234");
        String returnString="{\"friend_list\" : [";
        String id=vo.getId();
        List<Friend> allFriend=this.FriendRepository.findAll();

        for(Friend friends : allFriend){
            if(friends.getId().equals(id)){
                returnString+="{\"id\" : \""+friends.getId()+"\",";
                returnString+="\"friendid\" : \""+friends.getMember().getId()+"\"},";
            }
        }
        returnString=returnString.substring(0,returnString.length()-1);
        returnString+="]}";

        return returnString;
    }




}
