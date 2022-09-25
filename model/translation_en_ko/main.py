import config
import web
import logger
from translate import *
from change_data import *
from setting import *


# Globals
logger = logger.instance


def main():
    logger.info('Script started.')
    config.init_web()

    web.start()
    driver = web.driver
    driver.get('https://papago.naver.com/')
    # text = '안녕'
    # print(get_double_translation(driver, text))
    
    # set_honostic(driver)
    
    
    file_name = 'data_result/train_result(번역).csv'
    change_data_if_unchanged(driver, file_name)


if __name__ == '__main__':
    main()
    
