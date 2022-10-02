from transformers import BertModel
import numpy as np
import gluonnlp as nlp
from torch.utils.data import Dataset, DataLoader
from torch import nn
import torch
from KoBERT.kobert.pytorch_kobert import get_pytorch_kobert_model
from KoBERT.kobert_hf.kobert_tokenizer import KoBERTTokenizer
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
PATH = 'model/model_server/Model/Model_0616/'
# 전체 모델을 통째로 불러옴, 클래스 선언 필수
model = torch.load(PATH + 'Model_0616model12399.pt',
                   map_location='cpu')
# state_dict를 불러 온 후, 모델에 저장
model.load_state_dict(torch.load(
    PATH + 'Model_0616model12399_state_dict.pt', map_location='cpu'))


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

        test_eval = []
        for i in out:
            logits = i
            logits = logits.detach().cpu().numpy()

            if np.argmax(logits) == 0:
                test_eval.append("공포가")
            elif np.argmax(logits) == 1:
                test_eval.append("놀람이")
            elif np.argmax(logits) == 2:
                test_eval.append("분노가")
            elif np.argmax(logits) == 3:
                test_eval.append("슬픔이")
            elif np.argmax(logits) == 4:
                test_eval.append("중립이")
            elif np.argmax(logits) == 5:
                test_eval.append("행복이")
            elif np.argmax(logits) == 6:
                test_eval.append("혐오가")

        print(">> 입력하신 내용에서 " + test_eval[0] + " 느껴집니다.")


# 질문 무한반복하기! 0 입력시 종료
end = 1
while end == 1:
    sentence = input("하고싶은 말을 입력해주세요 : ")
    if sentence == "0":
        break
    predict(sentence)
    print("\n")
