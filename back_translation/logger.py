import logging
from colorlog import ColoredFormatter

instance = logging.getLogger('papago_crawling')
instance.setLevel(logging.DEBUG)

ch = logging.StreamHandler()
ch.setLevel(logging.DEBUG)


# formatter = logging.Formatter(
#     '%(asctime)s - %(levelname)s {%(pathname)s:%(lineno)d} - %(message)s',
#     '%Y-%m-%d %H:%M:%S')

formatter = ColoredFormatter(
    '%(log_color)s %(asctime)s - %(levelname)s {%(pathname)s:%(lineno)d} - %(message)s',
    '%Y-%m-%d %H:%M:%S')



# add formatter to ch
ch.setFormatter(formatter)
instance.addHandler(ch)
