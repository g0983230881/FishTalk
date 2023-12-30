const express = require('express')
const cors = require('cors');
const https = require('https');
const fs = require('fs');
const mongoose = require('mongoose');

//keyword certs for https
const options = {
  key: fs.readFileSync('OpenSSL/openssl_192.168.1.102/keystore.key'),
  cert: fs.readFileSync('OpenSSL/openssl_192.168.1.102/keystore.crt')
};

const app = express()
app.use(
  cors({
    origin: "*",
    methods: ["GET", "POST"],
  })
)


//keyword Mongoose for MongoDB
let DB_name = "FishDB"
let collectionName = "FishCollection"

mongoose.set('strictQuery', true);
mongoose.connect('mongodb://127.0.0.1:27017/'+DB_name, { useNewUrlParser: true })
  .then(() => {
    console.log('Connected to '+DB_name);
  })
  .catch((error) => {
    console.error('Error connecting to MongoDB', error);
  });

const fishSchema = new mongoose.Schema({
    rctime: String,
    Chlorine: Number,
    Salinity: Number,
    PH: Number,
    DO: Number,
    EC: Number,
    Turbidity: Number,
    Chlorophyll: Number,
    ORP: Number,
    NH4: Number,
    Temperature: Number,
    WL: Number,
    date: String,
    pool: String,
    isDead: Number,
}, { collection: collectionName }); 



// keyword router
app.get('/', (req, res) => {
  res.set('Access-Control-Expose-Headers', 'ETag');   
  const hello = 'Hello World!';
  const data = { message: hello }
  res.json(data)
  console.log(data)
});

const port = 443
https.createServer(options, app).listen(port, () => {
  console.log(`Server is listening on port ${port}`)
})

const current_id = '6438cc71d6e3e690fbc8fc9c';

app.get('/data', (req, res) => {
  res.set('Access-Control-Allow-Origin', '*')
  res.set('Access-Control-Allow-Private-NetWork', 'true')
  res.set('Access-Control-Expose-Headers', 'Cache-Control', 'no-store')

  const fishModel = mongoose.model('fishModel', fishSchema);
  fishModel.findOne({ _id: { $gt: current_id }}, function(err, nextDocument) {
    if (err) {
      // console.log(err);
      const data = { message: err }
      res.json(data)
      console.log(data)
      console.log("data");
    } else {
      // console.log(nextDocument);
      const jsonString = JSON.stringify(nextDocument.toObject());
      const jsonParse = JSON.parse(jsonString);
      current_id = jsonParse._id;

      setTimeout(() => {
        console.log("---start---");
        res.send(jsonString);
        console.log(jsonParse);
        console.log("---nothing---");
      }, 2000);
    }
  });
})


//test url 
/*
https://192.168.1.101/data
*/
