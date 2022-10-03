from KoBERT.kobert_hf.kobert_tokenizer import KoBERTTokenizer
from KoBERT.kobert.pytorch_kobert import get_pytorch_kobert_model
from kiwipiepy import Kiwi
from model import *
import json
from flask import Flask, request, render_template, jsonify
# 필요한 라이브러리 불러오기

import torch
import numpy as np


device = torch.device("cpu")

tokenizer = KoBERTTokenizer.from_pretrained('skt/kobert-base-v1')


# 학습 모델 로드
PATH = 'model/model_server/Model/Kaggle(single label), batch size=64, learning rate=5e-05/'
# 전체 모델을 통째로 불러옴, 클래스 선언 필수
model = torch.load(PATH + 'Model.pt', map_location='cpu')
model.load_state_dict(torch.load(PATH + 'Model_state_dict.pt',
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

        # test_eval = []
        for i in out:
            logits = i
            logits = logits.detach().cpu().numpy()

            return np.argmax(logits)


app = Flask(__name__)


@app.route('/')
def home():
    return render_template('test.html')


@app.route('/Diary', methods=['POST', 'GET'])
def interact():
    # sentence = request.form['sentence']
    # result = predict(sentence)
    # data = {'문장': sentence,
    #         '결과': result
    # }
    # return jsonify(data)
    data = request.json
    text = data['Text']
    kiwi = Kiwi()
    sent_list = kiwi.split_into_sents(text, return_tokens=False)

    emotion_list = [0 for i in range(7)]

    for sent in sent_list:
        pred = predict(sent.text)
        emotion_list[pred] += 1

    response = {
        'joy': emotion_list[0],
        'love': emotion_list[1],
        'sadness': emotion_list[2],
        'anger': emotion_list[3],
        'surprise': emotion_list[4],
        'fear': emotion_list[5],
        'neutral': emotion_list[6],
    }
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

    # response = {
    #     '문장': sentence,
    #     '결과': result,
    # }

    return json.dumps(response), 200


if __name__ == '__main__':
    app.run(host='192.168.35.3', debug=True, port=5000)
