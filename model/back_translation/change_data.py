import pandas as pd
from tqdm import tqdm
from translate import *
import web
import csv


def change_data(driver, file_name):
    f = open('result2.csv','w', newline='')
    wr = csv.writer(f)
    wr.writerow(['main'])
    
    df = pd.read_csv(file_name, encoding='utf-8')
    
    for i in tqdm(range(695, len(df))):
        wr.writerow([get_japanese_translation(driver, df['main'][i])])
        
        
    # df.to_csv('결과.csv', encoding='utf-8-sig', index=False)
    f.close()

