import pandas as pd
from tqdm import tqdm
from translate import *
import web
import csv


def change_data(driver, file_name):
    df = pd.read_csv(file_name, encoding='utf-8')
    
    count = 1
    
    for i in tqdm(range(len(df))):
        if (count % 1000) == 0:
            driver.close()
            
            web.start()
            driver = web.driver
            driver.get('https://papago.naver.com/')
            
        df.at[i, 'sentence'] = get_english_translation(driver, df.at[i, 'sentence'])
        
        count += 1
        
    df.to_csv('test(번역).csv', encoding='utf-8-sig', index=False)
    
    
def change_data_if_unchanged(driver, file_name):
    df = pd.read_csv(file_name, encoding='utf-8')
    
    count = 1
    
    unchanged_list = df[df['ko_sentence'].isnull()].index.tolist()
    
    for i in tqdm(unchanged_list):
        if (count % 1000) == 0:
            driver.close()
            
            web.start()
            driver = web.driver
            driver.get('https://papago.naver.com/')
            
        df.at[i, 'ko_sentence'] = get_english_translation(driver, df.at[i, 'en_sentence'])
        
        count += 1
        
    df.to_csv('data_result/train_result(번역).csv', encoding='utf-8-sig', index=False)

