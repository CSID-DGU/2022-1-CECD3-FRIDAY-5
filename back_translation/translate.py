import web
from selenium.webdriver.common.by import By
import time

def get_japanese_translation(driver, first_text):
    # driver = web.driver
    # driver.get('https://papago.naver.com/')
    
    # 파파고 번역창 Xpath
    xpath_text = '//*[@id="txtSource"]'
    xpath_button = '//*[@id="btnTranslate"]'
    xpath_translated_text = '//*[@id="txtTarget"]'
    xpath_reset_button = '//*[@id="sourceEditArea"]/button'
    xpath_select_language_scroll = '//*[@id="ddTargetLanguage"]/div[1]/button[2]'
    xpath_japanese = '//*[@id="ddTargetLanguage"]/div[2]/ul/li[3]'
    xpath_trade_button = '//*[@id="root"]/div/div[1]/section/div/div[1]/div[1]/div/div[2]/button'
    # xpath_honostic = '//*[@id="root"]/div/div[1]/section/div/div[1]/div[2]/div/div[6]/div/button'
    
        # 번역할 텍스트 입력창
    input_box = driver.find_element(By.XPATH, xpath_text)
    # 번역 버튼
    translate_button = driver.find_element(By.XPATH, xpath_button)
    language_scroll_button = driver.find_element(By.XPATH, xpath_select_language_scroll)
    result_box = driver.find_element(By.XPATH, xpath_translated_text)
    reset_button = driver.find_element(By.XPATH, xpath_reset_button)
    trade_button = driver.find_element(By.XPATH, xpath_trade_button)
    
   
    # # 첫번째 번역할 텍스트
    # first_text = '안녕'
    
    input_box.send_keys(first_text)
    # language_scroll_button.click()
    
    # japanese_select_button = driver.find_element(By.XPATH, xpath_japanese)
    # japanese_select_button.click()
    translate_button.click()
    # time.sleep(1)
    # honostic_button = driver.find_element(By.XPATH, xpath_honostic)
    # honostic_button.click()
    time.sleep(5)
    
    
    # result_box = driver.find_element(By.XPATH, xpath_translated_text)
    # first_translated_text = result_box.text
    # print(first_translated_text)
    
    
    # reset_button.click()
    # input_box.send_keys(first_translated_text)
    # translate_button.click()
    trade_button.click()
    time.sleep(5)
    
    
    
    result_translated_text = result_box.text
    
    trade_button.click()
    reset_button.click()
    time.sleep(1)
    # print(result_translated_text)
    
    return result_translated_text
    
    
    