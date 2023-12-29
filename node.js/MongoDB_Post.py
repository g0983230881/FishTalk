# import requests as req
# url = 'http://colin.hr-env.com/API/GetWaterData?sEid=30010'
# receive_msg = req.get(url)
# print(receive_msg.text)

import pymongo
from pymongo import MongoClient

client = MongoClient('localhost', 27017)#建立連線
db = client.Fish_info#連接資料庫:"."後接上資料庫名稱
message = {"float": "4.321"}#資料
datas = db.Fish_test#取得collection:"."後接上collection名稱
_id = datas.insert_one(message).inserted_id#傳送資料後順帶取得其_id
print ("post id is ", _id)
