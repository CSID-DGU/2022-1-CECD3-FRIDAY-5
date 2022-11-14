fear="\n               공포스러워요.\n"
surprised="\n               놀랐어요.\n"
anger="\n               화나요.\n"
sadness="\n               슬퍼요.\n"
neutrality=""
happiness="\n               행복해요.\n"
disgust="\n               혐오스러워요.\n"


emoji_table = {"😱": fear,
              
               
               
               "🤭": surprised, "😨": surprised,"🙀": surprised,"◎_◎": surprised,
               "⊙_⊙": surprised,"O.o": surprised,"O-o": surprised,"ㅇ.ㅇ": surprised,
               "OㅁO": surprised,"O口o": surprised,"O□o": surprised,"(๑°ㅁ°๑)‼": surprised,
               "(๑⊙ﾛ⊙๑)": surprised,"(๑•́o•̀๑)": surprised,"ㄴ（°□。）ㄱ": surprised,"＼(º □ º l|l)/": surprised,
               "(″ロ゛)": surprised,"(;;;*_*)": surprised,"(・人・)": surprised,"＼(〇_ｏ)／": surprised,
               "〜(＞＜)〜": surprised,"(((＞＜)))": surprised,"{{ (>_<) }}": surprised,"〣( ºΔº )〣": surprised,
               "ヽ(°.° )ﾉ": surprised,"(°ロ°)": surprised,"(o_O)": surprised,"(○Ａ○)": surprised,
               "( >﹏<。)": surprised,"（￣□￣;)": surprised,"ヽ(*。>Д<)o゜": surprised,"(⊙_☉)": surprised,
               "＋ㅁ＋ ": surprised,"±ㅁ±": surprised,"ㄴ0o0ㄱ": surprised,"ㄴO.oㄱ": surprised,
               "ㄴOㅁ0ㄱ": surprised,"ㄴOAOㄱ": surprised,
               
               
               
               "🤨": anger,"😬": anger,"😤": anger,"😡": anger,
               "👿": anger,"🤬": anger,"😠": anger,"😾": anger,
               "ㄱㅡ": anger,"ㄱ-": anger,"ㄱL-": anger,"(｀0´)": anger,
               "(ಠ益ಠ)": anger,"(⊙▂⊙)💢": anger,"(｀△´＋)": anger,"(+⓪ 益 ⓪)": anger,
               "(💢⓪ 益 ⓪)💢": anger,"(💢◥ 益 ◤)": anger,"(◣_◢💢)": anger,"(`A´)": anger,
               "(▽д▽）": anger,"(⋋▂⋌)": anger,"(♯▼皿▼)": anger,"(ʘ言ʘ+)": anger,
               "(ｙﾟ 益ﾟ;)ｙ": anger,"(•ˋㅁˊ•)": anger,
               
               
               
               "🥲": sadness,"😔": sadness,"😕": sadness,"🥺": sadness,
               "😞": sadness,"😭": sadness,"😩": sadness,"😢": sadness,
               "🙁": sadness,"☹": sadness,"😣": sadness,"😿": sadness,
               "ㅜㅜ": sadness,"ㅠㅠ": sadness,"ㅜ.ㅜ": sadness,"ㅠ.ㅠ": sadness,
               "ㅠ_ㅠ": sadness,"ㅜ_ㅜ": sadness,"ㅠ-ㅠ": sadness,"ㅜ-ㅜ": sadness,
               "T오T": sadness,"T모T": sadness,"T소T": sadness,"ㅡ.ㅜ": sadness,
               "ㅜ.ㅡ": sadness,"T.T": sadness,"T_T": sadness,"TAT": sadness,
               "ㅠㅁㅠ": sadness,"TㅁT": sadness,"( Ĭ ^ Ĭ )": sadness,"ŏ̥̥̥̥םŏ̥̥̥̥": sadness,
               "( T ʖ̯ T)": sadness,"(T⌓T)": sadness,"(｡•́︿•̀｡)": sadness,"( ; ω ; )": sadness,
               "(｡T ω T｡)": sadness,"(πーπ)": sadness,"(Ｔ▽Ｔ)": sadness,"〒_〒": sadness,
               "(个_个)": sadness,
    
    
                "😀": happiness, "😉": happiness,  "😁": happiness,  "😆": happiness, 
                "😅": happiness,  "😂": happiness,  "🤣": happiness,  "😊": happiness, 
                "🙂": happiness,  "😄": happiness,  "🙃": happiness,  "😇": happiness, 
                "😃": happiness,  "😚": happiness,  "🥰": happiness,  "😙": happiness, 
                "☺": happiness,  "😍": happiness,  "😛": happiness,  "🤩": happiness, 
                "😋": happiness,  "😝": happiness,  "🤗": happiness,  "🥳": happiness, 
                "🤠": happiness,  "😽": happiness,  "😸": happiness,  "😻": happiness, 
                "^_^": happiness,  "^-^": happiness,  "^ㅡ^": happiness,  "^V^": happiness,
               "^0^": happiness, "^ㅁ^": happiness,  "^ㅇ^": happiness,  "^◡^": happiness, 
                "^오^": happiness,  "^모^": happiness,  "^▽^": happiness,  "^~^": happiness, 
                "^U^": happiness,  "^3^": happiness,  "˘◡˘": happiness,  "'◡'": happiness, 
                ":-)": happiness,  ":)": happiness,  ":-▷": happiness,  ":-D": happiness, 
                "X-D": happiness,  ":-P": happiness,  ":D": happiness,  "<( ^.^ )>": happiness, 
                "<(^_^<)": happiness,  "＼(^0^)/": happiness,  "⁽⁽◝( ˙ ꒳ ˙ )◜⁾⁾": happiness,  "๑•‿•๑": happiness, 
                "(∗❛⌄❛∗)": happiness,  "◝(⁰▿⁰)◜": happiness,  "(✪ꆚ✪)": happiness,  "◉‿◉": happiness,
               
              
              
              
              
              "🤮": disgust, "🤢": disgust
              
              
              }




