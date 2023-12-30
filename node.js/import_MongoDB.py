import pandas as pd
import pymongo

# 讀取 CSV 檔案
df = pd.read_csv('allEnvironmentData.csv')

# 連接到 MongoDB 伺服器
client = pymongo.MongoClient('mongodb://localhost:27017/')

# 選擇資料庫和集合
db = client['FishDB']
collection = db['FishCollection']

# 將 CSV 資料轉換為字典格式
data = df.to_dict('records')

# 將資料載入到集合中
result = collection.insert_many(data)

# 顯示載入結果
print("載入了 {0} 筆資料。".format(len(result.inserted_ids)))