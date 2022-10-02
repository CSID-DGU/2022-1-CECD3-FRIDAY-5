from KoBERT.kobert_hf.kobert_tokenizer import KoBERTTokenizer
from KoBERT.kobert.pytorch_kobert import get_pytorch_kobert_model
from model import *
import json
from flask import Flask, request, render_template, jsonify
# 필요한 라이브러리 불러오기

import torch
import numpy as np


device = torch.device("cpu")

tokenizer = KoBERTTokenizer.from_pretrained('skt/kobert-base-v1')


# 학습 모델 로드
PATH = 'Model_0616/'
# 전체 모델을 통째로 불러옴, 클래스 선언 필수
model = torch.load(PATH + 'Model_0616model12399.pt', map_location='cpu')
model.load_state_dict(torch.load(PATH + 'Model_0616model12399_state_dict.pt',
                      map_location='cpu'))  # state_dict를 불러 온 후, 모델에 저장


def softmax(x):

    return np.exp(x) / np.sum(np.exp(x))

# 예측 모델 설정


def predict(predict_sentence):
    # 토큰화
    tok = tokenizer.tokenize

    data = [predict_sentence, '0']
    dataset_another = [data]

    another_test = BERTDataset(
        dataset_another, 0, 1, tok, vocab, max_len, True, False)
    test_dataloader = DataLoader(
        another_test, batch_size=batch_size, num_workers=0)

    model.eval()

    for batch_id, (token_ids, valid_length, segment_ids, label) in enumerate(test_dataloader):
        token_ids = token_ids.long().to(device)
        segment_ids = segment_ids.long().to(device)

        valid_length = valid_length
        label = label.long().to(device)

        out = model(token_ids, valid_length, segment_ids)

        test_eval = []
        for i in out:
            logits = i
            logits = logits.detach().cpu().numpy()

            return logits.tolist()

            # if np.argmax(logits) == 0:
            #     test_eval.append("간호사 서비스")
            # elif np.argmax(logits) == 1:
            #     test_eval.append("의사 서비스")
            # elif np.argmax(logits) == 2:
            #     test_eval.append("투약 및 치료과정")
            # elif np.argmax(logits) == 3:
            #     test_eval.append("병원환경")
            # elif np.argmax(logits) == 4:
            #     test_eval.append("환자 권리보장")
            # elif np.argmax(logits) == 5:
            #     test_eval.append("전반적 평가")
            # elif np.argmax(logits) == 6:
            #     test_eval.append("상담")
            # elif np.argmax(logits) == 7:
            #     test_eval.append("대기시간")
            # else:
            #     test_eval.append("기타")

        # print(test_eval[0])
        # return test_eval[0]


app = Flask(__name__)


@app.route('/')
def home():
    return render_template('test.html')


@app.route('/test', methods=['POST', 'GET'])
def interact():
    # sentence = request.form['sentence']
    # result = predict(sentence)
    # data = {'문장': sentence,
    #         '결과': result
    # }
    # return jsonify(data)
    data = request.json
    sentence = data['sentence']
    result = predict(sentence)

    # for i in range(len(result)):
    #     result[i] = float(result[i])

    # result = softmax(result).tolist()

    # for i in range(len(result)):
    #     result[i] = str(int(result[i] * 100)) + '%'

    # response = {
    #     '문장': sentence,
    #     'nurseService': result[0],
    #     'doctorService': result[1],
    #     'treatment': result[2],
    #     'hospitalEnvironment': result[3],
    #     'patientRights': result[4],
    #     'overallEvaluation': result[5],
    #     'consulting': result[6],
    #     'waitingTime': result[7],
    #     'etc': result[8]
    # }

    response = {
        '문장': sentence,
        '결과': result,
    }

    return json.dumps(response), 200


if __name__ == '__main__':
    app.run(host='192.168.35.101', debug=True, port=5000)


# from enum import Enum
# from fastapi.responses import HTMLResponse
# from typing import Optional
# from pydantic import BaseModel
# from fastapi import FastAPI, Request, Form


# class Item(BaseModel):
#     name: str
#     description: Optional[str] = None
#     price: float
#     tax: Optional[float] = None


# app = FastAPI()


# def generate_html_response():
#     html_content = """
#     <html>
#         <head>
#             <title>Some HTML in here</title>
#         </head>
#         <body>
#              <form action="/practice" method="post" style="display:inline;">
#             <button type="submit" id="user_text" style="float: right;">전송</button>
#             <textarea class="card-description kor-sentence"
#                 style="font-size: 18px; color: black; font-weight: bold; width: 100%; height: 80%; margin:0;"
#                 placeholder="테스트"></textarea>
#             </form>
#         </body>
#     </html>
#     """
#     return HTMLResponse(content=html_content, status_code=200)


# @app.get("/items/", response_class=HTMLResponse)
# async def read_items():
#     return generate_html_response()


# @app.post("/practice")
# async def test(sentence: str = Form()):
#     return "hello"
