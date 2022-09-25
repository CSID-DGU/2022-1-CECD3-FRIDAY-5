import web
from selenium.webdriver.common.by import By
import time

def get_english_translation(driver, text):
    driver = web.driver
    driver.get('https://papago.naver.com/')
    
    # 파파고 번역창 Xpath
    xpath_text = '//*[@id="txtSource"]'
    xpath_button = '//*[@id="btnTranslate"]'
    xpath_translated_text = '//*[@id="txtTarget"]'
    xpath_reset_button = '//*[@id="sourceEditArea"]/button'
    xpath_select_language_scroll = '//*[@id="ddTargetLanguage"]/div[1]/button[2]'
   
    
        # 번역할 텍스트 입력창
    input_box = driver.find_element(By.XPATH, xpath_text)
    # 번역 버튼
    translate_button = driver.find_element(By.XPATH, xpath_button)
    result_box = driver.find_element(By.XPATH, xpath_translated_text)
    reset_button = driver.find_element(By.XPATH, xpath_reset_button)
    
    
    input_box.send_keys(text)
    translate_button.click()
    time.sleep(1)
    
    result_translated_text = result_box.text
    reset_button.click()
    
    return result_translated_text
    
    
    