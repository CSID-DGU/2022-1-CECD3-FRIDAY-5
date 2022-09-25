import os

from dotenv import load_dotenv

import logger

logger = logger.instance

chrome_driver_bin = None
chrome_bin = None

env_path = os.path.dirname(os.path.realpath(__file__)) + '/.env'
print(env_path)
load_dotenv(dotenv_path=env_path)

headless = os.getenv('HEADLESS') == 'true'


def init_web():
    '''
    Initialize environment variables in preparation for web interaction.
    '''
    global chrome_driver_bin
    global chrome_bin

    chrome_driver_bin = os.getenv('CHROME_DRIVER_BIN')
    chrome_bin = os.getenv('CHROME_BIN')
