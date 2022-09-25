from selenium.webdriver.common.by import By
import time


def set_honostic(driver):
    driver.get('https://papago.naver.com/')
    time.sleep(1)
    
    xpath_text = '//*[@id="txtSource"]'
    xpath_select_language_scroll = '//*[@id="ddTargetLanguage"]/div[1]/button[2]'
    xpath_japanese = '//*[@id="ddTargetLanguage"]/div[2]/ul/li[3]'
    xpath_honostic = '//*[@id="root"]/div/div[1]/section/div/div[1]/div[2]/div/div[6]/div/button'
    xpath_reset_button = '//*[@id="sourceEditArea"]/button'
    reset_button = driver.find_element(By.XPATH, xpath_reset_button)
    
    
    input_box = driver.find_element(By.XPATH, xpath_text)
    input_box.send_keys('안녕')
    
    language_scroll_button = driver.find_element(By.XPATH, xpath_select_language_scroll)
    language_scroll_button.click()
    
    japanese_select_button = driver.find_element(By.XPATH, xpath_japanese)
    japanese_select_button.click()
    
    honostic_button = driver.find_element(By.XPATH, xpath_honostic)
    honostic_button.click()
    reset_button.click()