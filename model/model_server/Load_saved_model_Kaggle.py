from transformers import BertModel
import numpy as np
import gluonnlp as nlp
from torch.utils.data import Dataset, DataLoader
from torch import nn
import torch
from KoBERT.kobert.pytorch_kobert import get_pytorch_kobert_model
from KoBERT.kobert_hf.kobert_tokenizer import KoBERTTokenizer
from kiwipiepy import Kiwi

tokenizer = KoBERTTokenizer.from_pretrained('skt/kobert-base-v1')
bertmodel, vocab = get_pytorch_kobert_model()


# 필요한 라이브러리 불러오기


# transformers

# # GPU 사용 시
# device = torch.device("cuda:0")
device = torch.device("cpu")


# 각 데이터가 BERT 모델의 입력으로 들어갈 수 있도록 tokenization, int encoding padding 등을 해주는 코드이다.
class BERTDataset(Dataset):
    def __init__(self, dataset, sent_idx, label_idx, bert_tokenizer, vocab, max_len,
                 pad, pair):

        transform = nlp.data.BERTSentenceTransform(
            bert_tokenizer, max_seq_length=max_len, vocab=vocab, pad=pad, pair=pair)

        self.sentences = [transform([i[sent_idx]]) for i in dataset]
        self.labels = [np.int32(i[label_idx]) for i in dataset]

    def __getitem__(self, i):
        return (self.sentences[i] + (self.labels[i], ))

    def __len__(self):
        return (len(self.labels))


# Setting parameters
max_len = 64
batch_size = 64
warmup_ratio = 0.1
num_epochs = 5
max_grad_norm = 1
log_interval = 200
learning_rate = 5e-5


class BERTClassifier(nn.Module):
    def __init__(self,
                 bert,
                 hidden_size=768,
                 num_classes=7,  # 클래스 수 조정##
                 dr_rate=None,
                 params=None):
        super(BERTClassifier, self).__init__()
        self.bert = bert
        self.dr_rate = dr_rate

        self.classifier = nn.Linear(hidden_size, num_classes)
        if dr_rate:
            self.dropout = nn.Dropout(p=dr_rate)

    def gen_attention_mask(self, token_ids, valid_length):
        attention_mask = torch.zeros_like(token_ids)
        for i, v in enumerate(valid_length):
            attention_mask[i][:v] = 1
        return attention_mask.float()

    def forward(self, token_ids, valid_length, segment_ids):
        attention_mask = self.gen_attention_mask(token_ids, valid_length)

        _, pooler = self.bert(input_ids=token_ids, token_type_ids=segment_ids.long(
        ), attention_mask=attention_mask.float().to(token_ids.device), return_dict=False)
        if self.dr_rate:
            out = self.dropout(pooler)
        return self.classifier(out)


# 학습 모델 로드
PATH = 'model/model_server/Model/Kaggle(single label), batch size=64, learning rate=5e-05/'
# 전체 모델을 통째로 불러옴, 클래스 선언 필수
model = torch.load(PATH + 'Model.pt',
                   map_location='cpu')
# state_dict를 불러 온 후, 모델에 저장
model.load_state_dict(torch.load(
    PATH + 'Model_state_dict.pt', map_location='cpu'))


# 토큰화
tok = tokenizer.tokenize


# 예측 모델 설정
def predict(predict_sentence):

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

            # if np.argmax(logits) == 0:
            #     test_eval.append("joy")
            # elif np.argmax(logits) == 1:
            #     test_eval.append("love")
            # elif np.argmax(logits) == 2:
            #     test_eval.append("sadness")
            # elif np.argmax(logits) == 3:
            #     test_eval.append("anger")
            # elif np.argmax(logits) == 4:
            #     test_eval.append("surprise")
            # elif np.argmax(logits) == 5:
            #     test_eval.append("fear")
            # elif np.argmax(logits) == 6:
            #     test_eval.append("neutral")

            return np.argmax(logits)


def softmax(x):
    return np.exp(x) / np.sum(np.exp(x))


test_sent = """
나는 즐거움의 감정을 느끼고 있어요. 
나는 사랑의 감정을 느끼고 있어요. 
나는 슬픔의 감정을 느끼고 있어요. 
나는 분노의 감정을 느끼고 있어요. 
나는 놀람의 감정을 느끼고 있어요. 
나는 두려움의 감정을 느끼고 있어요.
나는 중립의 감정을 느끼고 있어요.
"""

kiwi = Kiwi()
sent_list = kiwi.split_into_sents(test_sent, return_tokens=False)

emotion_list = [0 for i in range(7)]

for sent in sent_list:
    pred = predict(sent.text)
    emotion_list[pred] += 1

total = sum(emotion_list)

result = {
    'joy': round(emotion_list[0]/total, 4),
    'love': round(emotion_list[1]/total, 4),
    'sadness': round(emotion_list[2]/total, 4),
    'anger': round(emotion_list[3]/total, 4),
    'surprise': round(emotion_list[4]/total, 4),
    'fear': round(emotion_list[5]/total, 4),
    'neutral': round(emotion_list[6]/total, 4),
}

print(result)
