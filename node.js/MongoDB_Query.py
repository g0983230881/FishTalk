import pprint
from pymongo import MongoClient

client = MongoClient()#建立連線
db = client.FishDB#連接資料庫:"."後接上資料庫名稱
datas = db.FishCollection#取得collection:"."後接上collection名稱
for data in datas.find({}):
    # pprint.pprint(data)
    print(data)