import requests
import re
from bs4 import BeautifulSoup
from urllib.parse import quote
from kiwipiepy import Kiwi
import pandas as pd
import numpy as np

separators = ".", "!","?","\n","\t","#"
# urllib.parse.quote()는 아스키코드 형식이 아닌 글자를 URL 인코딩 시켜줍니다.


temporary_list_for_csv = []
line_num = 0

# 문장 단위로 자르기
def split_sentence(blog_texts):
    global temporary_list_for_csv
    global line_num
    returnList=[]
    kiwi = Kiwi()

    for line in kiwi.split_into_sents(blog_texts):
        if len(line.text) < 10:
            continue
        line_num += 1
        
        
        
        #문장에 한글 비중이 70퍼센트 초과 포함인 경우만 크롤링한다.
        #-> 링크와 같은 영어 문장이나 이모지 테이블에 포함되어 있지 않아 해석 불가능한 이모티콘이 포함된 문장을 제거
        korean_p = re.compile('[ㄱ-ㅎ|ㅏ-ㅣ|가-힣|\s]+')
        korean_count=0
        
        for t in line.text:
            if(korean_p.match(t)):
                korean_count+=1

        korean_ratio=korean_count/len(line.text)
        if(korean_ratio>0.7):
            returnList.append(line.text)
        else:
            print("한글 비중이 70% 이하입니다.")
            print(line.text)
    return returnList

# 텍스트 전처리
# 지금은 이모티콘 치환만
def preprocess_sentence_kr(text):
    text = emoji_substitution(text)

    return text

# zero width space 제거 
def remove_zero_width_space(w):
    w = w.strip()
    w = re.sub(r"[^0-9가-힣?.!,¿]+", " ", w) # \n도 공백으로 대체해줌
    w = w.strip()
    return w

# 이모티콘 치환
def emoji_substitution(blog_texts):
    for key in emoji_table.keys():
        if key in blog_texts:
            blog_texts = blog_texts.replace(key,emoji_table[key])
    
    return blog_texts
    

# 여러 구분자 사용 가능하게 해주는 함수
def custom_split(sepr_list, str_to_split):
    # create regular expression dynamically
    regular_exp = '|'.join(map(re.escape, sepr_list))
    return re.split(regular_exp, str_to_split)


def Diary_split(diary_text)
    diary_text = preprocess_sentence_kr(diary_text) # 텍스트 전처리
    returnList=split_sentence(diary_text)
    return returnList

